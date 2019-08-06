using System;
using BlankProject;
using Improbable;
using Improbable.Gdk.Core;
using Improbable.Gdk.Core.Commands;
using Improbable.Gdk.Subscriptions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Player
{
    [WorkerType(UnityClientConnector.WorkerType)]
    public class SphereSpawnerBehaviour : MonoBehaviour
    {
        // authoritative player
        [Require] private PositionWriter positionWriter;
        [Require] private WorldCommandSender worldCommandSender;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SendSpawnSphereCommand();
            }
        }

        private void SendSpawnSphereCommand()
        {
            var randomPosition = new Vector3
            {
                x = Random.Range(-50f, 50f),
                y = Random.Range(5f, 20f),
                z = Random.Range(-50f, 50f)
            };

            var sphereTemplate = EntityTemplates.CreateSphereTemplate(randomPosition);

            worldCommandSender.SendCreateEntityCommand(new WorldCommands.CreateEntity.Request(sphereTemplate));
        }
    }
}
