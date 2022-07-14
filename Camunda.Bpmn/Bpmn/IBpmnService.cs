using System.Threading.Tasks;

namespace Camunda.Bpmn.Bpmn
{
    public interface IBpmnService
    {
        Task DeployProcessDefinition();
        Task CleanupProcessInstances();
    }
}