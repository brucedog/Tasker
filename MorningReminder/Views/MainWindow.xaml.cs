using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Shell;
using MorningReminder.ViewModel;

namespace MorningReminder.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int MT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel(new Messenger.Messenger());
            TaskbarItemInfo taskbarItem = new TaskbarItemInfo();
            taskbarItem.Overlay = new BitmapImage(new Uri("Images/Icon.ico", UriKind.RelativeOrAbsolute));
        }

        private void TitleBarGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle, WM_NCLBUTTONDOWN, MT_CAPTION, 0);
        }
    }
}