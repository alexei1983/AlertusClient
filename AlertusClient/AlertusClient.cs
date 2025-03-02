using Llc.GoodConsulting.Web.EnhancedWebRequest;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Llc.GoodConsulting.Web.ThirdParty.Alertus
{
    /// <summary>
    /// 
    /// </summary>
    public class AlertusClient
    {
        readonly EnhancedWebRequest.EnhancedWebRequest request;
        readonly JsonSerializerOptions jsonOptions;

        /// <summary>
        /// 
        /// </summary>
        internal JsonSerializerOptions JsonOptions
        {
            get
            {
                return jsonOptions;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool LogToDebug { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool LogToConsole { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AlertusContacts Contacts { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public AlertusGroups Groups { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public AlertusVersion Version { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public AlertusContactMethods ContactMethods { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public AlertusSystemErrors SystemErrors { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public AlertusContactsGroups ContactsGroups { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public AlertusLocations Locations { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public AlertusDevices Devices { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public AlertusActivation Activation { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public AlertusPing Ping { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="apiUsername"></param>
        /// <param name="apiPassword"></param>
        public AlertusClient(string baseUri, string apiUsername, string apiPassword)
        {
            jsonOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, AllowTrailingCommas = true };

            request = new EnhancedWebRequest.EnhancedWebRequest(baseUri, new EnhancedWebRequestOptions()
            {
                Password = apiPassword,
                Username = apiUsername,
                UserAgent = AlertusConstants.UserAgent,
                UserAgentVersion = Assembly.GetExecutingAssembly().GetName().Version,
                JsonSerializerSettings = jsonOptions
            });

            request.ResponseReceived += (obj, e) =>
            {
                if (LogToDebug || LogToConsole)
                {
                    string? response = e.ResponseMessage?.Content?.ReadAsStringAsync()?.Result ?? string.Empty;

                    var logVal = new StringBuilder();
                    logVal.Append($"{e.Url} - {e.StatusCode}");
                    logVal.Append($"{(!string.IsNullOrEmpty(e.ContentType) ? $" - {e.ContentType}" : string.Empty)}");
                    logVal.AppendLine();
                    if (!string.IsNullOrEmpty(response))
                        logVal.AppendLine(response);

                    if (LogToConsole)
                    {
                        Console.WriteLine(logVal);
                        Console.WriteLine();
                    }
                    if (LogToDebug)
                        Debug.WriteLine(logVal);
                }
            };

            request.RequestSent += (obj, e) =>
            {
                if (LogToDebug || LogToConsole)
                {
                    string? request = e.RequestMessage?.Content?.ReadAsStringAsync()?.Result ?? string.Empty;

                    var logVal = new StringBuilder();
                    logVal.Append($"{e.HttpMethod} {e.Url}");
                    logVal.Append($"{(!string.IsNullOrEmpty(e.ContentType) ? $" - {e.ContentType}" : string.Empty)}");
                    logVal.AppendLine();
                    if (!string.IsNullOrEmpty(request))
                        logVal.AppendLine(request);

                    if (LogToConsole)
                    {
                        Console.WriteLine(logVal);
                        Console.WriteLine();
                    }
                    if (LogToDebug)
                        Debug.WriteLine(logVal);
                }
            };

            Contacts = new AlertusContacts(this);
            Groups = new AlertusGroups(this);
            Version = new AlertusVersion(this);
            ContactMethods = new AlertusContactMethods(this);
            SystemErrors = new AlertusSystemErrors(this);
            ContactsGroups = new AlertusContactsGroups(this);
            Locations = new AlertusLocations(this);
            Ping = new AlertusPing(this);
            Devices = new AlertusDevices(this);
            Activation = new AlertusActivation(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<HttpResponseMessage> PutJson<T>(T entity, string? uri) where T : class, new()
        {
            return await request.PutJsonEntity(entity, uri);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<HttpResponseMessage> PostJson<T>(T entity, string? uri) where T : class, new()
        {
            return await request.PostJsonEntity(entity, uri);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<HttpResponseMessage> Get(string? uri)
        {
            return await request.Get(uri);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<HttpResponseMessage> Get(Dictionary<string, string> queryParameters, string? uri)
        {
            return await request.Get(queryParameters, uri);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<HttpResponseMessage> Delete(string? uri)
        {
            return await request.Delete(uri);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal async Task<HttpResponseMessage> DeleteJsonEntity<T>(T entity, string? uri) where T : class, new()
        {
            return await request.DeleteContent(entity.ToJsonStringContent(JsonOptions), uri);
        }
    }
}
