using System;
using System.Drawing;
using System.Windows.Forms;
using SwiftDocs.Services;

namespace SwiftDocs.Forms
{
    public partial class MenuForm : Form
    {
        private readonly UserManager userManager;

        public MenuForm(UserManager userManager)
        {
            this.userManager = userManager;
            InitializeFormControls();
        }

        private void InitializeFormControls()
        {
            Text = "Menu";
            Size = new Size(500, 400);
            StartPosition = FormStartPosition.CenterScreen;

            var lblTitle = new Label
            {
                Text = "Swift Docs",
                Location = new Point(150, 100),
                AutoSize = true,
                Font = new Font("Consolas", 25)
            };

            var btnLogin = new Button
            {
                Text = "Login",
                Location = new Point(120, 200),
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
                Width = 100,
                Height = 50
            };
            btnLogin.Click += BtnLogin_Click;

            var btnSignup = new Button
            {
                Text = "Signup",
                Location = new Point(280, 200),
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
                Width = 100,
                Height = 50
            };
            btnSignup.Click += BtnSignup_Click;

            Controls.AddRange(new Control[]
            {
                lblTitle,
                btnLogin,
                btnSignup
            });
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Hide();

            var loginForm = new LoginForm(userManager);
            loginForm.FormClosed += AnyForm_FormClosed;
            loginForm.Show();
        }

        private void BtnSignup_Click(object sender, EventArgs e)
        {
            Hide();

            var signupForm = new SignupForm(userManager);
            signupForm.FormClosed += AnyForm_FormClosed;
            signupForm.Show();
        }

        private void AnyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Show();
        }
    }
}