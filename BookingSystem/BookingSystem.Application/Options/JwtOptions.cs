using System;

namespace BookingSystem.Application.Options
{
	public class JwtOptions
	{
        public string Secret { get; set; } = null!;
        public int ExpirationHours { get; set; } = 24;
    }
}
