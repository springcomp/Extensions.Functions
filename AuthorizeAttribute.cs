using Microsoft.Azure.WebJobs.Description;
using System;

namespace LplCloud.Functions.Extensions
{
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter)]
    public class AuthorizeAttribute : Attribute
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="AuthorizeAttribute" /> class.
        /// </summary>
        /// <param name="applications"></param>
        public AuthorizeAttribute(string applications)
        {
            Applications = applications;
        }

        /// <summary>
        /// The comma-separated list of allowed application identifiers (client-ids).
        /// May contain binding parameters.
        /// </summary>
        [AutoResolve]
        public string Applications { get; private set; }
    }
}
