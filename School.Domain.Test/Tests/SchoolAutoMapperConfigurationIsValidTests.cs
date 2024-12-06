using System.Text;
using AutoMapper;
using AutoMapper.Internal;
using School.Infrastructure.Mapper;
using Xunit;
using Assert = Xunit.Assert;

namespace School.Domain.Test.Tests;

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
            var a = _mapper.ConfigurationProvider as MapperConfiguration;
            foreach (var t in (_mapper.ConfigurationProvider as MapperConfiguration).Internal()
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