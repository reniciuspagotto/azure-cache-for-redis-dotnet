using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace AzureCacheForRedis.Api.Controllers
{
    [ApiController]
    [Route("api/client")]
    public class ClientController : ControllerBase
    {
        private readonly IDistributedCache _cache;

        public ClientController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            var cachedResponse = await _cache.GetStringAsync($"client-{id}");

            var data = JsonSerializer.Deserialize<object>(cachedResponse);

            return Ok(data);
        }

        [HttpPost("save-cache-ttl")]
        public async Task<IActionResult> SaveWithTTL()
        {
            var data = JsonSerializer.Serialize(new { Name = "Renicius", LastName = "Pagotto", City = "Porto Feliz" });
            var dataInBytes = Encoding.UTF8.GetBytes(data);

            await _cache.SetAsync($"client-{new Random().Next(0, int.MaxValue)}", dataInBytes, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
            });

            return Ok();
        }

        [HttpPost("save-cache-no-ttl")]
        public async Task<IActionResult> SaveWithoutTTL()
        {
            var data = JsonSerializer.Serialize(new { Name = "Renicius", LastName = "Pagotto", City = "Porto Feliz" });
            var dataInBytes = Encoding.UTF8.GetBytes(data);

            await _cache.SetAsync($"client-{new Random().Next(0, int.MaxValue)}", dataInBytes);

            return Ok();
        }
    }
}
