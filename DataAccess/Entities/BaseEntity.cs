using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
	public class BaseEntity
	{
		public BaseEntity()
		{
			CreatedAt = DateTime.UtcNow;
		}

		[Key]
		public int Id { get; private set; }

		[Required]
		public DateTime CreatedAt { get; private set; }
	}
}
