using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Services.Shared.Model
{
    [DataContract]
    public class DuplicataPendenteModel
    {
        [DataMember(Name = "faturas")]
        public List<int> faturas { get; set; }
    }
}
