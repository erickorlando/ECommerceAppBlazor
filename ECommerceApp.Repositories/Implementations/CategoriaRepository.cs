using ECommerceApp.DataAccess;
using ECommerceApp.Entities;
using ECommerceApp.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Repositories.Implementations;

public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(ECommerceDbContext context) 
        : base(context)
    {

    }

    public async Task<ICollection<Categoria>> ListAsync()
    {
        var list = new List<Categoria>();
        await using var connection = new SqlConnection(Context.Database.GetConnectionString());
        await using var command = connection.CreateCommand();
        command.CommandText = @"SELECT * FROM Categoria 
                                        WHERE Estado = 1 
                                        ORDER BY 1";

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();
        while (reader.Read())
        {
            list.Add(new Categoria
            {
                Id = reader.GetInt32(0),
                NombreCategoria = reader.GetString(1),
                Estado = reader.GetBoolean(2)
            });
        }

        return list;
    }
}