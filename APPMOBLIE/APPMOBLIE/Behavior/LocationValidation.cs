using Microcharts;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using Entry = Xamarin.Forms.Entry;
using Xamarin.Essentials;


namespace APPMOBLIE.Behavior
{
    class LocationValidation : Behavior<Entry>
    {
        protected static readonly BindablePropertyKey bindablePropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(LocationValidation), false);
        public static readonly BindableProperty bindableProperty = bindablePropertyKey.BindableProperty;

        public bool IsValid
        {
            get
            {
                return (bool)GetValue(bindableProperty);
            }
            set
            {
                this.SetValue(bindablePropertyKey, value);
            }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += HandlerTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= HandlerTextChanged;
        }

        void HandlerTextChanged(Object sender, TextChangedEventArgs e)
        {
            Entry entry;
            entry = (Entry)sender;
            if (e.NewTextValue != null)
            {
                if (e.NewTextValue.Length == 0)
                {
                    this.IsValid = false;
                }
                else
                {
                    this.IsValid = true;
                }
            }
            else
            {
                this.IsValid = false;
            }

            entry.TextColor = this.IsValid ? Color.Default : Color.Red;
        }
    }
}
