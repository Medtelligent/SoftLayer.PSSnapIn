using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using RestSharp;
using SoftLayer.API.Serialization;
using Newtonsoft.Json;
using System.Net;

namespace SoftLayer.API.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "SoftLayerClient")]
    public class CreateClientCommand : PSCmdlet
    {
        #region [Parameters]

        [Parameter(Position=0, Mandatory=true, ParameterSetName="ApiInfoParameterSet")]
        public string APIUsername { get; set; }

        [Parameter(Position=1, Mandatory=true, ParameterSetName="ApiInfoParameterSet")]
        public string APIKey { get; set; }

        [Parameter(Position=3, ParameterSetName="ApiInfoParameterSet")]
        public SwitchParameter UsePrivateNetwork { get; set; }

        #endregion

        protected override void ProcessRecord()
        {
            WriteObject(new Client(APIUsername, APIKey, UsePrivateNetwork.ToBool()));
        }
    }

    public class Client
    {
        const string PrivateAPIBaseName = "https://api.service.softlayer.com/rest/v3";
        const string PublicAPIBaseName = "https://api.softlayer.com/rest/v3";

        public string APIUsername { get; set; }
        public string APIKey { get; set; }
        public bool UsePrivateNetwork { get; set; }

        public Client(string apiUsername, string apiKey, bool usePrivateNetwork) 
        {
            APIUsername = apiUsername;
            APIKey = apiKey;
            UsePrivateNetwork = usePrivateNetwork;
        }

        public T Get<T>(string resource, Action<RestRequest> closure, Action<ErrorRecord> onError = null) where T : new()
        {
            return Invoke<T>(resource, Method.GET, closure, onError);
        }

        public T Put<T>(string resource, Action<RestRequest> closure, Action<ErrorRecord> onError = null) where T : new()
        {
            return Invoke<T>(resource, Method.PUT, closure, onError);
        }

        public T Post<T>(string resource, Action<RestRequest> closure, Action<ErrorRecord> onError = null) where T : new()
        {
            return Invoke<T>(resource, Method.POST, closure, onError);
        }

        public T Delete<T>(string resource, Action<RestRequest> closure, Action<ErrorRecord> onError = null) where T : new()
        {
            return Invoke<T>(resource, Method.DELETE, closure, onError);
        }

        private T Invoke<T>(string resource, Method method, Action<RestRequest> closure, Action<ErrorRecord> onError = null) where T : new()
        {
            var client = new RestClient(UsePrivateNetwork ? PrivateAPIBaseName : PublicAPIBaseName)
            {
                Authenticator = new HttpBasicAuthenticator(APIUsername, APIKey)
            };

            // prepare the request
            var request = new RestRequest(resource, method)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new SoftLayer.API.Serialization.JsonSerializer()
            };

            if (closure != null)
            {
                closure(request);
            }

            var response = client.Execute<T>(request);
            if (response.ErrorException != null)
            {
                if (response.ErrorException.GetType() == typeof(InvalidCastException))
                {
                    response.Data = (T)Convert.ChangeType(response.Content, typeof(T));
                }
                else
                {
                    throw response.ErrorException;
                }
            }
            else if ((int)response.StatusCode >= 400)
            {
                var error = default(Dictionary<string, string>);
                if (!string.IsNullOrWhiteSpace(response.Content)) 
                {
                    error = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content); 
                }
                onError(new ErrorRecord(new Exception(error != null && error.ContainsKey("error") ? error["error"] : string.Format("Request failed with status code: ", response.StatusCode)), error != null && error.ContainsKey("code") ? error["code"].Replace("_", "") : "ErrorAPIResponse", ErrorCategory.InvalidResult, request));
                return default(T);
            }
            return response.Data;
        }
    }
}
