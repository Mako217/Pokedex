using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Classes
{
    public class Pokemon
    {
        public string name { get; set; }
        public List<Abilities> abilities { get; set; }
        public Sprites sprites { get; set; }

        public class Abilities
        {
            public Ability ability { get; set; }
            public bool is_hidden { get; set; }
            public int slot { get; set; }

            public class Ability
            {
                public string name { get; set; }
                public string url { get; set; }
            }
        }

        public class Sprites
        {
            public Other other { get; set; }
            public class Other
            {
                public OfficialArtwork official_artwork { get; set; }
                public class OfficialArtwork
                {
                    public string front_default { get; set; }
                }
            }
        }



    }
    
}
