using System;
using Improbable.Gdk.Core;
using Improbable.Gdk.Core.Representation;
using Improbable.Gdk.GameObjectCreation;
using Improbable.Gdk.PlayerLifecycle;
using Improbable.Worker.CInterop;
using UnityEngine;

namespace BlankProject
{
    public class UnityClientConnector : WorkerConnector
    {
        [SerializeField] private EntityRepresentationMapping entityRepresentationMapping = default;

        public const string WorkerType = "UnityClient";

        private async void Start()
        {
            var connParams = CreateConnectionParameters(WorkerType);

            var builder = new SpatialOSConnectionHandlerBuilder()
                .SetConnectionParameters(connParams);

            if (!Application.isEditor)
            {
                var initializer = new CommandLineConnectionFlowInitializer();
                switch (initializer.GetConnectionService())
                {
                    case ConnectionService.Receptionist:
                        /*
                         * If we are connecting via the Receptionist we are either:
                         *      - connecting to a local deployment
                         *      - connecting to a cloud deployment via `spatial cloud connect external`
                         * in the first case, the security type must be Insecure.
                         * in the second case, its okay for the security type to be Insecure.
                        */
                        connParams.Network.Kcp.SecurityType = NetworkSecurityType.Insecure;
                        connParams.Network.Tcp.SecurityType = NetworkSecurityType.Insecure;
                        builder.SetConnectionFlow(new ReceptionistFlow(CreateNewWorkerId(WorkerType), initializer));
                        break;
                    case ConnectionService.Locator:
                        builder.SetConnectionFlow(new LocatorFlow(initializer));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                // We are in the Editor, so for the same reasons as above, the network security type should be Insecure.
                connParams.Network.Kcp.SecurityType = NetworkSecurityType.Insecure;
                connParams.Network.Tcp.SecurityType = NetworkSecurityType.Insecure;
                builder.SetConnectionFlow(new ReceptionistFlow(CreateNewWorkerId(WorkerType)));
            }

            await Connect(builder, new ForwardingDispatcher()).ConfigureAwait(false);
        }

        protected override void HandleWorkerConnectionEstablished()
        {
            PlayerLifecycleHelper.AddClientSystems(Worker.World);
            GameObjectCreationHelper.EnableStandardGameObjectCreation(Worker.World, entityRepresentationMapping);
        }
    }
}
