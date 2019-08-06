using Improbable;
using Improbable.Gdk.Subscriptions;
using UnityEngine;

namespace Scripts.Camera
{
    public class FollowPlayerCamera : MonoBehaviour
    {
        //for authority
        [Require] private PositionWriter positionWriter;

        // The min/max distance from camera to the player.
        private const float MinCameraDistance = 2.0f;
        private const float MaxCameraDistance = 10.0f;
        private const float ZoomScale = 10.0f;

        // The min/max vertical angles for the camera (so you can't clip through the floor).
        private const float MinYAngle = 0.0f;
        private const float MaxYAngle = 80.0f;

        // Origin offset to make camera orbit character's head rather than their feet.
        private static readonly Vector3 TargetOffset = new Vector3(0, 1, 0);

        // Keep track of the last recorded mouse input
        private float inputX;
        private float inputY;
        private float inputDistance;

        // Cache the main camera's transform
        private Transform mainCameraTransform;

        private void OnEnable()
        {
            inputY = 40f;
            inputDistance = 5f;

            mainCameraTransform = UnityEngine.Camera.main.transform;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void LateUpdate()
        {
            UpdateCameraInput();
            UpdateCameraTransform(transform.position);
        }

        private void UpdateCameraInput()
        {
            var x = inputX + Input.GetAxis("Mouse X");
            var y = inputY - Input.GetAxis("Mouse Y");
            var distance = inputDistance + Input.GetAxis("Mouse ScrollWheel") * ZoomScale;

            inputX = x % 360;
            inputY = Mathf.Clamp(y, MinYAngle, MaxYAngle);
            inputDistance = Mathf.Clamp(distance, MinCameraDistance, MaxCameraDistance);
        }

        private void UpdateCameraTransform(Vector3 targetPosition)
        {
            var dir = new Vector3(0, 0, -inputDistance);
            var orbitRotation = Quaternion.Euler(inputY, inputX, 0);
            var position = targetPosition + TargetOffset + orbitRotation * dir;
            var rotation = Quaternion.LookRotation(targetPosition + TargetOffset - position);

            mainCameraTransform.position = position;
            mainCameraTransform.rotation = rotation;
        }
    }
}