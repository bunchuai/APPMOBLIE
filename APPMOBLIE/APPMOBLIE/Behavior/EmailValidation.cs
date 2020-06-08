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

    class NumberOnlyValidation : Behavior<Entry>
    {
        public static readonly BindablePropertyKey bindablePropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(NumberOnlyValidation), false);
        public static readonly BindableProperty bindableProperty = bindablePropertyKey.BindableProperty;

        public bool IsValid
        {
            get
            {
                return (bool)this.GetValue(bindableProperty);
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
            this.IsValid = (Regex.IsMatch(e.NewTextValue, @"[\d]{1,4}([.,][\d]{1,2})?"));
            entry.TextColor = this.IsValid ? Color.Default : Color.Red;
        }
    }

    class NumberOnlyValidation2 : Behavior<Entry>
    {
        public static readonly BindablePropertyKey bindablePropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(NumberOnlyValidation2), false);
        public static readonly BindableProperty bindableProperty = bindablePropertyKey.BindableProperty;

        public bool IsValid
        {
            get
            {
                return (bool)this.GetValue(bindableProperty);
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
            this.IsValid = (Regex.IsMatch(e.NewTextValue, @"[\d]{1,4}([.,][\d]{1,2})?"));
            entry.TextColor = this.IsValid ? Color.Default : Color.Red;
        }
    }

    class EmptyValidation : Behavior<Entry>
    {
        public static readonly BindablePropertyKey bindablePropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmptyValidation), false);
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
    class EmptyValidation2 : Behavior<Entry>
    {
        public static readonly BindablePropertyKey bindablePropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmptyValidation2), false);
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
    class EmptyValidation3 : Behavior<Entry>
    {
        public static readonly BindablePropertyKey bindablePropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmptyValidation3), false);
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
    class EmptyValidation4 : Behavior<Entry>
    {
        public static readonly BindablePropertyKey bindablePropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmptyValidation4), false);
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
    class EmptyValidation5 : Behavior<Entry>
    {
        public static readonly BindablePropertyKey bindablePropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmptyValidation5), false);
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
