using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Vytah.Models;

namespace Vytah.Scenes
{
    public partial class Scene1 : UserControl
    {
        private bool _insideLift = false;

        public Scene1()
        {
            InitializeComponent();
            Loaded += Scene1_Loaded;
        }

        private void Scene1_Loaded(object sender, RoutedEventArgs e)
        {

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "", Text = "Večer. Typický panelák. Zářivka bliká jako vždy." },
                new() { Speaker = "", Text = "Výtah vypadá starý. Ale stejně jako každý den." },
            }, () => PostavyPanel.Visibility = Visibility.Visible);
        }

        private void HitVytah_Click(object sender, RoutedEventArgs e)
        {
            if (_insideLift) return;
            _insideLift = true;
            Models.GameState.Instance.Scene1_EnteredLift = true;
            PostavyPanel.Visibility = Visibility.Collapsed;

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "Paní", Text = "Nejezděte dneska nahoru." },
                new() { Speaker = "Muž",  Text = "Ignorujte ji." },
                new() { Speaker = "Paní", Text = "Ten výtah už dávno neměl jezdit." },
            }, () => LiftPanel.Visibility = Visibility.Visible);
        }

        private void FloorButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            string floor = btn.Tag?.ToString();

            if (floor == "3" || floor == "?" || floor == "??")
            {
                LiftPanel.Visibility = Visibility.Collapsed;
                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "BZZZT" },
                    new() { Speaker = "", Text = "Světla probliknou. Výtah se zastaví." },
                    new() { Speaker = "", Text = "Dveře se otevřou. Tohle není tvoje patro." },
                }, () => SceneManager.GoToScene(2));
            }
            else
            {
                MainWindow.ShowSingleDialog("", $"Zmáčknete {floor}. Výtah se nehýbe.");
            }
        }
    }
}