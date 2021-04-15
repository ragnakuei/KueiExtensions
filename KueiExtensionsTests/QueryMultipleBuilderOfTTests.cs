using System;
using System.Data;
using System.Linq;
using CreateDb;
using Dapper;
using KueiExtensions.DapperExtensions;
using NUnit.Framework;

namespace KueiExtensionsTests
{
    public class QueryMultipleBuilderOfTTests
    {
        private Guid _bGuid;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var testDbContext = DiFactory.GetService<TestDbContext>();

            testDbContext.Database.EnsureCreated();

            var aGuid = Guid.NewGuid();
            testDbContext.A.Add(new A
                                {
                                    Guid = aGuid,
                                    Name = "A",
                                    Details = new[]
                                              {
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = aGuid, Name = "A1", },
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = aGuid, Name = "A2", },
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = aGuid, Name = "A3", },
                                              }
                                });

            _bGuid = Guid.NewGuid();
            testDbContext.A.Add(new A
                                {
                                    Guid = _bGuid,
                                    Name = "B",
                                    Details = new[]
                                              {
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = _bGuid, Name = "B1", },
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = _bGuid, Name = "B2", },
                                              }
                                });

            testDbContext.SaveChanges();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            var testDbContext = DiFactory.GetService<TestDbContext>();
            testDbContext.Database.EnsureDeleted();
        }

        [Test]
        public void QueryMultiple_無參數()
        {
            var sql = @"
SELECT *
FROM [dbo].[A]
WHERE [Name] = N'A'

SELECT *
FROM [dbo].[ADetail] [d]
    JOIN [dbo].[A] [a]
         ON [d].[AGuid] = [a].[Guid]
WHERE [a].[Name] = N'A'
";
            var dbConnection = DiFactory.GetService<IDbConnection>();

            var a = dbConnection.MultipleResult<A>(sql)
                                .Read((boxDto, reader) => boxDto = reader.ReadFirstOrDefault<A>())
                                .Read((boxDto, reader) => boxDto.Details = reader.Read<ADetail>().ToArray())
                                .Query();

            Assert.True(a               != null);
            Assert.True(a.Details.Count == 3);
        }

        [Test]
        public void QueryMultiple_有參數_匿名()
        {
            var sql = @"
SELECT *
FROM [dbo].[A]
WHERE [Guid] = @Guid

SELECT *
FROM [dbo].[ADetail]
WHERE [AGuid] = @Guid
";
            var dbConnection = DiFactory.GetService<IDbConnection>();

            var param = new { Guid = _bGuid };

            var b = dbConnection.MultipleResult<A>(sql, param)
                                .Read((boxDto, reader) => boxDto = reader.ReadFirstOrDefault<A>())
                                .Read((boxDto, reader) => boxDto.Details = reader.Read<ADetail>().ToArray())
                                .Query();

            Assert.True(b               != null);
            Assert.True(b.Details.Count == 2);
        }

        [Test]
        public void QueryMultiple_有參數_DynamicParameters_具名()
        {
            var sql = @"
SELECT *
FROM [dbo].[A]
WHERE [Guid] = @Guid

SELECT *
FROM [dbo].[ADetail]
WHERE [AGuid] = @Guid
";
            var dbConnection = DiFactory.GetService<IDbConnection>();

            var param = new DynamicParameters();
            param.Add("Guid", _bGuid);

            var b = dbConnection.MultipleResult<A>(sql, param)
                                .Read((boxDto, reader) => boxDto = reader.ReadFirstOrDefault<A>())
                                .Read((boxDto, reader) => boxDto.Details = reader.Read<ADetail>().ToArray())
                                .Query();

            Assert.True(b               != null);
            Assert.True(b.Details.Count == 2);
        }

        [Test]
        public void QueryMultiple_有參數_DynamicParameters_匿名()
        {
            var sql = @"
SELECT *
FROM [dbo].[A]
WHERE [Guid] = @Guid

SELECT *
FROM [dbo].[ADetail]
WHERE [AGuid] = @Guid
";
            var dbConnection = DiFactory.GetService<IDbConnection>();

            var param = new DynamicParameters();
            param.AddDynamicParams(new { Guid = _bGuid });

            var b = dbConnection.MultipleResult<A>(sql, param)
                                .Read((boxDto, reader) => boxDto = reader.ReadFirstOrDefault<A>())
                                .Read((boxDto, reader) => boxDto.Details = reader.Read<ADetail>().ToArray())
                                .Query();

            Assert.True(b               != null);
            Assert.True(b.Details.Count == 2);
        }
    }
}
