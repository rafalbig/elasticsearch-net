using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	internal class PropertyCascadingQueryVisitor : IQueryVisitor
	{
		public int Depth { get; set; }
		public VisitorScope Scope { get; set; }

		private IQueryContainer _parent;

		public PropertyCascadingQueryVisitor(IQueryContainer parent)
		{
			_parent = parent;
		}

		private void Cascade(IQuery query)
		{
			if (query == null) return;
			query.IsStrict = query.IsStrict || _parent.IsStrict;
			query.IsVerbatim = query.IsVerbatim || _parent.IsVerbatim;
		}

		private void Cascade(IQueryContainer child)
		{
			if (child == null) return;
			child.IsStrict = child.IsStrict || _parent.IsStrict;
			child.IsVerbatim = child.IsVerbatim || _parent.IsVerbatim;
		}

		public void Visit(ICommonTermsQuery query) => Cascade(query);
		public void Visit(IDisMaxQuery query) => Cascade(query);
		public void Visit(IFuzzyQuery query) => Cascade(query);
		public void Visit(IFuzzyDateQuery query) => Cascade(query);
		public void Visit(IHasChildQuery query) => Cascade(query);
		public void Visit(IIdsQuery query) => Cascade(query);
		public void Visit(IMatchQuery query) => Cascade(query);
		public void Visit(IMoreLikeThisQuery query) => Cascade(query);
		public void Visit(INestedQuery query) => Cascade(query);
		public void Visit(IQueryStringQuery query) => Cascade(query);
		public void Visit(IRegexpQuery query) => Cascade(query);
		public void Visit(ITermQuery query) => Cascade(query);
		public void Visit(ITermsQuery query) => Cascade(query);
		public void Visit(IScriptQuery query) => Cascade(query);
		public void Visit(IGeoPolygonQuery query) => Cascade(query);
		public void Visit(IGeoDistanceQuery query) => Cascade(query);
		public void Visit(IGeoHashCellQuery query) => Cascade(query);
		public void Visit(IDateRangeQuery query) => Cascade(query);
		public void Visit(ITermRangeQuery query) => Cascade(query);
		public void Visit(ILimitQuery query) => Cascade(query);
		public void Visit(INotQuery query) => Cascade(query);
		public void Visit(IAndQuery query) => Cascade(query);
		public void Visit(ISpanNearQuery query) => Cascade(query);
		public void Visit(ISpanOrQuery query) => Cascade(query);
		public void Visit(ISpanQuery query) => Cascade(query);
		public void Visit(ISpanContainingQuery query) => Cascade(query);
		public void Visit(ISpanMultiTermQuery query) => Cascade(query);
		public void Visit(IGeoShapeQuery query) => Cascade(query);
		public void Visit(IGeoShapeMultiPolygonQuery query) => Cascade(query);
		public void Visit(IGeoShapePointQuery query) => Cascade(query);
		public void Visit(IGeoShapeLineStringQuery query) => Cascade(query);
		public void Visit(IGeoShapeCircleQuery query) => Cascade(query);
		public void Visit(IRawQuery query) => Cascade(query);
		public void Visit(IGeoShapeEnvelopeQuery query) => Cascade(query);
		public void Visit(IGeoShapeMultiLineStringQuery query) => Cascade(query);
		public void Visit(IGeoShapePolygonQuery query) => Cascade(query);
		public void Visit(IGeoShapeMultiPointQuery query) => Cascade(query);
		public void Visit(IGeoIndexedShapeQuery query) => Cascade(query);
		public void Visit(ISpanWithinQuery query) => Cascade(query);
		public void Visit(ISpanSubQuery query) => Cascade(query);
		public void Visit(ISpanTermQuery query) => Cascade(query);
		public void Visit(ISpanNotQuery query) => Cascade(query);
		public void Visit(ISpanFirstQuery query) => Cascade(query);
		public void Visit(IOrQuery query) => Cascade(query);
		public void Visit(IFilteredQuery query) => Cascade(query);
		public void Visit(ITemplateQuery query) => Cascade(query);
		public void Visit(INumericRangeQuery query) => Cascade(query);
		public void Visit(IExistsQuery query) => Cascade(query);
		public void Visit(IGeoBoundingBoxQuery query) => Cascade(query);
		public void Visit(IGeoDistanceRangeQuery query) => Cascade(query);
		public void Visit(IMissingQuery query) => Cascade(query);
		public void Visit(ITypeQuery query) => Cascade(query);
		public void Visit(IWildcardQuery query) => Cascade(query);
		public void Visit(ISimpleQueryStringQuery query) => Cascade(query);
		public void Visit(IRangeQuery query) => Cascade(query);
		public void Visit(IPrefixQuery query) => Cascade(query);
		public void Visit(IMultiMatchQuery query) => Cascade(query);
		public void Visit(IMatchAllQuery query) => Cascade(query);
		public void Visit(IIndicesQuery query) => Cascade(query);
		public void Visit(IHasParentQuery query) => Cascade(query);
		public void Visit(IFuzzyStringQuery query) => Cascade(query);
		public void Visit(IFuzzyNumericQuery query) => Cascade(query);
		public void Visit(IFunctionScoreQuery query) => Cascade(query);
		public void Visit(IConstantScoreQuery query) => Cascade(query);
		public void Visit(IBoostingQuery query) => Cascade(query);
		public void Visit(IBoolQuery query) => Cascade(query);
		public void Visit(IQuery query) => Cascade(query);
		public void Visit(IQueryContainer queryDescriptor) => Cascade(queryDescriptor);
	}
}
