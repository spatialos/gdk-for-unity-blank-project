using System.Collections.Generic;
using Improbable.Gdk.Core;
using Improbable.PlayerLifecycle;
using UnityEngine;
using Snapshot = Improbable.Gdk.Core.Snapshot;

namespace BlankProject.Editor
{
    internal static class SnapshotGenerator
    {
        public struct Arguments
        {
            public string OutputPath;
        }
        
        private static readonly List<string> UnityWorkers =
            new List<string> { UnityClientConnector.WorkerType, UnityGameLogicConnector.WorkerType };

        public static void Generate(Arguments arguments)
        {
            Debug.Log("Generating snapshot.");
            var snapshot = CreateSnapshot();

            Debug.Log($"Writing snapshot to: {arguments.OutputPath}");
            snapshot.WriteToFile(arguments.OutputPath);
        }

        private static Snapshot CreateSnapshot()
        {
            var snapshot = new Snapshot();

            AddPlayerSpawner(snapshot);
            return snapshot;
        }

        private static void AddPlayerSpawner(Snapshot snapshot)
        {
            var playerCreator = PlayerCreator.Component.CreateSchemaComponentData();
            var serverAttribute = UnityGameLogicConnector.WorkerType;
            
            var spawner = EntityBuilder.Begin()
                .AddPosition(0, 0, 0, serverAttribute)
                .AddMetadata("PlayerCreator", serverAttribute)
                .SetPersistence(true)
                .SetReadAcl(UnityWorkers)
                .AddComponent(playerCreator, serverAttribute)
                .Build();
            
            snapshot.AddEntity(spawner);
        }
    }
}
