using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pr4_DataBinding_Neuimina
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Phone currentPhone;
        public MainWindow()
        {
            InitializeComponent();
            CreateNewSmartphone();
        }

        private void CreateNewSmartphone()
        {
            currentPhone = new Phone
            {
                Id = GenerateUniqueId()
            };
            DataContext = currentPhone;
        }


        private int GenerateUniqueId()
        {
            Random random = new Random();
            int newId;
            do
            {
                newId = random.Next(1, 10000);
            }
            while (File.Exists($"{newId}.json"));

            return newId;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText($"{currentPhone.Id}.json", JsonSerializer.Serialize(currentPhone));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateNewSmartphone();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string searchId = Poisk.Text;
            if (string.IsNullOrEmpty(searchId))
            {
                MessageBox.Show("Введите ID для поиска", "Внимание", MessageBoxButton.OK);
                return;
            }

            string fileName = $"{searchId}.json";

            if (File.Exists(fileName))
            {
                try
                {
                    string jsonString = File.ReadAllText(fileName);
                    Phone? loadedPhone = JsonSerializer.Deserialize<Phone>(jsonString);

                    if (loadedPhone != null)
                    {
                        currentPhone = loadedPhone;
                        DataContext = currentPhone;
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка чтения файла: {ex.Message}", "Ошибка", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show($"Смартфон с ID {searchId} не найден.", "Результат поиска", MessageBoxButton.OK);
                return;
            }
        }


    }
}