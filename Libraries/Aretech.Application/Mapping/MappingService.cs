using AutoMapper;

namespace Aretech.Application.Mapping
{
	public class MappingService : IMappingService
	{
		private readonly IMapper _mapper;

		public MappingService(IMapper mapper)
		{
			_mapper = mapper;
		}

		public TDestination Map<TSource, TDestination>(TSource source)
		{
			return _mapper.Map<TSource, TDestination>(source);
		}
	}
}