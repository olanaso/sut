using System;

namespace Sut.Log
{
    public interface ILogService<T> where T : class
    {
        void Error(Exception ex);
        void Info(string msg);
    }
}
