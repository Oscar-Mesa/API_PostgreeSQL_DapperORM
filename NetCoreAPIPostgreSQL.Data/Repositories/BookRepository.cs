using Dapper;
using NetCoreAPIPostgreSQL.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIPostgreSQL.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private PostgreSQLConfiguration _connectionString;

        public BookRepository(PostgreSQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            var db = dbConnection();

            var sql = @"
                       SELECT ""PKId"", titulo, autor, genero, anio
                       FROM public.""Books"" ";

            return await db.QueryAsync<Book>(sql, new { });
        }

        public async Task<Book> GetBookDetails(int PKId)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT ""PKId"", titulo, autor, genero, anio
                        FROM public.""Books"" 
                        WHERE ""PKId"" = @PKId
                        ";
            return await db.QueryFirstOrDefaultAsync<Book>(sql, new { PKId });
        }

        public async Task<Book> InsertBook(Book book)
        {

            var db = dbConnection();

            var sql = @"
                       INSERT INTO public.""Books"" (titulo, autor, genero, anio)
                       VALUES(@titulo, @autor, @genero, @anio)";
            var result = await db.ExecuteAsync(sql, new { book.titulo, book.autor, book.genero, book.anio });

            return book;  
        }

        public async Task<Book> UpdateBook(Book book)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE public.""Books""
                        SET titulo = @titulo,
                        autor = @autor,
                        genero = @genero,
                        anio = @anio
                        WHERE ""PKId"" = @PKId";

            var result = await db.ExecuteAsync(sql, new { book.titulo, book.autor, book.genero, book.anio, book.PKId });
            return book;
        }

        public async Task<bool> DeleteBook(Book book)
        {
            var db = dbConnection();

            var sql = @"
                       DELETE 
                       FROM public.""Books""
                       WHERE ""PKId"" = @Id";
            var result = await db.ExecuteAsync(sql, new { Id = book.PKId });

            return result > 0;
        }
    }
}
