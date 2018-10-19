using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ClassicBrowser
{
    [DataContract]
    public class FavoriteManager
    {
        public static FavoriteManager FavoritesManager { get; set; }
        public static string FavoritesFile
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Favorites.json";

            }
        }

        public static void Save()
        {
            using (StreamWriter sw = new StreamWriter(FavoritesFile))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(FavoriteManager));
                ser.WriteObject(sw.BaseStream, FavoritesManager);
            }
        }

        public static void Load()
        {
            if (File.Exists(FavoritesFile))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(FavoritesFile))
                    {
                        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(FavoriteManager));
                        FavoritesManager = (FavoriteManager)ser.ReadObject(sr.BaseStream);
                    }
                }
                catch (Exception e)
                {

                }
            }
        }
        public FavoriteManager()
        {
            this.Favorites = new List<Favorite>();
        }
        [DataMember]
        public List<Favorite> Favorites { get; set; }
    }
}
