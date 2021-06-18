using System.ComponentModel.DataAnnotations.Schema;
using KueiExtensions.Dapper.Generator;

namespace KueiExtensionsTests.DapperTests.GenerateSqlServiceTests
{
    public class ClassB
    {
        [NotMapped]
        [KueiExtensions.Dapper.Generator.Column(ColumnType.Sn)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Age { get; set; }
    }
}