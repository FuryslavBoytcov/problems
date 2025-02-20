namespace Problems.Analyzers.Sample.RenameContracts.CreateOrder.Request;

public sealed record CreateOrderHeader
{
    public long Id { get; init; }
    public CreateOrderHeaderParameters Parameters { get; init; } = default!;
}