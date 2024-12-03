using AutoMapper;
using School.Domain.Entities;
using School.Domain.ValueObjects;
using School.Infrastructure.Mapping;
using SharedKernel.Dto.Features.School.Class.Query;
using SharedKernel.Dto.Features.School.Lecture.Query;
using SharedKernel.Dto.Features.School.Semester.Query;
using SharedKernel.Dto.Features.School.Subject.Query;
using SharedKernel.Dto.Features.School.User.Query;
using SharedKernel.Enums.Features.Semester;
using System;
using System.Collections.Generic;
using Moq;
using School.Domain.DomainServices.Interfaces;
using School.Domain.Test.Fakes.Semester;
using SharedKernel.Enums.Features.Authentication;
using Xunit;
using Assert = Xunit.Assert;
using AutoMapper.Internal;
using System.Text;

namespace School.Domain.Test
{
    public class SchoolAutoMapperConfigurationIsValidTests
    {
        private readonly IMapper _mapper;

        public SchoolAutoMapperConfigurationIsValidTests()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfileSchool>(); });
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