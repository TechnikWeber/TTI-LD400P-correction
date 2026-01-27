using System;
using System.Drawing;
using System.Windows.Forms;

namespace LD400PCalc
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CalcForm());
        }
    }

    public class CalcForm : Form
    {
        private TextBox txtMah;
        private TextBox txtDeviation;
        private Label lblPromptMah;
        private Label lblPromptDeviation;
        private Label lblResult;
        private Button btnCalculate;

        public CalcForm()
        {
            this.Text = "LD400P mAh-Korrektur";
            this.Size = new Size(450, 200);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Prompt Label für mAh
            lblPromptMah = new Label();
            lblPromptMah.Text = "Gemessene mAh eingeben:";
            lblPromptMah.Location = new Point(12, 20);
            lblPromptMah.AutoSize = true;
            this.Controls.Add(lblPromptMah);

            // TextBox für mAh
            txtMah = new TextBox();
            txtMah.Location = new Point(220, 17);
            txtMah.Size = new Size(100, 22);
            this.Controls.Add(txtMah);

            // Prompt Label für Abweichung
            lblPromptDeviation = new Label();
            lblPromptDeviation.Text = "Abweichung pro Stunde (Sek.):";
            lblPromptDeviation.Location = new Point(12, 60);
            lblPromptDeviation.AutoSize = true;
            this.Controls.Add(lblPromptDeviation);

            // TextBox für Abweichung
            txtDeviation = new TextBox();
            txtDeviation.Location = new Point(220, 57);
            txtDeviation.Size = new Size(100, 22);
            txtDeviation.Text = "23"; // Standardwert
            this.Controls.Add(txtDeviation);

            // Button
            btnCalculate = new Button();
            btnCalculate.Text = "Berechnen";
            btnCalculate.Location = new Point(340, 37);
            btnCalculate.Size = new Size(90, 25);
            btnCalculate.Click += BtnCalculate_Click;
            this.Controls.Add(btnCalculate);

            // Ergebnis Label
            lblResult = new Label();
            lblResult.Location = new Point(12, 100);
            lblResult.Size = new Size(410, 40);
            lblResult.Font = new Font(lblResult.Font.FontFamily, 10, FontStyle.Bold);
            this.Controls.Add(lblResult);
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtMah.Text, out double mah))
            {
                MessageBox.Show("Bitte gültigen mAh-Wert eingeben.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!double.TryParse(txtDeviation.Text, out double deviationSeconds))
            {
                MessageBox.Show("Bitte gültige Abweichung in Sekunden eingeben.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Faktor berechnen: 1 + (Sekundenfehler pro Stunde / 3600)
            double factor = 1 + (deviationSeconds / 3600.0);
            double corrected = mah * factor;

            lblResult.Text = $"Korrigiert: {corrected:F2} mAh\n(Eingestellte Abweichung: {deviationSeconds} s/h)";
        }
    }
}
