using Camunda.Api.Client;
using Camunda.Api.Client.ProcessDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camunda.Bpmn.Bpmn
{
    public class Test : AbstractBpmnService
    {
        public Test(string camundaRestApiUri) : base(camundaRestApiUri)
        {
        }

        public async Task DeployProcessDefinition()
        {
            await DeployProcessDefinition("Camunda.Bpmn.Bpmn.0-sistema-fenix-22.bpmn", "Test Deploy", "0-sistema-fenix-22.bpmn");
            await StartProcessFor();
        }

        public async Task CleanupProcessInstances()
        {
            await CleanupProcessInstances("fenix22");
        }

        public override async Task<string> StartProcessFor()
        {
            var processParams = new StartProcessInstance()
                .SetVariable("consultaRUNT", VariableValue.FromObject(true));

            processParams.BusinessKey = Guid.NewGuid().ToString();

            var processStartResult = await
                Camunda.ProcessDefinitions.ByKey("fenix22").StartProcessInstance(processParams);

            return processStartResult.Id;
        }
    }
}