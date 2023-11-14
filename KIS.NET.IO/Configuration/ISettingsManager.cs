namespace KIS.NET.IO.Configuration
{
    /// <summary>
    /// An interface declaring methods to wrap IO operations exclusively for an application-settings manager.
    /// </summary>
    public interface ISettingsManager
    {
        /// <summary>
        /// Loads settings from the file with the given path, or from the default path if none is specified.
        /// <para />
        /// Loaded settings will then be stored in a global class.
        /// </summary>
        /// <param name="settingsFilePath">Path of the settings file. Can be null if default path should be used.</param>
        void LoadSettings(string settingsFilePath = null);

        /// <summary>
        /// Saves settings to a file with the given path, or to the default path if none is given.
        /// </summary>
        /// <param name="settingsFilePath">Path of the settings file. Can be null if default path should be used.</param>
        void SaveSettings(string settingsFilePath = null);

        /// <summary>
        /// Saves the default settings to a file with the given path, or to the default path if none is given.
        /// </summary>
        /// <param name="settingsFilePath">Path of the settings file. Can be null if default path should be used.</param>
        void SaveDefaultSettings(string settingsFilePath = null);
    }
}