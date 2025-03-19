using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmacySystem.model;
using FarmacySystem.data;

namespace FarmacySystem.controller
{
    public class CrudSale
    {
        public void InsertSales(Sale sale)
        {
            using (var db = new AppDbContext())
            {
                db.Sales.Add(sale);
                db.SaveChanges();
            }
        }
        public List<string> ListSales()
        {
            List<string> SalesList = new List<string>();

            using (var db = new AppDbContext())
            {
                var sales = db.Sales.ToList();
                foreach (var sale in sales)
                {
                    SalesList.Add($"{sale.Id}{sale.Customer}{sale.SaleDate}{sale.TotalValue}{sale.SalesmanId}");
                }
            }
            return SalesList;
        }
        public void SalesUpdate(int id, string? customer = null, DateTime? sale_date = null, decimal? total_value = null, int? salesman_id = null)
        {
            using (var db = new AppDbContext())
            {
                var sale = db.Sales.Find(id);
                if (sale != null)
                {
                    sale.Customer = customer ?? sale.Customer;
                    sale.SaleDate = sale_date ?? sale.SaleDate;
                    sale.TotalValue = total_value ?? sale.TotalValue;
                    sale.SalesmanId = salesman_id ?? sale.SalesmanId;
                    db.SaveChanges();
                    System.Console.WriteLine("Venda atualizada com sucesso");
                }
                else
                {
                    System.Console.WriteLine("Venda não encontrada");
                }
            }
        }
        public void SalesDelete(int id)
        {
            using (var db = new AppDbContext())
            {
                var sale = db.Sales.Find(id);
                if (sale != null)
                {
                    db.Sales.Remove(sale);
                    db.SaveChanges();
                    System.Console.WriteLine("Venda deletada com sucesso");
                }
                else
                {
                    System.Console.WriteLine("Venda não encontrada");
                }
            }
        }
    }
}