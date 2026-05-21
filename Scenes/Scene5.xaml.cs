using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Vytah.Models;

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
            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "",    Text = "Dveře se otevřou." },
                new() { Speaker = "",    Text = "Není tam chodba." },
                new() { Speaker = "",    Text = "Jen bílá místnost. Nekonečná. Ticho." },
                new() { Speaker = "Muž", Text = "Tam. Tam je to. Já to cítím." },
                new() { Speaker = "Muž", Text = "Pusťte mě." },
                new() { Speaker = "",    Text = "Stojíš u ovládacího panelu. Dveře se ještě nezavřely." },
            }, () => ChoicePanel.Visibility = Visibility.Visible);
        }

        private void BtnPustit_Click(object sender, RoutedEventArgs e)
        {
            ChoicePanel.Visibility = Visibility.Collapsed;

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

            MainWindow.ShowDialog(new List<DialogEntry>
            {
                new() { Speaker = "",    Text = "Stisknete tlačítko. Dveře se začínají zavírat." },
                new() { Speaker = "Muž", Text = "Ne! Nesmíš! Já musím—" },
                new() { Speaker = "",    Text = "Výtah sjede dolů." },
                new() { Speaker = "",    Text = "..." },
                new() { Speaker = "",    Text = "Ráno. Normální panelák." },
            }, () => SceneManager.GoToEnding(letManOut: false));
        }
    }
}