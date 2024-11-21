using SharedKernel.Enums.Features.Evaluering.ExitSlip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Dto.Features.Evaluering.ExitSlip.Command
{
    public record UpdateExitSlipRequest(
        Guid Id,
        byte[] RowVersion,
        string Title,
        ExitSlipActiveStatus ActiveStatus);
}
