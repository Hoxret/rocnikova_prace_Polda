using System.Windows;
using System.Windows.Controls;

namespace Vytah.Scenes
{
    public partial class MenuScene : UserControl
    {
        public MenuScene()
        {
            InitializeComponent();
        }

        private void BtnNovaHra_Click(object sender, RoutedEventArgs e)
        {
            Models.GameState.Instance.Reset();
            SceneManager.GoToScene(1);
        }

        private void BtnUkoncit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}