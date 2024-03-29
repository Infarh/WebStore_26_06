﻿using System;
using System.Reflection;
using System.Xml;
using log4net;
using log4net.Core;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace WebStore.Logger
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _Log;

        public Log4NetLogger(string CategoryName, XmlElement Configuration)
        {
            var logger_repository = LoggerManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));

            _Log = LogManager.GetLogger(logger_repository.Name, CategoryName);

            log4net.Config.XmlConfigurator.Configure(logger_repository, Configuration);
        }

        public bool IsEnabled(LogLevel Level)
        {
            switch (Level)
            {
                default: throw new ArgumentOutOfRangeException(nameof(Level), Level, null);

                case LogLevel.Trace:
                case LogLevel.Debug:
                    return _Log.IsDebugEnabled;

                case LogLevel.Information:
                    return _Log.IsInfoEnabled;

                case LogLevel.Warning:
                    return _Log.IsWarnEnabled;

                case LogLevel.Error:
                    return _Log.IsErrorEnabled;

                case LogLevel.Critical:
                    return _Log.IsFatalEnabled;

                case LogLevel.None:
                    return false;
            }
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public void Log<TState>(LogLevel Level, EventId Id, TState State, Exception exception, Func<TState, Exception, string> formatter)
        {
            if(!IsEnabled(Level)) return;
            if(formatter is null) throw new ArgumentNullException(nameof(formatter));

            var msg = formatter(State, exception);

            if(string.IsNullOrEmpty(msg) && exception is null) return;

            switch (Level)
            {
                default: throw new ArgumentOutOfRangeException(nameof(Level), Level, null);


                case LogLevel.Trace:
                case LogLevel.Debug:
                    _Log.Debug(msg);
                    break;      

                case LogLevel.Information:
                    _Log.Info(msg);
                    break;

                case LogLevel.Warning:
                    _Log.Warn(msg);
                    break;

                case LogLevel.Error:
                    _Log.Error(msg ?? exception.ToString());
                    break;

                case LogLevel.Critical:
                    _Log.Fatal(msg ?? exception.ToString());
                    break;

                case LogLevel.None:
                    break;
            }
        }
    }
}