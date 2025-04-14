using ShopMax.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ShopMax.MVC.Components;

public class SummaryViewComponent : ViewComponent
{
	private readonly INotificator _notificator;

	public SummaryViewComponent(INotificator notificator)
	{
		_notificator = notificator;
	}

	public async Task<IViewComponentResult> InvokeAsync()
	{
		var notificacoes = await Task.FromResult(_notificator.ObterNotificacoes());
		notificacoes.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Mensagem));

		return View();
	}
}
