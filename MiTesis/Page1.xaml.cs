using System;
using System.Collections.Generic;
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
using Microsoft.Kinect;


namespace MiTesis
{
    /// <summary>
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        KinectSensor miKinect;
        public Page1()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            miKinect = KinectSensor.KinectSensors[0];

            miKinect.ColorStream.Enable();

            miKinect.ColorFrameReady += new EventHandler<ColorImageFrameReadyEventArgs>(MiKinect_ColorFrameReady);

            miKinect.Start();

        }

        private void MiKinect_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {
                if(colorFrame == null)
                {
                    return;
                }

                byte[] colorData = new byte[colorFrame.PixelDataLength];
                colorFrame.CopyPixelDataTo(colorData);

                KinectVideo.Source = BitmapSource.Create(
                    colorFrame.Width, colorFrame.Height, 96,96,PixelFormats.Bgr32, null, colorData, colorFrame.Width*colorFrame.BytesPerPixel
                    );
            }
        }
    }
}
