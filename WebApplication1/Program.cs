using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ 1. REGISTER SERVICES (BEFORE BUILD)
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// ✅ 2. BUILD APP
var app = builder.Build();

// ✅ 3. CONFIGURE PIPELINE
app.MapControllers();

// ✅ 4. RUN
app.Run();