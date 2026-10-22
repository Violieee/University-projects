using lab7;
using System;
using System.Windows.Forms;

namespace lab7
{
    public partial class Form1 : Form
    {
        private readonly ConveyorBelt conveyor =
            new ConveyorBelt(100);

        private readonly MasterWorker master = new();
        private readonly Engineer engineer = new();
        private readonly Administration admin = new();

        public Form1()
        {
            InitializeComponent();

            conveyor.CriticalStop += () =>
            {
                AddLog(master.StopLine());
            };

            conveyor.CriticalStop += () =>
            {
                AddLog(engineer.Diagnose());
            };

            conveyor.CriticalStop += () =>
            {
                AddLog(admin.RecordDowntime());
                lblStatus.Text = "Стан: АВАРІЯ!";
            };

            conveyor.Restarted += () =>
            {
                AddLog(master.RestartLine());
            };

            conveyor.Restarted += () =>
            {
                AddLog(engineer.CheckAfterRestart());
                lblStatus.Text = "Стан: конвеєр працює";
            };

            conveyor.ItemProcessed += () =>
            {
                AddLog("Деталь успішно оброблено.");
            };

            conveyor.WeightChecked += () =>
            {
                AddLog("Перевірку ваги виконано.");
            };

            conveyor.MessageGenerated += AddLog;
            conveyor.MessageGenerated +=
                message => System.Diagnostics.Debug.WriteLine(message);

            btnCheck.Click += btnCheck_Click;
            btnReset.Click += btnReset_Click;
        }

        private void AddLog(string message)
        {
            lstLog.Items.Add(
                $"[{DateTime.Now:HH:mm:ss}] {message}");

            lstLog.TopIndex = lstLog.Items.Count - 1;
        }

        private void btnCheck_Click(object? sender, EventArgs e)
        {
            if (!double.TryParse(txtWeight.Text,
                out double weight) ||
                weight < 0 || double.IsInfinity(weight) ||
                double.IsNaN(weight))
            {
                MessageBox.Show(
                    "Введіть коректну невід'ємну вагу!");
                return;
            }

            AddLog($"Надійшла деталь вагою {weight} кг");
            conveyor.ProcessItem(weight);
        }

        private void btnReset_Click(object? sender, EventArgs e)
        {
            conveyor.Reset();
        }
    }
}
