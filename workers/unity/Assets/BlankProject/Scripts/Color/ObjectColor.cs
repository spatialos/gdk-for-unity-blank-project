using BlankProject;
using Improbable.Gdk.Subscriptions;
using UnityEngine;

namespace Scripts.Color
{
    [WorkerType(UnityClientConnector.WorkerType)]
    public class ObjectColor : MonoBehaviour
    {
        public UnityEngine.Color color;
        private Renderer renderer;

        private void OnEnable()
        {
            renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            renderer.material.color = color;
        }
    }
}
