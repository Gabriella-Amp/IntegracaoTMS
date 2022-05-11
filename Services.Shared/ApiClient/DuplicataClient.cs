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
        public async Task<DuplicataPendenteModel> GetDuplicataPendente()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "/api/fatura/buscarRegistrosParaIntegracao/100"));
            return await GetAsync<DuplicataPendenteModel>(requestUrl);
        }

        public async Task<DuplicataModel> PostDuplicataCompleta(int idDupl)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "/api/fatura/recuperarDados"));
            return await PostAsyncDuplicata<DuplicataModel>(requestUrl, idDupl);
        }

        public async Task<RetornoModel> PostDuplicataRetorno(DuplicataRetornoModel req)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "/api/fatura/salvarRetornoFatura"));
            return await PostAsyncRetorno<RetornoModel>(requestUrl, req);
        }
    }
}
