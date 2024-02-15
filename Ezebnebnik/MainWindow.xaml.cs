using System.IO;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
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
        List<Note> zametka = new List<Note>();
        public static class Serializer
        {
            public static void SerializeToJson<T>(T xyi, string filename)
            {
                string json = JsonConvert.SerializeObject(xyi);
                File.WriteAllText(filename, json);
            }

            public static T DeserializeFromJson<T>(string filename)
            {
                string json = File.ReadAllText(filename);
                return JsonConvert.DeserializeObject<T>(json);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            
            if (Name.Text == "" && Description_box.Text == "" && DataPicker.Text == "")
            {
                return;
            } 
            else
            {
                zametka = Serializer.DeserializeFromJson<List<Note>>(DataPicker.Text + ".json");
                Note notes = new Note(Name.Text, Description_box.Text, Convert.ToDateTime(DataPicker.Text));
                zametka.Add(notes);
                string filename = DataPicker.Text + ".json";
                Serializer.SerializeToJson(zametka, filename);
                BoxList.ItemsSource = zametka;

            }
        }

        private void BoxList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Note listbox = BoxList.SelectedItem as Note;
            if (listbox == null)
            {
                Name.Text = " ";
                Description_box.Text = " ";
            }
            else
            {
                Description_box.Text = listbox.Description;
                Name.Text = listbox.Title;
            }
        }

        private void DataPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!File.Exists(DataPicker.Text + ".json"))
            {
                File.WriteAllText(DataPicker.Text + ".json", "[]");
                BoxList.ItemsSource = zametka;
                BoxList.ItemsSource = Serializer.DeserializeFromJson<List<Note>>(DataPicker.Text + ".json");
                Name.Text = "";
                Description_box.Text = "";
            } 
            else
            {
                BoxList.ItemsSource = zametka;
                BoxList.ItemsSource = Serializer.DeserializeFromJson<List<Note>>(DataPicker.Text + ".json");
                Name.Text = "";
                Description_box.Text = "";
            }
        }

        private void DELETE_Click(object sender, RoutedEventArgs e)
        {
            if (BoxList.SelectedItem == null)
            {
                return;
            }
            Note selectedNote = (Note)BoxList.SelectedItem;

            zametka.Remove(selectedNote);

            string filename = DataPicker.Text + ".json";
            Serializer.SerializeToJson(zametka, filename);

            BoxList.ItemsSource = null;
            BoxList.ItemsSource = zametka;

            Name.Text = "";
            Description_box.Text = "";
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            Note selectedNote = (Note)BoxList.SelectedItem;
            string filename = DataPicker.Text + ".json";

            selectedNote.Title = Name.Text;
            selectedNote.Description = Description_box.Text;

            Serializer.SerializeToJson(zametka, filename);

            BoxList.ItemsSource = null;
            BoxList.ItemsSource = zametka;

            Name.Text = "";
            Description_box.Text = "";

        }
    }
}