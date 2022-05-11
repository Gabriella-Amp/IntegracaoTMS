using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Services.Shared.Model
{
    [DataContract]
    public class DuplicataRetornoModel
    {
        [DataMember(Name = "codigoMensagem")]
        public string codigoMensagem { get; set; }

        [DataMember(Name = "statusIntegracao")]
        public string statusIntegracao { get; set; }

        [DataMember(Name = "serieFatura")]
        public string serieFatura { get; set; }

        [DataMember(Name = "cnpjEmissor")]
        public string cnpjEmissor { get; set; }

        [DataMember(Name = "mensagem")]
        public string mensagem { get; set; }

        [DataMember(Name = "numeroFatura")]
        public string numeroFatura { get; set; }

    }
}
