using ECorp.Integration.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System.Threading;

namespace ECorp.Realtime.SignalR
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    //.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((host) => true)
                    //.DisallowCredentials()
                    .AllowCredentials()
                );
            });

            services.AddHttpContextAccessor();
            services.AddSignalR();
            //services.AddAssemblyNameAsApplicationId();
            services
                .AddConnectionProvider()
                .AddRabbitConnectionProvider();
            services
                .AddRabbitMQIntegration("realtime-broker",
                    (connectionProvider) => connectionProvider
                        .GetConnectorOptions<Integration.RabbitMQ.ConnectionOptions>("RabbitMQConnectionOptions")
                        , ServiceLifetime.Singleton
                );
            services
                .AddEventHandler<Events.Handlers.IRealtimeEventHandler, Events.Handlers.RealtimeEventHandler>(ServiceLifetime.Singleton);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapHub<Hubs.NotificationHub>("/events/{eventName}/subscribe", (opt) =>
                {
                    opt.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransports.All;
                });

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
            ConfigureEventBus(app.ApplicationServices.GetRequiredService<IEventBus>());
        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
        private void ConfigureEventBus(IEventBus eventBus)
        {
            eventBus.Subscribe<Events.RealtimeEventDescriptor, Events.Handlers.IRealtimeEventHandler>();
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            eventBus.Connect(cancellationTokenSource.Token);
        }
    }
}
