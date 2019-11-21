using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows;
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
        private RotationProducer rotationProducer;
        private RotationConsumer rotationConsumer;

        private uint visualizerHeight;
        private uint visualizerWidth;

        public MainWindow()
        {
            InitializeComponent();

            visualizerImage = FindName("D3DImage") as D3DImage;

            visualizerHeight = (uint)visualizerImage.PixelHeight;
            visualizerWidth = (uint)visualizerImage.PixelWidth;

            HRESULT.Check(SetSize(1024, 1024));
            HRESULT.Check(SetAlpha(false));
            HRESULT.Check(SetNumDesiredSamples(4));

            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
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

        [DllImport("D3DVisualizer.dll")]
        static extern int GetBackBufferNoRef(out IntPtr pSurface);

        [DllImport("D3DVisualizer.dll")]
        static extern int SetSize(uint width, uint height);

        [DllImport("D3DVisualizer.dll")]
        static extern int SetAlpha(bool useAlpha);

        [DllImport("D3DVisualizer.dll")]
        static extern int SetNumDesiredSamples(uint numSamples);

        [DllImport("D3DVisualizer.dll")]
        static extern int AdjustRotationSpeed(float newSpeed);

        [DllImport("D3DVisualizer.dll")]
        static extern int CreateEpoch();

        DispatcherTimer _sizeTimer;
        DispatcherTimer _adapterTimer;
        TimeSpan _lastRender;

        [DllImport("D3DVisualizer.dll")]
        static extern int SetAdapter(POINT screenSpacePoint);

        [DllImport("D3DVisualizer.dll")]
        static extern int Render();

        [DllImport("D3DVisualizer.dll")]
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
            App.Pause();

            FileRetriever fileRetriever = new FileRetriever();
            fileRetriever.OnSuccess = (string fileName) =>
            {
                App.ChangeTrack(fileName);
            };

            fileRetriever.OnFail = () =>
            {
                App.Pause();
                throw new Exception("Could not open file.");
            };

            fileRetriever.RetrieveFile();
        }

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
