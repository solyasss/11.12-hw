using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Microsoft.Win32;

namespace hw
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<person> _people = new ObservableCollection<person>();
        public ObservableCollection<person> people
        {
            get { return _people; }
            set
            {
                _people = value;
                on_property_changed("people");
                CommandManager.InvalidateRequerySuggested();
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
                CommandManager.InvalidateRequerySuggested();
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
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ICommand add_command { get; set; }
        public ICommand delete_command { get; set; }
        public ICommand modify_command { get; set; }
        public ICommand save_command { get; set; }
        public ICommand load_command { get; set; }

        public ViewModel()
        {
            add_command = new command(execute_add, can_execute_add);
            delete_command = new command(execute_delete, can_execute_delete);
            modify_command = new command(execute_modify, can_execute_modify);
            save_command = new command(execute_save, can_execute_save);
            load_command = new command(execute_load, can_execute_load);
        }

        private void execute_add(object parameter)
        {
            var p = new person(new_person.fullname, new_person.address, new_person.phone);
            people.Add(p);
            new_person = new person();
        }

        private bool can_execute_add(object parameter)
        {
            return !string.IsNullOrWhiteSpace(new_person.fullname) &&
                   !string.IsNullOrWhiteSpace(new_person.address) &&
                   !string.IsNullOrWhiteSpace(new_person.phone);
        }

        private void execute_delete(object parameter)
        {
            if (selected_person != null)
            {
                people.Remove(selected_person);
                selected_person = null;
            }
        }

        private bool can_execute_delete(object parameter)
        {
            return selected_person != null;
        }

        private void execute_modify(object parameter)
        {
            if (selected_person != null)
            {
                selected_person.fullname = new_person.fullname;
                selected_person.address = new_person.address;
                selected_person.phone = new_person.phone;
                new_person = new person();
            }
        }

        private bool can_execute_modify(object parameter)
        {
            return selected_person != null &&
                   !string.IsNullOrWhiteSpace(new_person.fullname) &&
                   !string.IsNullOrWhiteSpace(new_person.address) &&
                   !string.IsNullOrWhiteSpace(new_person.phone);
        }

        private void execute_save(object parameter)
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

        private bool can_execute_save(object parameter)
        {
            return people != null && people.Count > 0;
        }

        private void execute_load(object parameter)
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

        private bool can_execute_load(object parameter)
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void on_property_changed(string prop_name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop_name));
        }
    }
}
