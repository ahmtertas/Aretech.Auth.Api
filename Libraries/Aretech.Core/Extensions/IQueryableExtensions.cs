using Aretech.Core.Abstract;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace Aretech.Core.Extensions
{
	public static class IQueryableExtensions
	{
		const int minPage = 1;
		const int minPageSize = 5;
		const int maxPageSize = 1000;

		public static IQueryable<T> PageBy<T>([NotNull] this IQueryable<T> query, int? page, int? pageSize)
		{
			page = (page ?? 0) < minPage ? minPage : page ?? 0;
			pageSize = (pageSize ?? 0) > maxPageSize ? maxPageSize : pageSize ?? 0;
			pageSize = (pageSize ?? 0) < minPageSize ? minPageSize : pageSize ?? 0;

			query = query.Skip(((page ?? 1) - 1) * pageSize ?? 0).Take(pageSize ?? 0);
			return query;
		}

		public static IEnumerable<T> PageBy<T>([NotNull] this IEnumerable<T> query, int? page, int? pageSize)
		{
			var result = PageBy<T>(query.AsQueryable(), page, pageSize);
			return result.ToList();
		}

		public static IQueryable<T> WhereIf<T>([NotNull] this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
		{
			return condition ? query.Where(predicate) : query;
		}

		public static IQueryable<T> WhereIfWithOperators<T>([NotNull] this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
		{
			return condition ? query.Where(predicate) : query;
		}

		public class CaseInsensitiveComparer : IComparer<string>
		{
			public int Compare(string x, string y)
			{
				return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
			}
		}


		public static IQueryable<T> AddSortList<T>([NotNull] this IQueryable<T> query, List<SortingData> sortModels)
		{
			if (sortModels == null || sortModels.Count == 0)
				return query;

			var firstSort = sortModels.OrderBy(x => x.Priority).FirstOrDefault();
			if (firstSort.Direction == System.DirectoryServices.SortDirection.Ascending)
				query = query.OrderBy(GetSortExpression<T>(firstSort.FieldName));
			if (firstSort.Direction == System.DirectoryServices.SortDirection.Descending)
				query = query.OrderByDescending(GetSortExpression<T>(firstSort.FieldName));

			foreach (var sortModel in sortModels)
			{
				if (sortModel.Direction == System.DirectoryServices.SortDirection.Ascending)
					query = ((IOrderedQueryable<T>)query).ThenBy(GetSortExpression<T>(sortModel.FieldName));

				if (sortModel.Direction == System.DirectoryServices.SortDirection.Descending)
					query = ((IOrderedQueryable<T>)query).ThenByDescending(GetSortExpression<T>(sortModel.FieldName));
			}

			return query;
		}

		public static IList<T> AddSortList<T>([NotNull] this IList<T> query, List<SortingData> sortModels)
		{
			var result = AddSortList<T>(query.AsQueryable(), sortModels);
			return result.ToList();
		}

		public static IQueryable<T> WhereDynamic<T>([NotNull] this IQueryable<T> source, string propertyName, string value, OperatorType operatorType)
		{
			var documentType = typeof(T);
			documentType.GetProperties();
			var propertyInfo = documentType.GetProperty(propertyName);

			var parameterExpression = Expression.Parameter(documentType, "documnet");
			var memberAccessExpression = Expression.MakeMemberAccess(parameterExpression, propertyInfo);
			var convertedValue = Convert.ChangeType(value, propertyInfo.PropertyType);
			var constantExpression = Expression.Constant(convertedValue, propertyInfo.PropertyType);
			Expression expression = Expression.Equal(memberAccessExpression, constantExpression);

			switch (operatorType)
			{
				case OperatorType.Equals:
					expression = Expression.Equal(memberAccessExpression, constantExpression);
					break;

				case OperatorType.NotEquals:
					expression = Expression.NotEqual(memberAccessExpression, constantExpression);
					break;

				case OperatorType.GreaterThan:
					expression = Expression.GreaterThan(memberAccessExpression, constantExpression);
					break;

				case OperatorType.LessThan:
					expression = Expression.LessThan(memberAccessExpression, constantExpression);
					break;

				case OperatorType.GreaterThanOrEqual:
					expression = Expression.GreaterThanOrEqual(memberAccessExpression, constantExpression);
					break;

				case OperatorType.LessThanOrEqual:
					expression = Expression.LessThanOrEqual(memberAccessExpression, constantExpression);
					break;

				case OperatorType.Contained:
					MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
					expression = Expression.Call(memberAccessExpression, method, constantExpression);
					break;
			}

			var lambdaExpression = Expression.Lambda<Func<T, bool>>(expression, parameterExpression);
			return source.Where(lambdaExpression);
		}


		public static IQueryable<T> DynamicFiltering<T>(this IQueryable<T> source, IEnumerable<FilterByRequest> filterByRequests)
		{
			if (filterByRequests is not null)
			{
				filterByRequests.ToList().ForEach(filter => source.WhereDynamic(filter.Name, filter.Value, filter.Operator));
			}

			return source;
		}

		public static IEnumerable<T> AddSortList<T>([NotNull] this IEnumerable<T> query, List<SortingData> sortModels)
		{
			var result = AddSortList<T>(query.AsQueryable(), sortModels);
			return result.ToList();
		}

		private static Expression<Func<T, object>> GetSortExpression<T>(string propertyName)
		{
			var parameter = Expression.Parameter(typeof(T));
			var property = Expression.Property(parameter, propertyName);
			var propertyAsObject = Expression.Convert(property, typeof(object));
			return Expression.Lambda<Func<T, object>>(propertyAsObject, parameter);
		}


		public class FilterByRequest
		{
			public string Name { get; set; }
			public string Value { get; set; }

			public OperatorType Operator { get; set; }
		}

		public enum OperatorType
		{
			Equals = 1,
			NotEquals = 2,
			GreaterThan = 3,
			LessThan = 4,
			GreaterThanOrEqual = 5,
			LessThanOrEqual = 6,
			Contained = 7,
		}
	}
}
