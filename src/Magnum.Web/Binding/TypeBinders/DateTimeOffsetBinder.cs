// Copyright 2007-2008 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Magnum.Web.Binding.TypeBinders
{
	using System;
	using System.Xml;

	public class DateTimeOffsetBinder :
		ObjectBinder<DateTimeOffset>
	{
		public object Bind(BinderContext context)
		{
			object value = context.PropertyValue;

			if (value == null)
				return null;

			if (value is DateTimeOffset)
				return value;

			if (value is DateTime)
				return new DateTimeOffset((DateTime) value);

			string text = value.ToString();

			DateTimeOffset result;
			if (DateTimeOffset.TryParse(text, out result))
				return result;

			return XmlConvert.ToDateTimeOffset(text);
		}
	}
}