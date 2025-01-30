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

namespace WpfKeszAppGyakProj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            loadList(null, null);
        }
        private void addToList(object sender, RoutedEventArgs e)
        {
            string item = textBox.Text;
            if (listBox.SelectedItem == null)
            {
                if (item != "")
                {
                    listBox.Items.Add(item);
                    textBox.Text = "";
                }
            }
            else
            {
                int index = listBox.SelectedIndex;
                listBox.Items.RemoveAt(index);
                listBox.Items.Insert(index, item);
            }
            saveList(null, null);
        }
        private void removeFromList(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                listBox.Items.RemoveAt(listBox.SelectedIndex);
            }
            saveList(null, null);
        }
        private void saveList(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string item in listBox.Items)
            {
                sb.AppendLine(item);
            }
            System.IO.File.WriteAllText("list.txt", sb.ToString());
        }
        private void loadList(object sender, RoutedEventArgs e)
        {
            string filePath = "list.txt";
            if (System.IO.File.Exists(filePath))
            {
                string[] items = System.IO.File.ReadAllLines(filePath);
                foreach (string item in items)
                {
                    listBox.Items.Add(item);
                }
            }
        }
        private void addBtnEnable(object sender, TextChangedEventArgs e)
        {
            buttonAdd.IsEnabled = textBox.Text != "";
        }
        private void removeBtnEnable(object sender, SelectionChangedEventArgs e)
        {
            buttonRemove.IsEnabled = listBox.SelectedIndex != null;
            textBox.Text = listBox.SelectedItem != null ? listBox.SelectedItem.ToString() : null;
        }
        private void searchBtnEnable(object sender, TextChangedEventArgs e)
        {
            buttonSearch.IsEnabled = searchBox.Text != "";
        }
        private void searchFromList(object sender, RoutedEventArgs e)
        {
            bool found = false;
            foreach (string item in listBox.Items)
            {
                if(item == searchBox.Text)
                {
                    MessageBox.Show(item + " found in the list at " + listBox.Items.IndexOf(item) + " index!");
                    searchBox.Text = "";
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                MessageBox.Show(searchBox.Text + " not found in the list!");
                searchBox.Text = "";
            }
        }
    }
}