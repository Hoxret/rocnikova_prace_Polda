using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Vytah.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Vytah.Scenes
{
    public partial class Scene4 : UserControl
    {
        // Správné pořadí: Červená(0), Modrá(1), Zelená(2)
        private readonly string[] _correctOrder = { "červená", "modrá", "zelená" };
        private readonly string[] _slotColors = new string[3];
        private int _nextSlot = 0;
        private bool _revealDone = false;

        // Barvy pojistek pro puzzle
        private readonly string[] _fuseOptions = { "červená", "modrá", "zelená" };
        private int _fuseIndex = 0;

        public Scene4()
        {
            InitializeComponent();
            Loaded += Scene4_Loaded;
        }

        private void Scene4_Loaded(object sender, RoutedEventArgs e)
        {
            BgImage.Source = ImageHelper.Load("scena4_vytah.png");
            ImgMuz.Source = ImageHelper.LoadChar("muz_creepy.png");
            SoundManager.PlayAmbient("ambient_ohen");

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "", Text = "Výtah se otevře. Dusno. Popel." },
                new() { Speaker = "", Text = "Tohle patro vyhořelo. Kdy? Proč tu stojíš?" },
                new() { Speaker = "", Text = "Potichu hraje alarm. Tak potichu, že si nejsi jistý jestli ho slyšíš." },
            });
        }

        private void HitPopel_Click(object sender, RoutedEventArgs e)
        {
            if (!_revealDone)
            {
                _revealDone = true;
                ImgPani.Visibility = Visibility.Visible;
                GameState.Instance.MuzhDied = true;

                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "",     Text = "V popeli najdeš... fotografie. Nájemníci tohoto domu." },
                    new() { Speaker = "",     Text = "Dům vyhořel. Všichni, kdo tu bydleli... " },
                    new() { Speaker = "",     Text = "Muž v obleku. Stará paní. Holčička." },
                    new() { Speaker = "",     Text = "Ty." },
                    new() { Speaker = "Paní", Text = "Právě proto tu jsme. Právě proto výtah jezdí." },
                    new() { Speaker = "Muž",  Text = "Když dojedeme nahoru, všechno bude zase normální." },
                    new() { Speaker = "Paní", Text = "Právě proto tam nesmí." },
                });
            }
            else
            {
                MainWindow.ShowSingleDialog("", "Popel. Všude popel.");
            }
        }

        private void HitAlarm_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlaySFX("alarm_low");
            MainWindow.ShowSingleDialog("", "Požární alarm. Baterka už skoro dochází. Tiché pípání.");
        }

        private void HitRozvod_Click(object sender, RoutedEventArgs e)
        {
            if (!_revealDone)
            {
                MainWindow.ShowSingleDialog("", "Rozvodná skříň. Musíš nejdřív zjistit, co se tu děje.");
                return;
            }
            ResetPuzzle();
            FusePuzzle.Visibility = Visibility.Visible;
        }

        // --- PUZZLE: zapojení pojistek ---
        private void SlotButton_Click(object sender, RoutedEventArgs e)
        {
            if (_nextSlot >= 3) return;

            var btn = (Button)sender;
            string color = _fuseOptions[_fuseIndex % _fuseOptions.Length];
            _fuseIndex++;

            _slotColors[_nextSlot] = color;
            btn.Content = $"[{color[0].ToString().ToUpper()}]";
            btn.Foreground = color switch
            {
                "červená" => new SolidColorBrush(Colors.IndianRed),
                "modrá" => new SolidColorBrush(Colors.CornflowerBlue),
                "zelená" => new SolidColorBrush(Colors.MediumSeaGreen),
                _ => new SolidColorBrush(Colors.Gray)
            };
            _nextSlot++;

            if (_nextSlot == 3) CheckPuzzle();
        }

        private void CheckPuzzle()
        {
            bool correct = _slotColors[0] == _correctOrder[0]
                        && _slotColors[1] == _correctOrder[1]
                        && _slotColors[2] == _correctOrder[2];

            if (correct)
            {
                FusePuzzle.Visibility = Visibility.Collapsed;
                SoundManager.PlaySFX("power_on");
                GameState.Instance.Scene4_PowerRestored = true;

                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "Klik. Proud teče. Výtah se probouzí." },
                    new() { Speaker = "", Text = "Poslední patro." },
                }, () => SceneManager.GoToScene(5));
            }
            else
            {
                MainWindow.ShowSingleDialog("", "Špatné zapojení. Třeskne jiskra.", ResetPuzzle);
            }
        }

        private void ResetPuzzle()
        {
            _nextSlot = 0;
            _fuseIndex = 0;
            for (int i = 0; i < 3; i++) _slotColors[i] = null;
            Slot1.Content = "[ ]"; Slot1.Foreground = new SolidColorBrush(Colors.Gray);
            Slot2.Content = "[ ]"; Slot2.Foreground = new SolidColorBrush(Colors.Gray);
            Slot3.Content = "[ ]"; Slot3.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void CancelPuzzle_Click(object sender, RoutedEventArgs e)
        {
            FusePuzzle.Visibility = Visibility.Collapsed;
        }
    }
}