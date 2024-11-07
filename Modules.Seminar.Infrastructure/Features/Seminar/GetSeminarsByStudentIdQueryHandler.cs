using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Seminar.Application.Features.Seminar.Query;
using Module.Seminar.Application.Features.Seminar.Query.Dto;
using Module.Seminar.Infrastructure.DbContexts;

namespace Module.Seminar.Infrastructure.Features.Seminar;

public class GetSeminarsByStudentIdQueryHandler : IRequestHandler<GetSeminarsByStudentIdQuery, IEnumerable<GetSeminarsResponse>>
{
    private readonly SeminarDbContext _seminarDbContext;
    private readonly IMapper _mapper;

    public GetSeminarsByStudentIdQueryHandler(SeminarDbContext seminarDbContext)
    {
        _seminarDbContext = seminarDbContext;
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Module.Seminar.Domain.Entity.Seminar, GetSeminarsResponse>();
        }).CreateMapper();
    }


    async Task<IEnumerable<GetSeminarsResponse>> IRequestHandler<GetSeminarsByStudentIdQuery,
        IEnumerable<GetSeminarsResponse>>.Handle(GetSeminarsByStudentIdQuery request,
        CancellationToken cancellationToken)
        => await _seminarDbContext.Seminars
            .Where(s => s.Students.Any(st => st.Id == request.StudentId))
            .ProjectTo<GetSeminarsResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
}