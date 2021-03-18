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
        public List<AbilityListed> abilities { get; set; }
        public List<MoveListed> moves { get; set; }
        public Sprites sprites { get; set; }

        public class AbilityListed
        {
            public NameAndUrl ability { get; set; }
            public bool is_hidden { get; set; }
            public int slot { get; set; }

            public class Ability:NameAndUrl
            {
            }
        }

        public class MoveListed
        {
            public NameAndUrl move { get; set; }
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
