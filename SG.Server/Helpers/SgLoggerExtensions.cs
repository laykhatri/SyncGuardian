using Serilog;
using System.Runtime.CompilerServices;

namespace SG.Server.Helpers
{
    internal static class SgLoggerExtensions
    {
        internal static void LogSgInfo(this ILogger logger, string message, [CallerMemberName] string? caller = null)
        {
            logger.Information($"[Info] - [{caller} : {message}");
        }

        internal static void LogSgWarning(this ILogger logger, string message, [CallerMemberName] string? caller = null)
        {
            logger.Warning($"[Warning] - [{caller} : {message}");
        }

        internal static void LogSgError(this ILogger logger, string message, [CallerMemberName] string? caller = null)
        {
            logger.Error($"[Error] - [{caller} : {message}");
        }

        internal static void LogSgDebug(this ILogger logger, string message, [CallerMemberName] string? caller = null)
        {
            logger.Debug($"[Debug] - [{caller} : {message}");
        }

        internal static void LogSgVerbose(this ILogger logger, string message, [CallerMemberName] string? caller = null)
        {
            logger.Verbose($"[Verbose] - [{caller} : {message}");
        }

        internal static void LogSgFatal(this ILogger logger, string message, [CallerMemberName] string? caller = null)
        {
            logger.Fatal($"[Fatal] - [{caller} : {message}");
        }

        internal static void LogSgCustom(this ILogger logger, string type, string message, [CallerMemberName] string? caller = null)
        {
            logger.Debug($"[{type}] - [{caller} : {message}");
        }
    }
}
