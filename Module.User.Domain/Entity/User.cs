using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User.Domain.Entity
{
    public class User
    {
        public Guid Id { get; protected set; }

        public string Firstname { get; protected set; }
        public string Lastname { get; protected set; }
        public string Email { get; protected set; }

        public IEnumerable<Seminar> Seminars { get; protected set; }


        protected User()
        {
        }


        private User(string firstname, string lastname, string email)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
        }



        // Hvorfor er denne metode lavet som en Lambda? 
        public static User Create(string firstname, string lastname, string email)=>        
            new User(firstname, lastname, email);
        

        public void Update(string firstname, string lastname, string email)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
        }


    }
}
