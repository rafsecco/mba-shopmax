using ShopMax.Business.Interfaces;

namespace ShopMax.Business.Notifications;

public class Notificator : INotificator
{
	private List<Notification> _notifications;

	public Notificator()
	{
		_notifications = new List<Notification>();
	}

	public void Handle(Notification notification)
	{
		_notifications.Add(notification);
	}

	public List<Notification> GetNotifications()
	{
		return _notifications;
	}

	public bool HasNotification()
	{
		return _notifications.Any();
	}
}
