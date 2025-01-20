namespace Aretech.SharedKernel.SeedWork.Entities
{
	public abstract class EntityBase<TId> : EntityBase, IEntityBase<TId>, ISoftDelete
	{
		public virtual TId Id { get; set; }
		public virtual DateTime CreatedDate { get; set; }
		public virtual Guid CreatedBy { get; set; }
		public virtual DateTime? UpdatedDate { get; set; }
		public virtual Guid UpdatedBy { get; set; }
		public virtual DateTime? DeletedDate { get; set; }
		public virtual Guid DeletedBy { get; set; }

		public bool IsDeleted()
		{
			return DeletedDate != null;
		}

		public bool IsTransient()
		{
			return Id.Equals(default(TId));
		}


	}


	public abstract class EntityBase : IEntityBase
	{

	}
}
