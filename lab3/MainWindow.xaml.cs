using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimeSpan _timeLeft;
        private DispatcherTimer _timer;
        private bool _isTimerRunning;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_timeLeft == TimeSpan.Zero)
            {
                _timer.Stop();
                MessageBox.Show("Time's up!");
                btnStartStop.Content = "Start";
                _isTimerRunning = false;
                txtTimeInput.Visibility = Visibility.Visible;
                lblTimer.Visibility = Visibility.Collapsed;
            } else
            {
                _timeLeft = _timeLeft.Add(TimeSpan.FromSeconds(-1));
                UpdateTimerLabel();
            }
            
        }

        private void UpdateTimerLabel()
        {
            lblTimer.Content = _timeLeft.ToString(@"hh\:mm\:ss");
            if (_timeLeft <= TimeSpan.FromSeconds(10))
                lblTimer.Foreground = System.Windows.Media.Brushes.Red;
            else
                lblTimer.Foreground = SystemColors.ControlTextBrush;
        }

        private void BtnStartStop_Click(object sender, RoutedEventArgs e)
        {

            if (!_isTimerRunning)
            {
                StartTimer();
            }
            else
            {
                if (_timer.IsEnabled)
                {
                    PauseTimer();
                }
                else
                {
                    ResumeTimer();
                }
            }
        }

        private void StartTimer()
        {
            if (TimeSpan.TryParseExact(txtTimeInput.Text, @"hh\:mm\:ss", null, out _timeLeft))
            {
                UpdateTimerLabel();
                btnStartStop.Content = "Stop";
                _isTimerRunning = true;
                txtTimeInput.Visibility = Visibility.Collapsed;
                lblTimer.Visibility = Visibility.Visible;
                _timer.Start();
            }
            else
            {
                MessageBox.Show("Invalid time format! Please enter time in hh:mm:ss format.");
            }
        }

        private void PauseTimer()
        {
            _timer.Stop();
            btnStartStop.Content = "Resume";
        }

        private void ResumeTimer()
        {
            _timer.Start();
            btnStartStop.Content = "Pause";
            _isTimerRunning = true;
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            UpdateTimerLabel();
            _isTimerRunning = false;
            btnStartStop.Content = "Start";
            txtTimeInput.Visibility = Visibility.Visible;
            lblTimer.Visibility = Visibility.Collapsed;
            UpdateTimerLabel();
        }
    }
}
