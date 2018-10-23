using System.Collections.Generic;
using Improbable.Gdk.Core;
using Improbable.Gdk.PlayerLifecycle;
using Improbable.Gdk.TransformSynchronization;

namespace BlankProject
{
    public class UnityGameLogicConnector : WorkerConnector
    {
        public const string WorkerType = "UnityGameLogic";
        
        private static readonly List<string> AllWorkerAttributes =
            new List<string> { UnityClientConnector.WorkerType, WorkerType };
        
        private async void Start()
        {
            PlayerLifecycleConfig.CreatePlayerEntityTemplate = CreatePlayerEntityTemplate;
            await Connect(WorkerType, new ForwardingDispatcher()).ConfigureAwait(false);
        }

        protected override void HandleWorkerConnectionEstablished()
        {
            PlayerLifecycleHelper.AddServerSystems(Worker.World);
        }

        private static EntityTemplate CreatePlayerEntityTemplate(string workerId, Improbable.Vector3f position)
        {
            var clientAttribute = $"workerId:{workerId}";
            var serverAttribute = WorkerType;

            var entityBuilder = EntityBuilder.Begin()
                .AddPosition(0, 0, 0, serverAttribute)
                .AddMetadata("Player", serverAttribute)
                .SetPersistence(false)
                .SetReadAcl(AllWorkerAttributes)
                .SetEntityAclComponentWriteAccess(serverAttribute)
                .AddPlayerLifecycleComponents(workerId, clientAttribute, serverAttribute)
                .AddTransformSynchronizationComponents(clientAttribute);

            return entityBuilder.Build();
        }
    }
}
