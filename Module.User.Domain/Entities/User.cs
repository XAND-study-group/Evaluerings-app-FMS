﻿using Module.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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



    }
}
