﻿using System;
using System.Diagnostics;
using Newtonsoft.Json;
using NServiceBus.Logging;
using NServiceBus.Saga;

#region CustomLogger
public class CustomLogger
{
    static ILog logger = LogManager.GetLogger<CustomLogger>();

    public IDisposable StartTimer(string name)
    {
        return new Log(name);
    }

    public void WriteSaga(IContainSagaData sagaData)
    {
        var serialized = JsonConvert.SerializeObject(sagaData, Formatting.Indented);
        logger.Warn("Saga State: \r\n" + serialized);
    }

    class Log : IDisposable
    {
        string name;
        Stopwatch stopwatch;

        public Log(string name)
        {
            stopwatch = Stopwatch.StartNew();
            this.name = name;
        }

        public void Dispose()
        {
            logger.Warn($"{name} took {stopwatch.ElapsedMilliseconds}ms to process");
        }
    }
}

#endregion
