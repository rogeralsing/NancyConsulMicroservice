﻿using System;
using System.Threading.Tasks;
using Microphone.Core.ClusterProviders;
using Microphone.Core.ClusterProviders;
namespace Microphone.Core
{
    public static class Cluster
    {
        private static IClusterProvider _clusterProvider;
        private static IFrameworkProvider _frameworkProvider;

        public static Task<ServiceInformation[]> FindServiceInstancesAsync(string name)
        {
            return _clusterProvider.FindServiceInstancesAsync(name);
        }

        public static Task<ServiceInformation> FindServiceInstanceAsync(string name)
        {
            return _clusterProvider.FindServiceInstanceAsync(name);
        }

        public static void Bootstrap(IFrameworkProvider frameworkProvider, IClusterProvider clusterProvider, string serviceName, string version)
        {
            _frameworkProvider = frameworkProvider;
            var uri = _frameworkProvider.Start(serviceName, version);
            var serviceId = serviceName + Guid.NewGuid();
            _clusterProvider = clusterProvider;
            _clusterProvider.RegisterServiceAsync(serviceName, serviceId, version, uri);
        }          
    }
}