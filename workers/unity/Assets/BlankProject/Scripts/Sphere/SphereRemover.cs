using BlankProject;
using Improbable.Gdk.Core;
using Improbable.Gdk.Core.Commands;
using Improbable.Gdk.Subscriptions;
using UnityEngine;

namespace Scripts.Sphere
{
    public class SphereRemover : MonoBehaviour
    {
        [Require] private HealthWriter healthWriter;
        [Require] private WorldCommandSender worldCommandSender;
        [Require] private EntityId entityId;

        private void OnEnable()
        {
            healthWriter.OnHealthUpdate += OnHealthUpdate;
        }

        private void OnHealthUpdate(int obj)
        {
            if (obj <= 0)
            {
                worldCommandSender.SendDeleteEntityCommand(new WorldCommands.DeleteEntity.Request(entityId));
            }
        }
    }
}