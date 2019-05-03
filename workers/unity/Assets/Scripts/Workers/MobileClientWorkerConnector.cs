using Improbable.Gdk.Core;
using Improbable.Gdk.Mobile;
using Improbable.Gdk.PlayerLifecycle;

namespace BlankProject
{
    public class MobileClientWorkerConnector : DefaultMobileWorkerConnector
    {
        public const string WorkerType = "MobileClient";

        private async void Start()
        {
            await Connect(WorkerType, new ForwardingDispatcher()).ConfigureAwait(false);
        }

        protected override void HandleWorkerConnectionEstablished()
        {
            PlayerLifecycleHelper.AddClientSystems(Worker.World);
        }
    }
}
