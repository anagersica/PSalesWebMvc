using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PSalesWebMvc.Data;
using PSalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace PSalesWebMvc.Services
{
    public class SalesRecordService
    {
        /*  readonly para que essa dependência não seja alterada
 */
        private readonly PSalesWebMvcContext _context;
        public SalesRecordService(PSalesWebMvcContext context)
        {

            _context = context;
        }

        //Esse método recebe as datas min e max para a busca
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;//pega o obj do tipo DbSet e construir um obj do tipo IQueryable 
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);//expressão lambda que expressa a restrição de data
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                //include é ferramenta do Entity Frame
                .Include(x => x.Seller)//faz inner join
                .Include(x => x.Seller.Department)//join com tb de departamento
                .OrderByDescending(x => x.Date)//ordenar por data
                .ToListAsync();
        }
        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)// quando agrupa os resultados, o retorno será do tipo coleção IGrouping e não List
                .ToListAsync();
        }
    }
}
