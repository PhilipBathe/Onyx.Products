namespace Onyx.Products.WebService.Database;

public class ColourData : IColourData
{
    private readonly string _connectionString;

    public ColourData(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ProductConnectionString")!;
    }

    public async Task<IDictionary<int, string>> GetAllAsync()
    {
        var sql = @"SELECT 
                        [Id] AS [Key]
                        , [Name] AS [Value]
                    FROM [dbo].[Colour]
                    ";

        using (var conn = new SqlConnection(_connectionString))
        {
            return (await conn.QueryAsync<KeyValuePair<int, string>>(sql))
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }

    public async Task<string?> GetByIdAsync(int id)
    {
        var sql = @"SELECT [Name]
                    FROM [dbo].[Colour]
                    WHERE [Id] = @id
                    ";

        using (var conn = new SqlConnection(_connectionString))
        {
            return await conn.QuerySingleOrDefaultAsync<string>(sql, new { id });
        }
    }
}
