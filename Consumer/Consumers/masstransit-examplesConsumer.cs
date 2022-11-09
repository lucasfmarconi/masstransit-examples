namespace Company.Consumers
{
    using System.Threading.Tasks;
    using MassTransit;
    using Contracts;

    public class masstransit_examplesConsumer :
        IConsumer<masstransit_examples>
    {
        public Task Consume(ConsumeContext<masstransit_examples> context)
        {
            return Task.CompletedTask;
        }
    }
}