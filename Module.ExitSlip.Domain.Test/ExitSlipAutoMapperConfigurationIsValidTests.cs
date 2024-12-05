using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using Module.ExitSlip.Domain.Entities;
using Module.ExitSlip.Domain.Test.Fakes;
using Module.ExitSlip.Domain.ValueObjects;
using Module.ExitSlip.Infrastructure.Mapper;
using SharedKernel.Dto.Features.Evaluering.Answer.Query;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Enums.Features.Evaluering.ExitSlip;

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

        [Fact]
        public void AllAutoMapperProfilesIsValid()
        {
            //Arrange
            var actual = new StringBuilder();
            //Act
            TypeMap typeMap = null;
            try
            {
                var a = _mapper.ConfigurationProvider as AutoMapper.MapperConfiguration;
                foreach (var t in (_mapper.ConfigurationProvider as AutoMapper.MapperConfiguration).Internal()
                         .GetAllTypeMaps())
                {
                    typeMap = t;
                    _mapper.ConfigurationProvider.Internal().AssertConfigurationIsValid(t);
                }
            }
            catch (Exception e)
            {
                actual.AppendLine(typeMap?.Profile.Name);
                actual.AppendLine(e.Message);
            }
            //Assert
            Assert.True(actual.Length == 0, actual.ToString());
        }
    }
}