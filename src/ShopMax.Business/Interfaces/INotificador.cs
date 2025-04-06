using ShopMax.Business.Notificacoes;

namespace ShopMax.Business.Interfaces;

public interface INotificador
{
	bool TemNotificacao();
	List<Notificacao> ObterNotificacoes();
	void Handle(Notificacao notificacao);
}
