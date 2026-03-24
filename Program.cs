using Microsoft.EntityFrameworkCore;
using MVC.Context;
using MVC.Interface;
using MVC.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TarefaMvcContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoSqlServer")));

// Add services to the container.
builder.Services.AddControllersWithViews();



// "TarefaRepository" é a classe que implementa "ITarefaRepository"
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
