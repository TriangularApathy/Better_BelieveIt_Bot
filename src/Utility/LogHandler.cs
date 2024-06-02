using Discord;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better_BelieveIt_Bot.Utility {
    internal static class LogHandler {
        public static Task OnLogAsync(ILogger logger, LogMessage message) {
            switch (message.Severity) {
                case LogSeverity.Debug:
                    logger.LogInformation("{Message}",message.ToString());
                    break;
                case LogSeverity.Verbose:
                    logger.LogInformation("{Message}", message.ToString());
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
