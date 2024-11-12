using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.Enums.Features.Semester;
using SharedKernel.ValueObjects;

namespace Module.User.Domain.Entities
{
    public class Semester : Entity
    {
        public string Name { get; protected set; }
        public SemesterNumber SemesterNumber { get; protected set; }
        public EducationRange EducationRange { get; protected set; }
        public SchoolType School { get; protected set; }
        private readonly List<User> _semesterResponsibles = [];

        public IReadOnlyCollection<User> SemesterResponsibles => _semesterResponsibles;

        void thing()
        {
            
        }



    }
}
