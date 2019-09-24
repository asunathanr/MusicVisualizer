using System.Collections.Generic;
using System.Windows;

namespace _3DMusicVisualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<System.Func<bool>> pauseSubscribers;

        public MainWindow()
        {
            InitializeComponent();
            App.PlayMusic();
        }

        public void AddPauseButtonSubscriber(System.Func<bool> subscriber)
        {
            pauseSubscribers.Add(subscriber);
        }

        private void PauseVisualizerOnClick(object sender, RoutedEventArgs e)
        {
            App.PauseMusic();
        }
    }
}
