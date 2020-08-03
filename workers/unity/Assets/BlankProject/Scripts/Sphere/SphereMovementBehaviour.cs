using System.Collections;
using Improbable;
using Improbable.Gdk.Subscriptions;
using UnityEngine;

namespace Scripts.Sphere
{
    public class SphereMovementBehaviour : MonoBehaviour
    {
        // to ensure we are authoritative
        [Require] private PositionWriter positionWriter;

        [SerializeField] private float forcePerPush = 1000f;
        [SerializeField] private float pushInterval = 5f;

        private Rigidbody rigidbody;
        private Vector3 workerOrigin;

        private Coroutine applyForceCoroutine;

        private void OnEnable()
        {
            rigidbody = GetComponent<Rigidbody>();
            workerOrigin = GetComponent<LinkedEntityComponent>().Worker.Origin;

            applyForceCoroutine = StartCoroutine(ApplyForce());
        }

        private void OnDisable()
        {
            StopCoroutine(applyForceCoroutine);
        }

        IEnumerator ApplyForce()
        {
            while (true)
            {
                yield return new WaitForSeconds(pushInterval);

                if (rigidbody != null)
                {
                    var direction = (workerOrigin - transform.position).normalized;
                    rigidbody.AddForce(forcePerPush * direction);
                }
            }
        }
    }
}
