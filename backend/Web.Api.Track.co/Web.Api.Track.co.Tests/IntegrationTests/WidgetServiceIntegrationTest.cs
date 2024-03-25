using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Web.Api.Track.co.Data;
using Web.Api.Track.co.DTOs.Widget;
using Web.Api.Track.co.DTOs.NPS;
using Web.Api.Track.co.Service;

namespace Web.Api.Track.co.Tests.IntegrationTests
{
    [TestClass]
    public class WidgetServiceIntegrationTest
    {
        private readonly WidgetService _widgetService;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<WidgetLogger> _widgetLogger;
        private readonly ILogger<AppDbContext> _logger;
        
        public WidgetServiceIntegrationTest()
        {
            _logger = new Mock<ILogger<AppDbContext>>().Object;
            _widgetLogger = new Mock<ILogger<WidgetLogger>>().Object;
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {
                        "ConnectionStrings:DefaultConnection",
                        "Host=127.0.0.1; Port=5432; Database=db; Username=user; Password=password;"
                    }
                }!)
                .Build();

            _context = new AppDbContext(_configuration, _logger);
            _widgetService = new WidgetService(_widgetLogger, _context);
        }
        
        [TestMethod]
        public void TestCreateWidget()
        {
            _widgetLogger.LogInformation("Starting TestCreateWidget...");

            // Arrange
            _widgetLogger.LogInformation("Arranging...");

            var widgetCreateDtos = new List<WidgetCreateDto>
            {
                new()
                {
                    Title = "Test Widget 1", Link = "https://www.test1.com", Question = "What is your favorite color?"
                },
                new()
                {
                    Title = "Test Widget 2", Link = "https://www.test2.com", Question = "What is your favorite food?"
                },
                new()
                {
                    Title = "Test Widget 3", Link = "https://www.test3.com", Question = "What is your favorite movie?"
                },
                new()
                {
                    Title = "Test Widget 4", Link = "https://www.test4.com", Question = "What is your favorite book?"
                },
                new()
                {
                    Title = "Test Widget 5", Link = "https://www.test5.com", Question = "What is your favorite song?"
                }
            };

            var npsCreateDtos = new List<NPSCreateDto>
            {
                new() { Rating = 5, Answer = "Blue" },
                new() { Rating = 4, Answer = "Pizza" },
                new() { Rating = 3, Answer = "The Matrix" },
                new() { Rating = 2, Answer = "The Great Gatsby" },
                new() { Rating = 1, Answer = "Bohemian Rhapsody" }
            };

            // Act
            _widgetLogger.LogInformation("Acting...");

            var createdWidgets = new List<Widget>();
            foreach (var widgetCreateDto in widgetCreateDtos)
            {
                var widgetId = _widgetService.CreateWidget(widgetCreateDto).Id;
                var createdWidget = _widgetService.GetWidgetById(widgetId);
                createdWidgets.Add(createdWidget);

                foreach (var npsCreateDto in npsCreateDtos)
                {
                    _widgetService.CreateNps(widgetId, npsCreateDto);
                }
            }

            // Assert
            _widgetLogger.LogInformation("Asserting...");

            Assert.AreEqual(widgetCreateDtos.Count, createdWidgets.Count);
            foreach (var createdWidget in createdWidgets)
            {
                Assert.IsNotNull(createdWidget);
                Assert.IsTrue(createdWidget.Nps.Any());
            }

            _widgetLogger.LogInformation("TestCreateWidget completed.");
        }
        
        [TestMethod]
        public void TestGetWidgets()
        {
            _widgetLogger.LogInformation("Starting TestGetWidgets...");

            // Arrange
            _widgetLogger.LogInformation("Arranging...");

            // Act
            _widgetLogger.LogInformation("Acting...");
            var result = _widgetService.GetWidgets();

            // Assert
            _widgetLogger.LogInformation("Asserting...");
            Assert.AreEqual(5, result.Count);

            _widgetLogger.LogInformation("TestGetWidgets completed.");
        }
        
        [TestMethod]
        public void TestGetWidgetById()
        {
            _widgetLogger.LogInformation("Starting TestGetWidgetById...");

            // Arrange
            _widgetLogger.LogInformation("Arranging...");
            var widget = _widgetService.GetWidgets().First();

            // Act
            _widgetLogger.LogInformation("Acting...");
            var result = _widgetService.GetWidgetById(widget.Id);

            // Assert
            _widgetLogger.LogInformation("Asserting...");
            Assert.IsNotNull(result);
            Assert.AreEqual(widget.Id, result.Id);

            _widgetLogger.LogInformation("TestGetWidgetById completed.");
        }
        
        [TestMethod]
        public void TestUpdateWidget()
        {
            _widgetLogger.LogInformation("Starting TestUpdateWidget...");

            // Arrange
            _widgetLogger.LogInformation("Arranging...");
            var widget = _widgetService.GetWidgets().First();
            var widgetUpdateDto = new WidgetUpdateDto
            {
                Title = "Updated Title",
                Link = "https://www.updated.com",
                Question = "What is your favorite color?"
            };

            // Act
            _widgetLogger.LogInformation("Acting...");
            var updatedWidget = _widgetService.UpdateWidget(widget.Id, widgetUpdateDto);

            // Assert
            _widgetLogger.LogInformation("Asserting...");
            Assert.AreEqual(widget.Id, updatedWidget.Id);
            Assert.AreEqual(widgetUpdateDto.Title, updatedWidget.Title);
            Assert.AreEqual(widgetUpdateDto.Link, updatedWidget.Link);
            Assert.AreEqual(widgetUpdateDto.Question, updatedWidget.Question);

            _widgetLogger.LogInformation("TestUpdateWidget completed.");
        }
        
        [TestMethod]
        public void TestGenerateHTML()
        {
            // Arrange
            var widget = new Widget { Id = 1, Question = "Test Question" };

            // Act
            var result = _widgetService.GenerateHTML(widget);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains(widget.Question));
        }

        [TestMethod]
        public void TestUpdateWidget_NonExistentWidget_ThrowsException()
        {
            // Arrange
            var nonExistentWidgetId = 999; // Assuming this ID does not exist
            var widgetUpdateDto = new WidgetUpdateDto
            {
                Title = "New Title",
                Link = "https://www.newlink.com",
                Question = "Updated question?"
            };

            // Act & Assert
            Assert.ThrowsException<Exception>(() => _widgetService.UpdateWidget(nonExistentWidgetId, widgetUpdateDto));
        }
        
        [TestMethod]
        public void TestDeleteWidget_NonExistentWidget_ThrowsException()
        {
            // Arrange
            var nonExistentWidgetId = 999; // Assuming this ID does not exist

            // Act & Assert
            Assert.ThrowsException<Exception>(() => _widgetService.DeleteWidget(nonExistentWidgetId));
        }
        
        [TestMethod]
        public void TestDeleteAllWidgets()
        {
            _widgetLogger.LogInformation("Starting TestDeleteAllWidgets...");

            // Arrange
            _widgetLogger.LogInformation("Arranging...");
            var allWidgets = _widgetService.GetWidgets();
            var widgetIds = allWidgets.Select(w => w.Id).ToList();

            // Act
            _widgetLogger.LogInformation("Acting...");
            foreach (var widgetId in widgetIds)
            {
                _widgetService.DeleteWidget(widgetId);
                _widgetLogger.LogInformation($"Deleted widget with ID {widgetId}");
            }

            // Assert
            _widgetLogger.LogInformation("Asserting...");
            var result = _widgetService.GetWidgets();
            Assert.AreEqual(0, result.Count);

            _widgetLogger.LogInformation("TestDeleteAllWidgets completed.");
        }
    }
}