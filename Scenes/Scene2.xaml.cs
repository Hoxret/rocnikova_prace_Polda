using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Vytah.Models;

namespace Vytah.Scenes
{
    public partial class Scene2 : UserControl
    {
        private bool _nastenkaProhlizena = false;
        private bool _koloProhlizeno = false;

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
                new() { Speaker = "", Text = "Dveře výtahu se otevřou." },
                new() { Speaker = "", Text = "Toto není tvoje patro." },
                new() { Speaker = "", Text = "Vlastně... nevíš které patro to je. Žádné číslo na dveřích." },
                new() { Speaker = "", Text = "Muž v obleku vyjde za tebou. Rozhlédne se. Jako by tady byl doma." },
                new() { Speaker = "Muž", Text = "Pojďte." },
                new() { Speaker = "", Text = "Nikam nejde. Jen stojí." },
            });
        }

        // FÁZE 1: dialog nástěnky
        // FÁZE 2: chová se jako rozvodná skříň
        private void HitNastenka_Click(object sender, RoutedEventArgs e)
        {
            if (!_nastenkaProhlizena)
            {
                // První klik — dialog nástěnky
                _nastenkaProhlizena = true;

                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "Nástěnka. Ztracená kočka — Micka. Chybí od 12. 3." },
                    new() { Speaker = "", Text = "Nevíš jaký je dnes měsíc." },
                    new() { Speaker = "", Text = "Odtáhneš nástěnku od zdi. Za ní je zapuštěná rozvodná skříň. Otevřená." },
                    new() { Speaker = "", Text = "Chybí pojistka." },
                });
            }
            else
            {
                // Druhý a každý další klik — chová se jako rozvodná skříň
                if (!GameState.Instance.HasItem("pojistka"))
                {
                    MainWindow.ShowDialog(new List<DialogEntry>
                    {
                        new() { Speaker = "", Text = "Rozvodná skříň za nástěnkou. Otevřená." },
                        new() { Speaker = "", Text = "Chybí pojistka." },
                    });
                    return;
                }

                GameState.Instance.RemoveItem("pojistka");
                GameState.Instance.Scene2_PowerRestored = true;

                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "Zašroubuješ pojistku do skříně." },
                    new() { Speaker = "", Text = "Ticho." },
                    new() { Speaker = "", Text = "Pak klik." },
                    new() { Speaker = "", Text = "Světla se rozsvítí naplno. Výtah zabzučí." },
                    new() { Speaker = "", Text = "Zavíráš dvířka skříně. Na vnitřní straně je přilepený papír." },
                    new() { Speaker = "", Text = "Nákres. Tužkou. Výtah s lidmi uvnitř." },
                    new() { Speaker = "", Text = "Paní. Muž. Holčička. A jedno místo prázdné." },
                    new() { Speaker = "", Text = "Tvoje místo." },
                }, () =>
                {
                    GameState.Instance.Scene2_FoundDrawing = true;
                    DrawingOverlay.Visibility = Visibility.Visible;
                });
            }
        }

        private void HitSchranky_Click(object sender, RoutedEventArgs e)
        {
            if (!GameState.Instance.Scene2_FoundFuse)
            {
                GameState.Instance.Scene2_FoundFuse = true;

                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "Schránky. Dvacet schránek." },
                    new() { Speaker = "", Text = "Jména jsou nečitelná — jako by je někdo přelepil prázdným papírem." },
                    new() { Speaker = "", Text = "Jedna schránka je otevřená. Uvnitř je dopis." },
                    new() { Speaker = "", Text = "Adresa příjemce: '?. patro, byt ??'." },
                    new() { Speaker = "", Text = "Odesílatel: prázdné." },
                    new() { Speaker = "", Text = "Uvnitř je jen jedna věta: 'Nevstupuj do výtahu.'" },
                    new() { Speaker = "", Text = "A pod dopisem... stará pojistka." },
                    new() { Speaker = "", Text = "Vezmeš ji. Nikdy nevíš." },
                }, () =>
                {
                    GameState.Instance.AddItem(
                        new InventoryItem("pojistka", "pojistka", "Stará keramická pojistka. Vypadá funkčně."));
                });
            }
            else
            {
                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "Schránky. Dopis je pořád tam." },
                    new() { Speaker = "", Text = "'Nevstupuj do výtahu.'" },
                    new() { Speaker = "", Text = "Pozdě." },
                });
            }
        }

        private void HitSvetlo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "", Text = "Žárovka na stropě. Jedna. Slabá." },
                new() { Speaker = "", Text = "Bliká. Ale ne náhodně." },
                new() { Speaker = "", Text = "Krátká — krátká — dlouhá. Krátká — krátká — dlouhá." },
                new() { Speaker = "", Text = "Přestaneš počítat. Radši ne." },
            });
        }

        private void HitKolo_Click(object sender, RoutedEventArgs e)
        {
            if (!_koloProhlizeno)
            {
                _koloProhlizeno = true;
                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "Červené dětské kolo. Opřené o zeď." },
                    new() { Speaker = "", Text = "Je tady docela čisté. Jako by ho někdo pravidelně používal." },
                    new() { Speaker = "", Text = "Na řídítkách je uvázaná červená stuha." },
                    new() { Speaker = "Holčička", Text = "To je moje." },
                    new() { Speaker = "", Text = "Otočíš se. Nikdo tam není." },
                    new() { Speaker = "Holčička", Text = "Ve čtvrtém patře už nikdo nebydlí. Jen ten zvuk." },
                    new() { Speaker = "", Text = "Hlas přišel odjinud. Nebo ze všech směrů najednou." },
                });
            }
            else
            {
                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "Červené kolo. Stuha na řídítkách se lehce hýbe." },
                    new() { Speaker = "", Text = "Průvan tady není." },
                });
            }
        }

        private void CloseDrawing_Click(object sender, RoutedEventArgs e)
        {
            DrawingOverlay.Visibility = Visibility.Collapsed;

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "Muž", Text = "Výborně." },
                new() { Speaker = "", Text = "Otočíš se. Muž stojí přesně tam kde stál. Ani se nepohnul." },
                new() { Speaker = "", Text = "Stará paní... není. Nebyla tady od chvíle kdy se dveře výtahu otevřely." },
            }, () => SceneManager.GoToScene(3));
        }
    }
}