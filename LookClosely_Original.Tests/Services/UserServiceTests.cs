using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Identity;
using LookClosely_Original.Services.Core;
using LookClosely_Original.Data.Repository.Contracts;
using LookClosely_Original.Data.Models;
using LookClosely_Original.GCommon.Exceptions;
using LookClosely_Original.LookCloselyViewModels.Event;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace LookClosely_Original.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> mockUserRepo;
        private Mock<UserManager<ApplicationUser>> mockUserManager;
        private UserService userService;

        [SetUp]
        public void SetUp()
        {
            mockUserRepo = new Mock<IUserRepository>();

            var store = new Mock<IUserStore<ApplicationUser>>();
            mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            userService = new UserService(mockUserManager.Object, mockUserRepo.Object);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "images", "avatars");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        [Test]
        public async Task GetUserProfileAsync_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var userId = "test-id";
            var expectedUser = new ApplicationUser { Id = userId, UserName = "Ivan" };
            mockUserRepo.Setup(r => r.GetByIdWithScoresAsync(userId)).ReturnsAsync(expectedUser);

            // Act
            var result = await userService.GetUserProfileAsync(userId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(userId));
        }

        [Test]
        public void GetUserProfileAsync_ThrowsException_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "wrong-id";
            mockUserRepo.Setup(r => r.GetByIdWithScoresAsync(userId))
                        .ReturnsAsync((ApplicationUser)null);

            // Act
            Func<Task> act = async () => await userService.GetUserProfileAsync(userId);

            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await act());
        }

        [Test]
        public async Task UpdateUserProfileAsync_UpdatesBio_WhenUserExists()
        {
            // Arrange
            var userId = "user-123";
            var user = new ApplicationUser { Id = userId, Bio = "Old Bio" };
            var model = new EditProfileViewModel { Bio = "New Updated Bio", AvatarFile = null };

            mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user);
            mockUserManager.Setup(m => m.UpdateAsync(It.IsAny<ApplicationUser>()))
                           .ReturnsAsync(IdentityResult.Success);

            // Act
            await userService.UpdateUserProfileAsync(userId, model);

            // Assert
            Assert.That(user.Bio, Is.EqualTo("New Updated Bio"));
            mockUserManager.Verify(m => m.UpdateAsync(user), Times.Once);
        }

        [Test]
        public void UpdateUserProfileAsync_ThrowsException_WhenUserNotFound()
        {
            // Arrange
            var userId = "non-existent";
            mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync((ApplicationUser)null);

            // Act
            Func<Task> act = async () => await userService.UpdateUserProfileAsync(userId, new EditProfileViewModel());

            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await act());
        }

        [Test]
        public void UpdateUserProfileAsync_ThrowsException_WhenUpdateFails()
        {
            // Arrange
            var userId = "user-123";
            var user = new ApplicationUser { Id = userId };
            var model = new EditProfileViewModel { Bio = "Bio" };

            mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user);
            mockUserManager.Setup(m => m.UpdateAsync(user))
                           .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

            // Act
            Func<Task> act = async () => await userService.UpdateUserProfileAsync(userId, model);

            // Assert
            Assert.ThrowsAsync<EntityEditPersistFailException>(async () => await act());
        }

        [Test]
        public async Task UpdateUserProfileAsync_UpdatesAvatar_WhenNewFileIsProvided()
        {
            // Arrange
            var userId = "user-123";
            var user = new ApplicationUser { Id = userId, AvatarPath = "old-photo.jpg" };

            var content = "fake-image-content";
            var fileName = "new-avatar.png";
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(content));
            var mockFile = new Mock<IFormFile>();
            mockFile.Setup(_ => _.OpenReadStream()).Returns(ms);
            mockFile.Setup(_ => _.FileName).Returns(fileName);
            mockFile.Setup(_ => _.Length).Returns(ms.Length);

            var model = new EditProfileViewModel
            {
                Bio = "Updated Bio",
                AvatarFile = mockFile.Object
            };

            mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user);
            mockUserManager.Setup(m => m.UpdateAsync(It.IsAny<ApplicationUser>()))
                           .ReturnsAsync(IdentityResult.Success);

            // Act
            await userService.UpdateUserProfileAsync(userId, model);

            // Assert
            Assert.That(user.AvatarPath, Does.Contain(".png"));
            Assert.That(user.AvatarPath, Is.Not.EqualTo("old-photo.jpg"));
            mockUserManager.Verify(m => m.UpdateAsync(user), Times.Once);
        }
    }
}