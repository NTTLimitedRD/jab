using System.Linq;
using Xunit.Abstractions;
namespace jab.Extensions
{

    public class ActiveFactMessageBus : IMessageBus
    {
        readonly IMessageBus innerBus;

        public ActiveFactMessageBus(IMessageBus innerBus)
        {
            this.innerBus = innerBus;
        }

        public int DynamicallySkippedTestCount { get; private set; }

        public void Dispose() { }

        public bool QueueMessage(IMessageSinkMessage message)
        {
            var testFailed = message as ITestFailed;
            if (testFailed != null)
            {
                var exceptionType = testFailed.ExceptionTypes.FirstOrDefault();
                if (exceptionType == typeof(ActiveTestException).FullName)
                {
                    DynamicallySkippedTestCount++;
                    return innerBus.QueueMessage(new TestSkipped(testFailed.Test, testFailed.Messages.FirstOrDefault()));
                }
            }

            // Nothing we care about, send it on its way
            return innerBus.QueueMessage(message);
        }
    }
}
