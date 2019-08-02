using System;
using System.Collections;
using BlankProject;
using Improbable.Gdk.Subscriptions;
using UnityEngine;

namespace Scripts.Sphere
{
    public class SphereHealthReducer : MonoBehaviour
    {
        [Require] private HealthWriter healthWriter;

        public int damage = 10;
        public float healthReductionInterval = 5f;

        private void OnEnable()
        {
            StartCoroutine(ReduceHealth());
        }

        private void OnDisable()
        {
            StopCoroutine(ReduceHealth());
        }

        IEnumerator ReduceHealth()
        {
            while (true)
            {
                var newHealth = Math.Max(healthWriter.Data.Health - damage, 0);

                healthWriter.SendUpdate(new Health.Update
                {
                    Health = newHealth
                });

                if (newHealth == 0)
                {
                    break;
                }

                yield return new WaitForSeconds(healthReductionInterval);
            }
        }
    }
}