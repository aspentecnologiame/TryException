using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testes.TryException.WebApi.Exceptions
{
	public struct TryException<TSuccess>
	{
		public ExceptionReadOnlyCollection Failure { get; }
		public TSuccess Success { get; }

		public bool IsFailure { get; }
		public bool IsSuccess => !IsFailure;

		internal TryException(ExceptionReadOnlyCollection failure)
		{
			IsFailure = true;
			Failure = failure;
			Success = default;
		}

		internal TryException(TSuccess success)
		{
			IsFailure = false;
			Failure = default;
			Success = success;
		}

		public TResult Resume<TResult>(
			Func<ExceptionReadOnlyCollection, TResult> failure,
			Func<TSuccess, TResult> success
		) => IsFailure ? failure(Failure) : success(Success);

		public Return Resume(
			Action<ExceptionReadOnlyCollection> failure,
			Action<TSuccess> success
		) => Resume(ToFunc(failure), ToFunc(success));

		public static implicit operator TryException<TSuccess>(Exception exception)
			=> new ExceptionReadOnlyCollection(exception);

		public static implicit operator TryException<TSuccess>(ExceptionReadOnlyCollection failure)
			=> new TryException<TSuccess>(failure);

		public static implicit operator TryException<TSuccess>(TSuccess success)
			=> new TryException<TSuccess>(success);

		private static Func<T, Return> ToFunc<T>(Action<T> action)
			=> o =>
			{
				action(o);
				return Return.Empty;
			};
	}
}
