using DataClasses.Library;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Expressions;
using Elsa.Services;
using Elsa.Services.Models;
using System;
using System.Threading.Tasks;
using Workflow.ActivityLibrary.Services;

namespace Workflow.ActivityLibrary
{
    [Activity(DisplayName = "Get Books Graph", Description = "Testing getting a book and related objects.", Category = "Custom Activities")]
    public class GetBookGraphActivity : Activity
    {
        public int BookId { get; set; }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            var response = await HttpService.Client.GetAsync($"library/getbookgraph?id={BookId}");
            var book = await HttpService.ProcessResponse<Book>(response);
            return Done(book);
        }
    }
}
