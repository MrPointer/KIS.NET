using System.Xml.Linq;

namespace KIS.NET.Core.Parse
{
    /// <summary>
    /// An abstract class representing a XML parser, used to parse DOM-trees in the form of <see cref="T:System.Xml.Linq.XElement" /> objects.
    /// </summary>
    /// <typeparam name="T">Type of the object that stores the parsed information.</typeparam>
    public interface IXmlParser<out T> : IParser<XElement, T> where T : new()
    {
    }
}