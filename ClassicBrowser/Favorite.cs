using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassicBrowser
{
    [DataContract]
    public class Favorite 
    {
        public Favorite()
        {
            Favorites = new List<Favorite>();
        }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public List<Favorite> Favorites { get; set; }
    }
}
