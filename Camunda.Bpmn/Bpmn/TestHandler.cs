using Camunda.Worker;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Camunda.Bpmn.Bpmn
{
    [HandlerTopics("get-github", LockDuration = 10000)]
    public class TestHandler : IExternalTaskHandler
    {
        public Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            return Task.FromResult<IExecutionResult>(new CompleteResult
            {
                Variables = new Dictionary<string, Variable>
                {
                    ["MESSAGE"] = Variable.String("Hello, Guest!")
                }
            });
        }
    }
}