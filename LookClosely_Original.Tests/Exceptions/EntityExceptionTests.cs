
using Moq; 
using LookClosely_Original.Data.Models;
using LookClosely_Original.GCommon.Exceptions;
using LookClosely_Original.Services.Core;
using LookClosely_Original.Data.Repository.Contracts;
using Microsoft.AspNetCore.Identity;

namespace LookClosely_Original.Tests.Exceptions
{
    [TestFixture]
    public class EntityExceptionTests
    {
        [Test]
        public void EntityAlreadyExistsException_ShouldSetMessageCorrectly()
        {
            string errorMessage = "Entity already exists!";
            var exception = new EntityAlreadyExistsException(errorMessage);
            Assert.That(exception.Message, Is.EqualTo(errorMessage));
        }

        [Test]
        public void HintModel_ShouldSetPropertiesCorrectly()
        {
            var expectedId = 1;
            var expectedContent = "Test Hint Content";
            var expectedLevelId = 5;

            var hint = new Hint
            {
                Id = expectedId,
                Content = expectedContent,
                LevelId = expectedLevelId
            };

            Assert.Multiple(() =>
            {
                Assert.That(hint.Id, Is.EqualTo(expectedId));
                Assert.That(hint.Content, Is.EqualTo(expectedContent));
                Assert.That(hint.LevelId, Is.EqualTo(expectedLevelId));
            });
        }

        [Test]
        public async Task GetUserProfileAsync_ShouldThrowEntityNotFoundException_WhenUserDoesNotExist()
        {
            // Arrange
            var repositoryMock = new Mock<IUserRepository>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(
            store.Object, null!, null!, null!, null!, null!, null!, null!, null!);
            var userService = new UserService(userManagerMock.Object, repositoryMock.Object);

            var userId = "non-existent-id";
            repositoryMock.Setup(r => r.GetByIdWithScoresAsync(userId))
                          .ReturnsAsync((ApplicationUser?)null);

            // Act
            Func<Task> act = async () => await userService.GetUserProfileAsync(userId);

            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await act());
        }

        [Test]
        public void EntityEditPersistFailException_ShouldHaveCorrectMessage()
        {
            EntityEditPersistFailException? ex = new EntityEditPersistFailException();
            Assert.That(ex, Is.Not.Null);
        }
    }
}