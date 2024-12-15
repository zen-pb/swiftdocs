using System;
using System.Drawing;
using System.Windows.Forms;
using SwiftDocs.Models;

namespace SwiftDocs.Forms
{
    public partial class MainForm : Form
    {
        private readonly User currentUser;

        public MainForm(User user)
        {
            FormClosing += MainForm_FormClosing;
            currentUser = user;
            InitializeFormControls();
        }

        private void InitializeFormControls()
        {
            Text = "Dashboard";
            Size = new Size(380, 240);
            StartPosition = FormStartPosition.CenterScreen;

            var lblWelcome = new Label
            {
                Text = $"Welcome, {currentUser.FirstName}",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font(FontFamily.GenericSansSerif, 16, FontStyle.Bold)
            };

            var btnDriversLicense = new Button
            {
                Text = "Driver's License",
                Location = new Point(20, 70),
                Size = new Size(150, 50),
                Font = new Font(FontFamily.GenericSansSerif, 10.0f, FontStyle.Bold)
            };
            btnDriversLicense.Click += BTNDriversLicense_Click;

            var btnPhilHealthID = new Button
            {
                Text = "PhilHealth ID",
                Location = new Point(200, 70),
                Size = new Size(150, 50),
                Font = new Font(FontFamily.GenericSansSerif, 10.0f, FontStyle.Bold)
            };
            btnPhilHealthID.Click += BTNPhilHealthID_Click;

            var btnPSA = new Button
            {
                Text = "PSA",
                Location = new Point(20, 140),
                Size = new Size(150, 50),
                Font = new Font(FontFamily.GenericSansSerif, 10.0f, FontStyle.Bold)
            };
            btnPSA.Click += BTNPSA_Click;

            var btnPagIbigFund = new Button
            {
                Text = "Pag-IBIG Fund",
                Location = new Point(200, 140),
                Size = new Size(150, 50),
                Font = new Font(FontFamily.GenericSansSerif, 10.0f, FontStyle.Bold)
            };
            btnPagIbigFund.Click += BTNPagIbigFund_Click;

            Controls.AddRange(new Control[]
            {
                lblWelcome,
                btnDriversLicense, btnPhilHealthID,
                btnPSA, btnPagIbigFund
            });
        }

        private void BTNDriversLicense_Click(object sender, EventArgs e)
        {
            Hide();
            
            var formatForm = new FormatForm("drivers-license", currentUser);
            formatForm.FormClosed += AnyForm_FormClosed;
            formatForm.Show();
        }

        private void BTNPhilHealthID_Click(object sender, EventArgs e)
        {
            Hide();
            
            var formatForm = new FormatForm("phil-health-id", currentUser);
            formatForm.FormClosed += AnyForm_FormClosed;
            formatForm.Show();
        }

        private void BTNPSA_Click(object sender, EventArgs e)
        {
            Hide();
            
            var formatForm = new FormatForm("psa", currentUser);
            formatForm.FormClosed += AnyForm_FormClosed;
            formatForm.Show();
        }

        private void BTNPagIbigFund_Click(object sender, EventArgs e)
        {
            Hide();
            
            var formatForm = new FormatForm("pag-ibig-fund", currentUser);
            formatForm.FormClosed += AnyForm_FormClosed;
            formatForm.Show();
        }
        
        private void AnyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes) Close();
                e.Cancel = true;
            }
        }
    }
}