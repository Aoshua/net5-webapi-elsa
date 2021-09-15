<center>
<h1>Elsa 2.0 Cheatsheet</h1>
<h6>Josh Abbott</h6>
</center>

## Getting Started with Elsa 2.0 Designer:
See [official docs](https://elsa-workflows.github.io/elsa-core/docs/next/quickstarts/quickstarts-aspnetcore-server-dashboard-and-api-endpoints).


##  Adding Elsa Server to the Web API:
Note: this is assuming you already have the correct database tables and that you're using SqlServer.
1. `cd` into the main directory of the Web API.
2. Add the following packages:
	```
	dotnet add package Elsa --prerelease 
	dotnet add package Elsa.Activities.Http --prerelease 
	dotnet add package Elsa.Activities.Temporal.Quartz --prerelease 
	dotnet add package Elsa.Persistence.EntityFramework.SqlServer --prerelease 
	dotnet add package Elsa.Server.Api --prerelease
	```
3. Add the following section to your `appsettings.json`:
	```
	"Elsa": {
	    "Http": {
	      "BaseUrl": "http://localhost:44358"
	    },
	    "Smtp": {
	      "Host": "localhost",
	      "Port": "2525"
	    },
	    "Timers": {
	      "SweepInterval": "0:00:00:10"
	    }
	  }
	```
4. In `ConfigureServices()` of your `Startup.cs`, add the following lines of code (Note: must have `UseSqlServer` from Elsa and not  `Microsoft.EntityFrameworkCore`):
	```
    var elsaSection = Configuration.GetSection("Elsa");
	services
	     .AddElsa(elsa => elsa
	         .UseEntityFrameworkPersistence(ef => ef.UseSqlServer(Configuration.GetConnectionString("EXSContext")))
	         .AddConsoleActivities()
	         .AddActivity<YourCustomActivity>()
	         .AddHttpActivities(elsaSection.GetSection("Server").Bind)
	         .AddQuartzTemporalActivities()
	         .AddWorkflowsFrom<Startup>()
	     );

	 services.AddElsaApiEndpoints();
	```
5. Lastly, add `app.UseHttpActivities();` to the `Configure()` method of `Startup.cs`

### Using the Designer with Docker:
* To do this, you first need an API acting as the Elsa server (plus whatever else you need your API to be). That API will be the `ELSA__SERVER__BASEURL` in the next step.
* Run `docker run -t -i -e ELSA__SERVER__BASEURL='https://localhost:44358' -e ASPNETCORE_ENVIRONMENT='Development' -p 13000:80 elsaworkflows/elsa-dashboard-and-server:latest` and you're good to go.

## General Notes:
Running Web API with the Elsa Server adds the following tables to your database:
* Elsa._EFMigrationsHistory
* Elsa.Bookmarks
* Elsa.WorkflowDefinitions
* Elsa.WorkflowExecutionLogRecords
* Elsa.WorkflowInstances

### Using the Designer:
* Access the output from the previous activity with the JS variable `input`. So if you prev. activity was an HTTP Request, then you can get the query string param with `input.QueryString["id"]`. When using that as an input for a new activity, it needs to be JavaScript as a string... like: `"${input.QueryString['id']}"` where the outer quotes are backticks. 
* If you have named (set the technical name of the activity) then the same can be done with `activityName.Output`
* If you want to get parameters passed into an HTTP Endpoint activity, they can be accessed like so `activityName.Output.QueryString["id"]` where `activityName` is the technical name of the activity and `"id"` is the parameter name.
