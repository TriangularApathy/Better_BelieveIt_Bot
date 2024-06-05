using Discord;
using Microsoft.Extensions.Logging;

namespace Better_BelieveIt_Bot.Utility {
    internal static class LogHandler {
        public static Task OnLogAsync(ILogger logger, LogMessage message) {
            switch (message.Severity) {
                case LogSeverity.Debug:
                    logger.LogDebug("{Message}", message.ToString());
                    break;
                case LogSeverity.Verbose:
                    logger.LogTrace("{Message}", message.ToString());
                    break;
                case LogSeverity.Info:
                    logger.LogInformation("{Message}", message.ToString());
                    break;
                case LogSeverity.Warning:
                    logger.LogWarning("{Message}", message.ToString());
                    break;
                case LogSeverity.Error:
                    logger.LogError("{Message}", message.ToString());
                    break;
                case LogSeverity.Critical:
                    logger.LogCritical("{Message}", message.ToString());
                    break;
            }
            return Task.CompletedTask;
        }
    }
}
