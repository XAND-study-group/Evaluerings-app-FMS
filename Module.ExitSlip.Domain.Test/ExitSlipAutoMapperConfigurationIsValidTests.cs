using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Module.ExitSlip.Domain.Entities;
using Module.ExitSlip.Infrastructure.Mapper;
using SharedKernel.Dto.Features.Evaluering.Answer.Query;

namespace Module.ExitSlip.Domain.Test
{
    public class ExitSlipAutoMapperConfigurationIsValidTests
    {
        private readonly IMapper _mapper;

        public ExitSlipAutoMapperConfigurationIsValidTests()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfileExitSlip>(); });

            _mapper = config.CreateMapper();

        }

        [Theory]
        [InlineData()]
        [InlineData()]
        public void MapsAnswerToDestinationCorrectly(string expectedText)
        {
            // Arrange
            var source = new Answer { Name = expectedText };

            // Act
            var destination = _mapper.Map<GetSimpleAnswerResponse>(source);

            // Assert
            Assert.Equal(expectedText, destination.FullName);
        }
    }
}