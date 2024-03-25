using Flunt.Notifications;
using Web.Api.Track.co.DTOs.Contract;
using Web.Api.Track.co.DTOs.Widget;

namespace Web.Api.Track.co.DTOs.NPS
{
    public class NPSCreateDto : Notifiable<Notification>
    {
        public int WidgetId { get; set; }
        public string Answer { get; set; }
        public int Rating { get; set; }

        public void Validate()
        {
            AddNotifications(new NpsCreateDtoContract(this));
        }
    }
}

