using BussinessObject.Models;
using Repository.Implement;
using Repository.Interface;
using Service.Implement;
using Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

<<<<<<< Updated upstream
=======
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

>>>>>>> Stashed changes
// Add repo to container
builder.Services.AddScoped<IBaseCRUD<Order>, OrderRepo>();
builder.Services.AddScoped<IBaseCRUD<OrderItem>, OrderItemRepo>();
builder.Services.AddScoped<IBaseCRUD<User>, UserAccountRepo>();
builder.Services.AddScoped<IBaseCRUD<Product>, ProductRepo>();

// Add service to container
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
<<<<<<< Updated upstream
builder.Services.AddScoped<IProductService, ProductService>();
=======
builder.Services.AddScoped<IWarrantyService, WarrantyService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();
builder.Services.AddSingleton<IEmailQueue, EmailQueue>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IDeliverymanagement, Deliverymanagement>();

// Register Quartz
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    // Configure SendEmailJob
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
>>>>>>> Stashed changes

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// Set the default page to Products/Index
app.MapFallbackToPage("/Products/Index");

app.Run();
