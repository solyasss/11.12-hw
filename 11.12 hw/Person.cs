using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace hw
{
    public class person : INotifyPropertyChanged
    {
        private string _fullname;
        private string _address;
        private string _phone;

        public string fullname
        {
            get => _fullname;
            set
            {
                _fullname = value;
                property_changed();
            }
        }

        public string address
        {
            get => _address;
            set
            {
                _address = value;
                property_changed();
            }
        }

        public string phone
        {
            get => _phone;
            set
            {
                _phone = value;
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