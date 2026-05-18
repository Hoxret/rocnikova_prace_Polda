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
            bool letOut = GameState.Instance.Scene5_FinalChoice;

            if (letOut)
            {
                EndingTitle.Text = "NĚKDO SE VRÁTIL.";
                EndingSubtitle.Text = "Na nástěnce příštího rána visí parte.\nBez fotky. Bez jména.";
            }
            else
            {
                EndingTitle.Text = "NĚKDO MĚL ODEJÍT.";
                EndingSubtitle.Text = "Na nástěnce příštího rána visí parte.\nS tvou fotkou.";
            }
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            GameState.Instance.Reset();
            SceneManager.GoToScene(0);
        }
    }
}