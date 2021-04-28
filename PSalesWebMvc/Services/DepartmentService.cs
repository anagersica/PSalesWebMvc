using PSalesWebMvc.Data;
using PSalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        //Alterando FindAll para  comunicação assincrona usando Tasks(asyn, await)
        public async Task<List<Department>> FindAllAsync()//Esse FindAll será implementado para que seja comunicação assincrona na parte de Services usando Tasks(asyn, await)
        {
            //o _context.Department.OrderBy(x => x.Name) é uma operação Linq que prepara a consulta, só executa quando provocada, nesse caso pelo ToList 
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();//Acessa o BD por meio do Entity Fra(comunicação lenta: sincrona) e busca todos os departamentos

        }
    }
}
