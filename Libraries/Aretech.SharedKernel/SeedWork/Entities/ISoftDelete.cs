namespace Aretech.SharedKernel.SeedWork.Entities
{
	public interface ISoftDelete
	{
		public DateTime CreatedDate { get; }
		public DateTime? UpdatedDate { get; }
		public DateTime? DeletedDate { get; }
	}
}