using FluentValidation;
using FluentValidation.Results;
using ShopMax.Business.Interfaces;
using ShopMax.Business.Models;
using ShopMax.Business.Notifications;

namespace ShopMax.Business.Services;

public abstract class BaseService
{
	private readonly INotificator _notificador;

	protected BaseService(INotificator notificador)
	{
		_notificador = notificador;
	}

	protected void Notificar(ValidationResult validationResult)
	{
		foreach (var item in validationResult.Errors)
		{
			Notificar(item.ErrorMessage);
		}
	}

	protected void Notificar(string mensagem)
	{
		_notificador.Handle(new Notification(mensagem));
	}

	protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade)
		where TV : AbstractValidator<TE>
		where TE : Entity
	{
		var validator = validacao.Validate(entidade);

		if (validator.IsValid) return true;

		Notificar(validator);

		return false;
	}
}
