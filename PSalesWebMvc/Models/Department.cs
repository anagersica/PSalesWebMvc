using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();//´coleção por causa do relacionamento Depart tem muitos vendedores e instanciou com List

        //construtor Default(sem argumentos)
        public Department()
        { }
        //construtor com argumentos
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);

        }
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));

        }


    }



}
