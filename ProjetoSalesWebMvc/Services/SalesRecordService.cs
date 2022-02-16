

using Microsoft.EntityFrameworkCore;
using ProjetoSalesWebMvc.Data;
using ProjetoSalesWebMvc.Models;

namespace ProjetoSalesWebMvc.Services {

    public class SalesRecordService {

        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context) {

            _context = context;
        }


        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate) {

            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue) {

                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue) {

                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        // Agrupar por departamento
        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate) {

            var result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue) {

                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue) {

                result = result.Where(x => x.Date <= maxDate.Value);
            }

            var data = await result
                .Include(s => s.Seller)
                .Include(s => s.Seller.Department)
                .OrderByDescending(se => se.Date)
                .ToListAsync();
            return data.GroupBy(x => x.Seller.Department).ToList();
            
            // return await result
            //     .Include(x => x.Seller)
            //     .Include(x => x.Seller.Department)
            //     .OrderByDescending(x => x.Date)
            //     .GroupBy(x => x.Seller.Department) // Quando você agrupa os resultados voce tem que usar o IGrouping<>
            //     .ToListAsync();
        }


    }
}