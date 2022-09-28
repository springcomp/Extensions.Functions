using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LplCloud.Functions.Extensions.Bindings
{
    internal class ClaimsPrincipalBinding : IBinding
    {
        private readonly AuthorizeAttribute attribute_;
        private readonly ILogger logger_;

        /// <summary>
        /// Initialize a new instance of the <see cref="ClaimsPrincipalBinding" /> class.
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="logger"></param>
        public ClaimsPrincipalBinding(AuthorizeAttribute attribute, ILogger logger)
        {
            attribute_ = attribute;
            logger_ = logger;
        }

        public bool FromAttribute => true;

        /// <summary>
        /// This method is unused and throws an exception;
        /// </summary>
        /// <param name="value"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
            => throw new NotImplementedException();

        public Task<IValueProvider> BindAsync(BindingContext context)
        {
            // The BindingContext object contains the following parameters:
            // - "$request" {Microsoft.AspNetCore.Http.DefaultHttpRequest}
            // - Query {IDictionary<String, StringValues>}
            // - Headers {IDictionary<String, StringValues>}
            // - req {Microsoft.AspNetCore.Http.DefaultHttpRequest}
            // - sys {Microsoft.Azure.WebJobs.Host.Bindings.SystemBindingData}

            var request = context.BindingData.ContainsKey("$request")
              ? context.BindingData["$request"] as HttpRequest
              : null
              ;

            if (request == null)
                throw new ApplicationException("Missing required HttpRequest parameter to the function.");

            return Task.FromResult<IValueProvider>(
              new FromHttpRequestClaimsPrincipalProvider(
                request,
                attribute_,
                logger_
                ));
        }

        public ParameterDescriptor ToParameterDescriptor()
            => new ParameterDescriptor();
    }
}