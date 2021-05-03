using Airport.Utilities.Api;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport.Utilities
{
    public class Logger
    {
        private readonly IMessageLogger logger;

        //ctor
        public Logger(IMessageLogger logger) => this.logger = logger;

        public void Log(string message) => logger.Log(message);

    }
}
