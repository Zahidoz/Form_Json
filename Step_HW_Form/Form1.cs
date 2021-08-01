using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Step_HW_Form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        class User
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; } 
            public string Age { get; set; }
            public string Phone { get; set; }
            public bool Gender { get; set; } 
             
        }



        private void btn_Register_Click(object sender, EventArgs e)
        {
            if (txt_Name.Text != "" &&
                    txt_Surname.Text != "" &&
                    txt_Age.Text != "" &&
                    txtMask_Phone.Text != "" &&
                    (rbtn_Male.Checked == true || rbtn_Female.Checked == true) &&
                    txt_Username.Text != "" &&
                    txt_Password.Text != "" ) 
            {
                User user = new User
                {
                    Name = txt_Name.Text,
                    Surname = txt_Surname.Text,
                    Age = txt_Age.Text,
                    Phone = txtMask_Phone.Text,
                    UserName = txt_Username.Text,
                    Password = txt_Password.Text
                };
                if (rbtn_Male.Checked) { user.Gender = true; }
                else user.Gender = false;
                var TextJson = JsonSerializer.Serialize(user, new JsonSerializerOptions() { WriteIndented = true });
                using (FileStream fs = new FileStream($"{user.UserName}.json", FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                    {
                        sw.WriteLine(TextJson);
                        MessageBox.Show("Json is writed");
                        txt_Name.Text = "";
                        txt_Surname.Text = "";
                        txt_Username.Text = "";
                        txt_Password.Text = "";
                        txt_Age.Text = "";
                        txtMask_Phone.Text = "";
                        rbtn_Male.Checked = false;
                        rbtn_Female.Checked = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Butun Xanalar Doldurulmalidir");
            }

        }

     

        private void txtx_Age_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) 
            {
                e.Handled = true;
                DialogResult da = MessageBox.Show("Digit Only");
            }
        }


        private void txt_Password_TextChanged(object sender, EventArgs e)
        {
            txt_Password.PasswordChar = '*';
        }

        private void chekBx_ShowPassword_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chekBx_ShowPassword.Checked)
                txt_Password.PasswordChar = '\0';
            else
                txt_Password.PasswordChar = '*';
        }

        private void btn_ScanJson_Click(object sender, EventArgs e)
        {
            try
            {
                var text = File.ReadAllText($"{txt_ScanJson.Text}.json");
                User JsonUser = JsonSerializer.Deserialize<User>(text);
                txt_Name.Text = JsonUser.Name;
                txt_Surname.Text = JsonUser.Surname;
                txt_Age.Text = JsonUser.Age;
                txtMask_Phone.Text = JsonUser.Phone; 
                txt_Username.Text = JsonUser.UserName;
                txt_Password.Text = JsonUser.Password;
                if (JsonUser.Gender) rbtn_Male.Checked = true;
                else rbtn_Female.Checked = true; 
            }
            catch (FileNotFoundException)
            {
                txt_ScanJson.Text = "File Not Found Exception"; 
            }
        }
    }
}
