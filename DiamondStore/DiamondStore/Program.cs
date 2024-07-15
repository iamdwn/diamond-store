using BussinessObject.Models;
using Repository.Implement;
using Repository.Interface;
using Service.Implement;
using Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add repo to container
builder.Services.AddScoped<IBaseCRUD<Order>, OrderRepo>();
builder.Services.AddScoped<IBaseCRUD<OrderItem>, OrderItemRepo>();
builder.Services.AddScoped<IBaseCRUD<User>, UserAccountRepo>();
builder.Services.AddScoped<IBaseCRUD<Role>, RoleRepo>();
builder.Services.AddScoped<IBaseCRUD<Delivery>, DeliveryRepo>();

// Add service to container
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
