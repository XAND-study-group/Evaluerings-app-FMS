using Module.Semester.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Module.Semester.Domain.Test.Fakes
{
    public class FakeSubject : Semester.Domain.Entities.Subject
    {
        #region Constructors
        public FakeSubject(string name, string description)
        {
            SetProperty(nameof(Name), name);
            SetProperty(nameof(Description), description);
        }
        #endregion

        #region Subject Business Logic Methods

        public void SetName(string name)
        {
            SetProperty(nameof(Name), name);
        }

        public void SetDescription(string description)
        {
            SetProperty(nameof(Description), description);
        }

        private void SetProperty(string propertyName, object value)
        {
            var property = typeof(Subject).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (property != null)
            {
                property.SetValue(this, value);
            }
        }

        #endregion
    }
}
