﻿using System.Linq;

namespace Nest
{
	internal static class QueryContainerExtensions
	{
		public static bool IsConditionless(this QueryContainer q) => q == null || (q.IsConditionless && !q.IsVerbatim);
	}

	public partial class QueryContainer : IQueryContainer, IDescriptor
	{
		bool IQueryContainer.IsConditionless => (ContainedQuery?.Conditionless).GetValueOrDefault(true);
		internal bool IsConditionless => Self.IsConditionless;

		bool IQueryContainer.IsStrict { get; set; }
		internal bool IsStrict => Self.IsStrict;

		bool IQueryContainer.IsVerbatim { get; set; }
		internal bool IsVerbatim => Self.IsVerbatim;

		public QueryContainer()
		{
		}
	
		public QueryContainer(QueryBase query) : this()
		{
			query?.WrapInContainer(this);
		}
	
		public static QueryContainer operator &(QueryContainer leftContainer, QueryContainer rightContainer)
		{
			QueryContainer queryContainer;
			return IfEitherIsEmptyReturnTheOtherOrEmpty(leftContainer, rightContainer, out queryContainer) 
				? queryContainer 
				: leftContainer.CombineAsMust(rightContainer);
		}
		
		public static QueryContainer operator |(QueryContainer leftContainer, QueryContainer rightContainer)
		{
			QueryContainer queryContainer;
			return IfEitherIsEmptyReturnTheOtherOrEmpty(leftContainer, rightContainer, out queryContainer) 
				? queryContainer 
				: leftContainer.CombineAsShould(rightContainer);
		}

		private static bool IfEitherIsEmptyReturnTheOtherOrEmpty(QueryContainer leftContainer, QueryContainer rightContainer, out QueryContainer queryContainer)
		{
			var combined = new[] {leftContainer, rightContainer};
			var any = combined.Any(qc => qc == null || (qc.IsConditionless && !qc.IsVerbatim));
			queryContainer = any ? combined.FirstOrDefault(qc => qc != null && (!qc.IsConditionless || qc.IsVerbatim)) : null;
			return any;
		}

		public static QueryContainer operator !(QueryContainer queryContainer) => queryContainer == null || (queryContainer.IsConditionless && !queryContainer.IsVerbatim)
			? null
			: new QueryContainer(new BoolQuery {MustNot = new[] {queryContainer}});

		public static QueryContainer operator +(QueryContainer queryContainer) => queryContainer == null || (queryContainer.IsConditionless && !queryContainer.IsVerbatim)
			? null
			: new QueryContainer(new BoolQuery {Filter = new[] {queryContainer}});

		public static bool operator false(QueryContainer a) => false;

		public static bool operator true(QueryContainer a) => false;

		public void Accept(IQueryVisitor visitor)
		{
			if (visitor.Scope == VisitorScope.Unknown) visitor.Scope = VisitorScope.Query;
			new QueryWalker().Walk(this, visitor);
		}
	}

}
