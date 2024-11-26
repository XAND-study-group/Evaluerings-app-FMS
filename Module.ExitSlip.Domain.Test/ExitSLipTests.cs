using Module.ExitSlip.Domain.Test.Fakes;
using SharedKernel.Enums.Features.Evaluering.ExitSlip;

namespace Module.ExitSlip.Domain.Test
{
    public class ExitSLipTests
    {
        #region Tests

        #region Create Tests
        [Theory]
        [MemberData(nameof(ValidExitSLipCreation))]
        public void Given_Valid_Data_Then_Create_Success(Guid subjectId, Guid lectureId, string title, int maxQuetionCount, ExitSlipActiveStatus activeStatus)
        {
            // Act
            var exitSLip = Entities.ExitSlip.Create(subjectId, lectureId, title, maxQuetionCount, activeStatus);

            // Assert
            Assert.NotNull(exitSLip);
        }


        #endregion



        #region Title Tests
        [Fact]
        public void Given_Null_Title_Then_Throw_ArgumentException()
        {
            // Arrange 
            var exitSlip = new FakeExitSlip();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => exitSlip.SetTitle(null!));
        }

        [Fact]
        public void Given_Empty_Title_Then_Throw_ArgumentException()
        {
            // Arrange
            var exitSlip = new FakeExitSlip();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => exitSlip.SetTitle(string.Empty));
        }

        [Fact]
        public void Given_WhiteSpace_Title_Then_Throw_ArgumentException()
        {
            // Arrange
            var exitSlip = new FakeExitSlip();

            // Act & Assert 
            Assert.Throws<ArgumentException>(() => exitSlip.SetTitle(" "));
        }
        [Fact]
        public void Given_Over_Hundred_Characters_Then_Throw_ArgumentException()
        {
            // Arrange
            var exitSlip = new FakeExitSlip();
            string overHundredCharTitle = new string('X', 200);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => exitSlip.SetTitle(overHundredCharTitle));
        }
        [Fact]
        public void Given_A_Hundred_Characters_Then_Void()
        {
            // Arrange
            var exitSlip = new FakeExitSlip();
            string hundredCharTitle = new string('A', 100);

            // Act & Assert
            exitSlip.SetTitle(hundredCharTitle);
        }
        [Fact]
        public void Given_Valid_Title_Then_Void()
        {
            // Arrange 
            var exitSlip = new FakeExitSlip();

            // Act 
            exitSlip.SetTitle("ValidTitle");
        }

        #endregion


        #region MaxQuestionCount Tests

        [Fact]
        public void Given_Negative_MaxQuestionCount_Then_Throws_ArgumentException()
        {
            // Arrange 
            var exitSlip = new FakeExitSlip();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => exitSlip.SetMaxQuestionCount(-1));
        }

        [Fact]
        public void Given_Zero_Then_Throws_ArgumentException()
        {
            // Arrange 
            var exitSlip = new FakeExitSlip();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => exitSlip.SetMaxQuestionCount(0));
        }
        [Fact]
        public void Given_Over_Ten_MaxQuestionCount_Then_Throws_ArgumentException()
        {
            // Arrange
            var exitSlip = new FakeExitSlip();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => exitSlip.SetMaxQuestionCount(12));
        }
        [Fact]
        public void Given_Ten_MaxQuestionCount_Then_Void()
        {
            // Arrange 
            var exitSlip = new FakeExitSlip();

            // Act & Assert
            exitSlip.SetMaxQuestionCount(10);
        }
        [Fact]
        public void Given_Valid_MaxQuestionCount_Then_Void()
        {
            // Arrange 
            var exitSlip = new FakeExitSlip();

            // Act 
            exitSlip.SetMaxQuestionCount(3);
        }
        #endregion



        #endregion

        #region MemberData Methodes

        public static IEnumerable<object[]> ValidExitSLipCreation()
        {
            yield return [Guid.NewGuid(), Guid.NewGuid(), "Data", 5, ExitSlipActiveStatus.Inactive];
            yield return [Guid.NewGuid(), Guid.NewGuid(), "Math", 5, ExitSlipActiveStatus.Active];
        }






        #endregion

    }
}