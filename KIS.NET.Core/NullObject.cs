using System;

namespace KIS.NET.Core
{
    /// <summary>
    /// A class following the Null-object design pattern,
    /// used instead of 'null' where appropriate -
    /// Creating a more readable and better maintained code design. <br />
    /// Note that this object simply inherits <see cref="T:System.Object" />,
    /// without adding any additional members.
    /// <remarks>
    /// This class implements a sort of a hybrid between the Singleton design pattern
    /// and the Factory design pattern - It stores a cached instance of the object,
    /// lazily instantiated, yet gives access to it through the <see cref="M:KIS.NET.Core.NullObject.Create" /> method,
    /// instead of a classic getter.
    /// </remarks>
    /// </summary>
    public class NullObject
    {
        private static readonly Lazy<NullObject> srm_CachedInstance;

        /// <summary>
        /// Creates a <see cref="T:KIS.NET.Core.NullObject" /> if necessary and returns a reference to it. <br />
        /// This method attempts to lazily instantiate the object on the first call
        /// to it, returning a cached instance afterward to save memory.
        /// </summary>
        /// <returns>Reference to the created object.</returns>
        public static NullObject Create()
        {
            return srm_CachedInstance.Value;
        }

        static NullObject()
        {
            srm_CachedInstance = new Lazy<NullObject>(() => new NullObject());
        }
    }
}