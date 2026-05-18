using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Vytah.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Vytah.Scenes
{
    public partial class Scene5 : UserControl
    {
        public Scene5()
        {
            InitializeComponent();
            Loaded += Scene5_Loaded;
        }

        private void Scene5_Loaded(object sender, RoutedEventArgs e)
        {
            BgImage.Source = ImageHelper.Load("scena5_vytah.png");
            ImgMuz.Source = ImageHelper.LoadChar("muz_creepy.png");
            SoundManager.StopAmbient();

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "", Text = "Dveře se otevřou." },
                new() { Speaker = "", Text = "Není tam chodba." },
                new() { Speaker = "", Text = "Jen bílá místnost. Nekonečná. Ticho." },
                new() { Speaker = "Muž", Text = "Tam. Tam je to. Já to cítím." },
                new() { Speaker = "Muž", Text = "Pusťte mě." },
                new() { Speaker = "",    Text = "Stojíš u knoflíku dveří." },
            }, ShowChoice);
        }

        private void ShowChoice()
        {
            ChoicePanel.Visibility = Visibility.Visible;
        }

        private void BtnPustit_Click(object sender, RoutedEventArgs e)
        {
            ChoicePanel.Visibility = Visibility.Collapsed;
            SoundManager.PlaySFX("heavy_door");

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "",    Text = "Pustíte dveře." },
                new() { Speaker = "Muž", Text = "..." },
                new() { Speaker = "",    Text = "Odejde. Jen tak. Do té bílé." },
                new() { Speaker = "",    Text = "Světla zhasnou." },
                new() { Speaker = "",    Text = "Výtah začne padat." },
            }, () => SceneManager.GoToEnding(letManOut: true));
        }

        private void BtnZavrit_Click(object sender, RoutedEventArgs e)
        {
            ChoicePanel.Visibility = Visibility.Collapsed;
            SoundManager.PlaySFX("vytah_dvere");

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "",    Text = "Stisknete tlačítko. Dveře se zavírají." },
                new() { Speaker = "Muž", Text = "Ne! Nesmíš! Já musím—" },
                new() { Speaker = "",    Text = "Výtah sjede dolů." },
                new() { Speaker = "",    Text = "Ráno." },
                new() { Speaker = "",    Text = "Normální panelák." },
            }, () => SceneManager.GoToEnding(letManOut: false));
        }
    }
}