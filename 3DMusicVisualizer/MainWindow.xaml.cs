using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;

namespace _3DMusicVisualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// <see cref="https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/walkthrough-hosting-direct3d9-content-in-wpf"/>
    /// </summary>
    public partial class MainWindow : Window
    {
        private D3DImage visualizerImage;
        private DispatcherTimer updateTimer;

        public MainWindow()
        {
            InitializeComponent();

            HRESULT.Check(SetSize(512, 512));
            HRESULT.Check(SetAlpha(false));
            HRESULT.Check(SetNumDesiredSamples(4));

            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);

            updateTimer = new DispatcherTimer();
            updateTimer.Tick += (object sender, EventArgs e) =>
            {
                App.UpdateVisualizer();
            };
            updateTimer.Interval = new TimeSpan(0, 0, 1);
            updateTimer.Start();
            visualizerImage = FindName("visualizerImageGUI") as D3DImage;
        }

        ~MainWindow()
        {
            Destroy();
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            RenderingEventArgs args = (RenderingEventArgs)e;

            // It's possible for Rendering to call back twice in the same frame 
            // so only render when we haven't already rendered in this frame.
            if (visualizerImage.IsFrontBufferAvailable && _lastRender != args.RenderingTime)
            {
                IntPtr pSurface = IntPtr.Zero;
                HRESULT.Check(GetBackBufferNoRef(out pSurface));
                if (pSurface != IntPtr.Zero)
                {
                    visualizerImage.Lock();
                    // Repeatedly calling SetBackBuffer with the same IntPtr is 
                    // a no-op. There is no performance penalty.
                    visualizerImage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, pSurface);
                    HRESULT.Check(Render());
                    visualizerImage.AddDirtyRect(new Int32Rect(0, 0, visualizerImage.PixelWidth, visualizerImage.PixelHeight));
                    visualizerImage.Unlock();

                    _lastRender = args.RenderingTime;
                }
            }
        }

        [DllImport("D3DCode.dll")]
        static extern int GetBackBufferNoRef(out IntPtr pSurface);

        [DllImport("D3DCode.dll")]
        static extern int SetSize(uint width, uint height);

        [DllImport("D3DCode.dll")]
        static extern int SetAlpha(bool useAlpha);

        [DllImport("D3DCode.dll")]
        static extern int SetNumDesiredSamples(uint numSamples);

        DispatcherTimer _sizeTimer;
        DispatcherTimer _adapterTimer;
        TimeSpan _lastRender;

        [StructLayout(LayoutKind.Sequential)]
        struct POINT
        {
            public POINT(Point p)
            {
                x = (int)p.X;
                y = (int)p.Y;
            }

            public int x;
            public int y;
        }

        [DllImport("D3DCode.dll")]
        static extern int SetAdapter(POINT screenSpacePoint);

        [DllImport("D3DCode.dll")]
        static extern int Render();

        [DllImport("D3DCode.dll")]
        static extern void Destroy();

        private void PauseVisualizerOnClick(object sender, RoutedEventArgs e)
        {
            App.Pause();
        }

        /// <summary>
        /// Opens and plays a selected audio file on click.
        /// File dialog section adapted from microsoft example documentation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RetrieveFileFromDialogOnClick(object sender, RoutedEventArgs e)
        {
            App.PauseMusic();
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c://";
                openFileDialog.Filter = Properties.Resources.openFileDialogFilter;
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    App.ChangeTrack(filePath, FindName("visualizerViewport") as Viewport3D);
                }
            }

        }
    }

    public static class HRESULT
    {
        [SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void Check(int hr)
        {
            Marshal.ThrowExceptionForHR(hr);
        }
    }
}
