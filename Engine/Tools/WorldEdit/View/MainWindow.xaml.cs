using System.Windows;
using System.Windows.Input;

namespace WorldEdit
{
    public partial class MainWindow : Window
    {
        EditorScene mainScene;

        public MainWindow()
        {

            InitializeComponent();

            OpenTkControl.Start();

            OpenTkControl.Focus();
            Keyboard.Focus(OpenTkControl);


            mainScene = new EditorScene(OpenTkControl);
        }

        private void OpenTkControl_OnRender(TimeSpan delta) // effectively the game loop
        {
            mainScene.Update((float)delta.TotalSeconds);
            mainScene.Render();

            Keyboard.Focus(OpenTkControl);
        }

    }
}
