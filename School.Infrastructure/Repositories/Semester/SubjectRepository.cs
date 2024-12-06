using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions.Semester;
using School.Infrastructure.DbContext;

namespace School.Infrastructure.Repositories.Semester;

public class SubjectRepository(SchoolDbContext dbContext) : ISubjectRepository
{
    public async Task<bool> DoesUserHaveSubject(Guid subjectId, Guid userId)
    {
        return (await dbContext.Classes.Include(@class => @class.Students)
            .FirstAsync(c => c.Subjects.Any(s => s.Id == subjectId))).Students.Any(s => s.Id == userId);
    }
}