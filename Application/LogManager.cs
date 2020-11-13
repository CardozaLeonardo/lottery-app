using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public sealed class LogManager
    {
        private static readonly LogManager _current = new LogManager();

        public static LogManager Current
        {
            get { return _current; }
        }

        public ILog Log
        {
            get
            {
                return log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
        }
    }
}
