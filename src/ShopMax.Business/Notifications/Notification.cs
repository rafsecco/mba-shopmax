namespace ShopMax.Business.Notifications;

public class Notification
{
	public string Mensagem { get; }

	public Notification(string mensagem)
	{
		Mensagem = mensagem;
	}
}
