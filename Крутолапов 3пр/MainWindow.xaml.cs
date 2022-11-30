using Lib_1;
using LibMatrix;
using System.Windows;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Matrix<int> matrix = new Matrix<int>(0, 0);

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int rows = int.Parse(tbNumberOfRows.Text),
                column = int.Parse(tbNumberOfColumns.Text),
                min = int.Parse(tbMin.Text),
                max = int.Parse(tbMax.Text);

                matrix.CreateMatrix(rows, column, min, max);
                dataGrid.ItemsSource = matrix.ToDataTable().DefaultView;
                buttonTask.IsEnabled = true;
                nullButton.IsEnabled = true;       
            }
            catch (System.Exception)
            {
                MessageBox.Show("Данные введены некорректно, либо отсутствуют");
            }               
        }

        private void ButtonTask(object sender, RoutedEventArgs e)
        {           
            tbTask.Text = matrix.Sum5().ToString();            
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Практическая работа №3 Крутолапов Валерий ИСП-31");
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            matrix.MatrixSerialize(@".\object.matrix");
        }

        private void LoadClick(object sender, RoutedEventArgs e)
        {
            matrix.MatrixDeserialize(@".\object.matrix");
            dataGrid.ItemsSource = matrix.ToDataTable().DefaultView;
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            matrix.DefaultInit();
            dataGrid.ItemsSource = matrix.ToDataTable().DefaultView;
        }
    }
}
