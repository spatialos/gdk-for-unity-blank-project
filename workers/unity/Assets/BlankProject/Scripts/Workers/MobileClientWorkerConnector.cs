using System;
using Improbable.Gdk.Core;
using Improbable.Gdk.Core.Representation;
using Improbable.Gdk.GameObjectCreation;
using Improbable.Gdk.Mobile;
using Improbable.Gdk.PlayerLifecycle;
using Improbable.Worker.CInterop;
using UnityEngine;

namespace BlankProject
{
    public class MobileClientWorkerConnector : WorkerConnector, MobileConnectionFlowInitializer.IMobileSettingsProvider
    {
        [SerializeField] private EntityRepresentationMapping entityRepresentationMapping = default;

#pragma warning disable 649
        [SerializeField] private string ipAddress;
#pragma warning restore 649

        public const string WorkerType = "MobileClient";

        public async void Start()
        {
            var connParams = CreateConnectionParameters(WorkerType, new MobileConnectionParametersInitializer());

            var flowInitializer = new MobileConnectionFlowInitializer(
                new MobileConnectionFlowInitializer.CommandLineSettingsProvider(),
                new MobileConnectionFlowInitializer.PlayerPrefsSettingsProvider(),
                this);

            var builder = new SpatialOSConnectionHandlerBuilder()
                .SetConnectionParameters(connParams);

            switch (flowInitializer.GetConnectionService())
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
                    builder.SetConnectionFlow(new ReceptionistFlow(CreateNewWorkerId(WorkerType),
                        flowInitializer));
                    break;
                case ConnectionService.Locator:
                    builder.SetConnectionFlow(new LocatorFlow(flowInitializer));
                    break;
                default:
                    throw new ArgumentException("Received unsupported connection service.");
            }

            await Connect(builder, new ForwardingDispatcher()).ConfigureAwait(false);
        }

        protected override void HandleWorkerConnectionEstablished()
        {
            PlayerLifecycleHelper.AddClientSystems(Worker.World);
            GameObjectCreationHelper.EnableStandardGameObjectCreation(Worker.World, entityRepresentationMapping);
        }

        public Option<string> GetReceptionistHostIp()
        {
            return string.IsNullOrEmpty(ipAddress) ? Option<string>.Empty : new Option<string>(ipAddress);
        }

        public Option<string> GetDevAuthToken()
        {
            var token = Resources.Load<TextAsset>("DevAuthToken")?.text.Trim();
            return token ?? Option<string>.Empty;
        }

        public Option<ConnectionService> GetConnectionService()
        {
            return Option<ConnectionService>.Empty;
        }
    }
}
