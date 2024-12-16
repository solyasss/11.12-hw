using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
                on_property_changed();
            }
        }

        private person _selected_person;
        public person selected_person
        {
            get { return _selected_person; }
            set
            {
                _selected_person = value;
                on_property_changed();
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
                on_property_changed();
            }
        }

        public ViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void on_property_changed([CallerMemberName] string prop_name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop_name));
        }
    }
}