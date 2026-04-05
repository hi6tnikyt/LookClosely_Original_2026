using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LookClosely_Original.Services.Core;
using LookClosely_Original.Data.Repository.Contracts;
using LookClosely_Original.Data.Models;
using LookClosely_Original.GCommon.Exceptions;
using MockQueryable.Moq;
using MockQueryable.EntityFrameworkCore;
using MockQueryable;

namespace LookClosely_Original.Tests.Services
{
    [TestFixture]
    public class ScoreServiceTests
    {
        private Mock<IScoreRepository> mockRepo;
        private ScoreService scoreService;

        [SetUp]
        public void SetUp()
        {
            mockRepo = new Mock<IScoreRepository>();
            scoreService = new ScoreService(mockRepo.Object);
        }

        [Test]
        public async Task AddScoreAsync_ThrowsException_WhenPointsAreNegative()
        {
            // Arrange  
            int levelId = 1;
            string userId = "user1";
            int invalidPoints = -10;
            int time = 5;

            // Act & Assert
            Assert.ThrowsAsync<EntityInputDataException>(() =>
                scoreService.AddScoreAsync(levelId, userId, invalidPoints, time));
        }

        [Test]
        public async Task GetScoresCountAsync_ReturnsCorrectCount()
        {
            // Arrange  
            var scores = new List<Score>
            {
                new Score { Id = 1, Points = 100, UserId = "user1" },
                new Score { Id = 2, Points = 200, UserId = "user2" }
            };

            var mockQuery = MockQueryableExtensions.BuildMock<Score>(scores);
            mockRepo.Setup(r => r.GetAllScoresQuery()).Returns(mockQuery);

            // Act  
            var result = await scoreService.GetScoresCountAsync();

            // Assert  
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public async Task GetTopScoresAsync_ReturnsLimitedResults()
        {
            // Arrange  
            var data = new List<Score>
            {
                new Score { Points = 100, TimeInSeconds = 10, Level = new Level { Name = "L1" }, User = new ApplicationUser { UserName = "U1" } },
                new Score { Points = 50, TimeInSeconds = 20, Level = new Level { Name = "L1" }, User = new ApplicationUser { UserName = "U2" } }
            };

            var mockQuery = MockQueryableExtensions.BuildMock<Score>(data);
            mockRepo.Setup(r => r.GetAllScoresQuery()).Returns(mockQuery);

            // Act  
            var result = await scoreService.GetTopScoresAsync(1);

            // Assert  
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Points, Is.EqualTo(100));
        }

        [Test]
        public async Task GetPagedLeaderboardAsync_WithSearchTerm_ReturnsFilteredResults()
        {
            // Arrange
            var scores = new List<Score>
            {
                new Score { Points = 100, User = new ApplicationUser { UserName = "Ivan" }, Level = new Level { Name = "Easy" } },
                new Score { Points = 200, User = new ApplicationUser { UserName = "Maria" }, Level = new Level { Name = "Easy" } },
                new Score { Points = 150, User = new ApplicationUser { UserName = "Ivana" }, Level = new Level { Name = "Hard" } }
            };

            var mockQuery = MockQueryableExtensions.BuildMock<Score>(scores);
            mockRepo.Setup(r => r.GetAllScoresQuery()).Returns(mockQuery);

            // Act
            var result = await scoreService.GetPagedLeaderboardAsync(page: 1, pageSize: 10, searchTerm: "Ivan");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.All(s => s.UserName.Contains("Ivan")), Is.True);
        }

        [Test]
        public async Task GetTopScoresAsync_ReturnsScoresOrderedByPointsThenByTime()
        {
            // Arrange
            var scores = new List<Score>
            {
                new Score { Points = 100, TimeInSeconds = 50, User = new ApplicationUser { UserName = "U1" }, Level = new Level { Name = "L1" } },
                new Score { Points = 200, TimeInSeconds = 30, User = new ApplicationUser { UserName = "U2" }, Level = new Level { Name = "L1" } },
                new Score { Points = 100, TimeInSeconds = 40, User = new ApplicationUser { UserName = "U3" }, Level = new Level { Name = "L1" } }
            };

            var mockQuery = MockQueryableExtensions.BuildMock<Score>(scores);
            mockRepo.Setup(r => r.GetAllScoresQuery()).Returns(mockQuery);

            // Act
            var result = (await scoreService.GetTopScoresAsync(3)).ToList();

            // Assert
            Assert.That(result[0].Points, Is.EqualTo(200));
            Assert.That(result[1].TimeInSeconds, Is.EqualTo(40));
            Assert.That(result[2].TimeInSeconds, Is.EqualTo(50));
        }

        [Test]
        public async Task AddScoreAsync_WhenBetterTimeIsAchieved_UpdatesExistingScore()
        {
            // Arrange
            string userId = "user123";
            int levelId = 1;
            var existingScore = new Score
            {
                UserId = userId,
                LevelId = levelId,
                TimeInSeconds = 100,
                Points = 50
            };

            mockRepo.Setup(r => r.GetScoreByUserAndLevelAsync(userId, levelId))
                    .ReturnsAsync(existingScore);

            mockRepo.Setup(r => r.UpdateScoreAsync(It.IsAny<Score>()))
                    .ReturnsAsync(true);

            // Act
            int newBetterTime = 80;
            int newPoints = 70;
            await scoreService.AddScoreAsync(levelId, userId, newPoints, newBetterTime);

            // Assert
            mockRepo.Verify(r => r.UpdateScoreAsync(It.Is<Score>(s =>
                s.TimeInSeconds == newBetterTime && s.Points == newPoints)), Times.Once);
        }
    }
}