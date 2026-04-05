using Moq;
using LookClosely_Original.Services.Core;
using LookClosely_Original.Data.Repository.Contracts;
using LookClosely_Original.Data.Models;
using LookClosely_Original.Services.Core.Interfaces;
using LookClosely_Original.ViewModels;
using LookClosely_Original.GCommon.Exceptions;
using System.Linq.Expressions;

namespace LookClosely_Original.Tests.Services
{
    [TestFixture]
    public class LevelServiceTests
    {
        private Mock<ILevelRepository> mockRepo;
        private Mock<IScoreService> mockScore;
        private LevelService levelService;

        [SetUp]
        public void SetUp()
        {
            mockRepo = new Mock<ILevelRepository>();
            mockScore = new Mock<IScoreService>();
            levelService = new LevelService(mockRepo.Object, mockScore.Object);
        }

        [Test]
        public async Task GetLevel_ReturnsCorrectLevel_WhenLevelExists()
        {
            // Arrange
            int levelId = 1;
            Level expectedLevel = new Level { Id = levelId, Name = "Hidden Forest" };

            mockRepo.Setup(r => r.GetLevelByIdAsync(levelId)).ReturnsAsync(expectedLevel);

            // Act
            LevelViewModel? result = await levelService.GetLevelByIdAsync(levelId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(levelId));
        }

        [Test]
        public void GetLevelById_ThrowsException_WhenLevelDoesNotExist()
        {
            // Arrange
            int levelId = 999;
            mockRepo.Setup(r => r.GetLevelByIdAsync(levelId)).ReturnsAsync((Level?)null);

            // Act
            AsyncTestDelegate action = async () => await levelService.GetLevelByIdAsync(levelId);

            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(action);
        }

        [Test]
        public async Task CheckClick_ReturnsTrue_AndAddsScore_WhenClickIsInsideRadius()
        {
            // Arrange
            int levelId = 1;
            string userId = "test-user";
            Level? level = new Level
            {
                Id = levelId,
                TargetX = 100,
                TargetY = 100,
                TargetRadius = 10,
                IsDeleted = false
            };

            mockRepo.Setup(r => r.GetLevelByIdAsync(levelId)).ReturnsAsync(level);

            // Act
            bool result = await levelService.CheckClickAsync(levelId, 105, 100, 15, userId);

            // Assert
            Assert.That(result, Is.True);
            mockScore.Verify(s => s.AddScoreAsync(levelId, userId, 100, 15), Times.Once);
        }

        [Test]
        public void CheckClick_ThrowsException_WhenTimeIsNegative()
        {
            // Arrange
            int levelId = 1;
            Level? level = new Level { Id = levelId, IsDeleted = false };
            mockRepo.Setup(r => r.GetLevelByIdAsync(levelId)).ReturnsAsync(level);

            // Act
            AsyncTestDelegate action = async () => await levelService.CheckClickAsync(levelId, 10, 10, -5, "user123");

            // Assert
            Assert.ThrowsAsync<EntityInputDataException>(action);

        }

        [Test]
        public void CreateLevel_ThrowsException_WhenLevelNameAlreadyExists()
        {
            // Arrange
            LevelViewModel model = new LevelViewModel { Name = "Existing Forest" };
            List<Level> existingLevels = new List<Level> { new Level { Name = "Existing Forest" } };

            mockRepo.Setup(r => r.GetAllLevelsAsync(It.IsAny<Expression<Func<Level, bool>>>(), null))
           .ReturnsAsync(existingLevels);

            // Act
            AsyncTestDelegate action = async () => await levelService.CreateLevelAsync(model);

            // Assert
            Assert.ThrowsAsync<EntityInputDataException>(action);
        }

        [Test]
        public async Task GetAllLevels_ReturnsMappedAndOrderedViewModels()
        {
            // Arrange 
            List<Level> levels = new List<Level>
    {
        new Level { Name = "B", Difficulty = "2", IsDeleted = false, TargetObjectName = "Obj2" },
        new Level { Name = "A", Difficulty = "1", IsDeleted = false, TargetObjectName = "Obj1" }
    };

            mockRepo.Setup(r => r.GetAllLevelsAsync(
                It.IsAny<Expression<Func<Level, bool>>>(),
                null))
                .ReturnsAsync(levels);

            // Act
            List<LevelViewModel> result = (await levelService.GetAllLevelsAsync()).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("A"));
            Assert.That(result[1].Name, Is.EqualTo("B"));
            Assert.That(result[0].Difficulty, Is.EqualTo("1"));
        }

        [Test]
        public async Task EditLevel_UpdatesPropertiesAndSaves_WhenLevelExists()
        {
            // Arrange
            LevelViewModel model = new LevelViewModel { Id = 1, Name = "Updated Name", Difficulty = "3" };
            Level existingLevel = new Level { Id = 1, Name = "Old Name", Difficulty = "1" };

            mockRepo.Setup(r => r.GetLevelByIdAsync(model.Id)).ReturnsAsync(existingLevel);

            // Act
            await levelService.EditLevelAsync(model, "user123");

            // Assert
            Assert.That(existingLevel.Name, Is.EqualTo("Updated Name"));
            Assert.That(existingLevel.Difficulty, Is.EqualTo("3"));
            mockRepo.Verify(r => r.SaveChangeAsync(), Times.Once);
        }

        [Test]
        public async Task DeleteLevel_SetsIsDeletedToTrueAndSaves_WhenLevelExists()
        {
            // Arrange
            int levelId = 1;
            Level? level = new Level { Id = levelId, IsDeleted = false };
            mockRepo.Setup(r => r.GetLevelByIdAsync(levelId)).ReturnsAsync(level);

            // Act
            await levelService.DeleteLevelAsync(levelId);

            // Assert
            Assert.That(level.IsDeleted, Is.True);
 
            mockRepo.Verify(r => r.SaveChangeAsync(), Times.Once);
        }

        [Test]
        public async Task CheckClick_ThrowsEntityInputDataException_WhenTimeIsNegative()
        {
            // Arrange
            int levelId = 1;
            Level level = new Level { Id = levelId, IsDeleted = false };
            mockRepo.Setup(r => r.GetLevelByIdAsync(levelId)).ReturnsAsync(level);

            // Act
            AsyncTestDelegate action = async () => await levelService.CheckClickAsync(levelId, 100, 100, -1, "user123");

            // Assert
            var exception = Assert.ThrowsAsync<EntityInputDataException>(action)!;
            Assert.That(exception.Message, Is.EqualTo(LookClosely_Original.GCommon.Exceptions.ErrorMessages.InvalidTime));
        }

        [Test]
        public void CheckClick_ThrowsEntityNotFoundException_WhenLevelDoesNotExist()
        {
            // Arrange
            int levelId = 999;
            mockRepo.Setup(r => r.GetLevelByIdAsync(levelId)).ReturnsAsync((Level?)null);

            // Act
            AsyncTestDelegate action = async () => await levelService.CheckClickAsync(levelId, 100, 100, 10, "user123");

            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(action);
        }
    }
}