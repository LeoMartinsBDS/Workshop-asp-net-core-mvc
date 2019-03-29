using SalesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWeb.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebContext _context;

        public SalesRecordService(SalesWebContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? dtMin, DateTime? dtMax)
        {
            var result = from obj in _context.SalesRecord select obj;

            if (dtMin.HasValue)
            {
                result = result.Where(x => x.Date >= dtMin.Value);
            }
            if (dtMax.HasValue)
            {
                result = result.Where(x => x.Date <= dtMax.Value);
            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
        
        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? dtMin, DateTime? dtMax)
        {
            var result = from obj in _context.SalesRecord select obj;

            if (dtMin.HasValue)
            {
                result = result.Where(x => x.Date >= dtMin.Value);
            }
            if (dtMax.HasValue)
            {
                result = result.Where(x => x.Date <= dtMax.Value);
            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }

    }
}
