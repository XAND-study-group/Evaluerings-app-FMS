using SharedKernel.Models;
using SharedKernel.ValueObjects;

namespace School.Domain.Entities
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
        public RefreshToken? RefreshToken { get; set; }
        
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
            if (AccountClaims.Contains(claim))
                throw new ArgumentException("Brugeren har allerede denne rettighed");

            _accountClaims.Add(claim);
        }

        public void SetRefreshToken(string token, int days)
        {
            RefreshToken = RefreshToken;
        }

        #region User BusinessLogic Methodes

        protected void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Firstname cannot be empty or null.", nameof(name));        // her er jeg ikke helt sikker på hvorfor der skal tilføjes "nameof"

            if (name.Length > 100)
                throw new ArgumentException("Firstname cannot exceed 50 characters.", nameof(name));
        }
        
        #endregion
        
    }
}
