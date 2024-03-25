using Web.Api.Track.co.DTOs.NPS;

namespace Web.Api.Track.co.Tests.UnitTest
{
    public class CreateNpsUnitTest
    {

        [TestClass]
        public class NPSCreateDtoUnitTest
        {
            [TestMethod]
            public void NPSCreateDto_Should_Be_Valid_With_Correct_Values()
            {
                var nps = new NPSCreateDto()
                {
                    WidgetId = 1,
                    Answer = "This is a feedback",
                    Rating = 10
                };

                nps.Validate();
                Assert.IsTrue(nps.IsValid);
            }

            [TestMethod]
            public void NPSCreateDto_Should_Be_Invalid_With_Negative_WidgetId()
            {
                var nps = new NPSCreateDto()
                {
                    WidgetId = -1,
                    Answer = "This is a feedback",
                    Rating = 10
                };

                nps.Validate();
                Assert.IsFalse(nps.IsValid);
            }

            [TestMethod]
            public void NPSCreateDto_Should_Be_Invalid_With_Empty_Answer()
            {
                var nps = new NPSCreateDto()
                {
                    WidgetId = 1,
                    Answer = "",
                    Rating = 10
                };

                nps.Validate();
                Assert.IsFalse(nps.IsValid);
            }

            [TestMethod]
            public void NPSCreateDto_Should_Be_Invalid_With_Negative_Rating()
            {
                var nps = new NPSCreateDto()
                {
                    WidgetId = 1,
                    Answer = "This is a feedback",
                    Rating = -1
                };

                nps.Validate();
                Assert.IsFalse(nps.IsValid);
            }

            [TestMethod]
            public void NPSCreateDto_Should_Be_Invalid_With_Zero_Rating()
            {
                var nps = new NPSCreateDto()
                {
                    WidgetId = 1,
                    Answer = "This is a feedback",
                    Rating = 0
                };

                nps.Validate();
                Assert.IsFalse(nps.IsValid);
            }
        }
    }
}
