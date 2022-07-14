using System;
using System.Linq;
using System.Threading.Tasks;
using Camunda.Api.Client;
using Camunda.Api.Client.Deployment;
using Camunda.Api.Client.ProcessDefinition;
using Camunda.Api.Client.ProcessInstance;

namespace Camunda.Bpmn.Bpmn
{
    public class BpmnService : IBpmnService
    {
        private readonly CamundaClient camunda;

        public BpmnService(string camundaRestApiUri)
        {
            this.camunda = CamundaClient.Create(camundaRestApiUri);
        }

        public async Task CleanupProcessInstances()
        {
            //var instances = await camunda.ProcessInstances
            //    .Query(new ProcessInstanceQuery
            //    {
            //        ProcessDefinitionKey = "fenix22"
            //    })
            //    .List();

            //if (instances.Count > 0)
            //{
            //    await camunda.ProcessInstances.Delete(new DeleteProcessInstances
            //    {
            //        ProcessInstanceIds = instances.Select(i => i.Id).ToList()
            //    });
            //}
        }

        public async Task DeployProcessDefinition()
        {
            var bpmnResourceStream = this.GetType()
                .Assembly
                .GetManifestResourceStream($"Camunda.Bpmn.Bpmn.0-sistema-fenix-22.bpmn");

            try
            {
                var result = await camunda.Deployments.Create(
                    "Test Deploy",
                    true,
                    true,
                    null,
                    null,
                    new ResourceDataContent(bpmnResourceStream, "0-sistema-fenix-22.bpmn"));

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

        //public async Task<List<UserTaskInfo>> GetTasksForCandidateGroup(string group, string user)
        //{
        //    var groupTaskQuery = new TaskQuery
        //    {
        //        ProcessDefinitionKeys = { "Process_Hire_Hero" },
        //        CandidateGroup = group
        //    };
        //    var groupTasks = await camunda.UserTasks.Query(groupTaskQuery).List();

        //    if (user != null)
        //    {
        //        var userTaskQuery = new TaskQuery
        //        {
        //            ProcessDefinitionKeys = { "Process_Hire_Hero" },
        //            Assignee = user
        //        };
        //        var userTasks = await camunda.UserTasks.Query(userTaskQuery).List();

        //        groupTasks.AddRange(userTasks);    
        //    }

        //    return groupTasks;
        //}

        //public async Task<UserTaskInfo> ClaimTask(string taskId, string user)
        //{
        //    await camunda.UserTasks[taskId].Claim(user); 
        //    var task = await camunda.UserTasks[taskId].Get();
        //    return task;
        //}

        //public async Task<UserTaskInfo> CompleteTask(string taskId, Order order)
        //{
        //    var task = await camunda.UserTasks[taskId].Get();
        //    var completeTask = new CompleteTask()
        //        .SetVariable("orderStatus", VariableValue.FromObject(order.Status.ToString()));
        //    await camunda.UserTasks[taskId].Complete(completeTask);
        //    return task;
        //}

        //public async Task SendMessageInvoicePaid(Order order)
        //{
        //    await camunda.Messages.DeliverMessage(new CorrelationMessage
        //    {
        //        BusinessKey = order.Id.Value.ToString(),
        //        MessageName = "Message_InvoicePaid"
        //    });
        //}

    }
}