using Microsoft.EntityFrameworkCore;
using Module.Semester.Domain.Entity;

namespace Module.Semester.Application.Abstractions;

public interface ISemesterDbContext
{
    public DbSet<Semester.Domain.Entity.Class> Classes { get; set; }
    public DbSet<User> Users { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}