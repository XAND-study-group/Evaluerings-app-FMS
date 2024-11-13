using Module.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Module.User.Domain.Entities
{
    public class User : Entity
    {
        #region Properties

        public string Firstname { get; protected set; }
        public string Lastname { get; protected set; }
        public string Email { get; protected set; }
        public IEnumerable<Semester> Semesters { get; protected set; }
        private List<AccountClaim> _accountClaims = [];
        public IReadOnlyCollection<AccountClaim> AccountClaims => _accountClaims;
        
        #endregion

        #region Constructors
        protected User()
        {
        }

        private User(string fristname, string lastname, string email)
        {
            Firstname = fristname;
            Lastname = lastname;
            Email = email;

            ValidateName(Firstname);
            ValidateName(Lastname);
            ValidateEmail(Email);
        }

        #endregion

        #region User Methodes

        public static User Create(string firstname, string lastname, string email) =>
            new User(firstname, lastname, email);

        #endregion

        public void AddAccountClaim(AccountClaim claim)
        {
            // TODO: Assure user does not already have claim

            _accountClaims.Add(claim);
        }


        #region User BusinessLogic Methodes

        protected void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Firstname cannot be empty or null.", nameof(name));        // her er jeg ikke helt sikker på hvorfor der skal tilføjes "nameof"

            if (name.Length > 100)
                throw new ArgumentException("Firstname cannot exceed 50 characters.", nameof(name));
        }
        
        protected void ValidateEmail(string email)
        {
            var regexItem = new Regex (@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!regexItem.IsMatch(email))
                throw new ArgumentException("Invalid email format.", nameof(email));
        }
        
        #endregion

    }
}
