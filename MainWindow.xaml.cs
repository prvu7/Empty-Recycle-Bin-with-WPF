using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace empty_bin_app
{
    public partial class MainWindow : Window
    {
        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, uint dwFlags);

        private const uint SHERB_NOCONFIRMATION = 0x00000001;
        private const uint SHERB_NOPROGRESSUI = 0x00000002;  

        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnEmptyRecycleBin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnEmpty.IsEnabled = false;
                txtStatus.Text = "Emptying Recycle Bin...";

                uint flags = 0;
                if (checkSkipConfirmation.IsChecked == true)
                    flags |= SHERB_NOCONFIRMATION;

                int result = SHEmptyRecycleBin(IntPtr.Zero, null, flags);

                if (result == 0)
                {
                    txtStatus.Text = "Recycle Bin emptied succesfully!";
                }
                else
                {
                    txtStatus.Text = $"Error emptying Recycle Bin. Error code: {result}";
                }
            }
            catch (Exception ex)
            {
                txtStatus.Text = $"An error occured: {ex.Message}";
            }
            finally
            {
                btnEmpty.IsEnabled = true;
            }
        }
    }
}