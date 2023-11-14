namespace KIS.NET.IO.Configuration
{
    /// <summary>
    /// An interface declaring methods to manage a configuration model against a file on the file system.
    /// <para />
    /// This interface, as opposed to <see cref="T:KIS.NET.IO.Configuration.ISettingsManager" />,
    /// is used to manage instance classes of various configuration models.
    /// </summary>
    /// <typeparam name="TModel">Type of the configuration model.</typeparam>
    public interface IConfigManager<TModel> where TModel : new()
    {
        /// <summary>Loads a configuration model from the given path.</summary>
        /// <param name="configFilePath">Path to the configuration file that should be loaded.</param>
        /// <returns>Reference to a new <see cref="!:TModel" /> object loaded from the config file.</returns>
        TModel LoadConfiguration(string configFilePath = null);

        /// <summary>
        /// Saves the given configuration model to the given path.
        /// </summary>
        /// <param name="configuration">Configuration model to save.</param>
        /// <param name="configFilePath">Path to the configuration file.
        /// Defaults to null in case a default path is to be used.</param>
        void SaveConfig(TModel configuration, string configFilePath = null);
    }
}