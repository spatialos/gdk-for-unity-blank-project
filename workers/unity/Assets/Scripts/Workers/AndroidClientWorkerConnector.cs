using Improbable.Gdk.Core;
using Improbable.Gdk.Mobile;
#if UNITY_ANDROID
using Improbable.Gdk.Mobile.Android;
#endif
using Improbable.Gdk.PlayerLifecycle;
using System;
using UnityEngine;


namespace BlankProject
{
    public class AndroidClientWorkerConnector : MobileWorkerConnector
    {
        public const string WorkerType = "AndroidClient";

        [SerializeField] private string ipAddress;
        [SerializeField] private bool shouldConnectLocally;

        private async void Start()
        {
            await Connect(WorkerType, new ForwardingDispatcher()).ConfigureAwait(false);
        }

        protected override ConnectionService GetConnectionService()
        {
            return shouldConnectLocally ? ConnectionService.Receptionist : ConnectionService.AlphaLocator;
        }

        protected override void HandleWorkerConnectionEstablished()
        {
            PlayerLifecycleHelper.AddClientSystems(Worker.World);
        }

        protected override string GetHostIp()
        {
#if UNITY_ANDROID
            if (!string.IsNullOrEmpty(ipAddress))
            {
                return ipAddress;
            }

            return RuntimeConfigDefaults.ReceptionistHost;
#else
            throw new PlatformNotSupportedException(
                $"{nameof(AndroidClientWorkerConnector)} can only be used for the Android platform. Please check your build settings.");
#endif
        }
    }
}
