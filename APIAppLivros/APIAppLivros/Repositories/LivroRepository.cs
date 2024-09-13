using LivrosAPI.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace LivrosAPI.Repositories
{
    public class LivroRepository
    {
        private readonly string _connectionString;

        public LivroRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private IDbConnection Connection =>
            new MySqlConnection(_connectionString);

        public async Task<IEnumerable<Livro>> ListarTodosLivros()
        {
            var sql = "SELECT * FROM livros";
            using (var conn = Connection)
            {
                return await conn.QueryAsync<Livro>(sql);
            }
        }

        public async Task<Livro> BuscarPorId(int id)
        {
            var sql = "SELECT * FROM livros WHERE Id = @Id";
            using (var conn = Connection)
            {
                return await conn.QueryFirstOrDefaultAsync<Livro>(sql, new { Id = id });
            }
        }

        public async Task<Livro> BuscarPorTitulo(string titulo)
        {
            var sql = "SELECT * FROM livros WHERE Titulo = @Titulo";
            using (var conn = Connection)
            {
                return await conn.QueryFirstOrDefaultAsync<Livro>(sql, new { Titulo = titulo });
            }
        }

        public async Task<int> DeletarPorId(int id)
        {
            var sql = "DELETE FROM livros WHERE Id = @Id";
            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<int> Salvar(Livro dados)
        {
            var sql = "INSERT INTO livros (Titulo, Autor, AnoPublicacao, Genero, NumeroPaginas) " +
                      "VALUES (@Titulo, @Autor, @AnoPublicacao, @Genero, @NumeroPaginas)";
            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql, dados);
            }
        }

        public async Task<int> Atualizar(Livro dados)
        {
            var sql = "UPDATE livros SET Titulo = @Titulo, Autor = @Autor, AnoPublicacao = @AnoPublicacao, " +
                      "Genero = @Genero, NumeroPaginas = @NumeroPaginas WHERE Id = @Id";
            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql, dados);
            }
        }
    }
}