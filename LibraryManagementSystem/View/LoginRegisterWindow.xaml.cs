using LibraryManagementSystem.View;
using LibraryManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int loadingBar = 0;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            this.butonBack.Visibility = Visibility.Hidden;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton==MouseButtonState.Pressed) { DragMove(); }  
        }

        private void butonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState=WindowState.Minimized;
        }

        private void butonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void butonLoginMain_Click(object sender, RoutedEventArgs e)
        {
            this.butonLoginMain.Visibility = Visibility.Hidden;
            this.butonRegisterMain.Visibility = Visibility.Hidden;
            this.butonBack.Visibility = Visibility.Visible;
        }

        private void butonRegisterMain_Click(object sender, RoutedEventArgs e)
        {
            this.butonLoginMain.Visibility = Visibility.Hidden;
            this.butonRegisterMain.Visibility = Visibility.Hidden;
            this.butonBack.Visibility = Visibility.Visible;
        }

        private void butonBack_Click(object sender, RoutedEventArgs e)
        {
            this.butonLoginMain.Visibility = Visibility.Visible;
            this.butonRegisterMain.Visibility = Visibility.Visible;
            this.butonBack.Visibility = Visibility.Hidden;
        }
    }
}
