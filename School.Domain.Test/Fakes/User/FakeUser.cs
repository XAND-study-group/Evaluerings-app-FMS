using School.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace School.Domain.Test.Fakes.User
{
    public class FakeUser : Domain.Entities.User
    {
        public FakeUser(string email)
        {
            Email = UserEmail.Create(email, []);
        }

        public FakeUser()
        {
        }

        public FakeUser(string firstname, string lastname, string email)
        {
        }

        public void SetRefreshToken(string token, DateTime expirationDate)
            => RefreshToken = RefreshToken.Create(token, expirationDate);

        public void SetUserFirstname(string firstname)
            => Firstname = firstname;

        public void SetUserLastname(string lastname)
            => Lastname = lastname;

        public void SetUserEmail(string email, IEnumerable<string> otherEmails)
            => Email = UserEmail.Create(email, otherEmails);

    }
}
