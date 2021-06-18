using KueiExtensions.Dapper.Generator;

namespace KueiExtensionsTests.DapperTests.GenerateSqlServiceTests
{
    public class ClassA
    {
        [KueiExtensions.Dapper.Generator.Column(ColumnType.Sn)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Age { get; set; }
    }
}
