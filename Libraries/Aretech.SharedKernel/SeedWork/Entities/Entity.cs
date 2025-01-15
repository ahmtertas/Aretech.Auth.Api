namespace Aretech.SharedKernel.SeedWork.Entities
{
	public abstract class Entity : EntityBase<Guid>
	{
		public void SetCreatedBy(Guid byId)
		{
			CreatedBy = byId;
			CreatedDate = DateTime.Now;
		}

		public void SetModifiedBy(Guid byId)
		{
			UpdatedBy = byId;
			UpdatedDate = DateTime.Now;
		}

		public void SetDeletedBy(Guid byId)
		{
			UpdatedBy = byId;
			DeletedDate = DateTime.Now;
		}
	}
}
