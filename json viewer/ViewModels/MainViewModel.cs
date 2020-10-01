﻿using json_viewer.Commands;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text.Json;
using WebTools;

namespace json_viewer
{
    public class MainViewModel : BaseViewModel
    {
        private string url;
        private string jsonContent;
        private string Token;

        public string URL { 
            get => url;
            set { 
                url = value;
                OnPropertyChanged();
                GetJsonCommand.RaiseCanExecuteChanged();
            }
        }

        public string JsonContent
        {
            get => jsonContent;
            set { 
                jsonContent = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand<string> GetJsonCommand { get; set; }

        public MainViewModel()
        {
            GetJsonCommand = new DelegateCommand<string>(GetJson, CanGetJson);
            Token = "KLoYzcYP9IVeKe6PraUO";
            ApiHelper.SetAuthentificationBearer(Token);
            URL = "https://the-one-api.dev/v2/movie";
        }

        private bool CanGetJson(string url)
        {  
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }

        private async void GetJson(string url)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var temp = await response.Content.ReadAsStringAsync();
                    JsonContent = PrettyJson(temp);
                } else
                {
                    JsonContent = response.ReasonPhrase;
                }
            }
        }

        public string PrettyJson(string unPrettyJson)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            var jsonElement = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(unPrettyJson);

            return System.Text.Json.JsonSerializer.Serialize(jsonElement, options);
        }
        

    }
}
