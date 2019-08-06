using System;
using System.Collections;
using Improbable;
using Improbable.Gdk.Subscriptions;
using Unity.Entities;
using UnityEngine;

namespace Scripts.Sphere
{
    public class SphereMovementBehaviour : MonoBehaviour
    {
        // to ensure we are authoritative
        [Require] private PositionWriter positionWriter;

        public float forcePerPush = 250f;
        public float pushInterval = 2f;

        private Rigidbody rigidbody;
        private Vector3 workerOrigin;

        private void OnEnable()
        {
            rigidbody = GetComponent<Rigidbody>();
            workerOrigin = GetComponent<LinkedEntityComponent>().Worker.Origin;

            StartCoroutine(ApplyForce());
        }

        private void OnDisable()
        {
            StopCoroutine(ApplyForce());
        }

        IEnumerator ApplyForce()
        {
            while (true)
            {
                if (rigidbody != null)
                {
                    var direction = (workerOrigin - transform.position).normalized;
                    rigidbody.AddForce(forcePerPush * direction);
                }

                yield return new WaitForSeconds(pushInterval);
            }
        }
    }
}
