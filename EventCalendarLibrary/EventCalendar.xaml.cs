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
using System.Windows.Controls.Primitives;

namespace EventCalendarLibrary
{
    public partial class EventCalendar : UserControl, INotifyPropertyChanged
    {
        private List<CalendarDayButton> calendarButtons = new List<CalendarDayButton>();

        public EventCalendar()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty CalendarEventsProperty = DependencyProperty.RegisterAttached("CalendarEvents", typeof(List<CalendarEvent>), typeof(EventCalendar), new PropertyMetadata(CalendarEventsChanged));

        public List<CalendarEvent> CalendarEvents
        {
            get { return (List<CalendarEvent>)GetValue(CalendarEventsProperty); }
            set
            {
                if (value != null)
                {
                    SetValue(CalendarEventsProperty, (List<CalendarEvent>)value);
                }
            }
        }

        private Dictionary<DateTime, List<CalendarEvent>> _calendarEventsDictionary;
        public Dictionary<DateTime, List<CalendarEvent>> CalendarEventsDictionary
        {
            get
            {
                return _calendarEventsDictionary;
            }

            set
            {
                _calendarEventsDictionary = value;
                OnPropertyChanged("CalendarEventsDictionary");
            }
        }

        public static void CalendarEventsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var owner = d as EventCalendar;
            var parDate =  owner.CalendarEvents

                        .GroupBy(x => x.Date)

                        .ToDictionary(x => x.Key, x => x.ToList()
                            .Select(y => new CalendarEvent { Name = y.Name, Description = y.Description }).ToList()
                        );
            owner.CalendarEventsDictionary = parDate;
            
            owner.FillCalendar();
        }

        private void FillCalendar()
        {
            FillCalendar(new DateTime(InnerCalendar.DisplayDate.Year, InnerCalendar.DisplayDate.Month, 1));
        }

        private void FillCalendar(DateTime firstDate)
        {
            if (CalendarEvents != null && CalendarEvents.Count > 0)
            {
                int counter = 0;
                DateTime currentDay;

                int weekDay = (int)firstDate.DayOfWeek;
                if (weekDay == 0) weekDay = 7;
                if (weekDay == 1) weekDay = 8;

                foreach (CalendarDayButton button in calendarButtons)
                {

                    var panel = button.Parent as StackPanel;

                    currentDay = firstDate.AddDays(counter).AddDays(1 - weekDay);

                    int nbControls = panel.Children.Count;
                    for (int i = nbControls - 1; i > 0; i--)
                    {
                        panel.Children.RemoveAt(i);
                    }

                    if (CalendarEventsDictionary.ContainsKey(currentDay))
                    {
                        var events = CalendarEventsDictionary[currentDay];
                        foreach (CalendarEvent calendarEvent in events)
                        {
                            Button btn = new Button();
                            btn.Content = calendarEvent.Name;
                            panel.Children.Add(btn);
                        }
                    }
                    counter++;
                }
            }
        }

        private void CalendarItem_Loaded(object sender, RoutedEventArgs e)
        {
            var item = sender as CalendarItem;
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        #endregion

        private void CalendarDayButton_Loaded(object sender, RoutedEventArgs e)
        {
            var button = sender as CalendarDayButton;
            calendarButtons.Add(button);

            if (calendarButtons.Count == 42)
            {
                FillCalendar();
            }
        }
    }
}
