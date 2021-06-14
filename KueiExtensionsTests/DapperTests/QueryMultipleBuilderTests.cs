using System;
using System.Data;
using System.Linq;
using CreateDb;
using Dapper;
using KueiExtensions.Dapper;
using NUnit.Framework;

namespace KueiExtensionsTests.DapperTests
{
    public class QueryMultipleBuilderTests
    {
        private Guid _dGuid;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var testDbContext = DiFactory.GetService<TestDbContext>();

            testDbContext.Database.EnsureCreated();

            var aGuid = Guid.NewGuid();
            testDbContext.A.Add(new A
                                {
                                    Guid = aGuid,
                                    Name = "C",

                                    Details = new[]
                                              {
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = aGuid, Name = "C1", },
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = aGuid, Name = "C2", },
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = aGuid, Name = "C3", },
                                              }
                                });

            _dGuid = Guid.NewGuid();
            testDbContext.A.Add(new A
                                {
                                    Guid = _dGuid,
                                    Name = "D",

                                    Details = new[]
                                              {
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = _dGuid, Name = "D1", },
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = _dGuid, Name = "D2", },
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
WHERE [Name] = N'C'

SELECT *
FROM [dbo].[ADetail] [d]
    JOIN [dbo].[A] [a]
         ON [d].[AGuid] = [a].[Guid]
WHERE [a].[Name] = N'C'
";
            var dbConnection = DiFactory.GetService<IDbConnection>();

            var boxDto = new A();

            dbConnection.MultipleResult(sql)
                        .Query(reader =>
                               {
                                   boxDto         = reader.ReadFirstOrDefault<A>();
                                   boxDto.Details = reader.Read<ADetail>().ToArray();
                                   return boxDto;
                               });

            Assert.True(boxDto               != null);
            Assert.True(boxDto.Details.Count == 3);
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

            var param = new { Guid = _dGuid };

            var boxDto = new A();

            dbConnection.MultipleResult(sql, param)
                        .Query(reader =>
                               {
                                   boxDto         = reader.ReadFirstOrDefault<A>();
                                   boxDto.Details = reader.Read<ADetail>().ToArray();
                                   return boxDto;
                               });

            Assert.True(boxDto               != null);
            Assert.True(boxDto.Details.Count == 2);
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
            param.Add("Guid", _dGuid);

            var boxDto = new A();

            dbConnection.MultipleResult(sql, param)
                        .Query(reader =>
                               {
                                   boxDto         = reader.ReadFirstOrDefault<A>();
                                   boxDto.Details = reader.Read<ADetail>().ToArray();
                                   return boxDto;
                               });

            Assert.True(boxDto               != null);
            Assert.True(boxDto.Details.Count == 2);
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
            param.AddDynamicParams(new { Guid = _dGuid });

            var boxDto = new A();

            dbConnection.MultipleResult(sql, param)
                        .Query(reader =>
                               {
                                   boxDto         = reader.ReadFirstOrDefault<A>();
                                   boxDto.Details = reader.Read<ADetail>().ToArray();
                                   return boxDto;
                               });

            Assert.True(boxDto               != null);
            Assert.True(boxDto.Details.Count == 2);
        }
    }
}
