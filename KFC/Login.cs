using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE_KFC;
using BL_KFC;

namespace KFC
{
    public partial class Login : Form
    {
        Form1 form = new Form1();
        BL BLL = new BL();
        User ss = new User();
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
            (
                int nLeftRect,     // x-coordinate of upper-left corner
                int nTopRect,      // y-coordinate of upper-left corner
                int nRightRect,    // x-coordinate of lower-right corner
                int nBottomRect,   // y-coordinate of lower-right corner
                int nWidthEllipse, // width of ellipse
                int nHeightEllipse // height of ellipse
            );
        public Login()
        {
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;
            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox2.TextLength >= 1& textBox3.TextLength >= 1)
            {
                ss.username = textBox2.Text;
                ss.password = textBox3.Text;
                if (BLL.vorod(ss) == true)
                {
                    if(BLL.check_manager(ss.username)==true)
                    {
                        Form1.ADMIN_LOGN = true;
                    }
                    else
                    {
                        Form1.ADMIN_LOGN = false;
                    }
                    Form1.username = ss.username;
                    this.Hide();
                    form.Show();
                }
                else
                {
                    textBox3.Clear();
                    label4.Visible = true;
                    timer1.Start();
                }
            }
          
        }
        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Visible = false;
            timer1.Stop();

        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (BLL.search_user_Username(textBox2.Text) != null)
            {

                User user = BLL.search_user_Username(textBox2.Text);
                pictureBox1.ImageLocation = user.picture_User;
            }
            else
            {

                pictureBox1.Image = pictureBox1.BackgroundImage ;
            }
        }
    }
}
