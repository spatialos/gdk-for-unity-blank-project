using System.Collections.Generic;
using Improbable;
using Improbable.Gdk.Core;
using Improbable.Gdk.PlayerLifecycle;
using Improbable.Gdk.QueryBasedInterest;
using Improbable.Generated;
using UnityEngine;

namespace BlankProject.Scripts.Config
{
    public static class EntityTemplates
    {
        public static EntityTemplate CreatePlayerEntityTemplate(EntityId entityId, EntityId clientWorkerEntityId,
            byte[] serializedArguments)
        {
            var template = BaseTemplate();
            template.AddComponent(new Position.Snapshot(Coordinates.FromUnityVector(new Vector3(0, 1f, 0))));
            template.AddComponent(new Metadata.Snapshot("Player"));
            template.AddPlayerLifecycleComponents(clientWorkerEntityId);

            var interest = InterestTemplate.Create()
                .AddQueries(ComponentSets.PlayerClientSet, InterestQuery.Query(Constraint.RelativeCylinder(500)));
            template.AddComponent(interest.ToSnapshot());

            return template;
        }

        public static EntityTemplate CreatePlayerSpawner()
        {
            var template = BaseTemplate();
            template.AddComponent(new Position.Snapshot());
            template.AddComponent(new Metadata.Snapshot("PlayerCreator"));
            template.AddComponent(new Persistence.Snapshot());
            template.AddComponent(new PlayerCreator.Snapshot());

            return template;
        }

        public static EntityTemplate CreateLoadBalancingPartition()
        {
            var template = BaseTemplate();
            template.AddComponent(new Position.Snapshot());
            template.AddComponent(new Metadata.Snapshot("LB Partition"));

            var query = InterestQuery.Query(Constraint.Component<Position.Component>());
            var interest = InterestTemplate.Create().AddQueries(ComponentSets.AuthorityDelegationSet, query);
            template.AddComponent(interest.ToSnapshot());
            return template;
        }

        private static EntityTemplate BaseTemplate()
        {
            var template = new EntityTemplate();
            template.AddComponent(new AuthorityDelegation.Snapshot(new Dictionary<uint, long>
            {
                { ComponentSets.AuthorityDelegationSet.ComponentSetId, 1 }
            }));
            return template;
        }
    }
}
