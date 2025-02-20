using System;

namespace Problems.Analyzers.Sample.RenameContracts.CreateOrder.Request;

public sealed record CreateOrder
{
    public Guid OperationId { get; init; }
    public CreateOrderHeader Header { get; init; } = default!;
};