using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EmbeddingJSComponentsWASM_PSInShared
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
#if NETFX_CORE
        private void picker_Opened(object sender, object e)
        {
            CalendarDatePicker datePicker = sender as CalendarDatePicker;

            DatePickerOpenLabel.Text = datePicker.IsCalendarOpen ? "True" : "False";
        }

        private void picker_Closed(object sender, object e)
        {
            CalendarDatePicker datePicker = sender as CalendarDatePicker;

            DatePickerOpenLabel.Text = datePicker.IsCalendarOpen ? "True" : "False";
        }

        private void picker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (DateTimeOffset.TryParse(sender.Date.Value.ToString(), DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AssumeLocal, out var dto))
            {
                DatePickerDateLabel.Text = dto.ToString();
            }
        }
#endif
    }
}
