namespace Aretech.Application.Mapping
{
	public interface IMappingService
	{
		TDestination Map<TSource, TDestination>(TSource source);
	}
}
