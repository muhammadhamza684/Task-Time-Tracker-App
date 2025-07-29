using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task___Time_Tracker_App.Models;

namespace ContosoUniversity
{
    public class PaginatedList<T> : List<T>
    {
        private int count;
        private int pageSize;

        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<Tasks> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public PaginatedList(IEnumerable<T> collection, int count, int pageIndex, int pageSize) : base(collection)
        {
            this.count = count;
            PageIndex = pageIndex;
            this.pageSize = pageSize;
        }

        private void AddRange(List<Tasks> items)
        {
            throw new NotImplementedException();
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source,
            int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}