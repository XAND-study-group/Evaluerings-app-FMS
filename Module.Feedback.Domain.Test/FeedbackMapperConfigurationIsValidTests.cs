using AutoMapper;
using Module.Feedback.Domain;
using Module.Feedback.Domain.DomainServices.Interfaces;
using Module.Feedback.Infrastructure.Mapper;
using SharedKernel.Dto.Features.Evaluering.Feedback.Query;
using SharedKernel.Dto.Features.Evaluering.Proxy;
using SharedKernel.Dto.Features.Evaluering.Room.Query;
using SharedKernel.Enums.Features.Evaluering.Feedback;
using SharedKernel.Enums.Features.Vote;
using SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Module.Feedback.Domain.Test.Fakes;
using Xunit;
using AutoMapper.Internal;
using System.Text;

namespace Module.Feedback.Domain.Test
{
    public class FeedbackAutoMapperConfigurationIsValidTests
    {
        private readonly IMapper _mapper;
        public FeedbackAutoMapperConfigurationIsValidTests()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfileFeedback>(); });
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