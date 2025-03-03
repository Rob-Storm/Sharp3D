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

            CameraViewport.wpfControl.Start();

            CameraViewport.Focus();
            Keyboard.Focus(CameraViewport);

            mainScene = new EditorScene(CameraViewport.wpfControl);

            CameraViewport.GLRender += CameraViewport_OnRender;
        }

        private void CameraViewport_OnRender(float delta)
        {
            mainScene.Update(delta);
            mainScene.Render();
        }
    }
}
