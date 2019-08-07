using System;
using System.Collections;
using BlankProject;
using Improbable.Gdk.Subscriptions;
using UnityEngine;

namespace Scripts.Sphere
{
    [WorkerType(UnityClientConnector.WorkerType)]
    public class SphereAnimator : MonoBehaviour
    {
        [Require] private HealthReader healthReader;

        private bool growTriggered;

        private float minGrowth = 0.1f;
        private float maxGrowth = 0.2f;

        private float animLength = 0.1f;

        //basic animation curve following positive half of sine wave
        public AnimationCurve animCurve;

        private void OnEnable()
        {
            healthReader.OnHealedEvent += OnHealEvent;
        }

        private void OnHealEvent(HealInfo obj)
        {
            if (growTriggered)
            {
                return;
            }

            var maxScale = obj.HealType == HealType.FULL ? maxGrowth : minGrowth;
            StartCoroutine(AnimateSphere(maxScale));
        }

        IEnumerator AnimateSphere(float maxScale)
        {
            growTriggered = true;

            var startTime = Time.time;
            var endTime = startTime + animLength;

            do
            {
                var animVal = animCurve.Evaluate((Time.time - startTime) / animLength);

                var scale = 1f + animVal * maxScale;
                transform.localScale = Vector3.one * scale;

                yield return null;
            }
            while (Time.time <= endTime);

            growTriggered = false;
        }
    }
}
