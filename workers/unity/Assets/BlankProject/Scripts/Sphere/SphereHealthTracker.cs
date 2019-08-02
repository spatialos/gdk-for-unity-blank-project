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
            healthBar = Instantiate(healthBarPrefab, transform).GetComponent<HealthBarUI>();
            healthBar.ModifyHealth(healthReader.Data.Health);

            healthReader.OnHealthUpdate += healthBar.ModifyHealth;
        }
    }
}
