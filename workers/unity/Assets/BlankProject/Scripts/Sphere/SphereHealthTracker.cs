using BlankProject;
using Improbable.Gdk.Subscriptions;
using Scripts.HealthBar;
using UnityEngine;

namespace Scripts.Sphere
{
    [WorkerType(UnityClientConnector.WorkerType)]
    public class SphereHealthTracker : MonoBehaviour
    {
        [Require] private HealthReader healthReader;

        [SerializeField] private GameObject healthBarPrefab;

        private HealthBarUI healthBar;

        private void OnEnable()
        {
            if (healthBar == null)
            {
                healthBar = Instantiate(healthBarPrefab, transform).GetComponent<HealthBarUI>();
            }
        }

        private void Update()
        {
            healthBar.VisualiseHealth(healthReader.Data.Health);
        }
    }
}
