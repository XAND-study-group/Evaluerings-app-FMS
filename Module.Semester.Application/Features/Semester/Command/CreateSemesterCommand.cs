﻿using MediatR;
using Module.Semester.Application.Abstractions;
using Module.Shared.Abstractions;
using SharedKernel.Dto.Features.Semester.Command;

namespace Module.Semester.Application.Features.Semester.Command;

public record CreateSemesterCommand(CreateSemesterRequest CreateSemesterRequest) : IRequest, ITransactionalCommand;

public class CreateSemesterCommandHandler : IRequestHandler<CreateSemesterCommand>
{
    private readonly ISemesterRepository _semesterRepository;

    public CreateSemesterCommandHandler(ISemesterRepository semesterRepository)
    {
        _semesterRepository = semesterRepository;
    }
    async Task IRequestHandler<CreateSemesterCommand>.Handle(CreateSemesterCommand request, CancellationToken cancellationToken)
    {
        // Load
        var otherSemesters = await _semesterRepository.GetAllSemesters();
        
        // Do
        var semester = Domain.Entities.Semester.Create(
            request.CreateSemesterRequest.Name, 
            request.CreateSemesterRequest.SemesterNumber,
            request.CreateSemesterRequest.StartDate,
            request.CreateSemesterRequest.EndDate,
            request.CreateSemesterRequest.School,
            otherSemesters);
        
        // Save
        await _semesterRepository.CreateSemesterAsync(semester);
    }
}