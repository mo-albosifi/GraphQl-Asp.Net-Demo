using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.Entities;
using GraphQl_HotChocolate_Demo.Repositories;
using HotChocolate;
using HotChocolate.Subscriptions;

namespace GraphQl_HotChocolate_Demo.GraphQlServices
{
    public class BookQuery
    {
        
        public async Task<List<Book>> AllBooks([Service] BooksRepo booksRepo) =>
            await booksRepo.GetAll();

        
        public async Task<Book> GetBookById([Service] BooksRepo booksRepo, 
            [Service]ITopicEventSender eventSender, int id)
        {
            Book gottenEmployee = booksRepo.GetById(id);
            await eventSender.SendAsync("ReturnedBook", gottenEmployee);
            return gottenEmployee;
        }           

         
    }
}