using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace EventCalendarLibrary
{
    public class CalendarEvent
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime Date { get; set; }
    }
}
