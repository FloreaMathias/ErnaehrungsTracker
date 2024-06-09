using Serilog.Core;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErnaehrungsTracker;

public class LoggerClass
{
    public static Logger logger { get; private set; } = new LoggerConfiguration().WriteTo.File("Log.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7).CreateLogger();
}