using Microsoft.AspNetCore.Mvc;

using System;

using TestApp.Services;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly CacheService cacheService;

        public MainController(CacheService cacheService)
        {
            this.cacheService = cacheService;
        }

        [HttpGet]
        [Route("reload/{userId}")]
        public IActionResult Reload(Guid userId)
        {   
            cacheService.UpdateOrSet(userId);

            return Ok();
        }

        [HttpGet]
        [Route("get/{userId}")]
        public IActionResult Get(Guid userId)
        {
            var result = cacheService.Get(userId);

            return Ok(result);
        }
    }
}
