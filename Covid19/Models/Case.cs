using Covid19.Shared.Base;
using System;

namespace Covid19.Models
{
    public class Case: ModelBase
    {
        public string Country 
        {
            get => Get<string>();
            set => Set(value); 
        }

        public DateTime LastUpdate
        {
            get => Get<DateTime>();
            set => Set(value);
        }

        public string Latitude
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Longitude
        {
            get => Get<string>();
            set => Set(value);
        }

        public long Confirmed
        {
            get => Get<long>();
            set => Set(value);
        }

        public long Deaths
        {
            get => Get<long>();
            set => Set(value);
        }

        public long Recovered
        {
            get => Get<long>();
            set => Set(value);
        }

        public long Active
        {
            get => Get<long>();
            set => Set(value);
        }

        public override string ToString()
        {
            return $"{Country} - {Confirmed}";
        }
    }
}
