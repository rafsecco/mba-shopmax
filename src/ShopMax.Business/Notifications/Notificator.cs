using ShopMax.Business.Interfaces;

namespace ShopMax.Business.Notifications;

public class Notificator : INotificator
{
	private List<Notification> _notificacoes;

	public Notificator()
	{
		_notificacoes = new List<Notification>();
	}

	public void Handle(Notification notificacao)
	{
		_notificacoes.Add(notificacao);
	}

	public List<Notification> ObterNotificacoes()
	{
		return _notificacoes;
	}

	public bool TemNotificacao()
	{
		return _notificacoes.Any();
	}
}
