using ShopMax.Business.Notifications;

namespace ShopMax.Business.Interfaces;

public interface INotificator
{
	bool HasNotification();
	List<Notification> GetNotifications();
	void Handle(Notification notificacao);
}
