using FluentValidation;
using FluentValidation.Results;
using ShopMax.Business.Interfaces;
using ShopMax.Business.Models;
using ShopMax.Business.Notifications;

namespace ShopMax.Business.Services;

public abstract class BaseService
{
	private readonly INotificator _notifier;

	protected BaseService(INotificator notifier)
	{
		_notifier = notifier;
	}

	protected void Notify(ValidationResult validationResult)
	{
		foreach (var item in validationResult.Errors)
		{
			Notify(item.ErrorMessage);
		}
	}

	protected void Notify(string message)
	{
		_notifier.Handle(new Notification(message));
	}

	protected bool RunValidation<TV, TE>(TV validation, TE entity)
		where TV : AbstractValidator<TE>
		where TE : Entity
	{
		var validator = validation.Validate(entity);

		if (validator.IsValid) return true;

		Notify(validator);

		return false;
	}
}
