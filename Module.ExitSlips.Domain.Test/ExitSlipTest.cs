using Module.ExitSlip.Domain.Test.Fakes;
using SharedKernel.Enums.Features.Evaluering.ExitSlip;
using Xunit;

namespace Module.ExitSlip.Domain.Test
{
    public class ExitSlipTests
    {
        #region Tests

        #region Create Tests
        [Theory]
        [MemberData(nameof(ValidExitSLipCreationData))]
        public void Given_Valid_Data_Then_Create_Success(Guid subjectId, Guid lectureId, string title, int maxQuetionCount, ExitSlipActiveStatus activeStatus)
        {
            // Act
            var exitSLip = Entities.ExitSlip.Create(subjectId, lectureId, title, maxQuetionCount, activeStatus);

            // Assert
            Assert.NotNull(exitSLip);
        }

        #endregion

        #region Update Title Tests

        [Theory]
        [MemberData(nameof(InvalidUpdateExitSLipActiveStatus))]
        public void Given_Valid_Title_While_Status_Active_Then_Throw_ArgumentException(FakeExitSlip exitSlipToUpdate, string title)
        {
            // Arrange
            var expectedStatus = exitSlipToUpdate.ActiveStatus;

            // Act & Assert

            Assert.Throws<ArgumentException>(() => exitSlipToUpdate.Update(title));
            Assert.Equal(exitSlipToUpdate.ActiveStatus, expectedStatus);
        }


        [Theory]
        [MemberData(nameof(InvalidUpdateExitSLipTitle))]
        public void Given_Invalid_Title_While_Status_Inactive_Then_Throw_ArgumentException(FakeExitSlip exitSlipToUpdate, string title)
        {
            // Arrange
            var expectedTitle = exitSlipToUpdate.Title;
            var expectedStatus = exitSlipToUpdate.ActiveStatus;

            // Act & Assert

            Assert.Throws<ArgumentException>(() => exitSlipToUpdate.Update(title));
            Assert.Equal(exitSlipToUpdate.Title, expectedTitle);
            Assert.Equal(exitSlipToUpdate.ActiveStatus, expectedStatus);
        }


        [Theory]
        [MemberData(nameof(ValidUpdateExitSlipData))]
        public void Given_Valid_Data_Then_Success(FakeExitSlip exitSlipToUpdate, string title)
        {
            // Act
            exitSlipToUpdate.Update(title);

            // Assert 
            Assert.Equal(title, exitSlipToUpdate.Title);
        }


        #endregion

        #region Update ActiveStatus Tests
        [Theory]
        [MemberData(nameof(ValidUpdateExitSlipActiveStatus))]

        public void Given_Valid_Data_Then_Update_ActiveStatus_Success(FakeExitSlip exitSlipToUpdate, ExitSlipActiveStatus activeStatus)
        {
            // Act  
            exitSlipToUpdate.UpdateActiveStatus(activeStatus);

            // Assert
            Assert.Equal(activeStatus, exitSlipToUpdate.ActiveStatus);
        }


        #endregion

        #region Delete Tests
        [Fact]
        public void Having_ActiveStatus_Then_Delete_Throw_ArgumentException()
        {
            // Arrange
            var exitSlipToDelete = new FakeExitSlip(ExitSlipActiveStatus.Active);

            // Assert
            Assert.Throws<ArgumentException>(exitSlipToDelete.Delete);
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
            string overHundredCharTitle = new string('X', 101);

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
        public void Given_One_Character_Title_Then_Void()
        {
            // Arrange 
            var exitSlip = new FakeExitSlip();

            // Act 
            exitSlip.SetTitle("V");
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
        public void Given_One_MaxQuestionCount_Then_Void()
        {
            // Arrange 
            var exitSlip = new FakeExitSlip();

            // Act & Assert
            exitSlip.SetMaxQuestionCount(1);
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

        #region Create
        public static IEnumerable<object[]> ValidExitSLipCreationData()
        {
            yield return [Guid.NewGuid(), Guid.NewGuid(), "Data", 5, ExitSlipActiveStatus.Inactive];
            yield return [Guid.NewGuid(), Guid.NewGuid(), "Math", 5, ExitSlipActiveStatus.Active];
        }

        #endregion

        #region Update Title

        public static IEnumerable<object[]> InvalidUpdateExitSLipActiveStatus()
        {
            yield return new object[]
            {
                new FakeExitSlip(ExitSlipActiveStatus.Active),
                "data"
            };
        }
        public static IEnumerable<object[]> InvalidUpdateExitSLipTitle()
        {
            yield return new object[]
            {
                new FakeExitSlip("Data" , ExitSlipActiveStatus.Inactive),
                ""
            };
            yield return new object[]

          {
                new FakeExitSlip("Data" , ExitSlipActiveStatus.Inactive),
                " "
          };
        }

        public static IEnumerable<object[]> ValidUpdateExitSlipData()
        {
            yield return new object[]
            {
                new FakeExitSlip("Data" , ExitSlipActiveStatus.Inactive),
                "Subject"
            };
            yield return new object[]

          {
                new FakeExitSlip("Data" , ExitSlipActiveStatus.Inactive),
                "Valid"
          };
        }

        #endregion

        #region Update ActiveStatus

        public static IEnumerable<object[]> ValidUpdateExitSlipActiveStatus()
        {
            yield return new object[]
            {
                new FakeExitSlip("Data" , ExitSlipActiveStatus.Inactive),
                ExitSlipActiveStatus.Active
            };
            yield return new object[]

          {
                new FakeExitSlip("Data" , ExitSlipActiveStatus.Active),
                ExitSlipActiveStatus.Inactive
          };
        }

        #endregion


        #endregion

    }
}
