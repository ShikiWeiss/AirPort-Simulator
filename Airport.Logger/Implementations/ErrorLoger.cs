using Airport.Utilities.Api;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport.Utilities.Implementations
{
    public class ErrorLoger : IErrorLogger
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public void Log(string message) => logger.Error(message);
    }
}
