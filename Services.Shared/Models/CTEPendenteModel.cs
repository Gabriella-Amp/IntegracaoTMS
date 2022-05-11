using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Services.Shared.Model
{
    [DataContract]
    public class CTEPendenteModel
    {
        [DataMember(Name = "ctes")]
        public List<int> ctes { get; set; }
    }
}
