using System.Collections.ObjectModel;
using System.Windows;

namespace hw
{
    public class ViewModel : DependencyObject
    {
        public static readonly DependencyProperty people_property =
            DependencyProperty.Register("people", typeof(ObservableCollection<person>), typeof(ViewModel), new PropertyMetadata(new ObservableCollection<person>()));

        public ObservableCollection<person> people
        {
            get { return (ObservableCollection<person>)GetValue(people_property); }
            set { SetValue(people_property, value); }
        }

        public static readonly DependencyProperty selected_person_property =
            DependencyProperty.Register("selected_person", typeof(person), typeof(ViewModel),
                new PropertyMetadata(null, OnSelectedPersonChanged));

        public person selected_person
        {
            get { return (person)GetValue(selected_person_property); }
            set { SetValue(selected_person_property, value); }
        }

        public static readonly DependencyProperty new_person_property =
            DependencyProperty.Register("new_person", typeof(person), typeof(ViewModel), new PropertyMetadata(null));

        public person new_person
        {
            get { return (person)GetValue(new_person_property); }
            set { SetValue(new_person_property, value); }
        }

        public ViewModel()
        {
            new_person = new person();
        }

        private static void OnSelectedPersonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view_model = d as ViewModel;
            if (view_model != null)
            {
                if (view_model.selected_person != null)
                {
                    view_model.new_person = new person(view_model.selected_person.name, view_model.selected_person.address, view_model.selected_person.phone);
                }
                else
                {
                    view_model.new_person = new person();
                }
            }
        }
    }
}
