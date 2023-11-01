using ASP.NET_CORE.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//middleware 1
app.Use(async (HttpContext context, RequestDelegate next) => {
    await context.Response.WriteAsync("Hello");
    await next(context);
});

// middleware 2
// app.UseMiddleware<MyCustomMiddleware>();
// app.UseMyCustomMiddleware
// app.UseNewCustomMiddleware();
app.UseWhen((HttpContext context) =>
    context.Request.Query.ContainsKey("firstName"),
    (app) =>
    {
        app.UseNewCustomMiddleware();
    });

//middleware 3
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("This is the last middleware");
});
app.Run();
