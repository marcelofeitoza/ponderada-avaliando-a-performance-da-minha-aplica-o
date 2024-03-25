using Flunt.Notifications;
using Web.Api.Track.co.DTOs.Contract;

namespace Web.Api.Track.co.DTOs.Widget
{
    public class WidgetCreateDto : Notifiable<Notification>
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Question { get; set; }

        public WidgetCreateDto()
        { }

        public void Validate() 
        {
            AddNotifications(new WidgetCreateDtoContract(this));
        } 
    }
}