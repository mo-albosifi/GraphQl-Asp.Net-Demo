using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Entities;
using GraphQl_HotChocolate_Demo.DataBase;
using GraphQl_HotChocolate_Demo.Repositories;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;

namespace GraphQl_HotChocolate_Demo.GraphQlServices
{
    public class BookQuery
    {
        
        // -----------------------------------------------------------
        // use Integration with EF Core
        // dotnet add package HotChocolate.Data.EntityFramework
        // [UseDbContext(typeof(GraphQlDemoDbContext))]
        // // services.AddPooledDbContextFactory<SomeDbContext>(b => b /*your configuration */)
        // public IQueryable<Book> GetUsers([ScopedService] GraphQlDemoDbContext someDbContext)
        // {
        //     return someDbContext.Books;
        // }
        //
        // -----------------------------------------------------------
        
        
        [GraphQLNonNullType]
        public async Task<List<Book>> AllBooks([Service] IBookRepo booksRepo) =>
            await booksRepo.GetAll();

        
        public async Task<Book> GetBookById([Service] IBookRepo booksRepo, 
            [Service]ITopicEventSender eventSender, int id)
        {
            Book gottenEmployee = booksRepo.GetById(id);
            await eventSender.SendAsync("ReturnedBook", gottenEmployee);
            return gottenEmployee;
        }           

         
    }
}