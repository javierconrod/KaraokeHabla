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

using Microsoft.Win32;

using NAudio.Wave;
using NAudio.Wave.SampleProviders;

using System.Windows.Threading;

namespace Reproductor
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer;

        //Lector de archivos
        AudioFileReader reader;
        //Comunicacion con la tarjeta de audio
        //exclusivo para salidas
        WaveOut output;

        bool dragging = false;
        public MainWindow()
        {
            InitializeComponent();
            btnReproducir.IsEnabled = false;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(!dragging)
            {
                sldReproduccion.Value = reader.CurrentTime.TotalSeconds;
                pbReproduccion.Value = reader.CurrentTime.TotalSeconds;
            }
        }

        private void BtnExaminar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                txtDirectorio.Text = openFileDialog.FileName;
                btnReproducir.IsEnabled = true;
            }
        }

        private void btnReproducir_Click(object sender, RoutedEventArgs e)
        {
            if (output != null && output.PlaybackState == PlaybackState.Paused)
            {
                //retomar la reproducción
                output.Play();
            }
            else
            {
                if (txtDirectorio.Text != null && txtDirectorio.Text != string.Empty)
                {
                    reader = new AudioFileReader(txtDirectorio.Text);
                    output = new WaveOut();
                    output.PlaybackStopped += Output_PlaybackStopped;
                    output.Init(reader);
                    output.Play();

                }
            }
            btnReproducir.IsEnabled = false;

            pbReproduccion.Maximum = reader.TotalTime.TotalSeconds;
            pbReproduccion.Value = reader.CurrentTime.TotalSeconds;

            sldReproduccion.Maximum = reader.TotalTime.TotalSeconds;
            sldReproduccion.Value = reader.CurrentTime.TotalSeconds;

            timer.Start();
        }

        private void Output_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            timer.Stop();
            reader.Dispose();
            output.Dispose();
        }

        private void btnPausa_Click(object sender, RoutedEventArgs e)
        {
            if (output != null)
            {
                output.Pause();
                btnReproducir.IsEnabled = true;
            }
        }

        private void btnDetener_Click(object sender, RoutedEventArgs e)
        {
            if (output != null)
            {
                output.Stop();
                btnReproducir.IsEnabled = true;
            }
        }
        private void SldTiempo_DragStarted(object sender, RoutedEventArgs e)
        {
            dragging = true;
        }
       private void SldTiempo_DragCompleted(object sender, RoutedEventArgs e)
       {
            dragging = false;
            if(reader != null && output != null && output.PlaybackState != PlaybackState.Stopped)
            {
                reader.CurrentTime = TimeSpan.FromSeconds(sldReproduccion.Value);
            }
       }
}
}
