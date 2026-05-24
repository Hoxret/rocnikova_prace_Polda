using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Vytah.Models;

namespace Vytah.Scenes
{
    public partial class Scene3 : UserControl
    {
        private const string SpravnyKod = "3742";
        private bool _obraz1 = false;
        private bool _komoda = false;
        private bool _tvProhlizena = false;
        private bool _muzPromluvil = false;

        public Scene3()
        {
            InitializeComponent();
            Loaded += Scene3_Loaded;
        }

        private void Scene3_Loaded(object sender, RoutedEventArgs e)
        {
            BgImage.Source = ImageHelper.Load("scena3_vytah.png");

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "", Text = "Výtah se otevře. Ale ne do tvého patra." },
                new() { Speaker = "", Text = "Mramor. Strop se světly. Rostlina v rohu." },
                new() { Speaker = "", Text = "Panel nad výtahem ukazuje: 3 ↑" },
                new() { Speaker = "", Text = "Tohle není panelák. Tohle nikdy panelák nebyl." },
                new() { Speaker = "Muž", Text = "Tady jsme měli skončit." },
                new() { Speaker = "Muž", Text = "Tohle je správné místo." },
                new() { Speaker = "", Text = "Vyjde z výtahu. Rozhlédne se pomalu. Jako by se vracel domů." },
            });
        }

        private void HitObraz1_Click(object sender, RoutedEventArgs e)
        {
            if (!_obraz1)
            {
                _obraz1 = true;
                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "Velký obraz. Abstraktní — tmavé a zlaté tahy." },
                    new() { Speaker = "", Text = "V pravém dolním rohu, skoro nečitelně: '37'." },
                    new() { Speaker = "", Text = "Není to podpis. Je to jinak napsané. Jako číslo bytu." },
                }, ZkusOdemknoutDvere);
            }
            else
            {
                MainWindow.ShowSingleDialog("", "Tmavé a zlaté tahy. Číslo '37' v rohu.");
            }
        }

        private void HitKomoda_Click(object sender, RoutedEventArgs e)
        {
            if (!_komoda)
            {
                _komoda = true;
                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "Komoda pod televizí. Mramorová deska nahoře." },
                    new() { Speaker = "", Text = "Otevřeš prostřední šuplík." },
                    new() { Speaker = "", Text = "Prázdný. Skoro." },
                    new() { Speaker = "", Text = "Na dně šuplíku je propiskou napsané číslo: '42'." },
                    new() { Speaker = "", Text = "Čerstvě napsané. Inkoust se ještě leskne." },
                }, ZkusOdemknoutDvere);
            }
            else
            {
                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "Šuplík. Číslo '42' na dně." },
                    new() { Speaker = "", Text = "Někdo ho napsal nedávno." },
                });
            }
        }

        private void ZkusOdemknoutDvere()
        {
            if (_obraz1 && _komoda)
            {
                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "37. 42." },
                    new() { Speaker = "", Text = "Obraz. Šuplík. Tady. V tomhle patře." },
                    new() { Speaker = "", Text = "Někdo je sem dal záměrně." },
                    new() { Speaker = "", Text = "Zkusíš: 3742." },
                }, () => HitDvere.Visibility = Visibility.Visible);
            }
        }

        private void HitTV_Click(object sender, RoutedEventArgs e)
        {
            if (!_tvProhlizena)
            {
                _tvProhlizena = true;
                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "",   Text = "Televize na zdi. Velká. Vypnutá." },
                    new() { Speaker = "",   Text = "Na černé obrazovce se odráží chodba." },
                    new() { Speaker = "",   Text = "V odrazu... jsi tam sám. Muž v obleku tam není." },
                    new() { Speaker = "",   Text = "Otočíš se. Muž tam stojí." },
                    new() { Speaker = "TV", Text = "VÍTEJTE DOMA." },
                    new() { Speaker = "",   Text = "Televize se sama zapne. Pak zase vypne." },
                    new() { Speaker = "",   Text = "Obrazovka je zase černá. V odrazu jsi opět sám." },
                });
            }
            else
            {
                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "Černá obrazovka." },
                    new() { Speaker = "", Text = "V odrazu jsi sám." },
                });
            }
        }

        private void HitMuz_Click(object sender, RoutedEventArgs e)
        {
            if (!_muzPromluvil)
            {
                _muzPromluvil = true;
                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "Muž", Text = "Hledáte něco?" },
                    new() { Speaker = "",    Text = "Nedívá se na tebe. Dívá se na výtah." },
                    new() { Speaker = "Muž", Text = "Všechno je tady. Vždycky bylo." },
                    new() { Speaker = "Muž", Text = "Jenom ten kód." },
                    new() { Speaker = "Muž", Text = "Ten si nepamatuji." },
                });
            }
            else
            {
                int nahodna = new System.Random().Next(3);
                string text = nahodna switch
                {
                    0 => "Stojí. Dívá se na výtah.",
                    1 => "Nemrká. Ani teď ne.",
                    _ => "Ruce složené před sebou. Čeká."
                };
                MainWindow.ShowSingleDialog("Muž", text);
            }
        }

        private void HitDvere_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowSingleDialog("", "Výtah. Elektronický zámek vedle dveří. Čeká na kód.", () =>
            {
                KeypadOverlay.Visibility = Visibility.Visible;
                CodeInput.Clear();
                CodeInput.Focus();
            });
        }

        private void ConfirmCode_Click(object sender, RoutedEventArgs e)
        {
            if (CodeInput.Text.Trim() == SpravnyKod)
            {
                KeypadOverlay.Visibility = Visibility.Collapsed;

                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "Zámek pípne. Zelené světlo." },
                    new() { Speaker = "", Text = "Výtah se otevře." },
                    new() { Speaker = "", Text = "Uvnitř na podlaze leží přístupová karta." },
                    new() { Speaker = "", Text = "Vezmeš ji." },
                    new() { Speaker = "Holčička", Text = "On tam chce zpátky." },
                    new() { Speaker = "", Text = "Otočíš se. Holčička stojí u komody." },
                    new() { Speaker = "Holčička", Text = "Ale nemůže. Ne dokud tam jsi ty." },
                    new() { Speaker = "", Text = "Ukáže na muže. Pak ukáže na tebe." },
                    new() { Speaker = "", Text = "Zamrkáš. Holčička zmizí." },
                }, () =>
                {
                    bool added = GameState.Instance.AddItem(
                        new InventoryItem("karta", "karta", "Přístupová karta. Čip svítí modře."));
                    GameState.Instance.Scene3_GotCard = added;
                    SceneManager.GoToScene(4);
                });
            }
            else
            {
                MainWindow.ShowSingleDialog("", "Červené světlo. Zámek odmítne kód.");
                CodeInput.Clear();
            }
        }

        private void CancelCode_Click(object sender, RoutedEventArgs e)
            => KeypadOverlay.Visibility = Visibility.Collapsed;
    }
}