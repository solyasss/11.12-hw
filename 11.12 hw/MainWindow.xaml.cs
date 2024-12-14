using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace hw
{
    public partial class MainWindow : Window
    {
        private List<person> people = new List<person>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            var p = new person(fullnameTextBox.Text, addressTextBox.Text, phoneTextBox.Text);
            people.Add(p);
            refresh();
            clear();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem is person selected)
            {
                people.Remove(selected);
                refresh();
                clear();
            }
        }

        private void modify_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem is person selected)
            {
                selected.fullname = fullnameTextBox.Text;
                selected.address = addressTextBox.Text;
                selected.phone = phoneTextBox.Text;
                refresh();
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "text files (*.txt)|*.txt";
            if (sfd.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    foreach (var p in people)
                    {
                        sw.WriteLine($"{p.fullname}|{p.address}|{p.phone}");
                    }
                }
            }
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "text files (*.txt)|*.txt";
            if (ofd.ShowDialog() == true)
            {
                people.Clear();
                using (StreamReader sr = new StreamReader(ofd.FileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var parts = line.Split('|');
                        if (parts.Length == 3)
                        {
                            people.Add(new person(parts[0], parts[1], parts[2]));
                        }
                    }
                }
                refresh();
            }
        }

        private void list_change(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (listBox.SelectedItem is person selected)
            {
                fullnameTextBox.Text = selected.fullname;
                addressTextBox.Text = selected.address;
                phoneTextBox.Text = selected.phone;
            }
        }

        private void refresh()
        {
            listBox.ItemsSource = null;
            listBox.ItemsSource = people;
        }

        private void clear()
        {
            fullnameTextBox.Text = "";
            addressTextBox.Text = "";
            phoneTextBox.Text = "";
        }
    }
}
