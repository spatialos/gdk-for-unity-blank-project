using BlankProject;
using Improbable;
using Improbable.Gdk.Subscriptions;
using Improbable.Worker.CInterop;
using Scripts.Sphere;
using UnityEngine;

namespace Scripts.Player
{
    public class SphereHealingBehaviour : MonoBehaviour
    {
        // position for authority
        [Require] private PositionWriter positionWriter;
        [Require] private HealthCommandSender healthCommandSender;

        [SerializeField] private GameObject crosshairPrefab;

        private UnityEngine.Camera mainCamera;

        private readonly Vector3 reticleOffset = new Vector3(0f, 50f, 0f);

        private void OnEnable()
        {
            mainCamera = UnityEngine.Camera.main;

            Instantiate(crosshairPrefab, mainCamera.transform);
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            var ray = mainCamera.ScreenPointToRay(Input.mousePosition + reticleOffset);
            if (!Physics.Raycast(ray, out var hit))
            {
                return;
            }

            if (hit.transform.GetComponent<SphereHealthTracker>() == null)
            {
                return;
            }

            var targetEntityId = hit.transform.GetComponent<LinkedEntityComponent>().EntityId;
            healthCommandSender.SendHealCommand(
                targetEntityId,
                new HealRequest(GameConstants.HealAmount),
                response =>
                {
                    if (response.StatusCode != StatusCode.Success)
                    {
                        Debug.LogWarning($"Command failed: {response.Message}");
                    }
                });
        }
    }
}
