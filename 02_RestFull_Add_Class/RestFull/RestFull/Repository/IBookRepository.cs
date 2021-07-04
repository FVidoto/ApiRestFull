using RestFull.Model;
using System.Collections.Generic;

namespace RestFull.Repository
{
    public interface IBookRepository : IRepository<Book>
    {
        Book Available(long id);
        List<Book> FindByName(string title, string author);
    }
}
