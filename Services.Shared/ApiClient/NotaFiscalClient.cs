using Services.Shared.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Service.Shared.ApiClient
{
    public partial class ClientApi
    {
        public async Task<RetornoModel> PostImportacaoXML(string arquivoXML)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "/api/importacaoXML/upload"));
            return await PostSyncImportacaoXML<RetornoModel>(requestUrl, arquivoXML);
        }
    }
}
