using System;
using Services.Shared.Enums;

namespace Services.Shared.Interfaces.Services.Logging
{
    public interface ILog
    {
        public string Origem { get; }

        public DateTime DataHora { get; }

        public string Mensagem { get; }

        public ELogLevel Nivel { get; }

        void DefinirNivel(ELogLevel nivel);

        string ObterMensagem(string terminador = ";**");

        void AlterarMensagem(string mensagem);

        void AlterarParaModoDebug();

        bool PossuiErro();
    }
}