using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CalendarProj.DAO.Models.Enums
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Return string value for enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Return string value defined in attribute enum value</returns>
        public static string GetStringValue(this Enum value)
        {
            Type type = value.GetType();

            FieldInfo fieldInfo = type.GetField(value.ToString());
            // Get the stringvalue attributes
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }
    }
}
