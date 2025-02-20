using System.Collections.Generic;

namespace Problems.Analyzers.Sample.RenameContracts.CreateOrder.Response;

public record CreateOrder
{
    public long Id { get; init; }
    public IReadOnlyCollection<Line>? Lines { get; init; }
}