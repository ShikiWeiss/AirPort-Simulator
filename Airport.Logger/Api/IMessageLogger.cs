using System;
using System.Collections.Generic;
using System.Text;

namespace Airport.Utilities.Api
{
    /// <summary>
    /// Interface for loggers. each logger should implement it.
    /// </summary>
    public interface IMessageLogger
    {
        /// <summary>
        /// Call to loger and Log the message into the appropriate loggerType.
        /// </summary>
        /// <param name="message"></param>
        void Log(string message);
    }
}
