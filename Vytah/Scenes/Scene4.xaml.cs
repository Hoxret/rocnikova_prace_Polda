using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Vytah.Models;

namespace Vytah.Scenes
{
    public partial class Scene4 : UserControl
    {
        private static readonly string[] Poradi = { "červená", "modrá", "zelená"  };
        private readonly string[] _sloty = { null, null, null };
        private readonly int[] _kliknuti = { 0, 0, 0 };
        private bool _revealHotov = false;
        private bool _schrankyProhlizeny = false;
        private bool _alarmsProhlizan = false;

        public Scene4()
        {
            InitializeComponent();
            Loaded += Scene4_Loaded;
        }

        private void Scene4_Loaded(object sender, RoutedEventArgs e)
        {
            BgImage.Source = ImageHelper.Load("scena4_vytah.png");

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "", Text = "Čtvrté patro." },
                new() { Speaker = "", Text = "Výtah se otevře." },
                new() { Speaker = "", Text = "Na zdi je velká čtyřka. Jako by ji někdo napsal čerstvě — ale inkoust je starý." },
                new() { Speaker = "", Text = "Strop se drolí. Podlaha křupe pod nohama." },
                new() { Speaker = "", Text = "Holčička říkala: 'Ve čtvrtém patře už nikdo nebydlí. Jen ten zvuk.'" },
                new() { Speaker = "", Text = "Teď slyšíš ten zvuk." },
                new() { Speaker = "", Text = "Tiché pípání. Odkudsi z dálky." },
            });
        }

        private void HitSchranky_Click(object sender, RoutedEventArgs e)
        {
            if (!_schrankyProhlizeny)
            {
                _schrankyProhlizeny = true;
                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "Schránky. Stejné jako ve druhém patře." },
                    new() { Speaker = "", Text = "Ale tady jsou otevřené. Všechny." },
                    new() { Speaker = "", Text = "Uvnitř jsou fotografie." },
                    new() { Speaker = "", Text = "Nájemníci. Tváře které neznáš." },
                    new() { Speaker = "", Text = "V poslední schránce... jsi tam ty." },
                    new() { Speaker = "", Text = "Fotka je stará. Ale jsi to ty." },
                }, () =>
                {
                    CheckIfBothViewed();
                    MainWindow.ShowDialog(new List<DialogEntry>
                    {
                        new() { Speaker = "Muž",  Text = "Říkal jsem vám. Tohle místo nás zná." },
                        new() { Speaker = "",     Text = "Stará paní se objeví vedle tebe. Jako by tu byla celou dobu." },
                        new() { Speaker = "Paní", Text = "Tady jsme všichni zemřeli." },
                        new() { Speaker = "Paní", Text = "Dávno." },
                        new() { Speaker = "Muž",  Text = "Když dojedeme nahoru, všechno bude zase normální." },
                        new() { Speaker = "Paní", Text = "Právě proto tam nesmí." },
                        new() { Speaker = "",     Text = "Podívají se na sebe. Pak oba na tebe." },
                        new() { Speaker = "",     Text = "Čekají." },
                    });
                });
            }
            else
            {
                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "Schránky. Tvoje fotka v té poslední." },
                    new() { Speaker = "", Text = "Pořád tam je." },
                });
            }
        }

        private void HitAlarm_Click(object sender, RoutedEventArgs e)
        {
            if (!_alarmsProhlizan)
            {
                _alarmsProhlizan = true;
                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "Nouzové světlo na zdi. Červené." },
                    new() { Speaker = "", Text = "Bliká ve stejném rytmu jako světlo ve '?' patře." },
                    new() { Speaker = "", Text = "3x krátká — 3x dlouhá — 3x krátká." },
                    new() { Speaker = "", Text = "Teprve teď si uvědomíš co to je." },
                    new() { Speaker = "", Text = "S.O.S." },
                }, CheckIfBothViewed);
            }
            else
            {
                MainWindow.ShowSingleDialog("", "Červená lampička. S.O.S. Pořád bliká.");
            }
        }

        private void CheckIfBothViewed()
        {
            if (_schrankyProhlizeny && _alarmsProhlizan)
            {
                _revealHotov = true;
            }
        }

        private void HitRozvod_Click(object sender, RoutedEventArgs e)
        {
            if (!_revealHotov)
            {
                MainWindow.ShowDialog(new List<DialogEntry>
                {
                    new() { Speaker = "", Text = "Rozvodná skříň vedle výtahu. Dráty visí ven." },
                    new() { Speaker = "", Text = "Výtah nefunguje." },
                    new() { Speaker = "", Text = "Nejdřív musíš pochopit kde jsi." },
                });
                return;
            }

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "", Text = "Rozvodná skříň. Tři prázdná místa pro pojistky." },
                new() { Speaker = "", Text = "Červená. Modrá. Zelená." },
                new() { Speaker = "", Text = "Pořadí je důležité." },
            }, () =>
            {
                ResetPuzzle();
                FusePuzzle.Visibility = Visibility.Visible;
            });
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
                    new() { Speaker = "", Text = "Pojistky na místě. Klik." },
                    new() { Speaker = "", Text = "Výtah zabzučí. Světla uvnitř se rozsvítí." },
                    new() { Speaker = "", Text = "Panel nad výtahem: 4 ↑" },
                    new() { Speaker = "Paní", Text = "Nechte ho jít." },
                    new() { Speaker = "",     Text = "Mluví k muži. Ne k tobě." },
                    new() { Speaker = "Muž",  Text = "Ještě ne." },
                    new() { Speaker = "",     Text = "Výtah čeká. Dveře otevřené." },
                    new() { Speaker = "",     Text = "Nastoupíš." },
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