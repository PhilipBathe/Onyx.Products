using Onyx.Products.WebService.Core;

namespace Onyx.Products.WebService.Database;

public class ProductData : IProductData
{
    private readonly string _connectionString;

    public ProductData(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ProductConnectionString")!;
    }

    public async Task<Product> CreateAsync(NewProductRequest newProductRequest)
    {
        var sql = @"INSERT INTO [dbo].[Product] (
                        [Name]
                        , [Description]
                        , [Price]
                        , [ColourId]
                        , [CreatedBy]
                    )
                    OUTPUT INSERTED.[Guid]
                    VALUES (
                        @name
                        , @description
                        , @price
                        , @colourId
                        , @createdBy
                    )
                    ";

        using (var conn = new SqlConnection(_connectionString))
        {
            var guid = await conn.ExecuteScalarAsync<Guid>(sql, newProductRequest);
            return await GetByIdAsync(guid);
        }
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return GetByColourIdAsync(-1);
    }

    public async Task<IEnumerable<Product>> GetByColourIdAsync(int colourId)
    {
        var sql = @"SELECT 
                        p.[Guid]
                        , p.[Name]
                        , p.[Description]
                        , p.[Price]
                        , p.[ColourId]
                        , c.[Name] AS ColourName
                    FROM [dbo].[Product] p
                    INNER JOIN [dbo].[Colour] c ON c.[Id] = p.[ColourId]
                    WHERE 
                        (p.[ColourId] = @colourId OR @colourId = -1)
                    ";

        using (var conn = new SqlConnection(_connectionString))
        {
            return await conn.QueryAsync<Product>(sql, new { colourId });
        }
    }

    public async Task<Product?> GetByIdAsync(Guid guid)
    {
        var sql = @"SELECT 
                        p.[Guid]
                        , p.[Name]
                        , p.[Description]
                        , p.[Price]
                        , p.[ColourId]
                        , c.[Name] AS ColourName
                    FROM [dbo].[Product] p
                    INNER JOIN [dbo].[Colour] c ON c.[Id] = p.[ColourId]
                    WHERE p.[Guid] = @guid
                    ";

        using (var conn = new SqlConnection(_connectionString))
        {
            return await conn.QuerySingleOrDefaultAsync<Product>(sql, new { guid });
        }
    }
}
