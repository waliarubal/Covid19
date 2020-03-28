using Covid19.Shared.Base;
using Xamarin.Forms;

namespace Covid19.Shared.Behaviors
{
    public class NumericValidationBehavior : BehaviorBase<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                var isValid = true;
                foreach(char character in args.NewTextValue)
                {
                    if (!char.IsDigit(character))
                    {
                        isValid = false;
                        break;
                    }
                }

                var entry = (Entry)sender;
                entry.Text = isValid ? args.NewTextValue : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
            }
        }
    }
}
