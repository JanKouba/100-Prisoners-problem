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

namespace Prisoners
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        double playgroundPrisoners = 0;
        double playgroundRepeat = 0;

        List<Playground> pg = new List<Playground>();

        private void buttonNew_Click(object sender, RoutedEventArgs e)
        {
            string message = string.Empty;
            if (SetPlaygroundParameters(out message))
                Launch();
            else
                MessageBox.Show(message, "Parameters error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private bool SetPlaygroundParameters(out string message)
        {
            bool prisonersIsValid = false;
            bool repeatIsValid = false;
            message = string.Empty;

            prisonersIsValid = Double.TryParse(textBoxNumberOfPrisoners.Text, out playgroundPrisoners);
            repeatIsValid = Double.TryParse(textBoxNumberOfRepeat.Text, out playgroundRepeat);

            if (!prisonersIsValid || playgroundPrisoners % 2 != 0 || playgroundPrisoners <= 0)
            { 
                prisonersIsValid = false;
                message = "Count of prisoners must be positive even number!";
            }

            if (!repeatIsValid || playgroundRepeat <= 0)
            { 
                repeatIsValid = false;
                message = "Count of repeat must be greater than 0!";
            }

            return prisonersIsValid && repeatIsValid;
        }

        private void Launch()
        {
            double passed = 0;

            dataGridPlaygrondList.ItemsSource = null;
            dataGridPrisonersList.ItemsSource = null;

            pg.Clear();

            for (double i = 1; i <= playgroundRepeat; i++)
            {
                Playground p = new Playground(Convert.ToInt32(playgroundPrisoners), (int)i - 1 );

                p.CreateBoxes();
                p.CreatePrisoners();

                foreach (Prisoner prisoner in p.Prisoners)
                    p.DoJourney(prisoner);

                if (p.Prisoners.FindAll(a => a.Result == Result.Passed).Count() == playgroundPrisoners)
                {
                    passed++;
                    p.Result = Result.Passed;
                }
                else
                    p.Result = Result.Failed;

                int countPassed = p.Prisoners.FindAll(a => a.Result == Result.Passed).Count();
                int countFailed = p.Prisoners.FindAll(a => a.Result == Result.Failed).Count();

                p.CountFailed = countFailed;
                p.CountPassed = countPassed;

                pg.Add(p);
            }

            labelSuccessRatio.Content = Math.Round(passed / playgroundRepeat, 2).ToString("#.##%");

            dataGridPlaygrondList.ItemsSource = pg;
        }

        private void dataGridPlaygroundList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((DataGrid)sender).SelectedItems.Count > 0)
            {
                Playground selectedPlayground = (Playground)((DataGrid)sender).CurrentItem;

                dataGridPrisonersList.ItemsSource = selectedPlayground.Prisoners;

                labelPassed.Content = selectedPlayground.CountPassed;
                labelFailed.Content = selectedPlayground.CountFailed;
            }
        }
    }
}
