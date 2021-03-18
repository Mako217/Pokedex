using Newtonsoft.Json;
using Pokedex.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Pokedex.Classes;
using System.Collections.ObjectModel;

namespace Pokedex
{
    /// <summary>
    /// Logika interakcji dla klasy PokemonWindow.xaml
    /// </summary>
    public partial class PokemonWindow : Window
    {
        public PokemonWindow(string pokemonUrl)
        {
            InitializeComponent();
            

            Pokemon pokemon = Task.Run(() => GetPokemon(pokemonUrl)).Result;
            url.Text = char.ToUpper(pokemon.name[0]) + pokemon.name.Substring(1);

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(pokemon.sprites.other.official_artwork.front_default);
            bitmap.EndInit();
            img.Source = bitmap;

            ObservableCollection<TextBoxTemplate> abilities = new ObservableCollection<TextBoxTemplate>();
            foreach(Pokemon.AbilityListed abilityListed in pokemon.abilities)
            {
                abilities.Add(new TextBoxTemplate { text = char.ToUpper(abilityListed.ability.name[0]) + (abilityListed.ability.name.Substring(1)).Replace("_", " ") });
            }
            icAbilities.ItemsSource = abilities;

            ObservableCollection<TextBoxTemplate> moves = new ObservableCollection<TextBoxTemplate>();
            foreach(Pokemon.MoveListed moveListed in pokemon.moves)
            {
                moves.Add(new TextBoxTemplate { text = char.ToUpper(moveListed.move.name[0]) + (moveListed.move.name.Substring(1)).Replace("_", " ") });
            }
            icMoves.ItemsSource = moves;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        public async Task<Pokemon> GetPokemon(string Url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(Url))
                {
                    using (HttpContent content = response.Content)
                    {
                        var data = await content.ReadAsStringAsync();
                        data = data.Replace("-", "_");
                        Pokemon pokemon = JsonConvert.DeserializeObject<Pokemon>(data);
                        pokemon.sprites.other.official_artwork.front_default = pokemon.sprites.other.official_artwork.front_default.Replace("official_artwork", "official-artwork");
                        return pokemon;
                    }
                }
            }
        }
        public class TextBoxTemplate
        {
            public string text { get; set; }
        }
    }

}
