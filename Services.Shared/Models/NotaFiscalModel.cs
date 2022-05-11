using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Services.Shared.Model
{
    [DataContract]
    public class NotaFiscalModel
    {
        [DataMember(Name = "Empresa")]
        public int Empresa { get; set; }

        [DataMember(Name = "TipoControle")]
        public int TipoControle { get; set; }

        [DataMember(Name = "CodControle")]
        public int CodControle { get; set; }

        [DataMember(Name = "NumDocumento")]
        public int NumDocumento { get; set; }

        [DataMember(Name = "DataMovimento")]
        public DateTime DataMovimento { get; set; }

        [DataMember(Name = "CodigoCliente")]
        public string CodigoCliente { get; set; }

        [DataMember(Name = "ChaveAcesso")]
        public string ChaveAcesso { get; set; }

        [DataMember(Name = "ArquivoXML")]
        public string ArquivoXML { get; set; }

        [DataMember(Name = "CodTransportadora")]
        public string CodTransportadora { get; set; }

        [DataMember(Name = "CnpjTransportadora")]
        public Int64 CnpjTransportadora { get; set; }
    }
}
