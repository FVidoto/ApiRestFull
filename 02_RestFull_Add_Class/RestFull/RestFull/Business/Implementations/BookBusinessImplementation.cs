﻿using RestFull.Data.Converter.Implementations;
using RestFull.Data.VO;
using RestFull.Hypermedia.Utils;
using RestFull.Model;
using RestFull.Repository;
using System.Collections.Generic;

namespace RestFull.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {

        private readonly IBookRepository _repository;

        private readonly BookConverter _converter;

        public BookBusinessImplementation(IBookRepository repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        // Method responsible for returning all people,
        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PagedSearchVO<BookVO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from books p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) query = query + $" and p.author like '%{name}%' ";
            query += $" order by p.title {sort} limit {size} offset {offset}";

            string countQuery = @"select count(*) from books p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $" and p.title like '%{name}%' ";

            var books = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<BookVO>
            {
                CurrentPage = page,
                List = _converter.Parse(books),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }

        // Method responsible for returning one book by ID
        public BookVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        public List<BookVO> FindByName(string title, string author)
        {
            return _converter.Parse(_repository.FindByName(title, author));
        }


        // Method responsible to crete one new book
        public BookVO Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Create(bookEntity);
            return _converter.Parse(bookEntity);
        }

        // Method responsible for updating one book
        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Update(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public BookVO Avaliable(long id)
        {
            var bookEntity = _repository.Available(id);
            return _converter.Parse(bookEntity);
        }

        // Method responsible for deleting a book from an ID
        public void Delete(long id)
        {
            _repository.Delete(id);
        }


	}
}

