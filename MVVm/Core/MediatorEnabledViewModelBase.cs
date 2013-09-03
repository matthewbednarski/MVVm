using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVm.Core
{
	public class MediatorEnabledViewModelBase<T>:ViewModelBase
	{

		
		public Mediator Mediator
		{
			get
			{
				return Mediator.Instance;
			}
		}
		public MediatorEnabledViewModelBase()
		{
			this.Mediator.Register(this);
		}
		public bool Notify(string key, T message)
		{
			return this.Mediator.NotifyColleagues(key, message);
		}
		public bool Notify(T message)
		{
			return this.Mediator.NotifyColleagues(message);
		}
		public void NotifyAsync(string key, T message, Action<bool> callback)
		{
			this.Mediator.NotifyColleaguesAsync(key, message, callback);
		}
		public void NotifyAsync(string key, T message)
		{
			this.Mediator.NotifyColleaguesAsync(key, message);
		}
		public void NotifyAsync(T message, Action<bool> callback)
		{
			this.Mediator.NotifyColleaguesAsync(message, callback);
		}
		public void NotifyAsync(T message)
		{
			this.Mediator.NotifyColleaguesAsync(message);
		}
		
	}
}
