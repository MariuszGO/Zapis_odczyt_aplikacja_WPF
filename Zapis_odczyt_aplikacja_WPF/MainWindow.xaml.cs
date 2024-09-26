using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;

namespace Zapis_odczyt_aplikacja_WPF
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
        public void zatrzymanie() {
            Thread.Sleep(2000);
            komunikat.Text = " ";

        }

        private void zapisz_do_bazy(object sender, RoutedEventArgs e)
        {
            
            string connectionString = "Data Source=database.db;Version=3;";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();


                string createTableQuery = @"CREATE TABLE IF NOT EXISTS osoby (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            imie TEXT NOT NULL,
                                            nazwisko TEXT NOT NULL
                                            );";

                using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }


                //Console.WriteLine("Podaj imię:");
                string im = imie.Text;
                // Console.WriteLine("Podaj nazwisko:");
                string naz = nazwisko.Text;


                string insertQuery = "INSERT INTO osoby (imie, nazwisko) VALUES (@imie, @nazwisko);";

                using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@imie", im);
                    cmd.Parameters.AddWithValue("@nazwisko", naz);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Dodano do bazy","Informacja",MessageBoxButton.OK);
                imie.Text = "";
                nazwisko.Text = "";

                
                

            }

           // zatrzymanie();

        }
    }
}