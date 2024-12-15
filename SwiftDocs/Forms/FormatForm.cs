using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Mail;
using SwiftDocs.Models;
using SwiftDocs.Services;

namespace SwiftDocs.Forms
{
    public partial class FormatForm : Form
    {
        private readonly User curentUser;
        private readonly string document;
        private Label lblDocument;

        public FormatForm(string document, User currentUser)
        {
            curentUser = currentUser;
            this.document = document;
            InitializeFormControls();
        }

        private void InitializeFormControls()
        {
            Text = "Document Format";
            Size = new Size(340, 200);
            StartPosition = FormStartPosition.CenterScreen;

            switch (document)
            {
                case "drivers-license":
                    lblDocument = new Label
                    {
                        Text = "Driver's License",
                        Location = new Point(160, 20),
                        AutoSize = true,
                        Font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold)
                    };
                    break;

                case "phil-health-id":
                    lblDocument = new Label
                    {
                        Text = "PhilHealth ID",
                        Location = new Point(190, 20),
                        AutoSize = true,
                        Font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold)
                    };
                    break;

                case "psa":
                    lblDocument = new Label
                    {
                        Text = "PSA",
                        Location = new Point(270, 20),
                        AutoSize = true,
                        Font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold)
                    };
                    break;

                case "pag-ibig-fund":
                    lblDocument = new Label
                    {
                        Text = "Pag-IBIG Fund",
                        Location = new Point(180, 20),
                        AutoSize = true,
                        Font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold)
                    };
                    break;
            }

            var btnPDF = new Button
            {
                Text = "PDF",
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
                Size = new Size(140, 40),
                Location = new Point(20, 100)
            };
            btnPDF.Click += BTNPDF_Click;

            var btnPrinted = new Button
            {
                Text = "Printed (Delivery)",
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(140, 40),
                Location = new Point(170, 100)
            };
            btnPrinted.Click += BTNPrinted_Click;

            Controls.AddRange(new Control[]
            {
                lblDocument,
                btnPDF, btnPrinted
            });
        }

        private void BTNPDF_Click(object sender, EventArgs e)
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Documents", $"{curentUser.LastName}-{lblDocument.Text}.pdf");
                var title = lblDocument.Text;
                string content = "";

                switch (lblDocument.Text)
                {
                    case "Driver's License":
                        content = "Last Name. First Name. Middle Name"
                                  + $"\n{curentUser.LastName},     {curentUser.FirstName}         {curentUser.MiddleName}\n"
                                  + "\nSex     Date of Birth"
                                  + $"\n  {curentUser.Sex[0]}       {curentUser.DateOfBirth}\n"
                                  + "\nAddress"
                                  + $"\n{curentUser.Street}, {curentUser.Barangay}, {curentUser.CityMunicipality}, {curentUser.Province}";
                        break;
                    case "PhilHealth ID":
                        content = $"{curentUser.LastName}, {curentUser.FirstName} {curentUser.MiddleName}\n"
                                  + $"{curentUser.DateOfBirth} - {curentUser.Sex}\n"
                                  + $"{curentUser.Street}, {curentUser.Barangay}, {curentUser.CityMunicipality}, {curentUser.Province}";
                        break;
                    case "PSA":
                        content = $"Province                {curentUser.Province}\n"
                                  + $"City/Municipality   {curentUser.CityMunicipality}\n"
                                  + "1. NAME              (First)                       (Middle)                       (Last)\n"
                                  + $"                            {curentUser.FirstName}                      {curentUser.MiddleName}                    {curentUser.LastName}\n"
                                  + "2. SEX (Male/Female)       3. DATE OF BIRTH               (Day)               (Month)             (Year)\n"
                                  + $"                  {curentUser.Sex}                                                                 {curentUser.DateOfBirth.Substring(8)}                      {curentUser.DateOfBirth.Substring(5, 2)}                  {curentUser.DateOfBirth.Substring(0, 4)}";
                        break;
                    case "Pag-IBIG Fund":
                        content = $"{curentUser.FirstName} {curentUser.MiddleName[0]} {curentUser.LastName}";
                        break;
                }
                
                var generate = new PDFGenerator();
                generate.GeneratePdf(filePath, title, content);
                
                MailMessage mm = new MailMessage();
                
                SmtpClient smtp = new SmtpClient("SMTP provider")
                {
                    Port = 123, //Change the port according to your SMTP provider.
                    Credentials = new System.Net.NetworkCredential("SMTP email", "SMPTP key"),
                    EnableSsl = true
                };
                
                mm.From = new MailAddress("noreply@swiftdocs.com");
                mm.To.Add(curentUser.Email);
                mm.Subject = $"{lblDocument.Text} PDF Document";
                mm.Body = "Please find the attached file";
                
                string pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "Documents", $"{curentUser.LastName}-{lblDocument.Text}.pdf");
                
                if (File.Exists(pdfPath))
                {
                    Attachment attachment = new Attachment(pdfPath);
                    mm.Attachments.Add(attachment);
                }
                else
                {
                    throw new FileNotFoundException("PDF file not found at the specified path.");
                }
                
                smtp.Send(mm);
                
                MessageBox.Show($"The {lblDocument.Text} PDF Document has been sent successfully to your email: {curentUser.Email}", "PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send email. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BTNPrinted_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"The printed copy of the {lblDocument.Text} Document will be sent to your address: {curentUser.Street}, {curentUser.Barangay}, {curentUser.CityMunicipality}, {curentUser.Province}", "Printed (Delivery)", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}