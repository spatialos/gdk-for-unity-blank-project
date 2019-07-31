using Improbable;
using Improbable.Gdk.Core;
using Improbable.Gdk.PlayerLifecycle;
using Improbable.Gdk.TransformSynchronization;
using UnityEngine;

namespace BlankProject
{
    public static class EntityTemplates
    {
        public static EntityTemplate CreatePlayerEntityTemplate(string workerId, byte[] serializedArguments)
        {
            var clientAttribute = EntityTemplate.GetWorkerAccessAttribute(workerId);
            var serverAttribute = UnityGameLogicConnector.WorkerType;

            var template = new EntityTemplate();
            template.AddComponent(new Position.Snapshot(), clientAttribute);
            template.AddComponent(new Metadata.Snapshot("Player"), serverAttribute);
            PlayerLifecycleHelper.AddPlayerLifecycleComponents(template, workerId, serverAttribute);

            template.SetReadAccess(UnityClientConnector.WorkerType, MobileClientWorkerConnector.WorkerType, serverAttribute);
            template.SetComponentWriteAccess(EntityAcl.ComponentId, serverAttribute);

            return template;
        }

        public static EntityTemplate CreateSphereTemplate(Vector3 position = default)
        {
            var serverAttribute = UnityGameLogicConnector.WorkerType;

            var template = new EntityTemplate();
            template.AddComponent(new Position.Snapshot(Coordinates.FromUnityVector(position)), serverAttribute);
            template.AddComponent(new Metadata.Snapshot("Sphere"), serverAttribute);
            template.AddComponent(new Persistence.Snapshot(), serverAttribute);

            template.SetReadAccess(UnityClientConnector.WorkerType, MobileClientWorkerConnector.WorkerType, serverAttribute);
            template.SetComponentWriteAccess(EntityAcl.ComponentId, serverAttribute);

            return template;
        }
    }
}
