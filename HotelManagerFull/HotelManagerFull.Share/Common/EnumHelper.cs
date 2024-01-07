using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace HotelManagerFull.Share.Common
{
    /// <summary>
    /// EnumHelper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class EnumHelper<T>
    {
        /// <summary>
        /// GetValues
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IList<T> GetValues(System.Enum value)
        {
            var enumValues = new List<T>();

            foreach (FieldInfo fi in value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                enumValues.Add((T)System.Enum.Parse(value.GetType(), fi.Name, false));
            }
            return enumValues;
        }

        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Parse(string value)
        {
            return (T)System.Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// IList
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IList<string> GetNames(System.Enum value)
        {
            return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }

        /// <summary>
        /// IList
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IList<string> GetDisplayValues(System.Enum value)
        {
            return GetNames(value).Select(obj => GetDisplayValue(Parse(obj))).ToList();
        }

        /// <summary>
        /// LookupResource
        /// </summary>
        /// <param name="resourceManagerProvider"></param>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        private static string LookupResource(Type resourceManagerProvider, string resourceKey)
        {
            foreach (PropertyInfo staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
                {
                    System.Resources.ResourceManager resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
                    if (resourceManager != null) return resourceManager.GetString(resourceKey);
                }
            }

            return resourceKey;
        }

        /// <summary>
        /// GetDisplayValue
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDisplayValue(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString()!);

            if (fieldInfo?.GetCustomAttributes(typeof(DisplayAttribute), false) is DisplayAttribute[] descriptionAttributes && descriptionAttributes.Any())
            {
                if (descriptionAttributes[0].ResourceType != null)
                    return LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

                return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
            }
            else
            {
                return fieldInfo?.Name;
            }
        }

        /// <summary>
        /// GetValueFromName
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetValueFromName(string name)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DisplayAttribute)) as DisplayAttribute;
                if (attribute != null)
                {
                    if (attribute.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == name)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentOutOfRangeException("name");
        }
    }

    /// <summary>
    /// EnumHelper
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// GetValueFromDescription
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T GetValueFromDescription<T>(this string description) where T : System.Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }
            return default(T);
        }

        /// <summary>
        /// GetDescription
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T enumValue)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                return null;
            }

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }
    }
}
