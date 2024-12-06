using System.Text;
using AutoMapper;
using AutoMapper.Internal;

namespace Module.Feedback.Domain.Test;

public class FeedbackAutoMapperConfigurationIsValidTests(IMapper mapper)
{
    [Fact]
    public void AllAutoMapperProfilesIsValid()
    {
        //Arrange
        var actual = new StringBuilder();

        //Act
        TypeMap? typeMap = null;
        try
        {
            var a = mapper.ConfigurationProvider as MapperConfiguration;
            foreach (var t in (mapper.ConfigurationProvider as MapperConfiguration).Internal()
                     .GetAllTypeMaps())
            {
                typeMap = t;
                mapper.ConfigurationProvider.Internal().AssertConfigurationIsValid(t);
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