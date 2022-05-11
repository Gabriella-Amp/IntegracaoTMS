using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Services.Shared.Model
{
    [DataContract]
    public class RetornoModel
    {
        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "mensagem")]
        public string mensagem { get; set; }
    }
}
