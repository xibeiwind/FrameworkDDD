using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Net.Http;
using System.Text;

namespace GeekTime.Mobile.Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region 
            //services.AddResponseCaching(options =>
            //{

            //});
            services.AddHealthChecks();



            var secrityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]));
            services.AddSingleton(secrityKey);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//�Ƿ���֤Issuer
                        ValidateAudience = true,//�Ƿ���֤Audience
                        ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                        ClockSkew = TimeSpan.FromSeconds(30),
                        ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                        ValidAudience = "localhost",//Audience
                        ValidIssuer = "localhost",//Issuer
                        IssuerSigningKey = secrityKey//�õ�SecurityKey
                    };
                });
            #endregion

            services.AddOcelot(Configuration);
            #region

            //HttpClientHandler handler = new HttpClientHandler();

            //SocketsHttpHandler socketsHttpHandler = new SocketsHttpHandler();
            //var cc = new HttpClient();
            //var client = new HttpClient(socketsHttpHandler, disposeHandler: false);

            //client.Dispose();
            services.AddControllers();

            services.AddHttpClient("sss").ConfigurePrimaryHttpMessageHandler(() =>
            {
                var hand = new SocketsHttpHandler();
                ///SET Your Proxy
                return new SocketsHttpHandler();
            }).SetHandlerLifetime(TimeSpan.FromSeconds(60));


            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
                options.ForwardedHeaders = ForwardedHeaders.All;
            });
            #endregion


            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseHttpsRedirection();

            if (Configuration.GetValue("USE_Forwarded_Headers", false))
            {
                app.UseForwardedHeaders();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            //app.UseResponseCaching();
            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/live");
                endpoints.MapHealthChecks("/ready");
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapControllers();

                endpoints.MapDefaultControllerRoute();
            });

            app.UseOcelot().Wait();
        }
    }
}
