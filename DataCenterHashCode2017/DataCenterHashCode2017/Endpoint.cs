using System.Net.NetworkInformation;

namespace DataCenterHashCode2017
{
    public struct Endpoint
    {
        public int nbLinkedCashServer;
        public int dataCenterLatency;

        public CacheServer[] cacheServers;
        public int[] latencyToCashServer;
    }
}