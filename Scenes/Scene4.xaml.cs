using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Vytah.Models;

namespace Vytah.Scenes
{
    public partial class Scene4 : UserControl
    {
        private static readonly string[] Poradi = { "červená", "modrá", "zelená" };
        private readonly string[] _sloty = { null, null, null };
        private readonly int[] _kliknuti = { 0, 0, 0 };
        private bool _revealHotov = false;

        public Scene4()
        {
            InitializeComponent();
            Loaded += Scene4_Loaded;
        }

        private void Scene4_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "", Text = "Výtah se otevře. Dusno. Popel všude." },
                new() { Speaker = "", Text = "Tohle patro vyhořelo. Alarm hraje potichu — tak potichu, že si nejsi jistý jestli ho slyšíš." },
            });
        }

        private void HitPopel_Click(object sender, RoutedEventArgs e)
        {
            if (_revealHotov)
            {
                MainWindow.ShowSingleDialog("", "Popel. Všude popel.");
                return;
            }

            _revealHotov = true;
            GameState.Instance.MuzhDied = true;
            PaniLabel.Visibility = Visibility.Visible;

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "",     Text = "V popeli najdeš... fotografie." },
                new() { Speaker = "",     Text = "Nájemníci tohoto domu." },
                new() { Speaker = "",     Text = "Dům kdysi vyhořel. Všichni, kdo tu bydleli..." },
                new() { Speaker = "",     Text = "Muž v obleku. Stará paní. Holčička." },
                new() { Speaker = "",     Text = "Ty." },
                new() { Speaker = "Muž",  Text = "Když dojedeme nahoru, všechno bude zase normální." },
                new() { Speaker = "Paní", Text = "Právě proto tam nesmí." },
            });
        }

        private void HitAlarm_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowSingleDialog("", "Požární alarm. Baterka skoro dochází. Tiché pípání.");
        }

        private void HitRozvod_Click(object sender, RoutedEventArgs e)
        {
            if (!_revealHotov)
            {
                MainWindow.ShowSingleDialog("", "Rozvodná skříň. Musíš nejdřív zjistit, co se tu děje.");
                return;
            }
            ResetPuzzle();
            FusePuzzle.Visibility = Visibility.Visible;
        }

        private void SlotButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            int idx = int.Parse(btn.Tag.ToString());

            _kliknuti[idx]++;
            int faze = _kliknuti[idx] % 4;

            switch (faze)
            {
                case 0:
                    _sloty[idx] = null;
                    btn.Content = "[ — ]";
                    btn.Foreground = new SolidColorBrush(Colors.Gray);
                    break;
                case 1:
                    _sloty[idx] = "červená";
                    btn.Content = "[ ● ]";
                    btn.Foreground = new SolidColorBrush(Color.FromRgb(0xCC, 0x44, 0x44));
                    break;
                case 2:
                    _sloty[idx] = "modrá";
                    btn.Content = "[ ● ]";
                    btn.Foreground = new SolidColorBrush(Color.FromRgb(0x44, 0x88, 0xCC));
                    break;
                case 3:
                    _sloty[idx] = "zelená";
                    btn.Content = "[ ● ]";
                    btn.Foreground = new SolidColorBrush(Color.FromRgb(0x44, 0xAA, 0x66));
                    break;
            }
            PuzzleStatus.Text = "";
        }

        private void OdeslitPuzzle_Click(object sender, RoutedEventArgs e)
        {
            if (_sloty[0] == null || _sloty[1] == null || _sloty[2] == null)
            {
                PuzzleStatus.Text = "Nejsou zapojeny všechny pojistky.";
                return;
            }

            bool spravne = _sloty[0] == Poradi[0]
                        && _sloty[1] == Poradi[1]
                        && _sloty[2] == Poradi[2];

            if (spravne)
            {
                FusePuzzle.Visibility = Visibility.Collapsed;
                GameState.Instance.Scene4_PowerRestored = true;

                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "Klik. Proud teče." },
                    new() { Speaker = "", Text = "Výtah se probouzí." },
                    new() { Speaker = "", Text = "Poslední patro." },
                }, () => SceneManager.GoToScene(5));
            }
            else
            {
                PuzzleStatus.Text = "Špatné zapojení. Jiskra.";
                ResetPuzzle();
            }
        }

        private void ResetPuzzle()
        {
            for (int i = 0; i < 3; i++) { _sloty[i] = null; _kliknuti[i] = 0; }
            var gray = new SolidColorBrush(Colors.Gray);
            Slot1.Content = "[ — ]"; Slot1.Foreground = gray;
            Slot2.Content = "[ — ]"; Slot2.Foreground = gray;
            Slot3.Content = "[ — ]"; Slot3.Foreground = gray;
            PuzzleStatus.Text = "";
        }

        private void CancelPuzzle_Click(object sender, RoutedEventArgs e)
            => FusePuzzle.Visibility = Visibility.Collapsed;
    }
}