using System;
using System.Collections;
using BlankProject;
using Improbable.Gdk.Subscriptions;
using UnityEngine;

namespace Scripts.Sphere
{
    public class SphereHealthManager : MonoBehaviour
    {
        [Require] private HealthWriter healthWriter;

        [Require] private HealthCommandReceiver healthCommandReceiver;

        public int damage = 10;
        public float healthReductionInterval = 5f;

        private void OnEnable()
        {
            healthCommandReceiver.OnHealRequestReceived += OnHealRequest;

            StartCoroutine(ReduceHealth());
        }

        private void OnHealRequest(Health.Heal.ReceivedRequest obj)
        {
            var health = healthWriter.Data.Health;
            if (health >= GameConstants.MaxHealth)
            {
                return;
            }

            health += obj.Payload.HealAmount;
            health = Math.Min(health, GameConstants.MaxHealth);

            healthWriter.SendUpdate(new Health.Update
            {
                Health = health
            });

            healthWriter.SendHealedEvent(new HealInfo
            {
                HealType = health == GameConstants.MaxHealth
                    ? HealType.FULL
                    : HealType.PARTIAL
            });
        }

        private void OnDisable()
        {
            StopCoroutine(ReduceHealth());
        }

        IEnumerator ReduceHealth()
        {
            while (true)
            {
                var health = healthWriter.Data.Health;
                if (health <= 0)
                {
                    break;
                }

                health -= damage;
                health = Math.Max(health, 0);

                healthWriter.SendUpdate(new Health.Update
                {
                    Health = health
                });

                yield return new WaitForSeconds(healthReductionInterval);
            }
        }
    }
}
