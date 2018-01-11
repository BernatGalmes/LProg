using LP_week6_DataGrid.Classes;
using System.Windows;

namespace LP_week6_DataGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Data d;

        public MainWindow()
        {
            InitializeComponent();
            d = new Data();
            this.dg_students.ItemsSource = d.Students;

        }

        private void button_fl_name_Click(object sender, RoutedEventArgs e)
        {
            this.dg_students.ItemsSource = d.filterByName(tb_name.Text);
        }

        private void button_fl_Surname_Click(object sender, RoutedEventArgs e)
        {
            this.dg_students.ItemsSource = d.filterBySurname(tb_surname.Text);
        }

        private void button_fl_Degree_Click(object sender, RoutedEventArgs e)
        {
            this.dg_students.ItemsSource = d.filterByDegree(tb_degree.Text);
        }
        
    }
}
