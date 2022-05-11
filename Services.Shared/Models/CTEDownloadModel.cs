using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Services.Shared.Model
{
    [XmlRoot("cteProc")]
    public class CTEDownloadModel
    {
        [DataMember(Name = "cnpjUnidade")]
        public string cnpjUnidade { get; set; }

        [DataMember(Name = "linkRepositorio")]
        public string linkRepositorio { get; set; }

        [DataMember(Name = "chave")]
        public string chave { get; set; }
    }
}
