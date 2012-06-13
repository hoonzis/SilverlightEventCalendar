using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using EventCalendarLibrary;

namespace EventCalendar
{
    public partial class MainPage : UserControl, INotifyPropertyChanged
    {
        private List<CalendarEvent> _calendarEvents;

        public List<CalendarEvent> CalendarEvents
        {
            get { return _calendarEvents; }
            set {
                _calendarEvents = value;
                OnPropertyChanged("CalendarEvents");
            }
        }
     

        public MainPage()
        {
            InitializeComponent();
            
            this.DataContext = this;
            List<CalendarEvent> values = new List<CalendarEvent>();
            values.Add(new CalendarEvent { Name = "Event 1", Description = "Description1", Date = DateTime.Today });
            values.Add(new CalendarEvent { Name = "Event 2", Description = "Descritpion 2", Date=DateTime.Today.Subtract(new TimeSpan(10, 0, 0, 0)) });
            values.Add(new CalendarEvent { Name = "Event 3", Description = "Descriptio,,,", Date = DateTime.Now });
            
            CalendarEvents = values;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
