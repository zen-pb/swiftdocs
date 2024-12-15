using System;
using System.Drawing;
using System.Windows.Forms;
using SwiftDocs.Services;

namespace SwiftDocs.Forms
{
    public partial class LoginForm : Form
    {
        private readonly UserManager userManager;
        private TextBox txtEmail;
        private TextBox txtPassword;

        public LoginForm(UserManager userManager)
        {
            this.userManager = userManager;
            InitializeFormControls();
        }

        private void InitializeFormControls()
        {
            Text = "Login";
            Size = new Size(300, 230);
            StartPosition = FormStartPosition.CenterScreen;

            var lblEmail = new Label
            {
                Text = "Email:",
                Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };

            txtEmail = new TextBox
            {
                Location = new Point(20, 50),
                Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold),
                Width = 250
            };

            var lblPassword = new Label
            {
                Text = "Password:",
                Location = new Point(20, 80),
                Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold),
                AutoSize = true
            };

            txtPassword = new TextBox
            {
                Location = new Point(20, 110),
                Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold),
                Width = 250,
                PasswordChar = '*'
            };

            var btnLogin = new Button
            {
                Text = "Login",
                Location = new Point(20, 150),
                Font = new Font(FontFamily.GenericSansSerif, 11, FontStyle.Bold),
                Width = 100,
                Height = 30
            };
            btnLogin.Click += BtnLogin_Click;

            var btnGoback = new Button
            {
                Text = "Go Back",
                Location = new Point(170, 150),
                Font = new Font(FontFamily.GenericSansSerif, 11, FontStyle.Bold),
                Width = 100,
                Height = 30
            };
            btnGoback.Click += BtnGoback_Click;

            Controls.AddRange(new Control[]
            {
                lblEmail, txtEmail,
                lblPassword, txtPassword,
                btnLogin, btnGoback
            });
        }
        
        private readonly EmailValidator emailValidator = new EmailValidator();
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text;
            var password = txtPassword.Text;

            if (!emailValidator.TryValidate(email, out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            try
            {
                if (userManager.ValidateUser(email, password))
                {
                    var user = userManager.GetUser(email);

                    if (user != null)
                    {
                        var mainform = new MainForm(user);
                        mainform.Show();
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Email or password is wrong!", "Login Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Account doesn't exist!", "Login Failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnGoback_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}