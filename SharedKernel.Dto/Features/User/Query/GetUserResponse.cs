using SharedKernel.Dto.Features.Semester.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Dto.Features.User.Query;

public record GetUserResponse(
    Guid Id,
    string Firstname,
    string Lastname,
    string Email,
    IEnumerable<GetSemestersResponse> Semesters);