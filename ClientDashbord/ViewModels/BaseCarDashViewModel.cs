using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ClientDashbord.ViewModels
{
	public abstract class BaseCarDashViewModel : BaseNotify
	{
		protected List<Action> ActionPostUpdate;

		protected BaseCarDashViewModel()
		{
			this.ActionPostUpdate = new List<Action>();
		}

		public virtual void Update(byte[] bytes)
		{
			IEnumerable<PropertyInfo> properties = this.GetType().GetProperties().Where(o => Attribute.IsDefined((MemberInfo) o, typeof(PositionByteAttribute)));

			foreach (PropertyInfo propertyInfo in properties)
			{
				IEnumerable<Attribute> attributes = propertyInfo.GetCustomAttributes(typeof(PositionByteAttribute)).ToList();
				if (attributes.Count() != 1)
				{
					continue;
				}

				if (attributes.First() is PositionByteAttribute positionByte)
				{
					byte[] bytesProperty = bytes.GetPartOfByteTab(positionByte);
					object newValue = bytesProperty.ConvertByte(propertyInfo.PropertyType);
					
					propertyInfo.SetValue(this, newValue);
				}
			}

			this.GetType().GetProperties().ToList().ForEach(o => this.OnPropertyChanged(o.Name));
			this.ActionPostUpdate.ForEach(o => o());
			this.ActionPostUpdate.Clear();
		}
	}
}