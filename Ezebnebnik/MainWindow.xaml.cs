using System.IO;
using System.Security.Policy;
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
using System.Xml;

using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Ezebnebnik.MainWindow;

namespace Ezebnebnik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static class Serializer
        {
            public static void SerializeToJson<T>(T xyi, string filePath)
            {
                string json = JsonConvert.SerializeObject(xyi);
                File.WriteAllText(filePath, json);
            }

            public static T DeserializeFromJson<T>(string filePath)
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        private List<Note> zametka;
        private string filePath = "notes.json";


        public MainWindow()
        {
            InitializeComponent();
            zametka = Serializer.DeserializeFromJson(filePath);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {   
            Name.IsEnabled = true;
            Description_box.IsEnabled = true;

        }

        private void CreateNote()
        {
            // Получаем введенные данные из полей ввода
            string title = Name.Text;
            string description = Description_box.Text;
            DateTime date = DateTime.Now; // Текущая дата и время

            // Создаем новую заметку
            Note newNote = new Note(title, description, date);

            // Сохраняем заметку
            BoxList.ItemsSource.

            // Очищаем поля ввода
            Name.Text = string.Empty;
            Description_box.Text = string.Empty;

            // Закрываем окно создания заметки
            this.Close();
        }
    }
        
}