using System.Windows;
using System.Windows.Controls;

namespace hw
{
    public class person : Control
    {
        public static readonly DependencyProperty name_property =
            DependencyProperty.Register("Name", typeof(string), typeof(person), new PropertyMetadata(string.Empty));

        public string name
        {
            get { return (string)GetValue(name_property); }
            set { SetValue(name_property, value); }
        }

        public static readonly DependencyProperty adress_property =
            DependencyProperty.Register("address", typeof(string), typeof(person), new PropertyMetadata(string.Empty));

        public string address
        {
            get { return (string)GetValue(adress_property); }
            set { SetValue(adress_property, value); }
        }

        public static readonly DependencyProperty phone_property =
            DependencyProperty.Register("phone", typeof(string), typeof(person), new PropertyMetadata(string.Empty));

        public string phone
        {
            get { return (string)GetValue(phone_property); }
            set { SetValue(phone_property, value); }
        }

        public person()
        {
        }

        public person(string name, string address, string phone)
        {
            this.name = name;
            this.address = address;
            this.phone = phone;
        }

        public override string ToString()
        {
            return $"{name}, {address}, {phone}";
        }
    }
}