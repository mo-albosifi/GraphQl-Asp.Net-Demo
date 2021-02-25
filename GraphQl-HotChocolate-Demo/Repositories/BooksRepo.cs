using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Entities;
using GraphQl_HotChocolate_Demo.DataBase;
using Microsoft.EntityFrameworkCore;

namespace GraphQl_HotChocolate_Demo.Repositories
{

    public interface IBookRepo
    {
       Task< List<Book>> GetAll();
        Book GetById(int bookId);
        Task<Book> AddNew(Book book);
    }
    public class BooksRepo : IBookRepo
    {
        private List<Book> _books = new List<Book>();
        private readonly GraphQlDemoDbContext _dbContext;

        public BooksRepo(GraphQlDemoDbContext dbContext)
        {
            InitData();
            _dbContext = dbContext;
        }

        
        public async Task<List<Book>> GetAll()
        {
            return _books;
        }

        public Book GetById(int bookId)
        {
            return _books   
                .FirstOrDefault(e => e.PagesCount == bookId);
            
        }

        public async Task<Book> AddNew(Book book)
        {
            var newBook = new Book()
            {
                Name = book.Name,
                About = book.About,
                PagesCount = book.PagesCount
            };
            _books.Add(newBook);
            return newBook;
        }
        
        
        void InitData()
        {
            _books.Add(new Book()
            {
                Name = "Book1",
                About = "about1",
                PagesCount = 1
            });
            
            _books.Add(new Book()
            {
                Name = "Book2",
                About = "about2",
                PagesCount = 2
            });
        }
    }
}