# RealtimeLogger

[![Build status](https://ci.appveyor.com/api/projects/status/f0600g4uqusbkvah?svg=true)](https://ci.appveyor.com/project/jpdillingham/realtimelogger)
[![codecov](https://codecov.io/gh/jpdillingham/RealtimeLogger/branch/master/graph/badge.svg)](https://codecov.io/gh/jpdillingham/RealtimeLogger)
[![NuGet version](https://badge.fury.io/nu/NLog.RealtimeLogger.svg)](https://badge.fury.io/nu/NLog.RealtimeLogger)

A C# class that works in conjunction with the NLog 'MethodCall' logging target to expose log messages via an event in real time.  

## Usage

### NLog Configuration

Copy the RealtimeLogger.cs file into your project and rename the namespace to fit your application.  In the example below we'll pretend we named it ```MyNamespace```.

In your NLog configuration, add the following target:

```xml
<target name="method" xsi:type="MethodCall" className="MyNamespace.RealtimeLogger, MyNamespace" methodName="AppendLog">
  <parameter layout="${threadid}"/>
  <parameter layout="${longdate}"/>
  <parameter layout="${level}"/>
  <parameter layout="${logger}"/>
  <parameter layout="${message} ${exception:format=tostring}"/>
</target>
```

Note that in the ```className``` attribute, the fully qualified name of the ```RealtimeLogger``` class is used.  This includes the namespace, which in this case is ```MyNamespace```.  The second part of the attribute must be set to the name of your assembly.  This will be defined in ```Properties\AssemblyInfo.cs``` within the ```AssemblyTitle``` attribute.

Next, add a logger for the method call to your NLog configuration:

```xml
<logger name="*" minlevel="Info" writeTo="method"/>
```

Change the ```minlevel``` attribute to whichever logging level you'd like to catch.

When you are finished, your NLog configuration should look something like this:

```xml
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd" 
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
    autoReload="true" 
    throwExceptions="false" 
    internalLogLevel="Off" 
    internalLogFile="c:\temp\nlog-internal.log"
  >
  <targets>
    <target name="method" xsi:type="MethodCall" className="Example.RealtimeLogger, Example" methodName="AppendLog">
	  <parameter layout="${threadid}"/>
      <parameter layout="${longdate}"/>
      <parameter layout="${level}"/>
      <parameter layout="${logger}"/>
      <parameter layout="${message} ${exception:format=tostring}"/>
    </target>
  </targets>
  <rules>
    <logger name="*" minlevel="Info" writeTo="method"/>
  </rules>
</nlog>
```

### Event Handler

Next, create a method that can be bound to the RealtimeLogger's ```LogArrived``` event, like so:

```c#
private void AppendLog(object sender, RealtimeLoggerEventArgs e)
{
  Console.WriteLine("New log: " + e.Message);
}
```

Bind this event handler to the ```LogArrived``` event:

```c#
RealtimeLogger.LogArrived += AppendLog;
```

Generally you'll want to put the statement above in Main().  Note that any number of handlers can be added.

That's it!  Your handler should be invoked each time a new log is added to the logger.

## RealtimeLoggerEventArgs

The properties of the event arguments ```RealtimeLoggerEventArgs``` for the ```LogArrived``` event are as follows:

Property | Type | Description
--- | --- | ---
ThreadID | int | The ID of the tread from which the log message originated.
DateTime | DateTime | The DateTime corresponding to the instant the log was added to the logger.
Level | LogLevel | The logging level used to add the log to the logger.
Logger | string | The name of the logger.
Message | string | The log message.

Note that the constructor for ```RealtimeLoggerEventArgs``` parses the ```ThreadID```, ```DateTime``` and ```LogLevel``` passed from NLog from strings.  If parsing fails the code will prefix the log message with an error before adding it to the log queue.

If the specified ```ThreadID``` value from NLog fails to be parsed, the ThreadID property will be set to ```0``` and the following message will be prepended to the log message:

```[Invalid ThreadID; substituted with default]```

If the specified ```DateTime``` value from NLog fails to be parsed, the DateTime property will be set to ```DateTime.Now``` and the following message will be prepended to the log message:

```[Invalid DateTime; substituted with DateTime.Now]```

The DateTime property for the log message will also be set to DateTime.Now.

If the specified ```LogLevel``` fails to be parsed, the LogLevel property will be set to ```LogLevel.Info``` and the following message will be prepended to the log message:

```[Invalid LogLevel; substituted with LogLevel.Info]```

## Log Queue

A queue of log messages is provided and may be accessed using the ```LogHistory``` property of type ```Queue<RealtimeLoggerEventArgs>```.  Logs will accumulate in the queue until it reaches the size specified in the ```LogHistoryLimit``` property, after which point logs will be dequeued when new logs are enqueued.

The variable ```logHistoryLimit``` is used to initialize the value of the ```LogHistoryLimit``` property.  This can be changed by editing the class, or ```LogHistoryLimit``` can be changed programmatically at runtime.

# Notes

If the ```async``` option is used for the logging target the log event will not fire precisely in real time.  It would be a good idea to use the ```AsyncWrapper``` functionality described [here](https://github.com/nlog/NLog/wiki/AsyncWrapper-target) to limit the asynchronous execution to only those targets that need it, rather than adding the ```async``` attribute to the ```<targets>``` node of the config file.

## configuration

Create and modify the NLog configuration variable "RealtimeLogger.LogHistoryLimit" to change the number of logs saved in the log history.  The default is 300.

```xml
  <variable name="RealtimeLogger.LogHistoryLimit" value="300"/>
```