using System.Windows;
using System.Windows.Controls;
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
            {
                EndingTitle.Text = "NĚKDO SE VRÁTIL.";
                EndingSubtitle.Text = "Na nástěnce příštího rána visí parte.\nBez fotky. Bez jména.\n\nVýtah jezdí dál.";
            }
            else
            {
                EndingTitle.Text = "NĚKDO MĚL ODEJÍT.";
                EndingSubtitle.Text = "Na nástěnce příštího rána visí parte.\nS tvou fotkou.\n\nVýtah jezdí dál.";
            }
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            GameState.Instance.Reset();
            SceneManager.GoToScene(0);
        }
    }
}