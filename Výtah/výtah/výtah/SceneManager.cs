using System.Windows.Controls;
using Vytah.Scenes;

namespace Vytah
{
    /// <summary>
    /// Spravuje přepínání UserControl scén v ContentPresenter.
    /// </summary>
    public static class SceneManager
    {
        private static ContentControl _host;

        public static void Initialize(ContentControl host)
        {
            _host = host;
        }

        public static void GoToScene(int sceneIndex)
        {
            Models.GameState.Instance.CurrentScene = sceneIndex;
            _host.Content = sceneIndex switch
            {
                0 => new MenuScene(),
                1 => new Scene1(),
                2 => new Scene2(),
                3 => new Scene3(),
                4 => new Scene4(),
                5 => new Scene5(),
                99 => new EndingScene(),
                _ => new MenuScene()
            };
        }

        public static void GoToEnding(bool letManOut)
        {
            Models.GameState.Instance.Scene5_FinalChoice = letManOut;
            _host.Content = new EndingScene();
        }
    }
}