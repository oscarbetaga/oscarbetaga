using System;
using Camunda.Worker;
using Camunda.Worker.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Camunda.Bpmn.Bpmn
{
    public static class BpmnInstaller
    {
        public static IServiceCollection AddCamunda(this IServiceCollection services, string camundaRestApiUri)
        {
            ////Service 1
            //services.AddSingleton<IBpmnService>(_ => new BpmnService(camundaRestApiUri));
            //services.AddHostedService(_ => new BpmnProcessDeployService(new BpmnService(camundaRestApiUri)));

            ////Service 2
            //services.AddSingleton<IBpmnService>(_ => new OtherBpmnService(camundaRestApiUri));
            //services.AddHostedService(_ => new OtherBpmnProcessDeployService(new OtherBpmnService(camundaRestApiUri)));

            services.AddSingleton<AbstractBpmnService>(_ => new Test(camundaRestApiUri));
            services.AddHostedService(_ => new AbstractBpmnProcessDeployService(new Test(camundaRestApiUri)));

            services.AddExternalTaskClient()
                .ConfigureHttpClient((provider, client) =>
                {
                    client.BaseAddress = new Uri(camundaRestApiUri);
                });

            services.AddCamundaWorker("sampleWorker")
                .AddHandler<TestHandler>();
                //.ConfigurePipeline(pipeline =>
                //{
                //    pipeline.Use(next => async context =>
                //    {
                //        var logger = context.ServiceProvider.GetRequiredService<ILogger<Startup>>();
                //        logger.LogInformation("Started processing of task {Id}", context.Task.Id);
                //        await next(context);
                //        logger.LogInformation("Finished processing of task {Id}", context.Task.Id);
                //    });
                //});

            return services;
        }
    }
}