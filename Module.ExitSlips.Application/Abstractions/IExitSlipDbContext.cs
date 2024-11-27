using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ExitSlip.Application.Abstractions;

public interface IExitSlipDbContext
{
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers{ get; set; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

