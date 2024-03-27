using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using System.Threading.RateLimiting;

namespace APIGateway.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RateLimiterOptions _options;

        public RateLimitingMiddleware(RequestDelegate next, IOptions<RateLimiterOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var limiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            {
                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Request.Headers.Host.ToString(),
                    partition => _options.FixedWindowOptions);
            });

            var result = await limiter.AcquireAsync(context);

            if (!result.IsAcquired)
            {
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync("Too many requests. Please try later again...");
                return;
            }

            await _next(context);
        }
    }

    public class RateLimiterOptions
    {
        public FixedWindowRateLimiterOptions FixedWindowOptions { get; set; }

    }
}
