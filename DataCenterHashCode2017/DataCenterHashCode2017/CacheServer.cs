using System;
using System.Collections.Generic;

namespace DataCenterHashCode2017
{
    public struct CacheServer
    {
        public int ID;
        public int contenu;
        public static int CAPACITY;
        public List<Endpoint> endpoints;
    }
}