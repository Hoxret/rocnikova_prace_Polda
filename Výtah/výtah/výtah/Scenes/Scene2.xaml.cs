using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Vytah.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Vytah.Scenes
{
    public partial class Scene2 : UserControl
    {
        private bool _nastenkaHotova = false;
        private bool _schrankyHotove = false;

        public Scene2()
        {
            InitializeComponent();
            Loaded += Scene2_Loaded;
        }

        private void Scene2_Loaded(object sender, RoutedEventArgs e)
        {
            BgImage.Source = ImageHelper.Load("scena2_vytah.png");

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "", Text = "Ticho. Absolutní ticho." },
                new() { Speaker = "", Text = "Žádná čísla na dveřích. Venku je tma." },
                new() { Speaker = "", Text = "Stará paní zmizela. Muž v obleku stojí za tebou." },
            });
        }

        private void HitNastenka_Click(object sender, RoutedEventArgs e)
        {
            if (!GameState.Instance.Scene2_FoundFuse)
            {
                GameState.Instance.Scene2_FoundFuse = true;
                bool added = GameState.Instance.AddItem(
                    new InventoryItem("pojistka", "pojistka", "Stará keramická pojistka."));

                _nastenkaHotova = true;
                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "Na nástěnce visí starý papír a... pojistka." },
                    new() { Speaker = "", Text = added ? "Vezmeš pojistku." : "Plné kapsy." },
                }, () =>
                {
                    if (added) HitRozvod.Visibility = Visibility.Visible;
                    ZkusNakres();
                });
            }
            else
            {
                _nastenkaHotova = true;
                MainWindow.ShowSingleDialog("", "Jen starý papír.", ZkusNakres);
            }
        }

        private void HitSchranky_Click(object sender, RoutedEventArgs e)
        {
            _schrankyHotove = true;
            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "", Text = "Schránky. Jména jsou nečitelná — jako by je někdo přepsal znovu a znovu." },
                new() { Speaker = "", Text = "V jedné je dopis. Adresa: '?. patro'. Bez odesílatele." },
            }, ZkusNakres);
        }

        private void HitSvetlo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowSingleDialog("",
                "Zářivka bliká v nepravidelném rytmu. Jako morse kód. Nebo ne.");
        }

        private void HitKolo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "Holčička", Text = "Ve čtvrtém patře už nikdo nebydlí. Jen ten zvuk." },
                new() { Speaker = "",         Text = "...Holčička nebyla tady ani vteřinu předtím." },
            });
        }

        private void HitRozvod_Click(object sender, RoutedEventArgs e)
        {
            if (!GameState.Instance.HasItem("pojistka"))
            {
                MainWindow.ShowSingleDialog("", "Rozvodná skříň. Chybí pojistka.");
                return;
            }

            GameState.Instance.RemoveItem("pojistka");
            GameState.Instance.Scene2_PowerRestored = true;

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "", Text = "Zašroubujete pojistku." },
                new() { Speaker = "", Text = "Klik. Světla se rozsvítí naplno." },
                new() { Speaker = "", Text = "Výtah funguje. Ale stará paní... není ve výtahu. Nikde." },
            }, () => SceneManager.GoToScene(3));
        }

        private void ZkusNakres()
        {
            if (_nastenkaHotova && _schrankyHotove && !GameState.Instance.Scene2_FoundDrawing)
            {
                GameState.Instance.Scene2_FoundDrawing = true;
                DrawingOverlay.Visibility = Visibility.Visible;
            }
        }

        private void CloseDrawing_Click(object sender, RoutedEventArgs e)
        {
            DrawingOverlay.Visibility = Visibility.Collapsed;
        }
    }
}