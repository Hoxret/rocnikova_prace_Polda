using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Vytah.Models;

namespace Vytah.Scenes
{
    public partial class EndingScene : UserControl
    {
        public EndingScene()
        {
            InitializeComponent();
            Loaded += EndingScene_Loaded;
        }

        private void EndingScene_Loaded(object sender, RoutedEventArgs e)
        {
            BgImage.Source = ImageHelper.Load("endingscene_vytah.png");

            if (GameState.Instance.Scene5_FinalChoice)
                SpustEndingA();
            else
                SpustEndingB();
        }

        private void ZobrazTituly()
        {
            TitlePanel.Visibility = Visibility.Visible;

            // EndingTitle — začne po 1s, trvá 2s
            var animTitle = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(System.TimeSpan.FromSeconds(2)),
                BeginTime = System.TimeSpan.FromSeconds(1)
            };
            EndingTitle.BeginAnimation(OpacityProperty, animTitle);

            // EndingSubtitle — začne po 3s, trvá 2s
            var animSubtitle = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(System.TimeSpan.FromSeconds(2)),
                BeginTime = System.TimeSpan.FromSeconds(3)
            };
            EndingSubtitle.BeginAnimation(OpacityProperty, animSubtitle);

            // BtnMenu — začne po 5s, trvá 1.5s
            var animBtn = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(System.TimeSpan.FromSeconds(1.5)),
                BeginTime = System.TimeSpan.FromSeconds(5)
            };
            BtnMenu.BeginAnimation(OpacityProperty, animBtn);
        }

        // ── ENDING A ──────────────────────────────────────────────
        private void SpustEndingA()
        {
            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "", Text = "Tma." },
                new() { Speaker = "", Text = "Pak pád." },
                new() { Speaker = "", Text = "Přízemí. Dveře se otevřou." },
                new() { Speaker = "", Text = "Ráno. Normální panelák. Žárovka bliká." },
                new() { Speaker = "", Text = "Zmáčkneš své patro." },
                new() { Speaker = "", Text = "Výtah se rozjede." },
                new() { Speaker = "", Text = "Podíváš se na lesklé dveře výtahu." },
                new() { Speaker = "", Text = "V odrazu vidíš prázdný výtah." },
                new() { Speaker = "", Text = "Tvůj odraz tam není." },
                new() { Speaker = "", Text = "Dveře se otevřou. Vyjdeš." },
                new() { Speaker = "", Text = "Na nástěnce v přízemí visí leták: Hledá se" },
                new() { Speaker = "", Text = "Bez fotky. Bez jména. Bez data." },
                new() { Speaker = "", Text = "Jen jedna věta:" },
                new() { Speaker = "", Text = "'Děkujeme, že jste nás zachránil.'" },
            }, () =>
            {
                EndingTitle.Text = "NĚKDO SE VRÁTIL.";
                EndingSubtitle.Text =
                    "V odrazu výtahu nikdo nebyl.\n" +
                    "Leták visí bez jména.\n\n" +
                    "Výtah jezdí dál.";
                ZobrazTituly();
            });
        }

        // ── ENDING B ──────────────────────────────────────────────
        private void SpustEndingB()
        {
            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "", Text = "Ticho." },
                new() { Speaker = "", Text = "Muž se náhle otočí. Vrací se zpátky do výtahu." },
                new() { Speaker = "", Text = "Paní ho rychle následuje." },
                new() { Speaker = "", Text = "Dveře se zavřou." },
                new() { Speaker = "", Text = "Odjíždějí." },
                new() { Speaker = "", Text = "Stojíš tam mezi výtahy." },
                new() { Speaker = "", Text = "Náhle tma." },
                new() { Speaker = "", Text = "Prázdno." },
                new() { Speaker = "", Text = "Přemýšlíš jestli..." },
                new() { Speaker = "", Text = "...jsi tu nezůstal sám." },
                new() { Speaker = "", Text = "Nebo uvězněn..." },
            }, () =>
            {
                EndingTitle.Text = "NĚKDO MĚL ODEJÍT.";
                EndingSubtitle.Text =
                    "Ale ty si to nedovolil.\n" +
                    "A doplatil si na to.\n\n" +
                    "Výtah jezdí dál.";
                ZobrazTituly();
            });
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            GameState.Instance.Reset();
            SceneManager.GoToScene(0);
        }
    }
}