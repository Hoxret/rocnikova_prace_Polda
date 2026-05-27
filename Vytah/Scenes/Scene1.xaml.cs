using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Vytah.Models;

namespace Vytah.Scenes
{
    public partial class Scene1 : UserControl
    {
        private bool _paniPromluvila = false;
        private bool _muzPromluvil = false;

        public Scene1()
        {
            InitializeComponent();
            Loaded += Scene1_Loaded;
        }

        private void Scene1_Loaded(object sender, RoutedEventArgs e)
        {
            BgImage.Source = ImageHelper.Load("scena1_vytah.png");

            BtnOldLady.IsEnabled = false;
            BtnMan.IsEnabled = false;
            BtnLiftPanel.IsEnabled = false;

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "", Text = "Výtah. Žárovka nahoře bliká." },
                new() { Speaker = "", Text = "Dva cizí lidé. Nikdo se na nikoho nedívá." },
                new() { Speaker = "", Text = "Panel ukazuje: 0↑" },
            }, () =>
            {
                BtnOldLady.IsEnabled = true;
            });
        }

        private void BtnOldLady_Click(object sender, RoutedEventArgs e)
        {
            if (!_paniPromluvila)
            {
                _paniPromluvila = true;
                BtnOldLady.IsEnabled = false;

                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "Paní", Text = "Jedete nahoru?" },
                    new() { Speaker = "Paní", Text = "Nejeďte. Prosím." },
                    new() { Speaker = "Paní", Text = "Já tady bydlím čtyřicet let. Nikdy jsem se nebála výtahu." },
                    new() { Speaker = "Paní", Text = "Ale dneska... dneska je to jinak." },
                    new() { Speaker = "", Text = "Podívá se na muže vedle tebe. Pak zmlkne." },
                }, () =>
                {
                    BtnOldLady.IsEnabled = true; // znovu povolit pro druhý klik
                    BtnMan.IsEnabled = true;
                });
            }
            else
            {
                // Druhý a každý další klik
                int nahodna = new System.Random().Next(3);
                string text = nahodna switch
                {
                    0 => "Dívá se do podlahy. Mlčí.",
                    1 => "Sevře kabelku oběma rukama. Nic neřekne.",
                    _ => "Pootevře ústa. Pak je zavře. Jako by si to rozmyslela."
                };
                MainWindow.ShowSingleDialog("Paní", text);
            }
        }

        private void BtnMan_Click(object sender, RoutedEventArgs e)
        {
            if (!_muzPromluvil)
            {
                _muzPromluvil = true;
                BtnMan.IsEnabled = false;

                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "Muž", Text = "Paní Horáková." },
                    new() { Speaker = "Muž", Text = "Výtah funguje normálně. Vždycky fungoval normálně." },
                    new() { Speaker = "Paní", Text = "To říkáte pokaždé." },
                    new() { Speaker = "", Text = "Muž se otočí na tebe." },
                    new() { Speaker = "Muž", Text = "Do kterého patra?" },
                    new() { Speaker = "", Text = "Nečeká na odpověď. Otočí se zpět." },
                }, () =>
                {
                    BtnMan.IsEnabled = true; // znovu povolit pro druhý klik
                    BtnLiftPanel.IsEnabled = true;
                });
            }
            else
            {
                // Druhý a každý další klik
                int nahodna = new System.Random().Next(3);
                string text = nahodna switch
                {
                    0 => "Stojí nehybně. Ani se neotočí.",
                    1 => "Dívá se na panel. Číslo stále ukazuje 0.",
                    _ => "Nemrká. Vůbec nemrká."
                };
                MainWindow.ShowSingleDialog("Muž", text);
            }
        }

        private void BtnLiftPanel_Click(object sender, RoutedEventArgs e)
        {
            BtnLiftPanel.IsEnabled = false;

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "", Text = "Zmáčkneš tlačítko svého patra." },
                new() { Speaker = "", Text = "Nic." },
                new() { Speaker = "", Text = "Zmáčkneš znovu." },
                new() { Speaker = "", Text = "Výtah se rozjede. Ale číslo na panelu se nemění." },
                new() { Speaker = "", Text = "Žárovka se rozbliká." },
                new() { Speaker = "", Text = "Výtah se zastaví." },
            }, () =>
            {
                GameState.Instance.Scene1_EnteredLift = true;
                SceneManager.GoToScene(2);
            });
        }
    }
}