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
using System.Collections.ObjectModel;

namespace Pokedex
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PokemonList pokemonList;
        ObservableCollection<PokemonButton> buttons;
        public MainWindow()
        {
            InitializeComponent();
            string baseUrl = "https://pokeapi.co/api/v2/pokemon?limit=100";
            

            try
            {
                pokemonList = Task.Run(() => GetPokemonList(baseUrl)).Result;
                buttons = new ObservableCollection<PokemonButton>();
                foreach (NameAndUrl pokemon in pokemonList.results)
                {
                    buttons.Add(new PokemonButton { ButtonContent = char.ToUpper(pokemon.name[0]) + pokemon.name.Substring(1), Url = pokemon.url});
                }

                ic.ItemsSource = buttons;

            }
            catch (Exception ex)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            PokemonWindow pokemonWindow = new PokemonWindow((string)((Button)sender).Tag);
            pokemonWindow.Show();
            this.Close();
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            if(textBox.Text != "")
            {
                buttons.Clear();
                foreach(NameAndUrl pokemon in pokemonList.results)
                {
                    if(pokemon.name.Contains(textBox.Text.ToLower()))
                    {
                        buttons.Add(new PokemonButton { ButtonContent = char.ToUpper(pokemon.name[0]) + pokemon.name.Substring(1), Url = pokemon.url });
                    }

                }          
            }
            else if (buttons.Count < 100)
            {
                buttons.Clear();
                foreach (NameAndUrl pokemon in pokemonList.results)
                {
                    buttons.Add(new PokemonButton { ButtonContent = char.ToUpper(pokemon.name[0]) + pokemon.name.Substring(1), Url = pokemon.url });
                }

            }
        }
    }

    public class PokemonButton
    {
        public string ButtonContent { get; set; }
        public string Url { get; set; }
    }
}
