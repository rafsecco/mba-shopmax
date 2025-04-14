using Microsoft.AspNetCore.Mvc;
using ShopMax.Business.Interfaces;

namespace ShopMax.MVC.Controllers;

public abstract class BaseController : Controller
{
	private readonly INotificator _notificator;

	protected BaseController(INotificator notificator)
	{
		_notificator = notificator;
	}

	protected bool OperacaoValida()
	{
		return !_notificator.TemNotificacao();
	}
}
