using Flunt.Validations;
using Web.Api.Track.co.DTOs.NPS;

namespace Web.Api.Track.co.DTOs.Contract
{
    public class NpsCreateDtoContract : Contract<NPSCreateDto>
    {
        public NpsCreateDtoContract(NPSCreateDto nps)
        {
            Requires()
                .IsGreaterThan(nps.WidgetId, 0, nameof(nps.WidgetId))
                .IsNotNullOrEmpty(nps.Answer, nameof(nps.Answer))
                .IsGreaterThan(nps.Rating, 0, nameof(nps.Rating));
        }
    }
}
