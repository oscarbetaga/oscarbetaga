using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Camunda.Bpmn.Bpmn
{
    public class BpmnProcessDeployService : IHostedService
    {
        private readonly IBpmnService _bpmnService;
        
        public BpmnProcessDeployService(IBpmnService bpmnService)
        {
            this._bpmnService = bpmnService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _bpmnService.DeployProcessDefinition();
            await _bpmnService.CleanupProcessInstances();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}