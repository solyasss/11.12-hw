using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace hw
{
    public partial class MainWindow : Window
    {
        private List<person> people = new List<person>();

        public ICommand add_command { get; }
        public ICommand delete_command { get; }
        public ICommand modify_command { get; }
        public ICommand save_command { get; }
        public ICommand load_command { get; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            add_command = new command(
                delegate(object o) { add_person(); },
                delegate(object o)
                {
                    return !string.IsNullOrWhiteSpace(fullnameTextBox.Text)
                           && !string.IsNullOrWhiteSpace(addressTextBox.Text)
                           && !string.IsNullOrWhiteSpace(phoneTextBox.Text);
                });

            delete_command = new command(
                delegate(object o) { delete_person(); },
                delegate(object o) { return listBox.SelectedItem is person; });

            modify_command = new command(
                delegate(object o) { modify_person(); },
                delegate(object o)
                {
                    return listBox.SelectedItem is person
                           && !string.IsNullOrWhiteSpace(fullnameTextBox.Text)
                           && !string.IsNullOrWhiteSpace(addressTextBox.Text)
                           && !string.IsNullOrWhiteSpace(phoneTextBox.Text);
                });

            save_command = new command(
                delegate(object o) { save(); },
                delegate(object o) { return people.Count > 0; });

            load_command = new command(
                delegate(object o) { load(); },
                delegate(object o) { return true; });
        }

        private void add_person()
        {
            var p = new person(fullnameTextBox.Text, addressTextBox.Text, phoneTextBox.Text);
            people.Add(p);
            refresh();
            clear();
        }

        private void delete_person()
        {
            if (listBox.SelectedItem is person selected)
            {
                people.Remove(selected);
                refresh();
                clear();
            }
        }

        private void modify_person()
        {
            if (listBox.SelectedItem is person selected)
            {
                selected.fullname = fullnameTextBox.Text;
                selected.address = addressTextBox.Text;
                selected.phone = phoneTextBox.Text;
                refresh();
            }
        }

        private void save()
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

        private void load()
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
            else
            {
                clear();
            }

            CommandManager.InvalidateRequerySuggested();
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