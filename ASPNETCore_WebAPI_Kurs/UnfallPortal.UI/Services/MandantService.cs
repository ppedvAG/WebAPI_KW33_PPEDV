using Newtonsoft.Json;
using System.Text;
using UnfallPortal.Shared.Entities;

namespace UnfallPortal.UI.Services
{
    public class MandantService : IMandantService
    {
        private readonly HttpClient _httpClient;
        private string baseURL = "https://localhost:7280/api/Mandants/";

        public MandantService(HttpClient client)
        {
            _httpClient = client;
        }

        

        public async Task<IList<Mandant>> GetAll()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseURL);
              
            //await = warten solange bis uns der HttpClient ein Ergebniss liefert
            HttpResponseMessage responseResult = await _httpClient.SendAsync(request);

            //Auslesen des Ergebnisses in json
            string jsonText = await responseResult.Content.ReadAsStringAsync();

            List<Mandant> mandantenListe = JsonConvert.DeserializeObject<List<Mandant>>(jsonText);

            return mandantenListe;
        }

        public async Task<Mandant> GetById(int id)
        {
            string extendedURL = baseURL + id.ToString();
          
            //await = warten solange bis uns der HttpClient ein Ergebniss liefert
            HttpResponseMessage responseResult = await _httpClient.GetAsync(extendedURL);

            //Auslesen des Ergebnisses in json
            string jsonText = await responseResult.Content.ReadAsStringAsync();

            Mandant mandanten = JsonConvert.DeserializeObject<Mandant>(jsonText);

            return mandanten;
        }

        public async Task Insert(Mandant mandant)
        {
            string jsonText = JsonConvert.SerializeObject(mandant);

            StringContent data = new StringContent(jsonText, Encoding.Default, "application/json");

            _httpClient.Timeout = TimeSpan.FromSeconds(100);

            //https://localhost:7280/api/Mandants
            HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:7280/NeuerMandant", data);


            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                //...Wenn ein 404 Fehler kommt, bekommen wir diesen mit 
            }
        }

        public async Task Update(int id, Mandant mandant)
        {
            string extendetURL = baseURL + id.ToString();

            string json = JsonConvert.SerializeObject(mandant);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(extendetURL, data);
        }

        public async Task Delete(int id)
        {
            string url = baseURL + id.ToString();
            HttpResponseMessage response = await _httpClient.DeleteAsync(url);
        }
    }
}
