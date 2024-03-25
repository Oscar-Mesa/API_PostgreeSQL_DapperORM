using NetCoreAPIPostgreSQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIPostgreSQL.Data.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBookDetails(int PKId); 
        Task<Book> InsertBook(Book book);
        Task<Book> UpdateBook(Book book);
        Task<bool> DeleteBook(Book book);
    }
}
