﻿using Module.User.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User.Application.Features.UserManagement.Query.Dto;

public record USerFullResponse(
    Guid Id,
    string Firstname,
    string Lastname,
    string Email,
    List<Semester> Classes);   
