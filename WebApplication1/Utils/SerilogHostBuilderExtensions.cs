using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Utils
{
    public static class SerilogHostBuilderExtensions
    {
        public static IHostBuilder UseSerilog(this IHostBuilder builder,
            Serilog.ILogger logger = null, bool dispose = false)
        {
            builder.ConfigureServices((context, collection) =>
                collection.AddSingleton<ILoggerFactory>(services => new SerilogLoggerFactory((ILogger)logger, dispose)));
            return builder;
        }
    }
}
