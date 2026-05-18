using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Vytah.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Vytah.Scenes
{
    public partial class Scene3 : UserControl
    {
        private const string SPRAVNY_KOD = "3742";
        private bool _obraz1 = false;
        private bool _obraz2 = false;

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
                new() { Speaker = "",    Text = "Dveře výtahu se otevřou. Panelák je... jiný." },
                new() { Speaker = "",    Text = "Čistý. Moderní. Mramor místo lina. Ale prázdný." },
                new() { Speaker = "Muž", Text = "Tady jsme měli skončit. Tohle je správné místo." },
                new() { Speaker = "",    Text = "Jeho oči se nehýbají. Nikdy nemrká." },
            });
        }

        private void HitObraz1_Click(object sender, RoutedEventArgs e)
        {
            _obraz1 = true;
            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "", Text = "Abstraktní obraz. V rohu dole — malé číslo '37'." },
                new() { Speaker = "", Text = "Divné místo na podpis malíře." },
            }, ZkusKod);
        }

        private void HitObraz2_Click(object sender, RoutedEventArgs e)
        {
            _obraz2 = true;
            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "", Text = "Krajina. Na rámu — označení bytu: '42'." },
            }, ZkusKod);
        }

        private void ZkusKod()
        {
            if (_obraz1 && _obraz2)
            {
                MainWindow.ShowSingleDialog("",
                    "37... 42... Zkombinuješ čísla: 3742. Zkusíš to na dveřích.");
                HitDvere.Visibility = Visibility.Visible;
            }
        }

        private void HitTV_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "",   Text = "Televize není zapnutá." },
                new() { Speaker = "TV", Text = "Cestující do posledního patra, připravte se." },
                new() { Speaker = "",   Text = "Strhneš pohled. Televize je pořád vypnutá." },
            });
        }

        private void HitDvere_Click(object sender, RoutedEventArgs e)
        {
            KeypadOverlay.Visibility = Visibility.Visible;
            CodeInput.Clear();
            CodeInput.Focus();
        }

        private void ConfirmCode_Click(object sender, RoutedEventArgs e)
        {
            if (CodeInput.Text.Trim() == SPRAVNY_KOD)
            {
                KeypadOverlay.Visibility = Visibility.Collapsed;

                bool added = GameState.Instance.AddItem(
                    new InventoryItem("karta", "karta", "Přístupová karta. Čip svítí modře."));
                GameState.Instance.Scene3_GotCard = added;

                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "",         Text = "Kód funguje. Za dveřmi je prázdná místnost — a na podlaze přístupová karta." },
                    new() { Speaker = "Holčička", Text = "On tam chce zpátky." },
                    new() { Speaker = "",         Text = "Holčička ukáže na muže. Pak zmizí." },
                }, () => SceneManager.GoToScene(4));
            }
            else
            {
                MainWindow.ShowSingleDialog("", "Špatný kód.");
                CodeInput.Clear();
            }
        }

        private void CancelCode_Click(object sender, RoutedEventArgs e)
            => KeypadOverlay.Visibility = Visibility.Collapsed;
    }
}