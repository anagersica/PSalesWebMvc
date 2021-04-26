using PSalesWebMvc.Data;
using PSalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSalesWebMvc.Services
{
    public class DepartmentService
    {
        /*  readonly para que essa dependência não seja alterada
         */
        private readonly  PSalesWebMvcContext _context;
        public DepartmentService(PSalesWebMvcContext context)
        {

            _context = context;
        }
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();//busca do BD todos os vendedores

        }
    }
}
