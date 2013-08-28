using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Input;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MVVm.Core
{
	public class ObservableCollectionWithCurrent<T> : ObservableCollection<T>, INotifyCollectionChanged
	{
		public System.ComponentModel.ICollectionView DefaultView
		{
			get
			{
				return System.Windows.Data.CollectionViewSource.GetDefaultView(this);
			}
		}

		public T CurrentItem
		{
			get
			{
				if(DefaultView.CurrentItem == null )
				{
					DefaultView.MoveCurrentToNext();
				//	(T)DefaultView.CurrentItem = (T)DefaultView.AsQueryable().Cast<T>().First();
				}
				return (T)DefaultView.CurrentItem;
			}
			set
			{
				T val = (T)value;
				this.MoveCurrentTo(val);
			}
		}
		public int CurrentPosition
		{
			get
			{
				return DefaultView.CurrentPosition;
			}
		}

		public ObservableCollectionWithCurrent(List<T> list)
			: base(list)
		{

		}

		public ObservableCollectionWithCurrent(IEnumerable<T> collection)
			: base(collection)
		{
		}
		public ObservableCollectionWithCurrent()
			: base()
		{
		}


		public T AddNew()
		{
			Type classType = typeof(T);
			ConstructorInfo classConstructor = classType.GetConstructor(new Type[] { });
			T classInstance = (T)classConstructor.Invoke(new object[] { });

			this.Add(classInstance);
			this.MoveCurrentTo(classInstance);
			return classInstance;
		}

		public void AddRange(IEnumerable<T> collection)
		{
			foreach (T item in collection)
			{
				if (!this.Contains(item))
				{
					this.Add(item);
				}
			}
		}

		public void RemoveCurrent()
		{
			T tmp = CurrentItem;
			if (this.IndexOf(tmp) > 0)
			{
				this.MovePrevious();
			}
			else
			{
				this.MoveNext();
			}
			this.Remove(tmp);
		}

		public void RemoveRange(IEnumerable<T> collection)
		{
			foreach (T item in collection)
			{
				if (this.Contains(item))
				{
					this.Remove(item);
				}
			}
		}

		public void MoveFirst()
		{
			DefaultView.MoveCurrentToFirst();
		}

		public void MovePrevious()
		{
			DefaultView.MoveCurrentToPrevious();
		}

		public void MoveNext()
		{
			DefaultView.MoveCurrentToNext();
		}

		public void MoveLast()
		{
			DefaultView.MoveCurrentToLast();
		}

		public void MoveCurrentTo(T item)
		{
			bool r = this.DefaultView.MoveCurrentTo(item);
			Console.Out.Write(r.ToString());
			if (r == false)
			{
				this.MoveCurrentToIndexOf(item);
			}
		}
		public void MoveCurrentToPosition(int pos)
		{
			this.DefaultView.MoveCurrentToPosition(pos);
		}
		public void MoveCurrentToIndexOf(T item)
		{
			int ind = this.IndexOf(item);
			bool r = this.DefaultView.MoveCurrentToPosition(ind);
			Console.Out.Write(r.ToString());
		}

		public void Sort(string property, ListSortDirection direction)
		{
			DefaultView.SortDescriptions.Add(new SortDescription(property, direction));
		}
		
		public new T MoveItem(int oldIndex, int newIndex)
		{
			base.Move(oldIndex, newIndex);
			T item = this.Items[newIndex];
			CurrentItem = item;
			return this.CurrentItem;
		}
		private LinkedList<T> _copyList;
		public LinkedList<T> CopyList
		{
			get{
				if(_copyList == null)
				{
					_copyList = new LinkedList<T>();
				}
				return _copyList;
			}
			private set{
				_copyList = value;
			}
		}
		private RelayCommand _copyCommand;
		public ICommand CopyCommand
		{
			get{
				if(_copyCommand == null)
				{
					_copyCommand = new RelayCommand(
						(param) => {
							if(this.CurrentItem != null)
							{
								if(CopyList.Contains(this.CurrentItem))
								{
									CopyList.Remove(this.CurrentItem);
								}
								while (CopyList.Count > 15)
								{
									CopyList.RemoveLast();
								}
								CopyList.AddFirst(this.CurrentItem);
							}
						},(param) => (param is T )
					);
				}
				return _copyCommand;
			}
		}

		private RelayCommand _cutCommand;
		public ICommand CutCommand
		{
			get{
				if(_cutCommand == null)
				{
					_cutCommand = new RelayCommand(
						(param) => {
							if(this.CurrentItem != null)
							{
								if(CopyList.Contains(this.CurrentItem))
								{
									CopyList.Remove(this.CurrentItem);
								}
								while (CopyList.Count > 15)
								{
									CopyList.RemoveLast();
								}
								CopyList.AddFirst(this.CurrentItem);
								this.Remove(this.CurrentItem);
							}
						},(param) => (param is T )
					);
				}
				return _cutCommand;
			}
		}
		private RelayCommand _pasteCommand;
		public ICommand PasteCommand
		{
			get{
				if(_pasteCommand == null)
				{
					_pasteCommand = new RelayCommand(
						(param) => {
							if(this.CurrentItem != null)
							{
								T tmp = CopyList.First();
								if(tmp != null)
								{
									int pos = this.CurrentPosition;
									this.Add(tmp);
									if(pos + 1 < this.Count)
									{
										this.MoveItem(this.IndexOf(tmp), pos + 1);
									}else{
										this.MoveItem(this.IndexOf(tmp), pos - 1);
									}
								}
							}
						},(param) =>  CopyList.Count > 0 
					);
				}
				return _pasteCommand;
			}
		}
		public void SetPasteCommand(RelayCommand pasteCommand)
		{
			_pasteCommand = pasteCommand;
		}
		private RelayCommand _moveUpCommand;
		public ICommand MoveUpCommand
		{
			get{
				if(_moveUpCommand == null)
				{
					_moveUpCommand = new RelayCommand(
						(param) => {
							if(this.CurrentItem != null)
							{
								this.MoveItem(this.CurrentPosition, this.CurrentPosition - 1);
							}
						},(param) => (param is T ) && this.IndexOf(this.CurrentItem) > 0
					);
				}
				return _moveUpCommand;
			}
		}
		private RelayCommand _moveDownCommand;
		public ICommand MoveDownCommand
		{
			get{
				if(_moveDownCommand == null)
				{
					_moveDownCommand = new RelayCommand(
						(param) => {
							if(this.CurrentItem != null)
							{
								this.MoveItem(this.CurrentPosition, this.CurrentPosition + 1);
							}
						},(param) => (param is T ) && this.IndexOf(this.CurrentItem) < this.Count - 1
					);
				}
				return _moveDownCommand;
			}
		}
		
		private RelayCommand _addBeforeCommand;
		public ICommand AddBeforeCommand
		{
			get{
				if(_addBeforeCommand == null)
				{
					_addBeforeCommand = new RelayCommand(
						(param) => {
							if(this.CurrentItem != null)
							{
								int pos = this.CurrentPosition;
								T item = this.AddNew();
								this.MoveItem(this.IndexOf(item), pos);
							}
						},(param) => (param is T )
					);
				}
				return _addBeforeCommand;
			}
		}
		private RelayCommand _addAfterCommand;
		public ICommand AddAfterCommand
		{
			get{
				if(_addAfterCommand == null)
				{
					_addAfterCommand = new RelayCommand(
						(param) => {
							if(this.CurrentItem != null)
							{
								int pos = this.CurrentPosition;
								T item = this.AddNew();
								this.MoveItem(this.IndexOf(item), pos + 1);
							}
						},(param) => (param is T )
					);
				}
				return _addAfterCommand;
			}
		}
		private RelayCommand _deleteCommand;
		public ICommand  DeleteCommand
		{
			get{
				if(_deleteCommand == null)
				{
					_deleteCommand = new RelayCommand(
						(param) => {
							if(this.CurrentItem != null)
							{
								this.RemoveCurrent();
							}
						},(param) => (param is T )
					);
				}
				return _deleteCommand;
			}
		}
		
	}
}
