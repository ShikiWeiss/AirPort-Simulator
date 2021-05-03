using System;
using System.Collections.Generic;
using System.Text;

namespace Airport.Utilities.Api
{
    /// <summary>
    /// Interface for ErrorLoggers.
    /// Each ErrorLogger should implement it.
    /// </summary>
    public interface IErrorLogger : IMessageLogger
    {
    }
}
