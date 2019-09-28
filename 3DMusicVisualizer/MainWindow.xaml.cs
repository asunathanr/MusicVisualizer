using Microsoft.DirectX.Direct3D;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace _3DMusicVisualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// <see cref="https://www.codeproject.com/Articles/28526/Introduction-to-D3DImage"/>
    /// </summary>
    public partial class MainWindow : Window
    { 

        public MainWindow()
        {
            InitializeComponent();


        }

        private void PauseVisualizerOnClick(object sender, RoutedEventArgs e)
        {
            App.Pause();
        }

        private void RetrieveFileFromDialogOnClick(object sender, RoutedEventArgs e)
        {
            App.PauseMusic();
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "mp3 files (*.mp3)|*.mp3|wav files (.wav)|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    App.ChangeTrack(filePath);
                }
            }

        }
    }

    /// <summary>
    /// See: https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/walkthrough-hosting-direct3d9-content-in-wpf
    /// </summary>
    public static class HRESULT
    {
        [SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void Check(int hr)
        {
            Marshal.ThrowExceptionForHR(hr);
        }
    }
}
