#if __WASM__
using System;
using System.Globalization;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Uno.Foundation;
using Uno.Extensions;
using Uno.UI.Runtime.WebAssembly;

namespace FlatpickrDemo.Shared
{
    [HtmlElement("input")] // Flatpickr requires an <input> HTML element
    public class FlatpickrView : FrameworkElement
    {
        // *************************
        // * Dependency Properties *
        // *************************

        public static readonly DependencyProperty SelectedDateTimeProperty = DependencyProperty.Register(
            "SelectedDateTime", typeof(DateTimeOffset?), typeof(FlatpickrView), new PropertyMetadata(default(DateTimeOffset?)));

        public DateTimeOffset? SelectedDateTime
        {
            get => (DateTimeOffset)GetValue(SelectedDateTimeProperty);
            set => SetValue(SelectedDateTimeProperty, value);
        }

        public static readonly DependencyProperty IsPickerOpenedProperty = DependencyProperty.Register(
            "IsPickerOpened", typeof(bool), typeof(FlatpickrView), new PropertyMetadata(false));

        public bool IsPickerOpened
        {
            get => (bool)GetValue(IsPickerOpenedProperty);
            set => SetValue(IsPickerOpenedProperty, value);
        }

        // ***************
        // * Constructor *
        // ***************

        public FlatpickrView()
        {
            // XAML behavior: a non-null background is required on an element to be "visible to pointers".
            // Uno reproduces this behavior, so we must set it here even if we're not using the background.
            // Not doing this will lead to a `pointer-events: none` CSS style on the control.
            Background = new SolidColorBrush(Colors.Transparent);

            // Load Flatpickr using JavaScript
            LoadJavaScript();

            // Register event handler for custom events from the DOM
            this.RegisterHtmlCustomEventHandler("DateChanged", OnDateChanged, isDetailJson: false);
            this.RegisterHtmlCustomEventHandler("OpenedStateChanged", OnOpenedStateChanged, isDetailJson: false);
        }

        // ******************
        // * Initialization *
        // ******************

        private void LoadJavaScript()
        {
            // For demo purposes, Flatpickr is loaded directly from CDN.
            // Uno uses AMD module loading, so you must give a callback when the resource is loaded.
            // We can access the corresponding DOM HTML Element by using the "element" variable available in the scope
            var javascript = $@"require([""https://cdn.jsdelivr.net/npm/flatpickr""], f => {{
                // Route Flatpickr events following Uno's documentation
                // https://platform.uno/docs/articles/wasm-custom-events.html
                const options = {{
                    onChange: (dates, str) => element.dispatchEvent(new CustomEvent(""DateChanged"", {{detail: str}})),
                    onOpen: () => element.dispatchEvent(new CustomEvent(""OpenedStateChanged"", {{detail: ""open""}})),
                    onClose: () => element.dispatchEvent(new CustomEvent(""OpenedStateChanged"", {{detail: ""closed""}}))
                    }};

                    // Instantiate Flatpickr on the element
                    f(element, options);
            }});";

            this.ExecuteJavascript(javascript);
        }

        // ******************
        // * Event Handlers *
        // ******************
        private void OnDateChanged(object sender, HtmlCustomEventArgs e)
        {
            if (DateTimeOffset.TryParse(e.Detail, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AssumeLocal, out var dto))
            {
                SelectedDateTime = dto;
            }
        }

        private void OnOpenedStateChanged(object sender, HtmlCustomEventArgs e)
        {
            switch (e.Detail)
            {
                case "open":
                    IsPickerOpened = true;
                    break;
                case "closed":
                    IsPickerOpened = false;
                    break;
            }
        }

    }
}
#endif
