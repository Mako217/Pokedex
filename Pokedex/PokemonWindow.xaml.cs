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
using System.Windows.Shapes;

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
            url.Text = pokemonUrl;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
