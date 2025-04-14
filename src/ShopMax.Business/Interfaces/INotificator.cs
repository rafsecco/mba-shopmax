using ShopMax.Business.Notifications;

namespace ShopMax.Business.Interfaces;

public interface INotificator
{
	bool TemNotificacao();
	List<Notification> ObterNotificacoes();
	void Handle(Notification notificacao);
}
