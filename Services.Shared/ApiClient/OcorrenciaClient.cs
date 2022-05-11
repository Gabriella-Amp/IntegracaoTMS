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
        public async Task<OcorrenciaPendenteModel> GetOcorrenciaPendente()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "/api/ocorrencia/buscarRegistrosParaIntegracao/100"));
            return await GetAsync<OcorrenciaPendenteModel>(requestUrl);
        }

        public async Task<OcorrenciaModel> PostOcorrenciaCompleta(int idOcorrencia)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "/api/ocorrencia/recuperarDados"));
            return await PostAsyncOcorrencia<OcorrenciaModel>(requestUrl, idOcorrencia);
        }

        public async Task<RetornoModel> PostOcorrenciaRetorno(OcorrenciaRetornoModel req)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "/api/ocorrencia/salvarRetornoEventoOcorrencia"));
            return await PostAsyncRetorno<RetornoModel>(requestUrl, req);
        }
    }
}
