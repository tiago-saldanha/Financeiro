using API.Configure;

var builder = WebApplication
    .CreateBuilder(args)
    .AddServices();

var app = builder
    .Build()
    .Setup();

app.Run();
