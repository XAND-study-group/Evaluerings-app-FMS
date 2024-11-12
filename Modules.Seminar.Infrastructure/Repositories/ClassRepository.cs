﻿using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Abstractions;
using Module.Semester.Domain.Entities;
using Module.Semester.Infrastructure.DbContexts;

namespace Module.Semester.Infrastructure.Repositories;

public class ClassRepository : IClassRepository
{
    private readonly SemesterDbContext _semesterDbContext;

    public ClassRepository(SemesterDbContext semesterDbContext)
    {
        _semesterDbContext = semesterDbContext;
    }

    #region Class

    async Task IClassRepository.CreateClassAsync(Class newClass)
    {
        await _semesterDbContext.Classes.AddAsync(newClass);
        await _semesterDbContext.SaveChangesAsync();
    }

    async Task<IEnumerable<Class>> IClassRepository.GetAllClassesAsync()
        => await _semesterDbContext.Classes.ToListAsync();

    async Task<Class> IClassRepository.GetClassByIdAsync(Guid classId)
        => await _semesterDbContext.Classes.SingleAsync(c => c.Id == classId);

    async Task<User> IClassRepository.GetUserByIdAsync(Guid studentId)
        => await _semesterDbContext.Users.SingleAsync(u => u.Id == studentId);

    async Task IClassRepository.AddUserToClassAsync()
    {
        await _semesterDbContext.SaveChangesAsync();
    }

    public async Task<Domain.Entities.Semester> GetSemesterById(Guid semesterId)
        => await _semesterDbContext.Semesters.SingleAsync(s => s.Id == semesterId);

    #endregion
}