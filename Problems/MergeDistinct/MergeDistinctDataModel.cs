namespace Problems.MergeDistinct;

public sealed record MergeDistinctDataModel(
    long Id,
    string Title,
    decimal Price,
    string UniqueExternalId1,
    long UniqueExternalId2,
    int UniqueExternalId3,
    int UniqueExternalId4);