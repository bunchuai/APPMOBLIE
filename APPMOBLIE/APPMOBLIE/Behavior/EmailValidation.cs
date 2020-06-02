using Microcharts;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using Entry = Xamarin.Forms.Entry;

namespace APPMOBLIE.Validations
{
    class EmailValidation : Behavior<Entry>
    {
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

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
            bool IsValid = false;
            IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex));
            if (IsValid == true)
            {
                ((Entry)sender).TextColor = Color.Default;
            }
            else
            {
                ((Entry)sender).TextColor = Color.Red;
            }
        }
    }

    class PasswordValidation : Behavior<Entry>
    {
        const string PasswordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";

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
            bool IsNull = false;
            IsNull = (Regex.IsMatch(e.NewTextValue, PasswordRegex));
            if (IsNull == true)
            {
                ((Entry)sender).TextColor = Color.Default;
                ((Entry)sender).BackgroundColor = Color.Default;
            }
            else
            {
                ((Entry)sender).TextColor = Color.Red;
                ((Entry)sender).BackgroundColor = Color.Red;
            }

            bool Isvalid = false;
            Isvalid = !string.IsNullOrEmpty(e.NewTextValue);
            if (Isvalid == true)
            {
                ((Entry)sender).TextColor = Color.Default;
                ((Entry)sender).BackgroundColor = Color.Default;
            }
            else
            {
                ((Entry)sender).TextColor = Color.Red;
                ((Entry)sender).BackgroundColor = Color.Red;
            }
        }
    }
}
