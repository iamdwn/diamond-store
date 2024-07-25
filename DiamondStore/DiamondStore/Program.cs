using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Repository.Implement;
using Repository.Interface;
using Service.Implement;
using Service.Interface;
using Service.Services.Impl;
using Service.Services.Quartzs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DiamondStoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add repo to container
builder.Services.AddScoped<IBaseCRUD<Order>, OrderRepo>();
builder.Services.AddScoped<IBaseCRUD<OrderItem>, OrderItemRepo>();
builder.Services.AddScoped<IBaseCRUD<User>, UserAccountRepo>();
builder.Services.AddScoped<IBaseCRUD<Warranty>, WarrantyRepo>();
builder.Services.AddScoped<IBaseCRUD<Product>, ProductRepo>();
builder.Services.AddScoped<IBaseCRUD<Role>, RoleRepo>();
builder.Services.AddScoped<IBaseCRUD<Delivery>, DeliveryRepo>();
builder.Services.AddScoped<IDeliverManagementRepo, DeliverymanagementRepo>();
builder.Services.AddScoped<IWarrantyRepo, WarrantyRepo>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();
// Add service to container
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<IWarrantyService, WarrantyService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();
builder.Services.AddSingleton<IEmailQueue, EmailQueue>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IDeliverymanagement, Deliverymanagement>();

// Register Quartz
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    var emailJobKey = new JobKey("SendEmailJob");
    q.AddJob<SendEmailJob>(opts => opts.WithIdentity(emailJobKey));

    var emailCronSchedule = builder.Configuration.GetSection("CronJobs:SendEmailJob")?.Value;
    if (string.IsNullOrWhiteSpace(emailCronSchedule))
    {
        throw new ArgumentException("The cron schedule for SendEmailJob is not configured properly.");
    }

    q.AddTrigger(opts => opts
        .ForJob(emailJobKey)
        .WithIdentity("SendEmailJob-trigger")
        .WithCronSchedule(emailCronSchedule));
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapFallbackToPage("/Login");

app.Run();
