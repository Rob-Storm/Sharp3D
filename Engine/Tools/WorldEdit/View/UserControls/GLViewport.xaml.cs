using OpenTK.Wpf;
using System.Windows.Controls;

namespace WorldEdit.View.UserControls
{
    public delegate void RenderEventHandler(float delta);

    /// <summary>
    /// Interaction logic for GLViewport.xaml
    /// </summary>
    public partial class GLViewport : UserControl
    {
        public event RenderEventHandler GLRender;

        private string _viewportLabel;

        public string ViewportLabel
        {
            get { return _viewportLabel; }
            set
            { 
                _viewportLabel = value;
                GLViewportLabel.Content = value; // HACK: replace with mvvm!
            }
        }

        public GLWpfControl wpfControl { get; private set; }


        public GLViewport()
        {
            wpfControl = OpenTkControl;
            InitializeComponent();
        }

        public void OpenTkControl_OnRender(TimeSpan delta)
        {
            GLRender?.Invoke((float)delta.TotalSeconds);
        }
    }
}
