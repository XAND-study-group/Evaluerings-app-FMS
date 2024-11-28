using SharedKernel.Enums.Features.Evaluering.ExitSlip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Dto.Features.Evaluering.ExitSlip.Command
{
    public record UpdateExitSlipActiveStatusRequest(
        Guid Id,
        byte[] RowVersion,
        ExitSlipActiveStatus ActiveStatus
        );

}
