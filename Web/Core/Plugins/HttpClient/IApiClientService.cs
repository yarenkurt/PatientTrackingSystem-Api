using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web.Core.Plugins.HttpClient
{
    public interface IApiClientService
    {
        Task<HttpResponseMessage> Get(string url, AuthenticationType type = AuthenticationType.Bearer);
        Task<HttpResponseMessage> Post(string url, string parameters = "", AuthenticationType type = AuthenticationType.Bearer);
        Task<HttpResponseMessage> Put(string url, string parameters, AuthenticationType type = AuthenticationType.Bearer);
        Task<HttpResponseMessage> Delete(string url, AuthenticationType type = AuthenticationType.Bearer);
        Task<HttpResponseMessage> DeleteRange(string url, IEnumerable<int> list, AuthenticationType type = AuthenticationType.Bearer);
    }
    public enum AuthenticationType
    {
        None,
        Bearer,
        Basic
    }
}