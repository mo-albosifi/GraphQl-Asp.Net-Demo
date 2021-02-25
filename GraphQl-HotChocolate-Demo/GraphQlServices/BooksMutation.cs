using System.Threading.Tasks;
using DataBase.Entities;
using GraphQl_HotChocolate_Demo.Repositories;
using HotChocolate;
using HotChocolate.Subscriptions;

namespace GraphQl_HotChocolate_Demo.GraphQlServices
{
    public class BooksMutation
    {
        public async Task<Book> CreateBook([Service] BooksRepo repo,
            [Service]ITopicEventSender eventSender, string name,int pagesCount,string about )
        {
            var newDepartment = new Book()
            {
                Name = name,
                About = about,
                PagesCount = pagesCount
            };
            var createdDepartment = await repo.AddNew(newDepartment);

            await eventSender.SendAsync("BookCreated", createdDepartment);

            
            return createdDepartment;            
        }
    }
}