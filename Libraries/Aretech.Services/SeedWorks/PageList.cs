namespace Aretech.Services.SeedWorks
{
	[System.Serializable]
	public partial class PageList<T> : List<T>, IPagedList<T>
	{
		public PageList(IList<T> source, int? pageIndex, int? pageSize, int? totalCount = null)
		{
			pageSize = pageSize ?? 10;
			pageIndex = pageIndex ?? 0;

			pageSize = Math.Max(pageSize.Value, 1);

			TotalCount = totalCount ?? source.Count;
			TotalPages = TotalCount / pageSize.Value;

			if (TotalCount % pageSize > 0)
				TotalPages++;

			PageSize = pageSize.Value;
			PageIndex = pageIndex.Value;
			AddRange(totalCount != null ? source : source.Skip(pageIndex.Value * pageSize.Value).Take(pageSize.Value));
		}

		public int PageIndex { get; }

		public int PageSize { get; }

		public int TotalCount { get; }

		public int TotalPages { get; }
	}
}
