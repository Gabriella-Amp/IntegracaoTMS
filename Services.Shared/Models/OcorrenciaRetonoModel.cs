using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Services.Shared.Model
{
    [DataContract]
    public class OcorrenciaRetornoModel
    {
        [DataMember(Name = "oidEventoOcorrencia")]
        public int oidEventoOcorrencia { get; set; }

        [DataMember(Name = "statusIntegracao")]
        public int statusIntegracao { get; set; }

    }
}
