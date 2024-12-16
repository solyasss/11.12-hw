using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace hw
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            var view_model = DataContext as ViewModel;
            if (view_model != null)
            {
                if (!string.IsNullOrWhiteSpace(view_model.new_person.name) &&
                    !string.IsNullOrWhiteSpace(view_model.new_person.address) &&
                    !string.IsNullOrWhiteSpace(view_model.new_person.phone))
                {
                    var p = new person(view_model.new_person.name, view_model.new_person.address, view_model.new_person.phone);
                    view_model.people.Add(p);
                    view_model.new_person = new person();
                }
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var view_model = DataContext as ViewModel;
            if (view_model != null && view_model.selected_person != null)
            {
                view_model.people.Remove(view_model.selected_person);
                view_model.selected_person = null;
            }
        }

        private void modify_Click(object sender, RoutedEventArgs e)
        {
            var view_model = DataContext as ViewModel;
            if (view_model != null && view_model.selected_person != null)
            {
                if (!string.IsNullOrWhiteSpace(view_model.new_person.name) &&
                    !string.IsNullOrWhiteSpace(view_model.new_person.address) &&
                    !string.IsNullOrWhiteSpace(view_model.new_person.phone))
                {
                    view_model.selected_person.name = view_model.new_person.name;
                    view_model.selected_person.address = view_model.new_person.address;
                    view_model.selected_person.phone = view_model.new_person.phone;
                    view_model.new_person = new person();
                }
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            var view_model = DataContext as ViewModel;
            if (view_model != null && view_model.people.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "text files (*.txt)|*.txt";
                if (sfd.ShowDialog() == true)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        foreach (var p in view_model.people)
                        {
                            sw.WriteLine($"{p.name}|{p.address}|{p.phone}");
                        }
                    }
                }
            }
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            var view_model = DataContext as ViewModel;
            if (view_model != null)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "text files (*.txt)|*.txt";
                if (ofd.ShowDialog() == true)
                {
                    view_model.people.Clear();
                    using (StreamReader sr = new StreamReader(ofd.FileName))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 3)
                            {
                                view_model.people.Add(new person(parts[0], parts[1], parts[2]));
                            }
                        }
                    }
                }
            }
        }
    }
}
