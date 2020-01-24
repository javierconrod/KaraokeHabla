﻿using System;
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

        public MainWindow()
        {
            InitializeComponent();
            btnReproducir.IsEnabled = true;
            lblLetra.Visibility = Visibility.Collapsed;
            btnReproducir.Visibility = Visibility.Visible;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            pbReproduccion.Value = reader.CurrentTime.TotalSeconds;
            if (pbReproduccion.Value > 30 && pbReproduccion.Value < 33)
            {
                lblLetra.Text = "DRIVING THIS ROAD DOWN TO PARADISE";
            }
            else if (pbReproduccion.Value > 34 && pbReproduccion.Value < 37)
            {
                lblLetra.Text = "LETTING THE SUNLIGHT INTO MY EYES";
            }
            else if (pbReproduccion.Value > 38 && pbReproduccion.Value < 41)
            {
                lblLetra.Text = "OUR ONLY PLAN IS TO IMPROVISE";
            }
            else if (pbReproduccion.Value > 41 && pbReproduccion.Value < 45)
            {
                lblLetra.Text = "AND IT'S CRYSTAL CLEAR THAT I DON'T EVER WAIT TO END";
            }
            else if (pbReproduccion.Value > 45&& pbReproduccion.Value < 49)
            {
                lblLetra.Text = "IF I HAD MY WAY, I WOULD NEVER LEAVE";
            }
            else if (pbReproduccion.Value > 49 && pbReproduccion.Value < 53)
            {
                lblLetra.Text = "KEEP BUILDING THESE RANDOM MEMORIES";
            }
            else if (pbReproduccion.Value > 53 && pbReproduccion.Value < 56)
            {
                lblLetra.Text = "TURNING OUR DAYS INTO MELODIES";
            }
            else if (pbReproduccion.Value > 56 && pbReproduccion.Value < 59)
            {
                lblLetra.Text = "BUT SINCE I CAN'T STAY";
            }


            else if (pbReproduccion.Value > 59 && pbReproduccion.Value < 62)
            {
                lblLetra.Text = "I'LL JUST KEEP PLAYING BACK";
            }
            else if (pbReproduccion.Value > 62 && pbReproduccion.Value < 66)
            {
                lblLetra.Text = "THESE FRAGMENTS OF TIME";
            }
            else if (pbReproduccion.Value > 66 && pbReproduccion.Value < 73)
            {
                lblLetra.Text = "EVERYWHERE I GO, THESE MOMENTS WILL SHINE";
            }
            else if (pbReproduccion.Value > 73 && pbReproduccion.Value < 76)
            {
                lblLetra.Text = "I'LL JUST KEEP PLAYING BACK";
            }
            else if (pbReproduccion.Value > 76 && pbReproduccion.Value < 80)
            {
                lblLetra.Text = "THESE FRAGMENTS OF TIME";
            }
            else if (pbReproduccion.Value > 80 && pbReproduccion.Value < 89)
            {
                lblLetra.Text = "EVERYWHERE I GO, THESE MOMENTS WILL SHINE";
            }


            lblTiempo.Text = pbReproduccion.Value.ToString();

        }

        private void btnReproducir_Click(object sender, RoutedEventArgs e)
        {
            reader = new AudioFileReader(@"audio/fragments.mp3");
            output = new WaveOut();

            output.Init(reader);
            output.Play();

            btnReproducir.Visibility = Visibility.Collapsed;
            lblLetra.Visibility = Visibility.Visible;
            pbReproduccion.Maximum = reader.TotalTime.TotalSeconds;
            pbReproduccion.Value = reader.CurrentTime.TotalSeconds;

            

            timer.Start();
        }
    }
}
