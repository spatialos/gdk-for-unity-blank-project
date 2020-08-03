using System;
using System.Collections.Generic;
using Improbable.Gdk.Core;
using Improbable.Gdk.Core.Representation;
using Improbable.Gdk.GameObjectCreation;
using Improbable.Gdk.Subscriptions;
using Improbable.Gdk.TransformSynchronization;
using Unity.Entities;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scripts.Worker
{
    public class GameObjectCreatorFromTransform : IEntityGameObjectCreator
    {
        private readonly string workerType;
        private readonly Vector3 workerOrigin;

        private readonly Dictionary<EntityId, GameObject> entityIdToGameObject = new Dictionary<EntityId, GameObject>();

        private readonly Type[] componentsToAdd =
        {
            typeof(Transform), typeof(Rigidbody), typeof(MeshRenderer)
        };

        public GameObjectCreatorFromTransform(string workerType, Vector3 workerOrigin)
        {
            this.workerType = workerType;
            this.workerOrigin = workerOrigin;
        }

        public void PopulateEntityTypeExpectations(EntityTypeExpectations entityTypeExpectations)
        {
            entityTypeExpectations.RegisterDefault(new[]
            {
                typeof(TransformInternal.Component)
            });
        }

        public void OnEntityCreated(SpatialOSEntityInfo entityInfo, GameObject prefab, EntityManager entityManager, EntityGameObjectLinker linker)
        {
            var transformInternal = entityManager.GetComponentData<TransformInternal.Component>(entityInfo.Entity);
            var position = transformInternal.Location.ToUnityVector() + workerOrigin;
            var rotation = transformInternal.Rotation.ToUnityQuaternion();

            var gameObject = Object.Instantiate(prefab, position, rotation);
            gameObject.name = $"{prefab.name}(SpatialOS: {entityInfo.SpatialOSEntityId}, Worker: {workerType})";

            entityIdToGameObject.Add(entityInfo.SpatialOSEntityId, gameObject);
            linker.LinkGameObjectToSpatialOSEntity(entityInfo.SpatialOSEntityId, gameObject, componentsToAdd);
        }

        public void OnEntityRemoved(EntityId entityId)
        {
            if (!entityIdToGameObject.TryGetValue(entityId, out var go))
            {
                return;
            }

            Object.Destroy(go);
            entityIdToGameObject.Remove(entityId);
        }
    }
}
