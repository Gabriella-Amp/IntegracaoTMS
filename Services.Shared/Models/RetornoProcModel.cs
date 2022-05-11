using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Services.Shared.Model
{
    [DataContract]
    public class RetornoProcModel
    {
        [DataMember(Name = "Status")]
        public int Status { get; set; }

        [DataMember(Name = "Mensagem")]
        public string Mensagem { get; set; }
    }
}
