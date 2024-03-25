namespace Web.Api.Track.co.Tests.UnitTest
{
    public class GetAllWidgetsTest
    {
        [TestClass]
        public class WidgetGetDtoTests
        {
            // [HttpGet]
            // public IActionResult Get()
            // {
            //     try
            //     {
            //         _logger.LogInformation("Getting all widgets with corresponding NPS records");
            //         var widgets = _context.Widgets
            //             .Include(w => w.NPS)
            //             .ToList();
            //     
            //         return Ok(widgets);
            //     }
            //     catch (Exception ex)
            //     {
            //         _logger.LogError(ex, "Error occurred while fetching widgets with NPS records");
            //         return StatusCode(500,
            //             new
            //             {
            //                 error = true, message = "Internal server error occurred while fetching widgets with NPS records"
            //             });
            //     }
            // }
        }
        // [TestClass]
        // public class WidgetUpdateDtoTests
        // {
        //     [TestMethod]
        //     public void WidgetUpdateDto_Validation_ShouldPass_WithValidFields()
        //     {
        //         var dto = new WidgetUpdateDto
        //         {
        //             Title = "Valid Title",
        //             Link = "https://example.com",
        //             Question = "Valid Question?",
        //             Color = "#FFFFFF"
        //         };
        //
        //         dto.Validate();
        //
        //         Assert.IsTrue(dto.IsValid);
        //     }
        //
        //     [TestMethod]
        //     [DataRow(null, "https://example.com", "Valid Question?", "#FFFFFF")]
        //     [DataRow("Valid Title", "not-a-url", "Valid Question?", "#FFFFFF")]
        //     [DataRow("Valid Title", "https://example.com", null, "#FFFFFF")]
        //     [DataRow("Valid Title", "https://example.com", "Valid Question?", "NotAHex")]
        //     public void WidgetUpdateDto_Validation_ShouldFail_WithInvalidFields(string title, string link, string question, string color)
        //     {
        //         var dto = new WidgetUpdateDto
        //         {
        //             Title = title,
        //             Link = link,
        //             Question = question,
        //             Color = color
        //         };
        //
        //         dto.Validate();
        //
        //         Assert.IsFalse(dto.IsValid);
        //     }
        // }
    }
}