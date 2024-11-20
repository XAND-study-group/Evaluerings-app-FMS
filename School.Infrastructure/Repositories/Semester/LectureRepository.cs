using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions.Semester;
using School.Domain.Entities;
using School.Infrastructure.DbContext;

namespace School.Infrastructure.Repositories.Semester;

public class LectureRepository : ILectureRepository
{
    private readonly SchoolDbContext _dbContext;

    public LectureRepository(SchoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    async Task ILectureRepository.CreateLecture(Lecture lecture)
    {
        await _dbContext.Lectures.AddAsync(lecture);
        await _dbContext.SaveChangesAsync();
    }

    async Task<Domain.Entities.Semester> ILectureRepository.GetSemesterById(Guid semesterId)
        => await _dbContext.Semesters.SingleAsync(s => s.Id == semesterId);
}