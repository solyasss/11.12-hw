﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace hw
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<person> _people = new ObservableCollection<person>();
        public ObservableCollection<person> people
        {
            get { return _people; }
            set
            {
                _people = value;
                on_property_changed("people");
            }
        }

        private person _selected_person;
        public person selected_person
        {
            get { return _selected_person; }
            set
            {
                _selected_person = value;
                on_property_changed("selected_person");
                if (selected_person != null)
                {
                    new_person = new person(selected_person.fullname, selected_person.address, selected_person.phone);
                }
                else
                {
                    new_person = new person();
                }
            }
        }

        private person _new_person = new person();
        public person new_person
        {
            get { return _new_person; }
            set
            {
                _new_person = value;
                on_property_changed("new_person");
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void add_click(object sender, RoutedEventArgs e)
        {
            var p = new person(new_person.fullname, new_person.address, new_person.phone);
            people.Add(p);
            new_person = new person();
        }

        private void delete_click(object sender, RoutedEventArgs e)
        {
            if (selected_person != null)
            {
                people.Remove(selected_person);
                selected_person = null;
            }
        }

        private void modify_click(object sender, RoutedEventArgs e)
        {
            if (selected_person != null)
            {
                selected_person.fullname = new_person.fullname;
                selected_person.address = new_person.address;
                selected_person.phone = new_person.phone;
                new_person = new person();
            }
        }

        private void save_click(object sender, RoutedEventArgs e)
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

        private void load_click(object sender, RoutedEventArgs e)
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
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void on_property_changed(string prop_name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop_name));
        }
    }
}
