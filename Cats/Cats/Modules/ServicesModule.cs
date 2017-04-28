using System.Net;
using System.Net.Http;
using Autofac;
using Cats.API;
using Cats.Services;

namespace Cats.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<APIConfiguration>().As<IAPIConfiguration>().SingleInstance();
            builder.RegisterType<JsonService>().As<IJsonService>().SingleInstance();

            builder.Register(x =>
            {
                var httpClientHandler = new HttpClientHandler { Proxy = WebRequest.DefaultWebProxy };
                return new HttpClient(httpClientHandler);
            });
        }
    }
}
