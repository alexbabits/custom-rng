using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace CustomRNG
{
    public partial class MainWindow : Window
    {
        // Nullable string 's' holds random number as a string
        //'randomNumber' holds generated random integer
        //'rand is an instance of Random class, used for generated random numbers
        public String? s;
        public int randomNumber;
        public Random rand = new Random();

        public MainWindow()
        {
            // Initializes the components defined in XAML, and assign default empty string value for 's'
            InitializeComponent();
            s = "";
            // Creates an instance of the DispatcherTimer class
            DispatcherTimer disTmr = new DispatcherTimer();
            // Registers the 'disTmr_Tick' method as the event handler to be called on each 'Tick' event
            disTmr.Tick += new EventHandler(disTmr_Tick);
            // Sets the interval for the timer to 5 seconds by default, and starts the timer
            UpdateTimerInterval(disTmr);
            disTmr.Start();
            // Registers the TextBox TextChanged event
            timeIntervalTextBox.TextChanged += (sender, e) => UpdateTimerInterval(disTmr);
        }

        private void UpdateTimerInterval(DispatcherTimer disTmr)
        {
            if (int.TryParse(timeIntervalTextBox.Text, out int interval) && interval >= 1 && interval <= 60)
            {
                disTmr.Interval = TimeSpan.FromSeconds(interval);
            }
            else
            {
                timeIntervalTextBox.Text = disTmr.Interval.TotalSeconds.ToString();
            }
        }

        // 'disTmr_Tick' method called every X seconds, when the DispatcherTimer 'Tick' event is raised
        // If 'auto' checkbox is checked, random number is generated, converted to string, assigned to the text box, and colored.
        public void disTmr_Tick(object? sender, EventArgs? e)
        {
            if (autoCheckBox.IsChecked == true)
            {
                randomNumber = rand.Next(0, 101);
                s = randomNumber.ToString();
                rngTextBlock.Text = s;
                ChangeTextColor(randomNumber);
            };
        }

        // If 'manual' button is checked, random number is generated, converted to string, assigned to the text box, and colored.
        private void manualButton_Click(object? sender, RoutedEventArgs e)
        {
            randomNumber = rand.Next(0, 101);
            s = randomNumber.ToString();
            rngTextBlock.Text = s;
            ChangeTextColor(randomNumber);
        }

        // 'ChangeTextColor' method changes the text in the textbox UI based on value generated.
        private void ChangeTextColor(int randomNumber)
        {
            SolidColorBrush color;
            bool isInverted = invertColorCheckBox.IsChecked == true;

            if (0 <= randomNumber && randomNumber <= 19)
            {
                color = isInverted ? Brushes.Red : Brushes.MidnightBlue;
            }
            else if (20 <= randomNumber && randomNumber <= 39)
            {
                color = isInverted ? Brushes.OrangeRed : Brushes.Blue;
            }
            else if (40 <= randomNumber && randomNumber <= 59)
            {
                color = Brushes.Purple;
            }
            else if (60 <= randomNumber && randomNumber <= 79)
            {
                color = isInverted ? Brushes.Blue : Brushes.OrangeRed;
            }
            else // 80 <= randomNumber && randomNumber <= 100
            {
                color = isInverted ? Brushes.MidnightBlue : Brushes.Red;
            }
            rngTextBlock.Foreground = color;
        }
    }
};
