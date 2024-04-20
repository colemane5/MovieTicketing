using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public readonly struct TopTheatersResult(int month, int rank, string name, string address, decimal sales)
    {
        public int Month { get; } = month;
        public int Rank { get; } = rank;
        public string Name { get; } = name;
        public string Address { get; } = address;
        public decimal Sales { get; } = sales;
        public string ShortSales => $"{decimal.Round(Sales / 1000000, 3)}M";
    }
}
