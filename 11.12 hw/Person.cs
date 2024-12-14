using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace hw
{
    public class person : INotifyPropertyChanged
    {
        private string name_person;
        private string address_person;
        private string phone_person;

        public string fullname
        {
            get => name_person;
            set
            {
                name_person = value;
                property_changed();
            }
        }

        public string address
        {
            get => address_person;
            set
            {
                address_person = value;
                property_changed();
            }
        }

        public string phone
        {
            get => phone_person;
            set
            {
                phone_person = value;
                property_changed();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void property_changed([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public person()
        {
        }

        public person(string fullname, string address, string phone)
        {
            this.fullname = fullname;
            this.address = address;
            this.phone = phone;
        }

        public override string ToString()
        {
            return $"{fullname}, {address}, {phone}";
        }
    }
}