using Gobi.Test.Jr.Application;
using Gobi.Test.Jr.Domain.Interfaces;
using Gobi.Test.Jr.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
.Services
.AddControllersWithViews()
.AddRazorRuntimeCompilation();

builder.Services.AddScoped<TodoItemService>();
builder.Services.AddTransient<ITodoItemRepository, TodoItemRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todo}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "edit",
    pattern: "Todo/Edit/{id}");

app.MapControllerRoute(
    name: "delete",
    pattern: "Todo/Delete/{id}");

app.Run();
