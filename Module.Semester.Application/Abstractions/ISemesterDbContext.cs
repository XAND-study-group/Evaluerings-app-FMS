using Microsoft.EntityFrameworkCore;
using Module.Semester.Domain.Entities;

namespace Module.Semester.Application.Abstractions;

public interface ISemesterDbContext
{
    public DbSet<Class> Classes { get; set; }
    public DbSet<User> Users { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}