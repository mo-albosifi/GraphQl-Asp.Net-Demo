using System.Threading;
using System.Threading.Tasks;
using DataBase.Entities;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;

namespace GraphQl_HotChocolate_Demo.GraphQlServices
{
    public class BookSubscription
    {
        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<Book>> OnBookCreate
        ([Service] ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync<string, Book>("BookCreated", cancellationToken);
        }


        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<Book>> OnBookGet([Service] ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync<string, Book>("ReturnedBook", cancellationToken);
        }
    }
}