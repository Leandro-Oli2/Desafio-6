using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmacySystem.model;
using FarmacySystem.data;

namespace FarmacySystem.controller
{
    public class CrudReport
    {
        public void InsertReport(int IdR, string DescriptionR, DateTime CreatedAtR, int UserIdR)
        {
            using (var db = new AppDbContext())
            {
                db.Reports.Add(new Report { Id = IdR, Description = DescriptionR, CreatedAt = CreatedAtR, UserId = UserIdR });
                db.SaveChanges();
            }
        }
        
        public List<string> ListReports()
        {
            List<string> ReportList = new List<string>();

            using (var db = new AppDbContext())
            {
                var reports = db.Reports.ToList();
                foreach (var report in reports)
                {
                    ReportList.Add($"{report.Id}|{report.Description}|{report.CreatedAt:yyyy-MM-dd}|{report.UserId}");
                }
            }
            return ReportList;
        }

        public void ReportsUpdate(int id, string? description = null, DateTime? createdAt = null, int? userId = null)
        {
            using (var db = new AppDbContext())
            {
                var report = db.Reports.Find(id);
                if (report != null)
                {
                    report.Description = description ?? report.Description;
                    report.CreatedAt = createdAt ?? report.CreatedAt;
                    report.UserId = userId ?? report.UserId;
                    db.SaveChanges();
                    System.Console.WriteLine("Relatório atualizado com sucesso");
                }
                else
                {
                    System.Console.WriteLine("Relatório não encontrado");
                }
            }
        }
        public void ReportsDelete(int id)
        {
            using (var db = new AppDbContext())
            {
                var report = db.Reports.Find(id);
                if (report != null)
                {
                    db.Reports.Remove(report);
                    db.SaveChanges();
                    System.Console.WriteLine("Relatório deletado com sucesso");
                }
                else
                {
                    System.Console.WriteLine("Relatório não encontrado");
                }
            }
        }
    }
}