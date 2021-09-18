using Amazon.SQS;
using AWSSQSAPI.Helpers.Contratos;
using AWSSQSAPI.Helpers.Interfaces;
using AWSSQSAPI.Models;
using AWSSQSAPI.Services.Contratos;
using AWSSQSAPI.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AWSSQSAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private readonly string MinhasEspecificacoesOrigem = "MinhasEspecificacoesOrigem";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var appSettings = Configuration.GetSection("ServiceConfiguration");
            services.AddAWSService<IAmazonSQS>();
            services.Configure<ServiceConfiguration>(appSettings);
            services.AddTransient<IAWSSQSService, AWSSQSService>();
            services.AddTransient<IAWSSQSHelper, AWSSQSHelper>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AWSSQSAPI", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: MinhasEspecificacoesOrigem, builder =>
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
                // .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AWSSQSAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(MinhasEspecificacoesOrigem);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}