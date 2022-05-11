using System;
using System.Linq;
using Services.Shared.Enums;
using Services.Shared.Interfaces.Services.Logging;

namespace Services.Shared.Models
{
    public class Log : ILog
    {
        private readonly ELogLevel[] _errorLevels = { ELogLevel.ErroNoDebug, ELogLevel.ErroGenerico, ELogLevel.ErroDeServidor };

        public Log(string origem, string mensagem, ELogLevel nivel)
        {
            Origem = origem;

            DataHora = DateTime.Now;

            Mensagem = mensagem;

            Nivel = nivel;
        }

        public string Origem { get; private set; }

        public DateTime DataHora { get; private set; }

        public string Mensagem { get; private set; }

        public ELogLevel Nivel { get; private set; }

        public void DefinirNivel(ELogLevel nivel)
        {
            Nivel = nivel;
        }

        public void AlterarParaModoDebug()
        {
            var nivelDebug = _errorLevels.Contains(Nivel) ? ELogLevel.ErroNoDebug : ELogLevel.InformacaoNoDebug;

            DefinirNivel(nivelDebug);
        }

        public bool PossuiErro() => _errorLevels.Contains(Nivel);

        public override string ToString()
        {
            return $"{DataHora:G} - {Origem}";
        }

        public string ObterMensagem(string terminador = ";**")
        {
            if (!string.IsNullOrWhiteSpace(terminador) && Mensagem.Contains(terminador))
            {
                return $"{Mensagem.Substring(0, Mensagem.IndexOf(terminador, StringComparison.OrdinalIgnoreCase))} - DATA: {DataHora:G}";
            }

            return Mensagem;
        }

        public void AlterarMensagem(string mensagem)
        {
            Mensagem = mensagem;
        }
    }
}