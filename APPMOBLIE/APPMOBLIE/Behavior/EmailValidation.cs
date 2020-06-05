using Microcharts;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using Entry = Xamarin.Forms.Entry;
using Xamarin.Essentials;

namespace APPMOBLIE.Validations
{
    class EmailValidation : Behavior<Entry>
    {
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        public static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(
            "IsValid",
            typeof(bool),
            typeof(EmailValidation),
            false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get
            {
                return (bool)this.GetValue(IsValidProperty);
            }
            set 
            {
                this.SetValue(IsValidPropertyKey, value);    
            }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += HandleTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry;
            entry = (Entry)sender;
            this.IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex));
            entry.TextColor = this.IsValid ? Color.Default : Color.Red;
        }
    }

    class PasswordValidation : Behavior<Entry>
    {
        const string PasswordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";

        public static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(
            "IsValid",
            typeof(bool), 
            typeof(PasswordValidation),
            false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get
            {
                return (bool)this.GetValue(IsValidProperty);
            }
            set
            {
                this.SetValue(IsValidPropertyKey, value);
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

        void HandlerTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry;
            entry = (Entry)sender;
            this.IsValid = (Regex.IsMatch(e.NewTextValue, PasswordRegex));
            entry.TextColor = this.IsValid ? Color.Default : Color.Red;
        }
    }

    class EmptyValidation : Behavior<Entry>
    {

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += HandlerTextChanged;
        }

        void HandlerTextChanged(Object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                ((Entry)sender).TextColor = Color.Default;
            }
            else
            {
                ((Entry)sender).TextColor = Color.Red;
            }
        }
    }
}
