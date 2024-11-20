using Microsoft.EntityFrameworkCore;
using Module.Semester.Domain.Entities;

namespace Module.Semester.Application.Abstractions;

public interface ISemesterDbContext
{
    public DbSet<Class> Classes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Domain.Entities.Semester> Semesters { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Lecture> Lectures { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}