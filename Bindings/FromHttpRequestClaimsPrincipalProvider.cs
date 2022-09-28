using System;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Logging;

namespace LplCloud.Functions.Extensions.Bindings
{
    /// <summary>
    /// This class is parses the incoming <see cref="HttpRequest" /> function parameter
    /// and extracts the corresponding user information as a <see cref="ClaimsPrincipal" /> object.
    /// </summary>
    internal class FromHttpRequestClaimsPrincipalProvider : IValueProvider
    {
        private readonly HttpRequest request_;
        private readonly AuthorizeAttribute attribute_;
        private readonly ILogger logger_;

        /// <summary>
        /// Initialize a new instance of the <see cref="FromHttpRequestClaimsPrincipalProvider" /> class.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="attribute"></param>
        /// <param name="logger"></param>
        public FromHttpRequestClaimsPrincipalProvider(HttpRequest request, AuthorizeAttribute attribute, ILogger logger)
        {
            request_ = request;
            attribute_ = attribute;
            logger_ = logger;
        }

        public Type Type => typeof(ClaimsPrincipal);

        public Task<object> GetValueAsync()
        {
            var claimsPrincipal = Parse(request_);
            return Task.FromResult((object)claimsPrincipal);
        }

        public string ToInvokeString()
          => string.Empty;

        private ClaimsPrincipal Parse(HttpRequest req)
            => req.HttpContext.User;
    }
}