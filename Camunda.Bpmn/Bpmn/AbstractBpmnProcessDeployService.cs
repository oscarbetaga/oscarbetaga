using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Camunda.Bpmn.Bpmn
{
    public class AbstractBpmnProcessDeployService : IHostedService
    {
        private readonly Test _bpmnService;

        public AbstractBpmnProcessDeployService(Test bpmnService)
        {
            _bpmnService = bpmnService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _bpmnService.DeployProcessDefinition();
            await _bpmnService.CleanupProcessInstances();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}