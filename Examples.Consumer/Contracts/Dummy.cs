namespace Examples.Consumer.Contracts;

public record Dummy
{
    public string Value { get; init; }
    public string Offset { get; set; }
}