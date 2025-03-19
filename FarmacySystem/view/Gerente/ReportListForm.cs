using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FarmacySystem.controller;

namespace FarmacySystem.view
{
    public class ReportListForm : Form
{
    private MainForm mainForm;
    private Panel headerPanel = null!;
    private Label lblHeader = null!;
    private DataGridView dgvReports = null!;
    private Button btnAtualizar = null!;
    private Button btnVoltar = null!;
    private CrudReport crudReport;
    private Panel scrollablePanel = null!; // Painel com rolagem

    public ReportListForm(MainForm mainForm)
    {
        this.mainForm = mainForm;
        crudReport = new CrudReport();
        InitializeComponent();
        this.Resize += new EventHandler(ResizeForm);
        CarregarReports();
    }

    private void InitializeComponent()
    {
        // Configurações gerais da janela
        this.Text = "DigiMed Pharmacy - Relatórios";
        this.Size = new Size(800, 600);
        this.MinimumSize = new Size(600, 500);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = Color.FromArgb(180, 180, 251);

        // Painel principal com barra de rolagem
        scrollablePanel = new Panel
        {
            AutoScroll = true, // Habilita a rolagem
            Dock = DockStyle.Fill // Faz o painel ocupar toda a área da Form
        };
        this.Controls.Add(scrollablePanel);

        // Painel de cabeçalho
        headerPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 60,
            BackColor = Color.FromArgb(75, 0, 110)
        };
        scrollablePanel.Controls.Add(headerPanel); // Adiciona ao painel rolável

        // Título do cabeçalho
        lblHeader = new Label
        {
            Text = "Relatórios",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 16F, FontStyle.Bold),
            AutoSize = true
        };
        headerPanel.Controls.Add(lblHeader);

        // DataGridView para exibir os relatórios
        dgvReports = new DataGridView
        {
            ReadOnly = true,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, // Ajuste automático das colunas
            BackgroundColor = Color.White,
            RowHeadersVisible = false,
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right // Ajusta para preencher a área disponível
        };
        dgvReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvReports.ColumnHeadersDefaultCellStyle.Font = new Font(dgvReports.Font, FontStyle.Bold);
        scrollablePanel.Controls.Add(dgvReports); // Adiciona ao painel rolável

        // Botão de atualizar
        btnAtualizar = new Button
        {
            Text = "Atualizar",
            Size = new Size(120, 35),
            BackColor = Color.FromArgb(75, 0, 110),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Arial", 12F, FontStyle.Bold),
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right // Fixa o botão na parte inferior
        };
        // Botão de voltar
        btnVoltar = new Button
        {
            Text = "Voltar",
            Size = new Size(120, 35),
            BackColor = Color.OrangeRed,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Arial", 12F, FontStyle.Bold),
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right // Fixa o botão na parte inferior
        };
        btnAtualizar.Click += new EventHandler(BtnAtualizar_Click);
        btnVoltar.Click += new EventHandler(BtnVoltar_Click);
        scrollablePanel.Controls.Add(btnAtualizar); // Adiciona ao painel rolável
        scrollablePanel.Controls.Add(btnVoltar); // Adiciona ao painel rolável

        ResizeForm(null, null); // Inicializa o layout
    }

    private void ResizeForm(object? sender, EventArgs? e)
    {
        // Ajusta o cabeçalho para o centro
        int centerX = this.ClientSize.Width / 2;
        lblHeader.Location = new Point((headerPanel.Width - lblHeader.Width) / 2, 15);

        // Ajusta a posição do DataGridView (preenchendo a área restante)
        dgvReports.Location = new Point(10, headerPanel.Bottom + 10);
        dgvReports.Size = new Size(this.ClientSize.Width - 20, this.ClientSize.Height - headerPanel.Bottom - 60);

        // Ajusta a posição e o tamanho do botão
        btnAtualizar.Location = new Point(centerX - btnAtualizar.Width / 2, dgvReports.Bottom + 10);
    }

    private void CarregarReports()
    {
        var reports = crudReport.ListReports();
        var dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("Descrição");
        dt.Columns.Add("Data");
        dt.Columns.Add("Usuário");

        foreach (var report in reports)
        {
            string[] parts = report.Split('|'); // Supondo que o retorno do banco tenha um delimitador

            if (parts.Length == 4)
            {
                dt.Rows.Add(parts[0], parts[1], parts[2], parts[3]);
            }
            else
            {
                // Caso o formato esteja errado, evita quebrar a tabela
                dt.Rows.Add("Erro", "Formato inválido", "-", "-");
            }
        }

        dgvReports.DataSource = dt;
    }

    private void BtnAtualizar_Click(object sender, EventArgs e)
    {
        CarregarReports();
    }

    private void BtnVoltar_Click (object sender, EventArgs e)
    {
        mainForm.TrocarTela(new ManagerForm(mainForm));
    }
}

}
