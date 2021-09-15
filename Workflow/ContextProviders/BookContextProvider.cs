using DataClasses;
using DataClasses.Library;
using Elsa.Activities.Http.Models;
using Elsa.Services;
using Elsa.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Workflow.ContextProviders
{
    /// <summary>
    /// Context Providers are useful for long-running workflows, to facilitate 
    /// loading an entity from the database when a workflow is resumed.
    /// </summary>
    public class BookContextProvider : WorkflowContextRefresher<Book>
    {
        private readonly IDbContextFactory<DataContext> bookContextFactoryFactory;

        public BookContextProvider(IDbContextFactory<DataContext> bookContextFactoryFactory)
        {
            this.bookContextFactoryFactory = bookContextFactoryFactory;
        }

        public override async ValueTask<Book> LoadAsync(LoadWorkflowContext context, CancellationToken cancellationToken = default)
        {
            var bookId = Convert.ToInt32(context.ContextId);
            await using var dbContext = bookContextFactoryFactory.CreateDbContext();
            return await dbContext.Books.AsQueryable().FirstOrDefaultAsync(x => x.Id == bookId, cancellationToken);
        }

        public override async ValueTask<string> SaveAsync(SaveWorkflowContext<Book> context, CancellationToken cancellationToken = default)
        {
            var book = context.Context;
            await using var dbContext = bookContextFactoryFactory.CreateDbContext();
            var dbSet = dbContext.Books;

            if (book == null) // New
            {
                book = ((HttpRequestModel)context.WorkflowExecutionContext.Input!).GetBody<Book>();

                // EF Core should Generate a new ID.

                // Set context.
                context.WorkflowExecutionContext.WorkflowContext = book;
                context.WorkflowExecutionContext.ContextId = book.Id.ToString();

                // Add book to DB.
                await dbSet.AddAsync(book, cancellationToken);
            }
            else // Existing
            {
                var existingBook = await dbSet.AsQueryable().Where(x => x.Id == book.Id).FirstAsync(cancellationToken);

                dbContext.Entry(existingBook).CurrentValues.SetValues(book);
            }

            await dbContext.SaveChangesAsync(cancellationToken);
            return book.Id.ToString();
        }
    }
}
