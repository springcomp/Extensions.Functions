using Microsoft.Azure.WebJobs;
using System;

namespace LplCloud.Functions.Extensions
{
    public static class EasyAuthHostBuilderExtensions
    {
        public static IWebJobsBuilder AddEasyAuth(this IWebJobsBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.AddEasyAuth(o => { });

            return builder;
        }

        public static IWebJobsBuilder AddEasyAuth(this IWebJobsBuilder builder, Action<EasyAuthOptions> configure)
        { 
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (configure == null)
                throw new ArgumentNullException(nameof(configure));

            builder.AddExtension<EasyAuthExtensionConfigProvider>()
                .ConfigureOptions<EasyAuthOptions>((config, path, options) =>
                {
                });

            return builder;
        }
    }
}
