using BlankProject;
using Improbable;
using Improbable.Gdk.Subscriptions;
using UnityEngine;

namespace Scripts.Sphere
{
    [WorkerType(UnityClientConnector.WorkerType)]
    public class ObjectColorBehaviour : MonoBehaviour
    {
        [Require] private PositionReader positionReader;

        [SerializeField] private Color color;

        private Renderer renderer;

        private void OnEnable()
        {
            renderer = GetComponent<Renderer>();
            SetColor(color);
        }

        public void SetColor(Color newColor)
        {
            color = newColor;
            renderer.material.color = color;
        }
    }
}