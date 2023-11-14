using System;
using System.Collections.Generic;
using System.Reflection;

namespace KIS.NET.Core.Parse
{
    /// <summary>
    /// A class used to parse an ini configuration, assuming it has the format of:
    /// 'PropertyName=PropertyValue'.
    /// <para />
    /// Please note that <see cref="!:TModel" /> MUST have all of its' convertible from a string value.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class IniConfigurationParser<TModel> : IParser<IEnumerable<string>, TModel> where TModel : new()
    {
        /// <inheritdoc />
        public TModel Parse(IEnumerable<string> i_ParsableObject)
        {
            if (i_ParsableObject == null)
                throw new ArgumentNullException(nameof(i_ParsableObject), "Object to parse can't be null");
            var model = new TModel();
            var stringQueue = new Queue<string>(i_ParsableObject);
            while (stringQueue.Count > 0)
            {
                string str = stringQueue.Dequeue();
                int length = str.IndexOf('=');
                string name = str.Substring(0, length);
                string convertedString = str.Substring(length + 1);
                var property = model.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public);
                if (property == null)
                    throw new NullReferenceException("Property " + name + " doesn't exist in " +
                                                     typeof(TModel));
                var propertyType = property.PropertyType;
                try
                {
                    object obj = ConversionUtils.ConvertFromString(convertedString, propertyType);
                    property.SetValue(model, obj, null);
                }
                catch (InvalidCastException ex)
                {
                    throw new InvalidCastException(
                        "Couldn't cast the property value represented as a string to a " + propertyType,
                        ex);
                }
            }

            return model;
        }
    }
}