
namespace BookingSystem.Application.Exceptions
{
	public class ValidationException:Exception
	{
		public IEnumerable<string> Errors { get; }
		public ValidationException(IEnumerable<string> errors):base(string.Join(", ", errors))
		{
			Errors = errors;
		}
	}
}