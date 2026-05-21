using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Vytah.Models;

namespace Vytah
{
    public partial class MainWindow : Window
    {
        private Queue<DialogEntry> _dialogQueue = new();
        private System.Action _onDialogFinished;

        public MainWindow()
        {
            InitializeComponent();
            SceneManager.Initialize(SceneHost);
            GameState.Instance.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(GameState.Inventory))
                    RefreshInventoryUI();
            };
            RefreshInventoryUI();
            SceneManager.GoToScene(0);
        }

        public static void ShowDialog(List<DialogEntry> entries, System.Action onFinished = null)
        {
            var win = (MainWindow)Application.Current.MainWindow;
            win._dialogQueue = new Queue<DialogEntry>(entries);
            win._onDialogFinished = onFinished;
            win.ShowNextDialog();
        }

        public static void ShowSingleDialog(string speaker, string text,
                                            System.Action onFinished = null)
        {
            ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = speaker, Text = text }
            }, onFinished);
        }

        private void ShowNextDialog()
        {
            if (_dialogQueue.Count == 0)
            {
                DialogBorder.Visibility = Visibility.Collapsed;
                _onDialogFinished?.Invoke();
                return;
            }
            var entry = _dialogQueue.Dequeue();
            SpeakerLabel.Text = string.IsNullOrEmpty(entry.Speaker)
                ? ""
                : $"— {entry.Speaker.ToUpper()} —";
            DialogLabel.Text = entry.Text;
            DialogBorder.Visibility = Visibility.Visible;
        }

        private void ContinueDialog_Click(object sender, RoutedEventArgs e)
            => ShowNextDialog();

        private void RefreshInventoryUI()
        {
            InventoryItems.ItemsSource = null;
            InventoryItems.ItemsSource = GameState.Instance.Inventory;

            EmptySlots.Items.Clear();
            int empty = 3 - GameState.Instance.Inventory.Count;
            for (int i = 0; i < empty; i++)
            {
                EmptySlots.Items.Add(new Border
                {
                    Width = 64,
                    Height = 64,
                    Margin = new Thickness(0, 0, 8, 0),
                    Background = new SolidColorBrush(Color.FromRgb(0x08, 0x08, 0x08)),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(0x1A, 0x1A, 0x1A)),
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(2)
                });
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Trigger layout refresh v SceneHost (aktualní scene)
            if (SceneHost.Content is UIElement element)
            {
                element.InvalidateMeasure();
            }
        }
    }
}