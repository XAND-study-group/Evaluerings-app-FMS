using MediatR;
using Module.Semester.Application.Abstractions;
using Module.Shared.Models;
using SharedKernel.Dto.Features.Lecture.Command;
using SharedKernel.Interfaces;

namespace Module.Semester.Application.Features.Lecture.Command;

public record CreateLectureCommand(CreateLectureRequest Request) : IRequest<Result<bool>>, ITransactionalCommand;

public class CreateLectureCommandHandler : IRequestHandler<CreateLectureCommand, Result<bool>>
{
    private readonly ILectureRepository _lectureRepository;

    public CreateLectureCommandHandler(ILectureRepository lectureRepository)
    {
        _lectureRepository = lectureRepository;
    }

    async Task<Result<bool>> IRequestHandler<CreateLectureCommand, Result<bool>>.Handle(CreateLectureCommand request,
        CancellationToken cancellationToken)
    {
        try
        {

            // Load
            var semester = await _lectureRepository.GetSemesterById(request.Request.SemesterId);
            var createLectureRequest = request.Request;

            // Do
            var lecture = semester.AddLectureToClass(
                createLectureRequest.LectureTitle,
                createLectureRequest.Description,
                createLectureRequest.StartTime,
                createLectureRequest.EndTime,
                createLectureRequest.Date,
                createLectureRequest.ClassRoom,
                createLectureRequest.ClassId, 
                createLectureRequest.SubjectId);
        
            // Save
            await _lectureRepository.CreateLecture(lecture);

            return Result<bool>.Create("Lektion oprettet", true, ResultStatus.Created);
        }
        catch (ArgumentException e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}