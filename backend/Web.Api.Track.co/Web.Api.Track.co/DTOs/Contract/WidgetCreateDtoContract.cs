using Flunt.Validations;
using Web.Api.Track.co.DTOs.Widget;

namespace Web.Api.Track.co.DTOs.Contract
{
    public class WidgetCreateDtoContract : Contract<WidgetCreateDto>
    {
        public WidgetCreateDtoContract(WidgetCreateDto widget)
        {
            Requires()
                .IsNotNullOrEmpty(widget.Title, nameof(widget.Title))
                .IsNotNullOrEmpty(widget.Link, nameof(widget.Link))
                .IsNotNullOrEmpty(widget.Question, nameof(widget.Question));
        }
    }
}