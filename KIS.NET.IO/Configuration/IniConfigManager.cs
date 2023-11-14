using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KIS.NET.Core.Parse;
using KIS.NET.IO.Stream;

namespace KIS.NET.IO.Configuration
{
    /// <summary>
    /// A class used to manage configuration files matching
    /// the popular 'ini' style, where each configuration line
    /// looks like the following:
    /// <para />
    /// 'Parameter=Value'.
    /// </summary>
    public class IniConfigManager<TModel> : IConfigManager<TModel> where TModel : new()
    {
        private readonly IParser<Queue<string>, TModel> m_configParser;
        private readonly IReader<IEnumerable<string>> m_reader;
        private readonly IWriter<IEnumerable<string>> m_writer;
        private readonly ITruncator m_truncator;

        public IniConfigManager(
            IParser<IEnumerable<string>, TModel> configParser = null,
            IReader<IEnumerable<string>> reader = null,
            IWriter<IEnumerable<string>> writer = null,
            ITruncator truncator = null)
        {
            m_configParser = configParser ??
                             new IniConfigurationParser<TModel>();
            m_reader = reader ?? new LineReader();
            m_writer = writer ?? new LineWriter();
            m_truncator = truncator ?? new BufferSizeTruncator();
        }

        /// <inheritdoc />
        public TModel LoadConfiguration(string configFilePath = null)
        {
            if (configFilePath == null)
            {
                throw new ArgumentNullException(nameof(configFilePath), "Config file's path can't be null");
            }

            if (string.IsNullOrWhiteSpace(configFilePath))
            {
                throw new ArgumentException("Config file's path can't be empty or contain only white spaces");
            }

            Queue<string> parsableObject;
            using (var inputStream = new FileStream(configFilePath, FileMode.Open, FileAccess.Read))
            {
                parsableObject = new Queue<string>(m_reader.Read(inputStream));
            }

            return m_configParser.Parse(parsableObject);
        }

        /// <inheritdoc />
        public void SaveConfig(TModel configuration, string configFilePath = null)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), "Configuration model can't be null");
            }

            if (configFilePath == null)
            {
                throw new ArgumentNullException(nameof(configFilePath), "Config file's path can't be null");
            }

            if (string.IsNullOrWhiteSpace(configFilePath))
            {
                throw new ArgumentException("Config file's path can't be empty of contain only white spaces",
                    nameof(configFilePath));
            }

            var data = configuration.GetType()
                .GetProperties()
                .Select(property => new { property, obj = property.GetValue(configuration, null) })
                .Select(@t => @t.property.Name + "=" + @t.obj).ToList();

            using (var outputStream = new MemoryStream())
            {
                m_writer.Write(outputStream, data);
                using (var iOutputStream2 = new FileStream(configFilePath, FileMode.Create, FileAccess.Write))
                {
                    m_truncator.Truncate(iOutputStream2, outputStream.ToArray());
                    iOutputStream2.Flush(true);
                }
            }
        }
    }
}