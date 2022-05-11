using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Services.Shared.Model
{
    [DataContract]
    public class OcorrenciaPendenteModel
    {
        [DataMember(Name = "occurrenceId")]
        public List<int> occurrenceId { get; set; }
    }
}
