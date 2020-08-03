using Improbable;
using Improbable.Gdk.Subscriptions;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerInputBehaviour : MonoBehaviour
    {
        //to check we're authoritative over this player
        [Require] private PositionWriter positionWriter;

        public float defaultSpeed = 5f;

        // Cache the main camera's transform
        private Transform mainCameraTransform;

        private void OnEnable()
        {
            mainCameraTransform = UnityEngine.Camera.main.transform;
        }

        private void Update()
        {
            var h = Input.GetAxisRaw("Horizontal");
            var v = Input.GetAxisRaw("Vertical");

            var forward = mainCameraTransform.forward;
            var right = mainCameraTransform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            var direction = (forward * v) + (right * h);
            var speed = Input.GetKey(KeyCode.LeftShift)
                ? 2 * defaultSpeed
                : defaultSpeed;

            transform.Translate(speed * Time.deltaTime * direction);
        }
    }
}
