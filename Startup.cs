using Common.Configs;
using HealthChecks.UI.Client;
using MasterData.Pool.Protos;
using MasterData.Area.Protos;
using MasterData.Repositories.Interface;
using MasterData.Services;
using MasterData.Repositories.MySql;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using MasterData.Repositories;
using MasterData.Repositories.Cache;
using System.Net;
using MasterData.Company.Protos;
using Elastic.Apm.NetCoreAll;


namespace MasterData
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            
            #region Http Rest Policy
            //var policyConfigs = new HttpClientPolicyConfiguration();
            //Configuration.Bind(policyConfigs);
            var policyConfigs = new HttpClientPolicyConfiguration();
            Configuration.Bind("HttpClientPolicies", policyConfigs);

            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(policyConfigs.RetryTimeoutInSeconds));

            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(r => r.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(policyConfigs.RetryCount, _ => TimeSpan.FromMilliseconds(policyConfigs.RetryDelayInMs));

            var circuitBreakerPolicy = HttpPolicyExtensions
               .HandleTransientHttpError()
               .CircuitBreakerAsync(policyConfigs.MaxAttemptBeforeBreak, TimeSpan.FromSeconds(policyConfigs.BreakDurationInSeconds));

            var noOpPolicy = Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();

            //register repo rest
            //services.AddHttpClient<SalesApi>(client =>
            //{
            //    client.BaseAddress = new Uri(Configuration["RestSettings:SalesUrl"]);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpContentMediaTypes.JSON));

            //})
            //.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            //{
            //    ClientCertificateOptions = ClientCertificateOption.Manual,
            //    ServerCertificateCustomValidationCallback =
            //    (httpRequestMessage, cert, cetChain, policyErrors) =>
            //    {
            //        return true;
            //    }
            //}).SetHandlerLifetime(TimeSpan.FromMinutes(policyConfigs.HandlerTimeoutInMinutes))
            //    .AddPolicyHandler(request => request.Method == HttpMethod.Get ? retryPolicy : noOpPolicy)
            //    .AddPolicyHandler(timeoutPolicy)
            //    .AddPolicyHandler(circuitBreakerPolicy);

            #endregion

            #region GRPC Client Configuration

            //services.AddGrpcClient<CompanyGrpcService.CompanyGrpcServiceClient>(o =>
            //{
            //    o.Address = new Uri(Configuration["GrpcService:CompanyService"]);
            //});

            //services.AddGrpcClient<EmployeeProtoService.EmployeeProtoServiceClient>
            //    (o => o.Address = new Uri(Configuration["GrpcService:EmployeeUrl"])).ConfigureChannel(options =>
            //    {
            //        options.HttpClient = HttpClientHelper.GetInsecure();
            //    });
            #endregion

            // Redis Configuration
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetValue<string>("CacheSettings:ConnectionString");
            });

            //services.AddSingleton<EmployeeGrpc>();

            //IOC
            services.AddScoped<PoolDb>();
            services.AddScoped<PoolCache>();
            services.AddScoped<AreaDb>();
            services.AddScoped<AreaCache>();
            services.AddScoped<CompanyDb>();
            services.AddScoped<CompanyCache>();
            services.AddScoped<ServiceTypeDb>();
            services.AddScoped<ServiceTypeCache>();

            //services.AddScoped<CompanyClient>();

            services.AddScoped<IPoolRepository, PoolRepository>();
            services.AddScoped<IAreaRepository, AreaRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();

            services.AddScoped<PoolGrpcService.PoolGrpcServiceBase, PoolService>();
            services.AddScoped<AreaGrpcService.AreaGrpcServiceBase, AreaService>();
            services.AddScoped<CompanyGrpcService.CompanyGrpcServiceBase, CompanyService>();

            services.AddAutoMapper(typeof(Startup));

            //services.AddGrpc();
            services.AddGrpc().AddJsonTranscoding();
            services.AddGrpcReflection();

            services.AddControllers();
            services.AddGrpcSwagger();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MasterData.Service", Version = "v1" });
                //c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                //var filePath = Path.Combine(System.AppContext.BaseDirectory, "Server.xml");
                //c.IncludeXmlComments(filePath);
                //c.IncludeGrpcXmlComments(filePath, includeControllerXmlComments: true);
            });
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAllElasticApm(Configuration);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MasterData.Service v1"));

            app.UseRouting();

            //app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<PoolService>();
                endpoints.MapGrpcService<AreaService>();
                endpoints.MapGrpcService<CompanyService>();
                endpoints.MapGrpcService<VehicleTypeService>();

                endpoints.MapGrpcReflectionService(); //  Focus!!!
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });

                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}