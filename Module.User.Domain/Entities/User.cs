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
        }

        #endregion

        #region User Methodes

        public static User Create(string firstname, string lastname, string email) =>
            new User(firstname, lastname, email);

        #endregion

        public void AddAccountClaim(AccountClaim claim)
        {
            _accountClaims.Add(claim);
        }
        
        // TODO: Assure user does not have claim

    }
}
