using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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

namespace imageRemake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bitmap img;
        private long quality = 95;
        Converter converttori;
        String newType;
        String fileName;
        String picturesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        long fileSize;
        long newSize;

        public MainWindow()
        {
            converttori = new Converter();
            InitializeComponent();
        }

        private void openImg_Click(object sender, RoutedEventArgs e)
        {
            rdyNotification.Text = "";
                // Open a popup with which the user can choose any picture with
            Microsoft.Win32.OpenFileDialog Opendialog = new Microsoft.Win32.OpenFileDialog();
            Opendialog.InitialDirectory = picturesFolder;
            Opendialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            Opendialog.ShowDialog();
            this.fileName = System.IO.Path.GetFileNameWithoutExtension(Opendialog.FileName);
                // Checks if the ShowDialog popup got canceled
            if(Opendialog.FileName != "") {
                img = new Bitmap(Opendialog.FileName);
                imgPreview.Source = new BitmapImage(new Uri(Opendialog.FileName));
                    // Display the image filesize in kilobytes unless it is less than one kilobyte
                this.fileSize = new System.IO.FileInfo(Opendialog.FileName).Length;
                if (fileSize > 1024)
                {
                    fileSizeDisplay.Text = "File size: " + fileSize / 1024 + " KB";
                }
                else
                {
                    fileSizeDisplay.Text = "File size: " + fileSize + " bytes";
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                // Save the new image and get its size
            this.newSize = converttori.SaveAs(newType, img, quality, fileName);
            rdyNotification.Text = "The image is now saved in " + picturesFolder + " as " + "RMK" + fileName + "." + newType;
            if (newSize > 1024)
            {
                fileSizeDisplay.Text = "New file size: " + newSize / 1024 + " KB";
            }
            else
            {
                fileSizeDisplay.Text = "New file size: " + newSize + " bytes";
            }
            // Updates the preview to the new
            // (possibly compressed) image
            imgPreview.Source = new BitmapImage(new Uri(picturesFolder + "\\" + "RMK" + fileName + "." + newType));
        }

        private void qualitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.quality = (long)qualitySlider.Value;
        }

        private void pngButton_Checked(object sender, RoutedEventArgs e){this.newType = "png";}
        private void bmpButton_Checked(object sender, RoutedEventArgs e){this.newType = "bmp";}
        private void jpegButton_Checked(object sender, RoutedEventArgs e){this.newType = "jpeg";}
    }
}