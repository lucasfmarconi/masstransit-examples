namespace Company.Consumers
{
    using MassTransit;

    public class masstransit_examplesConsumerDefinition :
        ConsumerDefinition<masstransit_examplesConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<masstransit_examplesConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}