using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Vytah.Models;

namespace Vytah.Scenes
{
    public partial class Scene5 : UserControl
    {
        private bool _paniPromluvila = false;
        private bool _muzPromluvil = false;
        private bool _hitvytahEnabled = false;

        public Scene5()
        {
            InitializeComponent();
            Loaded += Scene5_Loaded;
        }

        private void Scene5_Loaded(object sender, RoutedEventArgs e)
        {
            BgImage.Source = ImageHelper.Load("scena5_vytah.png");

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "", Text = "Páté patro." },
                new() { Speaker = "", Text = "Výtah se otevře." },
                new() { Speaker = "", Text = "Za dveřmi chodba." },
                new() { Speaker = "", Text = "Naproti další výtah." },
                new() { Speaker = "", Text = "V něm světlo. Bílé. Tiché. Nekonečné." },
                new() { Speaker = "", Text = "Všichni tři stojíte před ním." },
                new() { Speaker = "", Text = "Nikdo se nehýbe." },
            });
        }

        private void HitPani_Click(object sender, RoutedEventArgs e)
        {
            if (!_paniPromluvila)
            {
                _paniPromluvila = true;
                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "Paní", Text = "Já jsem tady čekala dlouho." },
                    new() { Speaker = "Paní", Text = "Čtyřicet let." },
                    new() { Speaker = "Paní", Text = "Myslela jsem, že to nikdy neskončí." },
                    new() { Speaker = "",     Text = "Otočí se na tebe." },
                    new() { Speaker = "Paní", Text = "Vy jste živý." },
                    new() { Speaker = "Paní", Text = "My ne." },
                    new() { Speaker = "Paní", Text = "Jen vy můžete rozhodnout." },
                }, CheckIfBothSpoke);
            }
            else
            {
                int nahodna = new System.Random().Next(3);
                string text = nahodna switch
                {
                    0 => "Dívá se do světla.",
                    1 => "Sevře kabelku. Čeká.",
                    _ => "Nic neříká."
                };
                MainWindow.ShowSingleDialog("Paní", text);
            }
        }

        private void HitMuz_Click(object sender, RoutedEventArgs e)
        {
            if (!_muzPromluvil)
            {
                _muzPromluvil = true;
                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "Muž", Text = "Tam je konec." },
                    new() { Speaker = "Muž", Text = "Tam to přestane." },
                    new() { Speaker = "",    Text = "Poprvé zní jeho hlas jinak. Ne chladně. Skoro prosebně." },
                    new() { Speaker = "Muž", Text = "Pusťte mě tam." },
                    new() { Speaker = "Muž", Text = "Prosím." },
                }, CheckIfBothSpoke);
            }
            else
            {
                int nahodna = new System.Random().Next(3);
                string text = nahodna switch
                {
                    0 => "Stojí. Hledí do světla.",
                    1 => "Ruce podél těla. Čeká.",
                    _ => "Tentokrát mrká."
                };
                MainWindow.ShowSingleDialog("Muž", text);
            }
        }

        private void CheckIfBothSpoke()
        {
            if (_paniPromluvila && _muzPromluvil)
                _hitvytahEnabled = true;
        }

        private void HitVytah_Click(object sender, RoutedEventArgs e)
        {
            if (!_hitvytahEnabled)
            {
                MainWindow.ShowSingleDialog("", "Nejdřív musíš promluvit s oběma.");
                return;
            }

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "", Text = "Výtah. Otevřený." },
                new() { Speaker = "", Text = "Uvnitř je jen světlo." },
                new() { Speaker = "", Text = "Panel ukazuje: 5↑" },
                new() { Speaker = "", Text = "Nahoru už nejde." },
                new() { Speaker = "", Text = "Tohle je konec linky." },
                new() { Speaker = "", Text = "Stojíš u ovládacího panelu." },
                new() { Speaker = "", Text = "Dveře jsou stále otevřené." },
            }, () => ChoicePanel.Visibility = Visibility.Visible);
        }

        private void BtnPustit_Click(object sender, RoutedEventArgs e)
        {
            ChoicePanel.Visibility = Visibility.Collapsed;

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "",     Text = "Ustoupíš." },
                new() { Speaker = "",     Text = "Muž se pohne jako první. Pomalu. Krok za krokem." },
                new() { Speaker = "Muž",  Text = "..." },
                new() { Speaker = "",     Text = "Vejde do světla. Zmizí." },
                new() { Speaker = "",     Text = "Stará paní se otočí na tebe." },
                new() { Speaker = "Paní", Text = "Děkuji vám." },
                new() { Speaker = "",     Text = "Vejde za ním. Světlo pohasne." },
                new() { Speaker = "",     Text = "Výtah se zavře." },
                new() { Speaker = "",     Text = "Vrátíš se zpátky do svého výtahu." },
            }, () => SceneManager.GoToEnding(letManOut: true));
        }

        private void BtnZavrit_Click(object sender, RoutedEventArgs e)
        {
            ChoicePanel.Visibility = Visibility.Collapsed;

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "",     Text = "Stiskneš tlačítko." },
                new() { Speaker = "Muž",  Text = "Ne—" },
                new() { Speaker = "",     Text = "Dveře se zavřou." },
            }, () => SceneManager.GoToEnding(letManOut: false));
        }
    }
}