using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Services.Shared.Model
{
    [DataContract]
    public class CTERetornoModel
    {
        [DataMember(Name = "numero")]
        public int numero { get; set; }

        [DataMember(Name = "statusIntegracao")]
        public string statusIntegracao { get; set; }

        [DataMember(Name = "codigoMensagem")]
        public string codigoMensagem { get; set; }

        [DataMember(Name = "mensagem")]
        public string mensagem { get; set; }



    }
}
