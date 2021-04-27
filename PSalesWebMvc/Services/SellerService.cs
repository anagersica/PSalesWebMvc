using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PSalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using PSalesWebMvc.Data;

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
       public List<Seller> FindAll()
        {
            return _context.Seller.ToList();//busca do BD todos os vendedores

        }
        public void Insert(Seller obj)//método inclui ação insert no botao create do form de novo funhcionário
        {
           //tiramos o objeto pq inserimos um caixa de seleção( obj.Department = _context.Department.First();//preenche com o primeiro id de Depart a col DepartId de Seller
            _context.Add(obj);
            _context.SaveChanges();
        }
        //implementando para deletar um seller
        public Seller FindById(int id )
        {
            return _context.Seller.FirstOrDefault(obj => obj.Id == id);
        }
        public void Remove (int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }
}
