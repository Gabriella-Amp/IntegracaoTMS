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
        public async Task<CTEPendenteModel> GetCTEPendente()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "/api/cte/buscarRegistrosParaIntegracao/100"));
            return await GetAsync<CTEPendenteModel>(requestUrl);
        }

        public async Task<CTEModel> PostCTECompleto(int idCTe)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "/api/cte/recuperarDados"));
            return await PostAsyncCTE<CTEModel>(requestUrl, idCTe);
        }

        public async Task<string> GetCTEDownload(string chaveCTe)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "/api/downloadXML/downloadCTe/"));
            return await GetAsyncCTEDownload<string>(requestUrl, chaveCTe);
        }

        public async Task<RetornoModel> PostCTERetorno(CTERetornoModel req)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "/api/cte/salvarRetornoCte"));
            return await PostAsyncRetorno<RetornoModel>(requestUrl, req);
        }

        /*public async Task<List<ListaGuiaModel>> GetListaGuia()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "/api/v2/doodoc/pagtrib/payment/history"));
            return await PostAsyncLista<List<ListaGuiaModel>>(requestUrl);
        }

        public async Task<GuiaTribModel> GetGuiaGNRE(decimal id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "/api/v2/doodoc/pagtrib/paymentBatch/" + id));
            return await GetAsyncGuia<GuiaTribModel>(requestUrl);
        }

        public async Task<List<ListaGuiaModel>> PostPagTrib(decimal numdocumento)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "/api/v2/doodoc/pagtrib/list"));
            return await PostAsyncListaGuia<List<ListaGuiaModel>>(requestUrl, numdocumento);
        }

        public async Task<List<ListaGuiaModel>> PostPagTribListAll(decimal numdocumento)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "/api/v2/doodoc/pagtrib/listAll"));
            return await PostAsyncListAll<List<ListaGuiaModel>>(requestUrl, numdocumento);
        }*/
    }
}
