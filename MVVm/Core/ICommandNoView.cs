/*
 * Matthew Carl Bednarski <matthew.bednarski@ekr.it>
 * 15/04/2013 - 11.57
 */

using System;

namespace MVVm.Core
{
	/// <summary>
	/// Description of ICommandMVVm.
	/// </summary>
	public interface ICommandNoView
	{
		event EventHandler CanExecuteChanged;
		
		bool CanExecute(Object parameter);
		void Execute(Object parameter);
		
	}
}
