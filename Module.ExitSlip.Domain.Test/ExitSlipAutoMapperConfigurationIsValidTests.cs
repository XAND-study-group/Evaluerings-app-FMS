using System.Text;
using AutoMapper;
using AutoMapper.Internal;
using Module.ExitSlip.Infrastructure.Mapper;
using Xunit;

namespace Module.ExitSlip.Domain.Test;

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