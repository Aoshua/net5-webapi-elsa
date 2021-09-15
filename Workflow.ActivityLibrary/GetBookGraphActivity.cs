using DataAccess;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Expressions;
using Elsa.Services;
using Elsa.Services.Models;
using System.Threading.Tasks;

namespace Workflow.ActivityLibrary
{
    [Activity(DisplayName = "Get Books Graph", Description = "Testing getting a book and related objects.", Category = "Custom Activities")]
    public class GetBookGraphActivity : Activity
    {
        private readonly LibraryStore libraryStore;

        [ActivityInput(Hint = "The books's ID.", DefaultSyntax = SyntaxNames.JavaScript, SupportedSyntaxes = new string[] { SyntaxNames.JavaScript })]
        public int BookId { get; set; }

        public GetBookGraphActivity(LibraryStore libraryStore)
        {
            this.libraryStore = libraryStore;
        }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            var book = await libraryStore.GetBookGraph(BookId);
            return Done(book);
        }
    }
}
