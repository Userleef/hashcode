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
        private Video[] vidarr;
        private CacheServer[] csarr;
        private Request[] requestList;


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
            //On lit le fichier source
            string file = File.ReadAllText(path);
            int index = 0;
            
            //Read the first line
            nbVid = ReadInteger(file, ref index);
            nbEndpoint = ReadInteger(file, ref index);
            nbRequest = ReadInteger(file, ref index);
            nbCache = ReadInteger(file, ref index);
            CacheServer.CAPACITY = ReadInteger(file, ref index);
            
            
            //Read the second line
            videoSize = new int[nbVid];
            //Set le tableau videoSize, la taille de chaque video
            for (int i = 0; i < nbVid; i++)
            {
                videoSize[i] = ReadInteger(file, ref index);
            }
            vidarr = CreateVideos(nbVid, videoSize);

            endpointList = new Endpoint[nbEndpoint];
            //Créer les Cache servers et initialise les valeurs
            CacheServer[] csarr = CreateCasheServer(nbCache);
            //Set tous les Endpoints
            ParseEndpointServerInfo(file, ref index);
            ParseRequestInfo(file, ref index );
        }

        
        
        public static void ParseRendu(string file)
        {
            //TODO
            throw new NotImplementedException();
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
                ep.cacheServers = new CacheServer[nbLinkedCasheServer];
                ep.latencyToCashServer = new int[nbCache];

                for (int i = 0; i < nbCache; i++)
                {
                    ep.latencyToCashServer[i] = -1;
                }
                for (int i = 0; i < nbLinkedCasheServer; i++)
                {
                    int idcs = ReadInteger(file, ref index);
                    ep.cacheServers[i] = csarr[idcs];
                    ep.latencyToCashServer[idcs] = ReadInteger(file, ref index);
                }

                endpointList[endpointID] = ep;
                    
                endpointID++;
            }

        }
        
        public void ParseRequestInfo(string file, ref int index)
        {
            for (int i = 0; i < nbRequest; i++)
            {
                Request req = requestList[i];
                req.video = vidarr[ReadInteger(file,ref index)];
                req.endpointFrom = endpointList[ReadInteger(file,ref index)];
                req.nbRequest = ReadInteger(file, ref index);
            }
        }
        
        //Renvoie l'entier lu et met à jour l'index
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
        
        
        /*
         *
         *
         *
         * Création des listes de cache serveur et videos
         *
         *
         * 
         */
        
        public CacheServer[] CreateCasheServer(int nbcasheservers)
        {
            CacheServer[] cashelist = new CacheServer[nbcasheservers];
            for (int i = 0; i < nbcasheservers; i++)
            {
                CacheServer cs = new CacheServer();
                cs.ID = i;
                cashelist[i] = cs;
            }

            return cashelist;
        }
        
        public Video[] CreateVideos(int nbvideos, int[] videosweight)
        {
            Video[] videos = new Video[nbvideos];
            for (int i = 0; i < nbvideos; i++)
            {
                Video v = new Video();
                v.ID = i;
                v.weight = videosweight[i];
                videos[i] = v;
            }

            return videos;
        }
    }
}