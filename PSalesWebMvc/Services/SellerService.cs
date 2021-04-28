using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PSalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using PSalesWebMvc.Data;
using PSalesWebMvc.Services.Exceptions;

//Serviços dentro de Model- contem operações/regra de negócio/atualizar/salvar/acessar dados referentes ao Seller
namespace PSalesWebMvc.Services
{
    public class SellerService
    {
        private readonly /*para que essa dependência não seja alterada*/ PSalesWebMvcContext _context;
        public SellerService(PSalesWebMvcContext context)
        {

            _context = context;
        }
        public async  Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();//Acessa o BD por meio do Entity Fra(comunicação lenta: sincrona) e busca todos os vendedores

        }
        public /*void*/ async Task InsertAsync(Seller obj)//método inclui ação insert no botao create do form de novo funhcionário
        {
            //tiramos o objeto pq inserimos um caixa de seleção( obj.Department = _context.Department.First();//preenche com o primeiro id de Depart a col DepartId de Seller
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        //implementando para deletar um seller
        public  async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);//sem o INCLUDE, por padrão carrega apenas dados da tabela SELLER 
            //o FirstOrDefault é o que acessa o BD por isso ele que recebeu o Async
        }
        public /*void*/ async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }
        public /*void*/ async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (! hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)//intercepta exceção de nível a dado e lança a nível de serviço:
            {
                throw new DbConcurrencyException(e.Message);//exceção a nível de serviço
            }
        }
    }
}

