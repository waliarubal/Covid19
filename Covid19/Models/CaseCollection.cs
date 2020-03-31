using Covid19.Shared.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Covid19.Models
{
    public class CaseCollection: ModelBase
    {
        public CaseCollection()
        {
            Cases = new ObservableCollection<Case>();
        }

        public CaseCollection(IEnumerable<Case> cases): this()
        {
            AddRange(cases);
        }

        #region properties

        public ObservableCollection<Case> Cases 
        {
            get => Get<ObservableCollection<Case>>();
            private set => Set(value);
        }

        public long Confirmed
        {
            get => Get<long>();
            private set => Set(value);
        }

        public long Deaths
        {
            get => Get<long>();
            private set => Set(value);
        }

        public long Recovered
        {
            get => Get<long>();
            private set => Set(value);
        }

        public long Active
        {
            get => Get<long>();
            private set => Set(value);
        }

        #endregion

        public void AddRange(IEnumerable<Case> cases)
        {
            if (cases == null)
                return;

            foreach (var caseRecord in cases)
                Add(caseRecord);
        }

        public void Add(Case caseRecord)
        {
            Cases.Add(caseRecord);
            Confirmed += caseRecord.Confirmed;
            Deaths += caseRecord.Deaths;
            Recovered += caseRecord.Recovered;
            Active += caseRecord.Active;
        }

        public void Remove(Case caseRecord)
        {
            if (!Cases.Remove(caseRecord))
                return;

            Confirmed -= caseRecord.Confirmed;
            Deaths -= caseRecord.Deaths;
            Recovered -= caseRecord.Recovered;
            Active -= caseRecord.Active;
        }
    }
}
