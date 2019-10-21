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
using System.Data.SqlClient;
using System.Data;
namespace WpfApp_ReturnCancel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.dbConn);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowDetails(object sender, RoutedEventArgs e)
        {


        }

        private void Button_Click_Fill(object sender, RoutedEventArgs e)
        {
            SqlCommand sqlcmd = new SqlCommand("select * from TeamA.Orders",sqlConnection);
            sqlConnection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlcmd);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);
            OrderHistory.ItemsSource = dataTable.DefaultView;
            sqlConnection.Close();

        }
    }
}
