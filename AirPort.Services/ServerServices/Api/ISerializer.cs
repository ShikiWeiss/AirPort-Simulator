using System;
using System.Collections.Generic;
using System.Text;

namespace AirPort.Services.ServerServices.Api
{
    public interface ISerializer
    {
        /// <summary>
        /// Method for serialization objects.
        /// Return the object as string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectGraph"></param>
        /// <returns></returns>
        string Serialize<T>(T objectGraph);

        /// <summary>
        /// Method for deserialization objects.
        /// Get string and return T object
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public T Deserialize<T>(string json);
    }
}
