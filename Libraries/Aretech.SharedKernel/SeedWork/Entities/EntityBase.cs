namespace Aretech.SharedKernel.SeedWork.Entities
{
	public abstract class EntityBase<TId> : EntityBase, IEntityBase<TId>, ISoftDelete
	{
		public virtual TId Id { get; protected set; }
		public virtual DateTime CreatedDate { get; protected set; }
		public virtual Guid CreatedBy { get; protected set; }
		public virtual DateTime? UpdatedDate { get; protected set; }
		public virtual Guid UpdatedBy { get; protected set; }
		public virtual DateTime? DeletedDate { get; protected set; }
		public virtual Guid DeletedBy { get; protected set; }

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
