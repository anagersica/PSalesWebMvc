
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PSalesWebMvc.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PSalesWebMvc.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public SaleStatus Status { get; set; }
        public Seller Seller { get; set; }//aqui por causa do relacionamento um SalesRecord tem um Seller


        //construtor Default(sem argumentos)
        public SalesRecord()
        { }
        //construtor com argumentos

        public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }


    }
}
