using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MediaHandleUtilities
{
	public static class EnumUtilities
	{
		public static List<string> GetStringValues(Type enumWithStringValues)
		{
			IEnumerable<Enum> enums = Enum.GetValues(enumWithStringValues).OfType<Enum>();

			return enums.Select(GetStringValue).ToList();
		}

		public static List<string> GetStringValuesExceptNone(Type enumWithStringValues)
		{
			return GetStringValues(enumWithStringValues).Where(i => i != "None").ToList();
		}

		public static string GetStringValue(Enum value)
		{
			string output = null;

			Type type = value.GetType();

			FieldInfo fieldInfo = type.GetField(value.ToString());

			StringValueAttribute[] stringAttributes = (StringValueAttribute[])fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false);

			if (stringAttributes.Length > 0)
			{
				output = stringAttributes[0].Value;
			}

			return output;
		} 
	}
}