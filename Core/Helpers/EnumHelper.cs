using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Resources;
using Core.Models;

namespace Core.Helpers
{
     public static class EnumHelper
    {
        public static Dictionary<int, string> List<T>()
        {
            var result = new Dictionary<int, string>();
            foreach (var e in Enum.GetValues(typeof(T)))
            {
                var id = (int) e;
                var name = e.ToString();
                var enm = (T) Enum.ToObject(typeof(T), id);
                var displayValue = GetDisplayValue(enm);
                result.Add(id, string.IsNullOrEmpty(displayValue) ? name : displayValue);
            }

            return result;
        }

        public static List<SelectList> ToList(this Dictionary<int, string> source)
        {
            return source.Select(item => new SelectList(item.Key, item.Value)).ToList();
        }

        public static string GetDisplayValue<T>(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString() ?? string.Empty);

            if (fieldInfo is null) return value.ToString() ?? string.Empty;

            var descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes != null && descriptionAttributes[0].ResourceType != null)
                return LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);
            return descriptionAttributes != null && descriptionAttributes.Length > 0 ? descriptionAttributes[0].Name : value.ToString();
        }

        private static string LookupResource(IReflect resourceManagerProvider, string resourceKey)
        {
            foreach (var staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType != typeof(ResourceManager)) continue;
                var resourceManager = (ResourceManager) staticProperty.GetValue(null, null);
                if (resourceManager != null) return resourceManager.GetString(resourceKey);
            }

            return resourceKey;
        }
    }
}