using System.Data;
using System.Linq;
using System.Windows;
using week6_library;

namespace Week6_DataSet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Data d;
        private DataSet ds;

        public MainWindow()
        {
            InitializeComponent();
            d = new Data();
            d.generateData();

            //Create a dataset with the students
            ds = new DataSet();
            DataTable t_st = d.DataTableStudents();
            DataTable t_dg = d.DataTableDegree();
            ds.Tables.Add(t_st);
            ds.Tables.Add(t_dg);
            this.columnDegree.ItemsSource = t_dg.Rows.OfType<DataRow>().ToList();
            columnDegree.SelectedValuePath = "[ID]";
            columnDegree.DisplayMemberPath = "[Name]";
            this.dg_students.DataContext = t_st.DefaultView;
        }
    }
}
