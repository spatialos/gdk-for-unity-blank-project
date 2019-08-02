using BlankProject;
using UnityEngine;

namespace Scripts.HealthBar
{
    public class HealthBarUI : MonoBehaviour
    {
        public RectTransform greenRectTransform;

        private float maxWidth = 150f;

        private Transform sphereTransform;
        private UnityEngine.Camera mainCamera;

        private readonly Vector3 sphereOffset = new Vector3(0f, 0.75f, 0f);

        private void OnEnable()
        {
            sphereTransform = transform.parent;
            mainCamera = UnityEngine.Camera.main;
        }

        public void ModifyHealth(int newHealth)
        {
            greenRectTransform.sizeDelta = new Vector2
            {
                x = newHealth * maxWidth / GameConstants.MaxHealth,
                y = greenRectTransform.sizeDelta.y,
            };
        }

        private void LateUpdate()
        {
            transform.localPosition = Quaternion.Inverse(sphereTransform.rotation) * sphereOffset;
            transform.LookAt(mainCamera.transform);
        }
    }
}
