using System.Net.NetworkInformation;

namespace DataCenterHashCode2017
{
    public class Endpoint
    {
        public int nbLinkedCashServer;
        public int dataCenterLatency;

        public int[] cashServer;
        public int[] latencyToCashServer;
        public int[] videos;
        public int[] request;

        public Endpoint(int dataCenterLatency, int nbLinkedCashServer)
        {
            this.nbLinkedCashServer = nbLinkedCashServer;
            this.dataCenterLatency = dataCenterLatency;
        }

        public void SetVars(int[] cashServer, int[] latencyToCashServer, int[] videos, int[] request)
        {
            this.cashServer = cashServer;
            this.latencyToCashServer = latencyToCashServer;
            this.videos = videos;
        }
    }
}