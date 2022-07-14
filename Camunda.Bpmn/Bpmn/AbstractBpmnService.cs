using Camunda.Api.Client;
using Camunda.Api.Client.Deployment;
using Camunda.Api.Client.ProcessInstance;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Camunda.Bpmn.Bpmn
{
    public abstract class AbstractBpmnService
    {
        private readonly CamundaClient _camunda;

        public CamundaClient Camunda { get => _camunda; }

        public AbstractBpmnService(string camundaRestApiUri)
        {
            _camunda = CamundaClient.Create(camundaRestApiUri);
        }

        public async Task DeployProcessDefinition(string manifestResourceName, string deploymentName, string bpmnFileName)
        {
            Stream bpmnResourceStream = this.GetType()
                .Assembly
                .GetManifestResourceStream(manifestResourceName);

            try
            {
                DeploymentInfo info = await _camunda.Deployments.Create(
                    deploymentName,
                    true,
                    true,
                    null,
                    null,
                    new ResourceDataContent(bpmnResourceStream, bpmnFileName));
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to deploy process definition", e);
            }
        }

        public async Task CleanupProcessInstances(string processId)
        {
            var instances = await _camunda.ProcessInstances
                .Query(new ProcessInstanceQuery
                {
                    ProcessDefinitionKey = processId
                })
                .List();

            //if (instances.Count > 0)
            //{
            //    await camunda.ProcessInstances.Delete(new DeleteProcessInstances
            //    {
            //        ProcessInstanceIds = instances.Select(i => i.Id).ToList()
            //    });
            //}
        }

        public abstract Task<string> StartProcessFor();
    }
}