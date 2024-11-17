using Module.User.Domain.Entities;
using SharedKernel.ValueObjects;
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
        public UserFirstname Firstname { get; protected set; }
        public UserLastname Lastname { get; protected set; }
        public UserEmail Email { get; protected set; }
        public IEnumerable<Semester> Semesters { get; protected set; }
        private List<AccountClaim> _accountClaims = [];
        public IReadOnlyCollection<AccountClaim> AccountClaims => _accountClaims;
        
        #endregion

        #region Constructors
        protected User()
        {
        }

        private User(string fristname, string lastname, string email, IEnumerable<User> otherUsers)
        {
            var otherUsersEmails = otherUsers.Select(e => e.Email.Value);

            Firstname = UserFirstname.Create(fristname);
            Lastname = UserLastname.Create(lastname);
            Email = UserEmail.Create(email, otherUsersEmails);         
        }

        #endregion

        #region User Methodes

        public static User Create(string firstname, string lastname, string email, IEnumerable<User> otherUsers) =>
            new User(firstname, lastname, email, otherUsers);

        #endregion

        public void AddAccountClaim(AccountClaim claim)
        {
            // TODO: Assure user does not already have claim

            _accountClaims.Add(claim);
        }
        
    }
}
