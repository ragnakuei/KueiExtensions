using System;
using System.Data;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CreateDb;

namespace DapperVsEfCore
{
    class Program
    {
        static void Main(string[] args)
        {
            SeedData();
            var summary = BenchmarkRunner.Run<TestRunner>();
            ClearSeedData();
        }

        private static void SeedData()
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

            var dGuid = Guid.NewGuid();
            testDbContext.A.Add(new A
                                {
                                    Guid = dGuid,
                                    Name = "D",

                                    Details = new[]
                                              {
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = dGuid, Name = "D1", },
                                                  new ADetail { Guid = Guid.NewGuid(), AGuid = dGuid, Name = "D2", },
                                              }
                                });

            testDbContext.SaveChanges();
        }

        private static void ClearSeedData()
        {
            var testDbContext = DiFactory.GetService<TestDbContext>();
            testDbContext.Database.EnsureDeleted();
        }

        public class TestRunner
        {
            [Benchmark]
            public void Dapper() => new DapperService().Run();

            [Benchmark]
            public void EfCore() => new EfCoreService().Run();
        }
    }

    internal class EfCoreService
    {
        private IDbConnection _sqlConnection;

        public EfCoreService()
        {
            _sqlConnection = DiFactory.GetService<IDbConnection>();
        }

        public void Run()
        {

        }
    }

    internal class DapperService
    {
        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
