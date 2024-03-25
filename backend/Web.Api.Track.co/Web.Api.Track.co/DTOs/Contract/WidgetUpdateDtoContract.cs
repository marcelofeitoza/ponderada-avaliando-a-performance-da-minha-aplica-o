using Flunt.Validations;

namespace Web.Api.Track.co.DTOs.Contract
{
    public class WidgetUpdateDtoContract : Contract<WidgetUpdateDto>
    {
        public WidgetUpdateDtoContract(WidgetUpdateDto widget)
        {
            Requires()
                .IsNotNullOrEmpty(widget.Title, nameof(widget.Title), "The title cannot be empty.")
                .IsUrl(widget.Link, nameof(widget.Link), "The link must be a valid URL.")
                .IsNotNullOrEmpty(widget.Question, nameof(widget.Question), "The question cannot be empty.")
                .Matches(widget.Color, "^#(?:[0-9a-fA-F]{3}){1,2}$", nameof(widget.Color), "The color must be a valid hex code.");
        }
    }
}
