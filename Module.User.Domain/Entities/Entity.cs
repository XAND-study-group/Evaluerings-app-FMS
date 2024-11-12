using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User.Domain.Entities
{
    public abstract class Entity
    {
        // Database Properties
        public Guid Id { get; protected set; }
        public byte[] RowVersion { get; protected set; }

        public bool Equals(Entity? other)
        {
            return other?.Id == Id;
        }

    }
}
