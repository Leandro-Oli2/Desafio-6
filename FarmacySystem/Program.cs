using System;
using System.Windows.Forms;
using FarmacySystem.view;
using Microsoft.Extensions.Configuration;
using System.IO;
using FarmacySystem.controller;

using FarmacySystem.model;

namespace FarmacySystem.view
{
    static class Program
    {
        [STAThread]

        static void Main()
        {
            // CrudSale crud = new CrudSale();
            
            // crud.InsertSales("Customer", DateTime.UtcNow, 1, 1);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
