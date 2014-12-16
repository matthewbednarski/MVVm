using System;

//using System.Windows.Input;

namespace MVVm.Core
{
    /// <summary>
    /// This ViewModelBase subclass requests to be removed
    /// from the UI when its CloseCommand executes.
    /// This class is abstract.
    /// </summary>
    public abstract class WorkspaceViewModel : ViewModelBase
    {
        #region Fields

        RelayCommand _closeCommand;

        #endregion // Fields

        #region Constructor

        protected WorkspaceViewModel()
        {
        }

        #endregion // Constructor

        #region CloseCommand

        /// <summary>
        /// Returns the command that, when invoked, attempts
        /// to remove this workspace from the user interface.
        /// </summary>
        public System.Windows.Input.ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new RelayCommand(param => this.OnRequestClose());

                return _closeCommand;
            }
        }

        #endregion // CloseCommand

        #region RequestClose [event]

        /// <summary>
        /// Raised when this workspace should be removed from the UI.
        /// </summary>
        public event EventHandler RequestClose;

        void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion // RequestClose [event]

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                if (value != _isBusy)
                {
                    _isBusy = value;
                    if (_isBusy == true)
                    {
                        WaitCursorActivate();
                    }
                    else
                    {
                        WaitCursorDeactivate();
                    }
                    OnPropertyChanged("IsBusy");
                }
            }
        }
		
        private string _busyMessage;
        public string BusyMessage
        {
            get
            {
                if (String.IsNullOrEmpty(_busyMessage))
                {
                    _busyMessage = "...";
                }
                return _busyMessage;
            }
            set
            {
                if (_busyMessage != value)
                {
                    _busyMessage = value;
                    OnPropertyChanged("BusyMessage");
                }
				
            }
        }

        #region Wait Cursor

        private void WaitCursorActivate()
        {
            #if __MonoCS__
            // Code for Mono C# compiler.
            #else
            // Code for Microsoft C# compiler.
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            #endif
        }
        private void WaitCursorDeactivate()
        {
            #if __MonoCS__
            // Code for Mono C# compiler.
            #else
            // Code for Microsoft C# compiler.
            System.Windows.Input.Mouse.OverrideCursor = null;
            #endif
        }

        #endregion //Wait Cursor
    }
}
