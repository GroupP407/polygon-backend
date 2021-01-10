using System.Reflection;
using App.Metrics;
using App.Metrics.Extensions.Configuration;
using App.Metrics.Formatters.Ascii;
using App.Metrics.Formatters.Prometheus;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Polygon.API
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
            services.AddAppMetricsCollectors();
            services.AddControllers();
            services.AddAutoMapper(Assembly.Load("Polygon.API"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Polygon.API", Version = "v1"});
            });
            services.AddAuthorization();
            services.AddMetrics();
            services.AddMetricsEndpoints(options =>
            {
                options.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
                options.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
            });
            services.AddMetricsTrackingMiddleware();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Polygon.API v1"));
            }

            app.UseMetricsAllEndpoints();
            app.UseMetricsAllMiddleware();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
        }
    }
}