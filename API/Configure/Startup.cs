using API.Endpoints;
using API.Handlers;
using Application.Dispatchers;
using Application.Handlers;
using Application.Interfaces.Dispatchers;
using Application.Interfaces.Handlers;
using CrossCutting;
using Domain.Events;

namespace API.Configure
{
    public static class Startup
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            builder.Services.AddScoped<IDomainEventHandler<TransactionPaidEvent>, TransactionPaidEventHandler>();
            builder.Services.AddScoped<IDomainEventHandler<TransactionReopenEvent>, TransactionReopenEventHandler>();
            builder.Services.AddScoped<IDomainEventHandler<TransactionCancelEvent>, TransactionCancelEventHandler>();
            builder.AddDependencies();
            return builder;
        }

        public static WebApplication Setup(this WebApplication app)
        {
            app.UseExceptionHandler();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapCategoryEndpoints();
            app.MapTransactionEndpoints();

            return app;
        }
    }
}
