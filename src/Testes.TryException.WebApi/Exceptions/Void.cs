using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testes.TryException.WebApi.Exceptions
{
	public struct Return
	{
		public static Return Empty { get; }

		static Return()
		{
			Empty = new Return();
		}
	}
}
