using MediatR;
using Module.Semester.Application.Abstractions;
using Module.Shared.Abstractions;
using SharedKernel.Dto.Features.Lecture.Command;

namespace Module.Semester.Application.Features.Lecture.Command;

public record CreateLectureCommand(CreateLectureRequest Request) : IRequest, ITransactionalCommand;

public class CreateLectureCommandHandler : IRequestHandler<CreateLectureCommand>
{
    private readonly ILectureRepository _lectureRepository;

    public CreateLectureCommandHandler(ILectureRepository lectureRepository)
    {
        _lectureRepository = lectureRepository;
    }

    async Task IRequestHandler<CreateLectureCommand>.Handle(CreateLectureCommand request,
        CancellationToken cancellationToken)
    {
        // Load
        var semester = await _lectureRepository.GetSemesterById(request.Request.SemesterId);

        // Do
        var lecture = semester.AddLectureToClass(
            request.Request.LectureTitle,
            request.Request.Description,
            request.Request.StartTime,
            request.Request.EndTime,
            request.Request.Date,
            request.Request.ClassRoom,
            request.Request.ClassId, 
            request.Request.SubjectId);
        
        // Save
        await _lectureRepository.CreateLecture(lecture);
    }
}