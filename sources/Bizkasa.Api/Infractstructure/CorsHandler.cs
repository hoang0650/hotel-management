using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Bizkasa.Api.Infractstructure
{
    public class CorsHandler : DelegatingHandler
    {
        private const string Origin = "Origin";
        private const string AccessControlRequestMethod = "Access-Control-Request-Method";
        private const string AccessControlRequestHeaders = "Access-Control-Request-Headers";
        private const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
        private const string AccessControlAllowMethods = "Access-Control-Allow-Methods";
        private const string AccessControlAllowCredentials = "Access-Control-Allow-Credentials";
        private const string AccessControlAllowHeaders = "Access-Control-Allow-Headers";

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool isCorsRequest = request.Headers.Contains(Origin);
            bool isPreflightRequest = request.Method == HttpMethod.Options;

            if (isCorsRequest)
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                bool accessControlAllowCredentials = request.Headers.Contains(AccessControlAllowCredentials);
                if (!accessControlAllowCredentials)
                {
                    response.Headers.Add(AccessControlAllowCredentials, "true");
                }

                if (isPreflightRequest)
                {
                  
                    response.Headers.Add(AccessControlAllowOrigin, request.Headers.GetValues(Origin).First());

                    string accessControlRequestMethod = request.Headers.GetValues(AccessControlRequestMethod).FirstOrDefault();
                    if (accessControlRequestMethod != null)
                    {
                        response.Headers.Add(AccessControlAllowMethods, accessControlRequestMethod);
                    }

                  

                    string requestedHeaders = string.Join(", ", request.Headers.GetValues(AccessControlRequestHeaders));
                    if (!string.IsNullOrEmpty(requestedHeaders))
                    {
                        response.Headers.Add(AccessControlAllowHeaders, requestedHeaders);
                    }

                    var tcs = new TaskCompletionSource<HttpResponseMessage>();
                    tcs.SetResult(response);
                    return tcs.Task;
                }

                return base.SendAsync(request, cancellationToken).ContinueWith(t =>
                {
                    HttpResponseMessage resp = t.Result;
                    resp.Headers.Add(AccessControlAllowOrigin, request.Headers.GetValues(Origin).First());
                    return resp;
                });
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}