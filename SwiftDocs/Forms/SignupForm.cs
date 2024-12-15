using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using SwiftDocs.Services;

namespace SwiftDocs.Forms
{
    public partial class SignupForm : Form
    {
        private readonly UserManager userManager;
        private ComboBox cbDay;
        private ComboBox cbMonth;
        private ComboBox cbYear;
        private RadioButton radioButtonFemale;
        private RadioButton radioButtonMale;
        private TextBox txtBarangay;
        private TextBox txtCityMunicipality;
        private TextBox txtConfirmPassword;

        private TextBox txtEmail;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtMiddleName;
        private TextBox txtPassword;
        private TextBox txtProvince;
        private TextBox txtStreet;

        public SignupForm(UserManager userManager)
        {
            this.userManager = userManager;
            InitializeFormControls();
        }

        private void InitializeFormControls()
        {
            Text = "Sign Up";
            Size = new Size(1155, 475);
            StartPosition = FormStartPosition.CenterScreen;

            var gbPersonalInfo = new GroupBox
            {
                Text = "Personal Information",
                Location = new Point(20, 20),
                Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold),
                Width = 350,
                Height = 400
            };

            txtFirstName = new TextBox
            {
                Location = new Point(20, 50),
                Width = 250
            };
            var lblFirstName = new Label
            {
                Text = "First Name:",
                Location = new Point(20, 20),
                AutoSize = true
            };

            txtMiddleName = new TextBox
            {
                Location = new Point(20, 120),
                Width = 250
            };
            var lblMiddleName = new Label
            {
                Text = "Middle Name:",
                Location = new Point(20, 90),
                AutoSize = true
            };

            txtLastName = new TextBox
            {
                Location = new Point(20, 190),
                Width = 250
            };
            var lblLastName = new Label
            {
                Text = "Last Name:",
                Location = new Point(20, 160),
                AutoSize = true
            };

            var lblDob = new Label
            {
                Text = "Date of Birth:",
                Location = new Point(20, 310),
                AutoSize = true
            };

            cbMonth = new ComboBox
            {
                Location = new Point(20, 340),
                Width = 80,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cbMonth.Items.AddRange(
                CultureInfo.CurrentCulture.DateTimeFormat.MonthNames
                    .Where(m => !string.IsNullOrEmpty(m))
                    .Cast<object>()
                    .ToArray()
            );

            cbDay = new ComboBox
            {
                Location = new Point(110, 340),
                Width = 60,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            for (var i = 1; i <= 31; i++) cbDay.Items.Add(i.ToString());

            cbYear = new ComboBox
            {
                Location = new Point(180, 340),
                Width = 70,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            var currentYear = DateTime.Now.Year;
            for (var i = currentYear; i >= 1900; i--) cbYear.Items.Add(i.ToString());

            var gbSex = new GroupBox
            {
                Text = "Sex",
                Location = new Point(20, 230),
                Width = 250,
                Height = 60
            };

            radioButtonMale = new RadioButton
            {
                Text = "Male",
                Location = new Point(10, 20),
                Width = 100
            };

            radioButtonFemale = new RadioButton
            {
                Text = "Female",
                Location = new Point(130, 20),
                Width = 100
            };
            
            gbSex.Controls.AddRange(new Control[]
            {
                radioButtonMale,
                radioButtonFemale
            });
            
            gbPersonalInfo.Controls.AddRange(new Control[]
            {
                lblFirstName, txtFirstName,
                lblMiddleName, txtMiddleName,
                lblLastName, txtLastName,
                lblDob, cbMonth, cbDay, cbYear,
                gbSex
            });
            
            var gbAddress = new GroupBox
            {
                Text = "Address Information",
                Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold),
                Location = new Point(400, 20),
                Width = 350,
                Height = 320
            };

            txtProvince = new TextBox
            {
                Location = new Point(20, 50),
                Width = 250
            };
            var lblProvince = new Label
            {
                Text = "Province:",
                Location = new Point(20, 20),
                AutoSize = true
            };

            txtCityMunicipality = new TextBox
            {
                Location = new Point(20, 120),
                Width = 250
            };
            var lblCityMunicipality = new Label
            {
                Text = "City/Municipality:",
                Location = new Point(20, 90),
                AutoSize = true
            };

            txtBarangay = new TextBox
            {
                Location = new Point(20, 190),
                Width = 250
            };
            var lblBarangay = new Label
            {
                Text = "Barangay:",
                Location = new Point(20, 160),
                AutoSize = true
            };

            txtStreet = new TextBox
            {
                Location = new Point(20, 260),
                Width = 250
            };
            var lblStreet = new Label
            {
                Text = "Street:",
                Location = new Point(20, 230),
                AutoSize = true
            };
            
            gbAddress.Controls.AddRange(new Control[]
            {
                lblProvince, txtProvince,
                lblCityMunicipality, txtCityMunicipality,
                lblBarangay, txtBarangay,
                lblStreet, txtStreet
            });
            
            var gbAccountInfo = new GroupBox
            {
                Text = "Account Details",
                Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold),
                Location = new Point(780, 20),
                Width = 350,
                Height = 250
            };

            txtEmail = new TextBox
            {
                Location = new Point(20, 50),
                Width = 250
            };
            var lblEmail = new Label
            {
                Text = "Email:",
                Location = new Point(20, 20),
                AutoSize = true
            };

            txtPassword = new TextBox
            {
                Location = new Point(20, 120),
                Width = 250,
                UseSystemPasswordChar = true
            };
            var lblPassword = new Label
            {
                Text = "Password:",
                Location = new Point(20, 90),
                AutoSize = true
            };

            txtConfirmPassword = new TextBox
            {
                Location = new Point(20, 190),
                Width = 250,
                UseSystemPasswordChar = true
            };
            var lblConfirmPassword = new Label
            {
                Text = "Confirm Password:",
                Location = new Point(20, 160),
                AutoSize = true
            };
            
            gbAccountInfo.Controls.AddRange(new Control[]
            {
                lblEmail, txtEmail,
                lblPassword, txtPassword,
                lblConfirmPassword, txtConfirmPassword
            });
            
            var btnCreateAccount = new Button
            {
                Text = "Create Account",
                Location = new Point(780, 395),
                Font = new Font(FontFamily.GenericSansSerif, 11, FontStyle.Bold),
                Width = 150,
                Height = 30
            };
            btnCreateAccount.Click += btnCreateAccount_Click;

            var btnGoBack = new Button
            {
                Text = "Go Back",
                Font = new Font(FontFamily.GenericSansSerif, 11, FontStyle.Bold),
                Location = new Point(980, 395),
                Width = 150,
                Height = 30
            };
            btnGoBack.Click += BtnGoback_Click;
            
            Controls.AddRange(new Control[]
            {
                gbPersonalInfo,
                gbAddress,
                gbAccountInfo,
                btnCreateAccount,
                btnGoBack
            });
        }

