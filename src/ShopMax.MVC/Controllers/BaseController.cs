using Microsoft.AspNetCore.Mvc;
using ShopMax.Business.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ShopMax.MVC.Controllers;

public abstract class BaseController : Controller
{
	private readonly INotificator _notificator;

	protected BaseController(INotificator notificator)
	{
		_notificator = notificator;
	}

	protected bool ValidateOperation()
	{
		return !_notificator.HasNotification();
	}
}
