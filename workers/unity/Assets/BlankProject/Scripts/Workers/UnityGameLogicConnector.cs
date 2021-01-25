using BlankProject.Scripts.Config;
using Improbable.Gdk.Core;
using Improbable.Gdk.Core.Representation;
using Improbable.Gdk.GameObjectCreation;
using Improbable.Gdk.LoadBalancing;
using Improbable.Gdk.PlayerLifecycle;
using Improbable.Generated;
using Improbable.Worker.CInterop;
using Playground.LoadBalancing;
using UnityEngine;

namespace BlankProject
{
    public class UnityGameLogicConnector : WorkerConnector
    {
        [SerializeField] private EntityRepresentationMapping entityRepresentationMapping = default;

        public const string WorkerType = "UnityGameLogic";

        private async void Start()
        {
            PlayerLifecycleConfig.CreatePlayerEntityTemplate = EntityTemplates.CreatePlayerEntityTemplate;
            PlayerLifecycleConfig.PlayerCreatorEntityId = EntityTemplates.PlayerCreatorEntityId;

            IConnectionFlow flow;
            ConnectionParameters connectionParameters;

            if (Application.isEditor)
            {
                flow = new ReceptionistFlow(CreateNewWorkerId(WorkerType));
                connectionParameters = CreateConnectionParameters(WorkerType);

                /*
                 * If we are in the Editor, it means we are either:
                 *      - connecting to a local deployment
                 *      - connecting to a cloud deployment via `spatial cloud connect external`
                 * in the first case, the security type must be Insecure.
                 * in the second case, its okay for the security type to be Insecure.
                */
                connectionParameters.Network.Kcp.SecurityType = NetworkSecurityType.Insecure;
                connectionParameters.Network.Tcp.SecurityType = NetworkSecurityType.Insecure;
            }
            else
            {
                flow = new ReceptionistFlow(CreateNewWorkerId(WorkerType),
                    new CommandLineConnectionFlowInitializer());
                connectionParameters = CreateConnectionParameters(WorkerType,
                    new CommandLineConnectionParameterInitializer());
            }

            var builder = new SpatialOSConnectionHandlerBuilder()
                .SetConnectionFlow(flow)
                .SetConnectionParameters(connectionParameters);

            await Connect(builder, new ForwardingDispatcher()).ConfigureAwait(false);
        }

        protected override void HandleWorkerConnectionEstablished()
        {
            Worker.World.GetOrCreateSystem<MetricSendSystem>();

            PlayerLifecycleHelper.AddServerSystems(Worker.World);
            GameObjectCreationHelper.EnableStandardGameObjectCreation(Worker.World, entityRepresentationMapping, gameObject);

            Worker.AddLoadBalancingSystems(configuration =>
            {
                configuration.AddPartitionManagement(WorkerType, UnityClientConnector.WorkerType, MobileClientWorkerConnector.WorkerType);
                configuration.AddClientLoadBalancing("Player", ComponentSets.PlayerClientSet);
                configuration.SetSingletonLoadBalancing(WorkerType, new EntityLoadBalancingMap(ComponentSets.DefaultServerSet).AddOverride("Player", ComponentSets.PlayerServerSet));
            });
        }
    }
}
