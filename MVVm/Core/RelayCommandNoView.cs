using System;
using System.Diagnostics;

namespace MVVm.Core
{
	/// <summary>
	/// A command whose sole purpose is to
	/// relay its functionality to other
	/// objects by invoking delegates. The
	/// default return value for the CanExecute
	/// method is 'true'.
	/// </summary>
	public class RelayCommandNoView : ICommandNoView
	{
		#region Fields

		readonly Action<object> _execute;
		readonly Predicate<object> _canExecute;
		event EventHandler _canExecuteChanged;
		#endregion // Fields

		#region Constructors

		/// <summary>
		/// Creates a new command that can always execute.
		/// </summary>
		/// <param name="execute">The execution logic.</param>
		public RelayCommandNoView(Action<object> execute)
			: this(execute, null)
		{
		}

		/// <summary>
		/// Creates a new command.
		/// </summary>
		/// <param name="execute">The execution logic.</param>
		/// <param name="canExecute">The execution status logic.</param>
		public RelayCommandNoView(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");

			_execute = execute;
			_canExecute = canExecute;
			
		}

		public virtual void RaiseCanExecuteChanged()
		{
			var tempEventHandler = _canExecuteChanged;
			if (tempEventHandler != null)
			{
				tempEventHandler(this, new EventArgs());
			}

		}

		#endregion // Constructors

		#region ICommand Members

		[DebuggerStepThrough]
		public bool CanExecute(object parameter)
		{
			return _canExecute == null ? true : _canExecute(parameter);
		}

		public event EventHandler CanExecuteChanged
		{
			add { _canExecuteChanged += value; }
			remove { _canExecuteChanged -= value; }
		}

		public void Execute(object parameter)
		{
			_execute(parameter);
		}

		#endregion // ICommand Members
	}
}