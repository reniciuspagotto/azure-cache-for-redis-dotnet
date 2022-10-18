var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = "az204-redis-pagotto.redis.cache.windows.net:6380,password=CM4FTSTNUliJLNsws7aN74RrHOSKw2QTmAzCaDlJ4Kc=,ssl=True,abortConnect=False";
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
