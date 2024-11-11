using Microsoft.EntityFrameworkCore;
using Module.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User.Application.Abstractions
{
    public interface IUserDbContext
    {
        public DbSet<Domain.Entities.Semester> Semesters { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
