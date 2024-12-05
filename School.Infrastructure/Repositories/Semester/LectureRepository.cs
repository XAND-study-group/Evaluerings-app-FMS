using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions.Semester;
using School.Domain.Entities;
using School.Infrastructure.DbContext;

namespace School.Infrastructure.Repositories.Semester;

public class LectureRepository(SchoolDbContext dbContext) : ILectureRepository
{
    async Task ILectureRepository.CreateLecture(Lecture lecture)
    {
        await dbContext.Lectures.AddAsync(lecture);
        await dbContext.SaveChangesAsync();
    }

    async Task<Domain.Entities.Semester> ILectureRepository.GetSemesterById(Guid semesterId)
        => await _dbContext.Semesters.FirstOrDefaultAsync(s => s.Id == semesterId) ??
           throw new ArgumentException("Semester ikke fundet");

    public async Task<bool> DoesUserHaveLecture(Guid lectureId, Guid userId)
        => (await dbContext.Classes
                .Include(c => c.Students)
                .Include(c => c.Subjects)
                .ThenInclude(s => s.Lectures)
                .FirstAsync(c => c.Subjects
                    .Any(s => s.Lectures
                        .Any(l => l.Id == lectureId))))
            .Students.Any(s => s.Id == userId);
}