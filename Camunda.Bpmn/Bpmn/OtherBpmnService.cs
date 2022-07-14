using Camunda.Api.Client;
using Camunda.Api.Client.ProcessDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camunda.Bpmn.Bpmn
{
    public class OtherBpmnService : IBpmnService
    {
        private readonly CamundaClient camunda;

        public OtherBpmnService(string camundaRestApiUri)
        {
            this.camunda = CamundaClient.Create(camundaRestApiUri);
        }

        public async Task CleanupProcessInstances()
        {
            
        }

        public async Task DeployProcessDefinition()
        {
            try
            {
                await StartProcessFor();
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to deploy process definition", e);
            }
        }

        public async Task<string> StartProcessFor()
        {
            var processParams = new StartProcessInstance()
                .SetVariable("consultaRUNT", VariableValue.FromObject(true));

            processParams.BusinessKey = Guid.NewGuid().ToString();

            var processStartResult = await
                camunda.ProcessDefinitions.ByKey("fenix22").StartProcessInstance(processParams);

            return processStartResult.Id;
        }
    }
}