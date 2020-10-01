using System.Net.Http;

namespace WebTools
{
    public static class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }
        public static void InitializeClient()
        {
            
            ApiClient = new HttpClient();           
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.
                Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
   

        }
        public static void SetAuthentificationBearer(string token)
        {
            ApiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
     
        }

        public static void ClearAuthentificationBearer()
        {
            ApiClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
