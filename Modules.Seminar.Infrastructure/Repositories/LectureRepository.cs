using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Abstractions;
using Module.Semester.Domain.Entities;
using Module.Semester.Infrastructure.DbContexts;

namespace Module.Semester.Infrastructure.Repositories;

public class LectureRepository : ILectureRepository
{
    private readonly SemesterDbContext _dbContext;

    public LectureRepository(SemesterDbContext dbContext)
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