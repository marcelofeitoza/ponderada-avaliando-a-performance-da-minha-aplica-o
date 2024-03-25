using Web.Api.Track.co.DTOs.NPS;
using Web.Api.Track.co.DTOs.Widget;


namespace Web.Api.Track.co.Tests.UnitTest
{

    [TestClass]
    public class CreateWidgetUnitTest
    {
        [TestMethod]
        public void Should_Be_Valid()
        {
            var widget = new WidgetCreateDto()
            {
                Title = "Title",
                Link = "https://example.com",
                Question = "?"
            };

            Assert.IsTrue(widget.IsValid);
        }


        [TestMethod]
        public void Should_Be_Invalid_With_Empty_Title()
        {
            var widget = new WidgetCreateDto()
            {
                Title = "",
                Link = "",
                Question = ""
            };
            widget.Validate();
            Assert.IsFalse(widget.IsValid);
        }

        [TestMethod]
        public void Should_Be_Invalid_With_Null_Title()
        {
            var widget = new WidgetCreateDto()
            {
                Title = null,
                Link = "",
                Question = ""
            };
            widget.Validate();
            Assert.IsFalse(widget.IsValid);
        }

        }
}
