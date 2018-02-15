using System;
using System.IO;
using System.Net.Cache;
using System.Reflection.Emit;

namespace DataCenterHashCode2017
{
    public class ParserIO
    {
        private int nbVid;
        private int nbEndpoint;
        private int nbRequest;
        private int nbCache;
        private int casheCapacity;

        private int[] videoSize;
        private Endpoint[] endpointList;
        
        
        public ParserIO()
        {
            nbEndpoint = 0;
            nbRequest = 0;
            nbVid = 0;
            nbCache = 0;
            casheCapacity = 0;
        }
        
        public void ParseFile(string path)
        {
            string file = File.ReadAllText(path);
            int index = 0;
            nbVid = ReadInteger(file, ref index);
            nbEndpoint = ReadInteger(file, ref index);
            nbRequest = ReadInteger(file, ref index);
            nbCache = ReadInteger(file, ref index);
            CasheServer.CAPACITY = ReadInteger(file, ref index);
            
            videoSize = new int[nbVid];
            //Parcours le tableau videoSize
            for (int i = 0; i < nbVid; i++)
            {
                videoSize[i] = ReadInteger(file, ref index);
            }

            endpointList = new Endpoint[nbEndpoint];
            

        }

        public string ReadCacheServerInfo(ref int index)
        {
            //TODO
            throw new Exception();
        }
        
        public static void ParseRendu(string file)
        {
            
        }

        public CasheServer[] CreateCasheServer(int nbcasheservers, int casheCapacity)
        {
            CasheServer[] cashelist = new CasheServer[nbcasheservers];
            for (int i = 0; i < nbcasheservers; i++)
            {
                CasheServer cs = new CasheServer();
                cs.ID = i;
                cashelist[i] = cs;
            }

            return cashelist;
        }
        
        public Video[] CreateVideos(int nbvideos, int videosweight)
        {
            Video[] videos = new Video[nbvideos];
            for (int i = 0; i < nbvideos; i++)
            {
                Video v = new Video();
                v.ID = i;
                videos[i] = v;
            }

            return videos;
        }

        public void ParseEndpointServerInfo(string file, ref int index)
        {
            int endpointID = 0;
            while (endpointID < nbEndpoint)
            {
                int nbLinkedCasheServer = ReadInteger(file, ref index);
                Endpoint ep = new Endpoint();
                
                ep.dataCenterLatency = ReadInteger(file, ref index);
                ep.nbLinkedCashServer = nbLinkedCasheServer;
                ep.casheServers = new CasheServer[nbLinkedCasheServer];
                for (int i = 0; i < nbLinkedCasheServer; i++)
                {
                }
            }
        }
        public int ReadInteger(string file, ref int index)
        {
            string integer = "";
            while (index < file.Length && file[index] >= '0' && file[index] <= '9')
            {
                integer += file[index];
                index++;
            }
            index++;
            return Int32.Parse(integer);
        }
    }
}