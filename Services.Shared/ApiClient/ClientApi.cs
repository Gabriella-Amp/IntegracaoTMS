using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.Shared.Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Service.Shared.ApiClient
{
    public partial class ClientApi
    {

        private readonly HttpClient _httpClient;
        private Uri BaseEndpoint { get; set; }

        public ClientApi(Uri baseEndpoint)
        {
            if (baseEndpoint == null)
            {
                throw new ArgumentNullException("baseEndpoint");
            }
            BaseEndpoint = baseEndpoint;
            _httpClient = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            addHeaders();
        }

        private async Task<T> GetAsync<T>(Uri requestUrl)
        {
            
            var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<T>(data);

            return json;
        }

        private async Task<CTEModel> PostAsyncCTE<T>(Uri requestUrl, int idCTe)
        {
            dynamic objReq = new ExpandoObject();
            objReq.cte = idCTe;
            var initialJson = JsonConvert.SerializeObject(objReq);
            var httpContent = new StringContent(initialJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(requestUrl, httpContent);

            //var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var dataJson = JObject.Parse(data);
            data = dataJson["ctes"].ToString();            

            var json = JsonConvert.DeserializeObject<List<CTEModel>>(data);

            return json[0];
        }

        private async Task<OcorrenciaModel> PostAsyncOcorrencia<T>(Uri requestUrl, int idOcorrencia)
        {
            dynamic objReq = new ExpandoObject();
            objReq.oidEventoOcorrencia = idOcorrencia;
            var initialJson = JsonConvert.SerializeObject(objReq);
            var httpContent = new StringContent(initialJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(requestUrl, httpContent);

            //var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var dataJson = JObject.Parse(data);
            data = dataJson["data"].ToString();

            var json = JsonConvert.DeserializeObject<List<OcorrenciaModel>>(data);

            return json[0];
        }

        private async Task<string> GetAsyncCTEDownload<T>(Uri requestUrl, string chaveCTe)
        {
            XmlDocument xmlDoc = new XmlDocument();
            var response = await _httpClient.GetAsync(requestUrl + "/" + chaveCTe, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();

            return data;
        }

        private async Task<RetornoModel> PostSyncImportacaoXML<T>(Uri requestUrl, string arquivoXML)
        {         
            //StringContent(initialJson, Encoding.UTF8, "application/json");
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent(arquivoXML), "arquivo");
            var response = await _httpClient.PostAsync(requestUrl, content);

            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var dataJson = JObject.Parse(data);

            var json = JsonConvert.DeserializeObject<RetornoModel>(data);

            return json;
        }

        private async Task<RetornoModel> PostAsyncRetorno<T>(Uri requestUrl, object requisicao)
        {
            var initialJson = JsonConvert.SerializeObject(requisicao);
            var httpContent = new StringContent(initialJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(requestUrl, httpContent);

            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var dataJson = JObject.Parse(data);

            var json = JsonConvert.DeserializeObject<List<RetornoModel>>(data);

            return json[0];
        }




        /// <summary>
        /// Common method for making POST calls
        /// </summary>
        private async Task<bool> PostAsyncArquivo<T>(Uri requestUrl, T content, string name)
        {
            var initialJson = "{\"filename\":\""+name+".txt\",\"content\":\""+content+"\"}";
            var httpContent = new StringContent(initialJson, Encoding.UTF8, "application/json");
            //var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            var response = await _httpClient.PostAsync(requestUrl.ToString(), httpContent);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
            //var data = await response.Content.ReadAsStringAsync();
            //return JsonConvert.DeserializeObject<Message<T>>(data);
        }

        private async Task<bool> PostAsyncArquivoJson<T>(Uri requestUrl, T content, string name)
        {
            var initialJson = "{\"filename\":\"" + name + ".json\",\"content\":\"" + content + "\"}";
            var httpContent = new StringContent(initialJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(requestUrl.ToString(), httpContent);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }

        private async Task<T> PostAsyncLista<T>(Uri requestUrl)
        {
            var initialJson = "{\"initialDate\":\"2019-07-02T17:11:20-0300\",\"finalDate\":\"2019-07-15T17:11:20-0300\"}";
            var httpContent = new StringContent(initialJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(requestUrl.ToString(), httpContent);
            //response.EnsureSuccessStatusCode();
           /// var data = await response.Content.ReadAsByteArrayAsync();

            var responseBytes = await response.Content.ReadAsByteArrayAsync();

            var _responseString = Encoding.UTF8.GetString(responseBytes, 0, responseBytes.Length);
            //return _responseString;

            return JsonConvert.DeserializeObject<T>(_responseString);
        }

        private async Task<T> PostAsyncListAll<T>(Uri requestUrl, decimal numdocumento)
        {
            dynamic objFilter = new ExpandoObject();
            objFilter.docNum = numdocumento;
            objFilter.pdfBytes = true;
            var initialJson = JsonConvert.SerializeObject(objFilter);
            var httpContent = new StringContent(initialJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(requestUrl.ToString(), httpContent);
            var responseBytes = await response.Content.ReadAsByteArrayAsync();
            var _responseString = Encoding.UTF8.GetString(responseBytes, 0, responseBytes.Length);
            return JsonConvert.DeserializeObject<T>(_responseString);
        }

        private async Task<T> PostAsyncListaGuia<T>(Uri requestUrl, decimal numdocumento)
        {
            var initialJson = "{\"docNum\":\"" + numdocumento + "\"}";
            var httpContent = new StringContent(initialJson, Encoding.UTF8, "application/json");
            //var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            var response = await _httpClient.PostAsync(requestUrl.ToString(), httpContent);
            response.EnsureSuccessStatusCode();

            var responseBytes = await response.Content.ReadAsByteArrayAsync();

            var _responseString = Encoding.UTF8.GetString(responseBytes, 0, responseBytes.Length);
            //return _responseString;

            return JsonConvert.DeserializeObject<T>(_responseString);
        }

        private Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            var endpoint = new Uri(BaseEndpoint, relativePath);
            var uriBuilder = new UriBuilder(endpoint);
            uriBuilder.Query = queryString;
            return endpoint;
        }

        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            var initialJson = "[{\"filename\":\"teste.txt\",\"content\":\"content\"}]";
            var array = JArray.Parse(initialJson);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }

        private void addHeaders()
        {
            _httpClient.DefaultRequestHeaders.Add("token", "");
        }
    }
}
