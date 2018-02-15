using System.Net.NetworkInformation;

namespace DataCenterHashCode2017
{
    public struct Endpoint
    {
        public int nbLinkedCashServer;
        public int dataCenterLatency;

        public CasheServer[] casheServers;
        public int[] latencyToCashServer;
        public Video[] videos;
        public int[] request;
    }
}