        private readonly EmailValidator emailValidator = new EmailValidator();
        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            var firstName = txtFirstName.Text.Trim();
            var middleName = txtMiddleName.Text.Trim();
            var lastName = txtLastName.Text.Trim();
            var sex = radioButtonMale.Checked ? radioButtonMale.Text :
                radioButtonFemale.Checked ? radioButtonFemale.Text :
                string.Empty;
            DateTime dateOfBirth;
            string formattedDateOfBirth;
            var selectedMonth = cbMonth.SelectedItem?.ToString();
            var selectedDay = cbDay.SelectedItem?.ToString();
            var selectedYear = cbYear.SelectedItem?.ToString();
            var province = txtProvince.Text.Trim();
            var cityMunicipality = txtCityMunicipality.Text.Trim();
            var barangay = txtBarangay.Text.Trim();
            var street = txtStreet.Text.Trim();
            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text;
            var confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(firstName))
            {
                MessageBox.Show("Please enter First Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(lastName))
            {
                MessageBox.Show("Please enter Last Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(sex))
            {
                MessageBox.Show("Please select Sex", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(selectedMonth) || string.IsNullOrEmpty(selectedDay) ||
                string.IsNullOrEmpty(selectedYear))
            {
                MessageBox.Show("Please select a valid Date of Birth", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (!DateTime.TryParseExact($"{selectedMonth} {selectedDay} {selectedYear}", "MMMM d yyyy",
                    CultureInfo.CurrentCulture, DateTimeStyles.None, out dateOfBirth))
            {
                MessageBox.Show("Invalid Date of Birth", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            formattedDateOfBirth = dateOfBirth.ToString("yyyy/MM/dd");

            if (string.IsNullOrEmpty(province))
            {
                MessageBox.Show("Please enter Province", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(cityMunicipality))
            {
                MessageBox.Show("Please enter City/Municipality", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(barangay))
            {
                MessageBox.Show("Please enter Barangay", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(street))
            {
                MessageBox.Show("Please enter Street", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (!emailValidator.TryValidate(email, out string emailErrorMessage))
            {
                MessageBox.Show(emailErrorMessage, "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter Email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please confirm Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (userManager.RegisterUser(firstName, middleName, lastName, sex, formattedDateOfBirth, province,
                    cityMunicipality,
                    barangay, street, email, password))
            {
                userManager.SaveUsersToFile();
                MessageBox.Show("Signup Successful!", "Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Email already exists!", "Signup Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGoback_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}