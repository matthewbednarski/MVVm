using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVm.Core
{
	public class MediatorEnabledViewModel<T>:WorkspaceViewModel
	{

		public Mediator Mediator
		{
			get
			{
				return Mediator.Instance;
			}
		}
		public MediatorEnabledViewModel()
		{
			this.Mediator.Register(this);
		}
	}
}
