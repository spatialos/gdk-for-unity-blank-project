using System.IO;
using BlankProject.Scripts.Config;
using Improbable.Gdk.Core;
using UnityEditor;
using UnityEngine;
using Snapshot = Improbable.Gdk.Core.Snapshot;

namespace BlankProject.Editor
{
    internal static class SnapshotGenerator
    {
        private static readonly string DefaultSnapshotPath = Path.GetFullPath(
            Path.Combine(
                Application.dataPath,
                "..",
                "..",
                "..",
                "snapshots",
                "default.snapshot"));

        [MenuItem("SpatialOS/Generate snapshot")]
        public static void Generate()
        {
            Debug.Log("Generating snapshot.");
            var snapshot = CreateSnapshot();

            Debug.Log($"Writing snapshot to: {DefaultSnapshotPath}");
            snapshot.WriteToFile(DefaultSnapshotPath);
        }

        private static Snapshot CreateSnapshot()
        {
            var snapshot = new Snapshot();
            snapshot.AddEntity(EntityTemplates.LoadBalancingPartitionEntityId, EntityTemplates.CreateLoadBalancingPartition());
            snapshot.AddEntity(EntityTemplates.PlayerCreatorEntityId, EntityTemplates.CreatePlayerSpawner());
            return snapshot;
        }
    }
}
