using RestFull.Model;
using RestFull.Model.Context;
using RestFull.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestFull.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(MySQLContext context) : base(context) { }

        public Book Available(long id)
        {
            if (!_context.Books.Any(p => p.Id.Equals(id))) return null;
            var user = _context.Books.SingleOrDefault(p => p.Id.Equals(id));
            if (user != null)
            {
                user.Available = false;
                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return user;
        }

        public List<Book> FindByName(string title, string author)
        {
            if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(author))
            {
                return _context.Books.Where(
                    p => p.Title.Contains(title)
                    && p.Author.Contains(author)).ToList();
            }
            else if (string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(author))
            {
                return _context.Books.Where(
                    p => p.Author.Contains(author)).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(author))
            {
                return _context.Books.Where(
                    p => p.Title.Contains(title)).ToList();
            }
            return null;
        }
    }
}
