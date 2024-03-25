using Flunt.Notifications;
using Web.Api.Track.co.DTOs.Contract;
using Web.Api.Track.co.DTOs.Widget;

public class WidgetUpdateDto : Notifiable<Notification>
{
    public string? Title { get; set; }
    public string? Link { get; set; }
    public string? Question { get; set; }
    public string? Color { get; set; }

    public WidgetUpdateDto()
    { }

    public void Validate()
    {
        AddNotifications(new WidgetUpdateDtoContract(this));
    }
}