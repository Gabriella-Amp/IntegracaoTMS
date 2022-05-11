using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Services.Shared.Model
{
    [DataContract]
    public class CTEModel
    {
        [DataMember(Name = "cnpjUnidade")]
        public string cnpjUnidade { get; set; }

        [DataMember(Name = "linkRepositorio")]
        public string linkRepositorio { get; set; }

        [DataMember(Name = "chave")]
        public string chave { get; set; }

    }
}
