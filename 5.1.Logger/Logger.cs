
using Serilog;
using Serilog.Events;

namespace _5._1.Logger
{
    public static class Logger
    {
        
        public static Serilog.ILogger GetLogger(){

          
            //Serilog - Config sem appsettings - metodo de log com template
            Log.Logger = new LoggerConfiguration()
                         .MinimumLevel.Debug()
                         .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                         .MinimumLevel.Override("System", LogEventLevel.Error)
                         .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                         .Enrich.FromLogContext()
                         .Enrich.WithProperty("System", "ZX - API - Parametros de Apuração ")
                         .WriteTo
                         .Console(
                                  outputTemplate: "{{" +
                                                  " System: {System}," +
                                                  " Timestamp:{Timestamp:HH:mm:ss}," +
                                                  " TraceId: {TraceId}," +
                                                  " SpanId: {SpanId}," +
                                                  " RequestId: {RequestId}," +
                                                  " CorrelationId: {CorrelationId}," +
                                                  " Level: {Level}," +
                                                  " Context: {SourceContext}," +
                                                  " Exception: {Exception}," +
                                                  " Message: {Message:lj}," +
                                                  " Enviroment: {Enviroment}," +
                                                  "" +
                                                  "}," +
                                                  "{NewLine}"
                                 )
                         .CreateLogger();

            return Log.Logger;

        }



    }
}