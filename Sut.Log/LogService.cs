using System;

namespace Sut.Log
{
    public class LogService<T> : ILogService<T> where T : class
    {
        private NLog.ILogger _logger { get; set; }

        public LogService()
        {
            _logger = NLog.LogManager.GetLogger(typeof(T).Name);
        }

        public void Error(Exception ex)
        {
            _logger.Error(ex.ToString());
        }

        public void Info(string msg)
        {
            _logger.Info(msg);
        }
    }
}
