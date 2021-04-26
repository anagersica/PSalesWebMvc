using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PSalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using PSalesWebMvc.Data;

namespace PSalesWebMvc.Services
{
    public class SellerService
    {
        private readonly /*para que essa dependência não seja alterada*/ PSalesWebMvcContext _context;
        public SellerService(PSalesWebMvcContext context)
        {

            _context = context;
        }
       public List<Seller> FindAll()
        {
            return _context.Seller.ToList();//busca do BD todos os vendedores

        }
    }
}
