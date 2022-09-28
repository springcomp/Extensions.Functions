using LplCloud.Functions.Extensions.Bindings;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Logging;

namespace LplCloud.Functions.Extensions
{
    [Extension("EasyAuth")]
    public class EasyAuthExtensionConfigProvider : IExtensionConfigProvider
    {
        private readonly ILogger logger_;

        /// <summary>
        /// Initialize a new instance of the <see cref="EasyAuthExtensionConfigProvider" /> class.
        /// This class is responsible for establishing a link from the <see cref="AuthorizeAttribute" /> attribute
        /// to its <see cref="ClaimsPrincipalBindingProvider" /> factory.
        /// </summary>
        /// <param name="logger"></param>
        public EasyAuthExtensionConfigProvider(ILogger<EasyAuthExtensionConfigProvider> logger)
            : this((ILogger)logger)
        { }

        /// <summary>
        /// Initialize a new instance of the <see cref="EasyAuthExtensionConfigProvider" /> class.
        /// This class is responsible for establishing a link from the <see cref="AuthorizeAttribute" /> attribute
        /// to its <see cref="ClaimsPrincipalBindingProvider" /> factory.
        /// </summary>
        /// <param name="logger"></param>
        public EasyAuthExtensionConfigProvider(ILogger logger)
        {
            logger_ = logger;
        }

        /// <summary>
        /// This callback is invoked by the WebJobs framework before the host starts execution. 
        /// It should add the binding rules and converters for our new <see cref="AuthorizeAttribute"/> 
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(ExtensionConfigContext context)
        {
            var rule = context.AddBindingRule<AuthorizeAttribute>();
            rule.Bind(new ClaimsPrincipalBindingProvider(logger_));
        }
    }
}
