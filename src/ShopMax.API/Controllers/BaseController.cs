using Microsoft.AspNetCore.Mvc;
using ShopMax.Business.Interfaces;

namespace ShopMax.API.Controllers;

[ApiController]
public class BaseController : ControllerBase
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
