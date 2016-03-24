using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.TermLevel.Term
{
	public class TermQueryUsageTests : QueryDslUsageTestsBase
	{
		public TermQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) {}

		protected override object QueryJson => new
		{
			term = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					value = "project description"
				}
			}
		};

		protected override QueryContainer QueryInitializer => new TermQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Value = "project description"
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Term(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.Value("project description")
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ITermQuery>(q => q.Term)
		{
			q=> q.Field = null,
			q=> q.Value = "  ",
			q=> q.Value = null
		};
	}

	public class TermQueryVerbatimUsageTests : QueryDslUsageTestsBase
	{
		public TermQueryVerbatimUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) {}

		protected override object QueryJson => new
		{
			@bool = new
			{
				must = new object []
				{
					new
					{
						term = new
						{
							description = new
							{
								value = ""
							}
						}
					},
					new
					{
						term = new
						{
							name = new
							{
								value = "foo"
							}
						}
					}
				}
			}
		};

		protected override QueryContainer QueryInitializer => new TermQuery
		{
			IsVerbatim = true,
			Field = "description",
			Value = ""
		} 
		&& new TermQuery
		{
			Field = "name",
			Value = "foo"
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Verbatim()
			.Bool(b => b
				.Must(qt => qt
					.Term(t => t
						//.Verbatim()
						.Field(p => p.Description)
						.Value("")
					), qt => qt
					.Term(t => t
						.Field(p => p.Name)
						.Value("foo")
					)
				)
			);
	}
}