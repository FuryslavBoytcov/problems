using System.Text.RegularExpressions;
using Xunit;

namespace Problems.DotnetChallenges;

public sealed class FindColumnToPropertyMappingsTemplate
{
    public static string CreateExpressionPattern()
    {
        return @"";
    }


    [Fact]
    public void Test()
    {
        var text = """""
                       public async ValueTask<AccountContract?> Find(string code, CancellationToken token)
                       {
                           ArgumentException.ThrowIfNullOrWhiteSpace(code);

                           var select = new CommandDefinition(
                               """
                               SELECT
                                   id AS id, 
                                   code AS code, 
                                   payer_account_number AS payeraccountnumber,
                                   legal_entity AS legalentity
                               FROM read.account_contracts
                               WHERE UPPER(code) = @code
                               ORDER BY batch_id DESC;
                               """,
                               new {code = code.ToUpperInvariant()},
                               cancellationToken: token,
                               flags: CommandFlags.None);

                           await using var connection = _connectionFactory.GetConnection();
                           return await connection.QueryFirstOrDefaultAsync<AccountContract?>(select);
                       }
                       
                       public async ValueTask<IReadOnlyCollection<AuthorityDocument>> Get(
                           IAuthorityDocumentsFilter filter,
                           CancellationToken token)
                       {
                           ArgumentNullException.ThrowIfNull(filter);

                           using var queryFactory = _queryFactoryProvider.GetQueryFactory();

                           var query = queryFactory
                               .Query("read.authority_documents")
                               .Select(
                                   "id AS id",
                                   "name AS name",
                                   "description AS description",
                                   "date AS date",
                                   "signer_full_name AS signerfullname",
                                   "regional_business_unit AS regionalbusinessunit")
                               .When(
                                   !String.IsNullOrWhiteSpace(filter.Search),
                                   f => f.WhereLike("name", $"%{filter.Search}%"))
                               .When(
                                   !String.IsNullOrWhiteSpace(filter.RegionalBusinessUnit),
                                   f => f.WhereRaw(
                                       "regional_business_unit = ?",
                                       filter.RegionalBusinessUnit!.Trim().ToUpperInvariant()));

                           var result = await query.GetAsync<AuthorityDocument>(cancellationToken: token);
                           return result.ToArray();
                       }

                       public async ValueTask<bool> Exists(string name, string regionalBusinessUnit, CancellationToken token)
                       {
                           ArgumentException.ThrowIfNullOrWhiteSpace(name);
                           ArgumentException.ThrowIfNullOrWhiteSpace(regionalBusinessUnit);

                           var select = new CommandDefinition(
                               @"
                   SELECT true
                   FROM read.authority_documents
                   WHERE UPPER(name) = @name AND UPPER(regional_business_unit) = @rbu;",
                               new
                               {
                                   name = name.ToUpperInvariant(),
                                   rbu = regionalBusinessUnit.ToUpperInvariant()
                               },
                               cancellationToken: token,
                               flags: CommandFlags.None);

                           await using var connection = _connectionFactory.GetConnection();
                           return await connection.ExecuteScalarAsync<bool>(select);
                       }
                   """"";

        var pattern = CreateExpressionPattern();
        var matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);

        Assert.Equal(10, matches.Count);
        Assert.Equal("id", matches[0].Groups["column"].Value);
        Assert.Equal("id", matches[0].Groups["property"].Value);
        Assert.Equal("code", matches[1].Groups["column"].Value);
        Assert.Equal("code", matches[1].Groups["property"].Value);
        Assert.Equal("payer_account_number", matches[2].Groups["column"].Value);
        Assert.Equal("payeraccountnumber", matches[2].Groups["property"].Value);
        Assert.Equal("legal_entity", matches[3].Groups["column"].Value);
        Assert.Equal("legalentity", matches[3].Groups["property"].Value);
        Assert.Equal("id", matches[4].Groups["column"].Value);
        Assert.Equal("id", matches[4].Groups["property"].Value);
        Assert.Equal("name", matches[5].Groups["column"].Value);
        Assert.Equal("name", matches[5].Groups["property"].Value);
        Assert.Equal("description", matches[6].Groups["column"].Value);
        Assert.Equal("description", matches[6].Groups["property"].Value);
        Assert.Equal("date", matches[7].Groups["column"].Value);
        Assert.Equal("date", matches[7].Groups["property"].Value);
        Assert.Equal("signer_full_name", matches[8].Groups["column"].Value);
        Assert.Equal("signerfullname", matches[8].Groups["property"].Value);
        Assert.Equal("regional_business_unit", matches[9].Groups["column"].Value);
        Assert.Equal("regionalbusinessunit", matches[9].Groups["property"].Value);
    }
}