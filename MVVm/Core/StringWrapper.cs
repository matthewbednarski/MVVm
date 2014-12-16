using System;
using System.ComponentModel;

namespace MVVm.Core
{
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://new.webservice.namespace")]
	//[System.Xml.Serialization.XmlTypeAttribute(TypeName="supertemplate", Namespace = "http://new.webservice.namespace")]
	public class StringWrapper : System.ComponentModel.INotifyPropertyChanged
	{
		private string _value;

		//[System.Xml.Serialization.XmlElementAttribute(ElementName = "supertemplate")]
		[System.Xml.Serialization.XmlTextAttribute()]
		public string Value
		{
			get
			{
				return _value;
			}
			set
			{
				PreviousValue = _value;
				_value = value;
				OnPropertyChanged("Value");
			}
		}

		private string _oldValue;
		[System.Xml.Serialization.XmlIgnore()]
		public string PreviousValue
		{
			get{
				return _oldValue;
			}
			set{
				_oldValue = value;
				OnPropertyChanged("PreviousValue");
			}
		}
		
		public StringWrapper()
		{
			this._value = "";
		}
		public StringWrapper(string s)
		{
			this._value = s;
		}


		public override string ToString()
		{
			return this.Value;
		}
		public bool Equals(string s)
		{
			int a = s.GetHashCode();
			int b = this.Value.GetHashCode();
			return a == b;
		}
		public bool Equals(StringWrapper sw)
		{
			int a = sw.Value.GetHashCode();
			int b = this.Value.GetHashCode();
			return a == b;
		}
//		public override bool Equals(object obj)
//		{
//			bool r = false;
//			if ((obj as string) != null)
//			{
//				int a = (obj as string).GetHashCode();
//				int b = this.GetHashCode();
//				r = a == b;
//			}
//			else if ((obj as StringWrapper) != null)
//			{
//				int a = (obj as StringWrapper).GetHashCode();
//				int b = this.GetHashCode();
//				r = a == b;
//			}
//			return r;
//		}
//		public override int GetHashCode()
//		{
//			int r = this.Value.GetHashCode();
//			return r;
//		}
	//	[System.Xml.Serialization.XmlIgnore()]
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if ((propertyChanged != null))
			{
				propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

	}
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://new.webservice.namespace")]
	//[System.Xml.Serialization.XmlTypeAttribute(TypeName="supertemplate", Namespace = "http://new.webservice.namespace")]
	public class IntWrapper : System.ComponentModel.INotifyPropertyChanged
	{
		private int _value;

		//[System.Xml.Serialization.XmlElementAttribute(ElementName = "supertemplate")]
		[System.Xml.Serialization.XmlTextAttribute()]
		public int Value
		{
			get
			{
				return _value;
			}
			set
			{
				PreviousValue = _value;
				_value = value;
				OnPropertyChanged("Value");
			}
		}

		private int _oldValue;
		[System.Xml.Serialization.XmlIgnore()]
		public int PreviousValue
		{
			get{
				return _oldValue;
			}
			set{
				_oldValue = value;
				OnPropertyChanged("PreviousValue");
			}
		}
		
		public IntWrapper()
		{
			this._value = 0;
		}
		public IntWrapper(int s)
		{
			this._value = s;
		}
		public IntWrapper(double s)
		{
			this._value = (int)s;
		}

		public override string ToString()
		{
			return this.Value.ToString();
		}

		public bool Equals(IntWrapper iw)
		{
			int a = iw.GetHashCode();
			int b = this.GetHashCode();
			return a == b;
		}
		public override bool Equals(object obj)
		{
			bool r = false;
			if (((int)obj) != 0)
			{
				int a = ((int)obj).GetHashCode();
				int b = this.GetHashCode();
				r = a == b;
			}
			else if ((obj as IntWrapper) != null)
			{
				int a = (obj as IntWrapper).GetHashCode();
				int b = this.GetHashCode();
				r = a == b;
			}
			return r;
		}
		public override int GetHashCode()
		{
			int r = this.Value.GetHashCode();
			return r;
		}
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if ((propertyChanged != null))
			{
				propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

	}
}
