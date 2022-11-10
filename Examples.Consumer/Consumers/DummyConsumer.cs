using Examples.Consumer.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Examples.Consumer.Consumers;

public class DummyConsumer : IConsumer<Dummy>
{
    ILogger<DummyConsumer> _logger;

    public DummyConsumer(ILogger<DummyConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<Dummy> context)
    {
        var dateReceived = DateTime.Now;
        if (context.Message.Value == null) 
            throw new ArgumentNullException();

        var dummy = context.Message;

        _logger.LogInformation("{timespam}|Message Received -- {@value}",
            dateReceived.ToString("yyyyMMdd'T'HH:mm:ss.fff"), dummy);
        
        Thread.Sleep(5000);

        return context.ConsumeCompleted;
    }
}
