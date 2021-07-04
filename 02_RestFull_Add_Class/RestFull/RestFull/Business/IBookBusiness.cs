using RestFull.Data.VO;
using RestFull.Hypermedia.Utils;
using System.Collections.Generic;

namespace RestFull.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO book);
        BookVO FindByID(long id);
        List<BookVO> FindByName(string title, string author);
        List<BookVO> FindAll();
        PagedSearchVO<BookVO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page);
        BookVO Update(BookVO book);
        BookVO Avaliable(long id);
        void Delete(long id);
    }
}
