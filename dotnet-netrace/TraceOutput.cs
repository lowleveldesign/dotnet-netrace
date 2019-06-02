using System;

namespace LowLevelDesign.NTrace
{
    internal interface ITraceOutput
    {
        void Write(double timeStampRelativeInMSec, int processId, int threadId, string eventName, string details);
    }

    internal class ConsoleTraceOutput : ITraceOutput
    {
        private readonly string eventNameFilter;

        public ConsoleTraceOutput(string eventNameFilter)
        {
            this.eventNameFilter = eventNameFilter;
        }

        public void Write(double timeStampRelativeInMSec, int processId, int threadId, string eventName, string details)
        {
            if (eventNameFilter == null || 
                eventName.IndexOf(eventNameFilter, StringComparison.OrdinalIgnoreCase) >= 0) {
                Console.WriteLine($"{timeStampRelativeInMSec:0.0000} ({processId}.{threadId}) {eventName} {details}");
            }
        }
    }
}
