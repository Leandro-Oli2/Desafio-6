using System;
using System.Drawing;
using System.Windows.Forms;
using FarmacySystem.controller;
using FarmacySystem.model;
using System.Collections.Generic;
using System.Linq;
using FarmacySystem.data;
using Microsoft.EntityFrameworkCore;

namespace FarmacySystem.view
{
    public class FornecedorForm : Form
    {
        private TextBox txtnome, txtcnpj, txtphone, txtzip;
        private NumericUpDown number;
        private Button btnEnviar;

        public FornecedorForm()
        {
            this.Text = "Cadastro de Fornecedor";
            this.Size = new Size(400, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            Panel topBar = new Panel
            {
                Size = new Size(this.Width, 50),
                BackColor = Color.Purple,
                Dock = DockStyle.Top
            };
            this.Controls.Add(topBar);

            // Adicionando o texto "DigiMed Pharmacy" na top bar
            Label lblTopBar = new Label
            {
                Text = "DigiMed Pharmacy",
                ForeColor = Color.White,
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(300, 10),
                AutoSize = true
            };
            topBar.Controls.Add(lblTopBar);

            Panel formPanel = new Panel
            {
                Size = new Size(750, 500),
                Location = new Point(20, 60),
                BackColor = Color.FromArgb(180, 180, 251)
            };
            this.Controls.Add(formPanel);

            int startX = 250;
            int startY = 20;
            int spacing = 70;

            Label lblNome = new Label { Text = "Nome:", Location = new Point(startX, startY) };
            txtnome = new TextBox { Size = new Size(200, 20), Location = new Point(startX + 80, startY) };

            Label lblCNPJ = new Label { Text = "CNPJ:", Location = new Point(startX, startY + spacing) };
            txtcnpj = new TextBox { Size = new Size(200, 20), Location = new Point(startX + 80, startY + spacing) };

            Label lblPhone = new Label { Text = "Phone:", Location = new Point(startX, startY + spacing * 2) };
            txtphone = new TextBox { Size = new Size(200, 20), Location = new Point(startX + 80, startY + spacing * 2) };

            Label lblZip = new Label { Text = "Zip:", Location = new Point(startX, startY + spacing * 3) };
            txtzip = new TextBox { Size = new Size(200, 20), Location = new Point(startX + 80, startY + spacing * 3) };

            Label lblQtd = new Label { Text = "Number:", Location = new Point(startX, startY + spacing * 4) };
            number = new NumericUpDown { Size = new Size(200, 20), Location = new Point(startX + 80, startY + spacing * 4) };

            btnEnviar = new Button
            {
                Text = "Enviar",
                ForeColor = Color.White,
                Size = new Size(100, 30),
                Location = new Point(startX + 80, startY + spacing * 5),
                BackColor = Color.FromArgb(150, 10, 30)
            };
            btnEnviar.Click += BtnEnviar_Click;

            formPanel.Controls.Add(lblNome);
            formPanel.Controls.Add(txtnome);
            formPanel.Controls.Add(lblCNPJ);
            formPanel.Controls.Add(txtcnpj);
            formPanel.Controls.Add(lblPhone);
            formPanel.Controls.Add(txtphone);
            formPanel.Controls.Add(lblZip);
            formPanel.Controls.Add(txtzip);
            formPanel.Controls.Add(lblQtd);
            formPanel.Controls.Add(number);
            formPanel.Controls.Add(btnEnviar);
        }

        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Dados enviados com sucesso!", "Confirmação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
