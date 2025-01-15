using System.DirectoryServices;

namespace Aretech.Core.Abstract
{
	public class SortableRequest
	{
		public List<SortingData>? SortList { get; set; }
	}

	public class SortingData
	{
		public int Priority { get; set; }
		public string FieldName { get; set; } = null!;
		public SortDirection Direction { get; set; }
	}
}