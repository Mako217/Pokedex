using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Classes
{
    public class PokemonList
    {
        public List<CPokemonListed> results { get; set; }
        public int count { get; set; }
        public string next { get; set; }
    }

    public class CPokemonListed
    {
        public string name { get; set; }
        public string url { get; set; }
    }


}
