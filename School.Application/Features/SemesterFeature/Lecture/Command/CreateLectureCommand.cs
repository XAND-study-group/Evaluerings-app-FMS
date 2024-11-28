using MediatR;
using School.Application.Abstractions.Semester;
using SharedKernel.Dto.Features.School.Lecture.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace School.Application.Features.SemesterFeature.Lecture.Command;

public record CreateLectureCommand(CreateLectureRequest Request) : IRequest<Result<bool>>, ITransactionalCommand;

public class CreateLectureCommandHandler : IRequestHandler<CreateLectureCommand, Result<bool>>, ITransactionalCommand
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