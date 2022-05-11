using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Services.Shared.Model
{
    [DataContract]
    public class DuplicataModel
    {
        [DataMember(Name = "empresa")]
        public string empresa { get; set; }

        [DataMember(Name = "oid")]
        public string oid { get; set; }

        [DataMember(Name = "cnpjEmissor")]
        public string cnpjEmissor { get; set; }

        [DataMember(Name = "observacao")]
        public string observacao { get; set; }

        [DataMember(Name = "dataGeracao")]
        public string dataGeracao { get; set; }

        [DataMember(Name = "dataVencimento")]
        public string dataVencimento { get; set; }

        [DataMember(Name = "valorLiquido")]
        public decimal valorLiquido { get; set; }

        [DataMember(Name = "numero")]
        public string numero { get; set; }

    }
}
