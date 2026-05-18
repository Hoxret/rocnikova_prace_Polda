using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace Vytah.Scenes
{
    public partial class MenuScene : UserControl
    {
        public MenuScene()
        {
            InitializeComponent();
            BgImage.Source = ImageHelper.Load("titulbeztlacitek_vytah.png");
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