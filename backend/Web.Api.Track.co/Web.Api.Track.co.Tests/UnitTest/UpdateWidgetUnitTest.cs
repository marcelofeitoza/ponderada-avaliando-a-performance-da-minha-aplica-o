namespace Web.Api.Track.co.Tests.UnitTest
{
    public class UpdateWidgetUnitTest
    {
        [TestClass]
        public class WidgetUpdateDtoTests
        {
            [TestMethod]
            public void WidgetUpdateDto_Validation_ShouldPass_WithValidFields()
            {
                var dto = new WidgetUpdateDto
                {
                    Title = "Valid Title",
                    Link = "https://example.com",
                    Question = "Valid Question?",
                    Color = "#FFFFFF"
                };

                dto.Validate();

                Assert.IsTrue(dto.IsValid);
            }

            [TestMethod]
            [DataRow(null, "https://example.com", "Valid Question?", "#FFFFFF")]
            [DataRow("Valid Title", "not-a-url", "Valid Question?", "#FFFFFF")]
            [DataRow("Valid Title", "https://example.com", null, "#FFFFFF")]
            [DataRow("Valid Title", "https://example.com", "Valid Question?", "NotAHex")]
            public void WidgetUpdateDto_Validation_ShouldFail_WithInvalidFields(string title, string link, string question, string color)
            {
                var dto = new WidgetUpdateDto
                {
                    Title = title,
                    Link = link,
                    Question = question,
                    Color = color
                };

                dto.Validate();

                Assert.IsFalse(dto.IsValid);
            }
        }
    }
}
