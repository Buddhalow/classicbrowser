using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassicBrowser
{
    [DataContract]
    public class FavoriteManager
    {
        public FavoriteManager()
        {
            Favorites = new List<Favorite>();
        }
        [DataMember]
        public List<Favorite> Favorites { get; set; }
    }
}
