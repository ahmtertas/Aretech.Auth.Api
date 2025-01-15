namespace Aretech.Services.SeedWorks
{
	public interface IPagedList<T> : IList<T>
	{
		int PageIndex { get; }
		int PageSize { get; }
		int TotalCount { get; }
		int TotalPages { get; }
	}
}
