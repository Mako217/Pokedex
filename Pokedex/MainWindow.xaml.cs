using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using Newtonsoft.Json;
using Pokedex.Classes;

namespace Pokedex
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string baseUrl = "https://pokeapi.co/api/v2/pokemon?limit=100";

            try
            {
                PokemonList pokemonList = Task.Run(() => GetPokemonList(baseUrl)).Result;
                List<Button> buttons = new List<Button>();
                foreach (CPokemonListed pokemon in pokemonList.results)
                {
                    buttons.Add(new Button { Content = pokemon.name });
                }

                ic.ItemsSource = buttons;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }



        }

        public async Task<PokemonList> GetPokemonList(string Url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(Url))
                {
                    using (HttpContent content = response.Content)
                    {
                        var data = await content.ReadAsStringAsync();

                        PokemonList pokemonList = JsonConvert.DeserializeObject<PokemonList>(data);

                        return pokemonList;
                    }
                }
            }
        }
    }
}
