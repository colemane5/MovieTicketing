using MovieTicketingAdmin.SqlInterfaces.Interfaces;
using SharedResources.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketingAdmin.SqlInterfaces
{
    public class SqlAggregateQueryRepo : IAggregateQueryRepo
    {
        private readonly string connectionString;

        public SqlAggregateQueryRepo(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IReadOnlyList<GenreRanksResult> GetGenreRanks(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<HourlySalesResult> GetSalesPerHourOfTheDay(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<TopTheatersResult> GetTopTheaters(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<TopMoviesResult> MovieStatisticsOverGivenPeriod(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            throw new NotImplementedException();
        }
    }
}
