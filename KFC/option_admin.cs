using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KFC
{
    public partial class option_admin : Form
    {
        public option_admin()
        {
            InitializeComponent();
        }
        bool A;
        bool B;
        bool C;
        bool D;
        bool E;
        bool F;
        bool G;
        //
        bool H;
        bool I;

        private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch1.Checked == true)
            {
                guna2ToggleSwitch2.Checked = true;
                guna2ToggleSwitch3.Checked = true;
                guna2ToggleSwitch4.Checked = true;
                guna2ToggleSwitch5.Checked = true;
                guna2ToggleSwitch6.Checked = true;
                guna2ToggleSwitch7.Checked = true;
                guna2ToggleSwitch8.Checked = true;

            }
            else
            {
                guna2ToggleSwitch2.Checked = false;
                guna2ToggleSwitch3.Checked = false;
                guna2ToggleSwitch4.Checked = false;
                guna2ToggleSwitch5.Checked = false;
                guna2ToggleSwitch6.Checked = false;
                guna2ToggleSwitch7.Checked = false;
                guna2ToggleSwitch8.Checked = false;
            }
        }

        private void guna2ToggleSwitch2_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch2.Checked == true)
            {
                Properties.Settings.Default.datagrid_9 = true;
                A = true;
                if (A == true | B == true | C == true | D == true | E == true)
                {
                    guna2ToggleSwitch7.Enabled = true;
                    guna2ToggleSwitch8.Enabled = true;
                }
            }
            else
            {
                Properties.Settings.Default.datagrid_9 = false;
               A = false;
                if (A == false & B == false & C == false & D == false & E == false)
                {
                    guna2ToggleSwitch7.Checked = false;
                    guna2ToggleSwitch8.Checked = false;
                    guna2ToggleSwitch7.Enabled = false;
                    guna2ToggleSwitch8.Enabled = false;
                    Properties.Settings.Default.report_admin = false;
                    Properties.Settings.Default.delete = false;
                }
            }
        }

        private void guna2ToggleSwitch3_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch3.Checked == true)
            {
                Properties.Settings.Default.datagrid_8 = true;
                B = true;
                if (A == true | B == true | C == true | D == true | E == true )
                {
                    guna2ToggleSwitch7.Enabled = true;
                    guna2ToggleSwitch8.Enabled = true;
                }
            }
            else
            {
                Properties.Settings.Default.datagrid_8 = false;
                Properties.Settings.Default.Save();
                B = false;
                if (A == false & B == false & C == false & D == false & E == false )
                {
                    if (guna2ToggleSwitch7.Checked == true) { guna2ToggleSwitch7.Checked = false; }
                    if (guna2ToggleSwitch8.Checked == true) { guna2ToggleSwitch8.Checked = false; }
                    guna2ToggleSwitch7.Enabled = false;
                    guna2ToggleSwitch8.Enabled = false;
                    Properties.Settings.Default.report_admin = false;
                    Properties.Settings.Default.delete = false;
                }
            }
        }

        private void guna2ToggleSwitch4_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch4.Checked == true)
            {
                Properties.Settings.Default.datagrid_7 = true;
                C= true;
                if (A == true | B == true | C == true | D == true | E == true )
                {
                    guna2ToggleSwitch7.Enabled = true;
                    guna2ToggleSwitch8.Enabled = true;
                }
            }
            else
            {
                Properties.Settings.Default.datagrid_7 = false;
                Properties.Settings.Default.Save();
                C = false;
                if (A == false & B == false & C == false & D == false & E == false )
                {
                    if (guna2ToggleSwitch7.Checked == true) { guna2ToggleSwitch7.Checked = false; }
                    if (guna2ToggleSwitch8.Checked == true) { guna2ToggleSwitch8.Checked = false; }
                    guna2ToggleSwitch7.Enabled = false;
                    guna2ToggleSwitch8.Enabled = false;
                    Properties.Settings.Default.report_admin = false;
                    Properties.Settings.Default.delete = false;
                }
            }
        }

        private void guna2ToggleSwitch5_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch5.Checked == true)
            {
                Properties.Settings.Default.datagrid_6 = true;
                D = true;
                if (A == true | B == true | C == true | D == true | E == true )
                {
                    guna2ToggleSwitch7.Enabled = true;
                    guna2ToggleSwitch8.Enabled = true;
                }
            }
            else
            {
                Properties.Settings.Default.datagrid_6 = false;
                Properties.Settings.Default.Save();
                D = false;
                if (A == false & B == false & C == false & D == false & E == false )
                {
                    if (guna2ToggleSwitch7.Checked == true) { guna2ToggleSwitch7.Checked = false; }
                    if (guna2ToggleSwitch8.Checked == true) { guna2ToggleSwitch8.Checked = false; }
                    guna2ToggleSwitch7.Enabled = false;
                    guna2ToggleSwitch8.Enabled = false;
                    Properties.Settings.Default.report_admin = false;
                    Properties.Settings.Default.delete = false;
                }
            }
        }

        private void guna2ToggleSwitch6_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch6.Checked == true)
            {
                Properties.Settings.Default.datagrid_5 = true;
                E = true;
                if (A == true | B == true | C == true | D == true | E == true )
                {
                    guna2ToggleSwitch7.Enabled = true;
                    guna2ToggleSwitch8.Enabled = true;
                }
            }
            else
            {
                Properties.Settings.Default.datagrid_5 = false;
                E = false;
                if (A == false & B == false & C == false & D == false & E == false )
                {
                    if (guna2ToggleSwitch7.Checked == true) { guna2ToggleSwitch7.Checked = false; }
                    if (guna2ToggleSwitch8.Checked == true) { guna2ToggleSwitch8.Checked = false; }
                    guna2ToggleSwitch7.Enabled = false;
                    guna2ToggleSwitch8.Enabled = false;
                    Properties.Settings.Default.report_admin = false;
                    Properties.Settings.Default.delete = false;
                }
            }
        }

        private void guna2ToggleSwitch7_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch7.Checked == true)
            {
                Properties.Settings.Default.delete = true;
                if (A == true | B == true | C == true | D == true | E == true ) 
                { 
                    guna2ToggleSwitch7.Enabled = true;
                    guna2ToggleSwitch8.Enabled = true;
                }
            }
            else
            {
                Properties.Settings.Default.delete = false;
                if (A == false & B == false & C == false & D == false & E == false)
                {
                    if (guna2ToggleSwitch7.Checked == true) { guna2ToggleSwitch7.Checked = false; }
                    if (guna2ToggleSwitch8.Checked == true) { guna2ToggleSwitch8.Checked = false; }
                    guna2ToggleSwitch7.Enabled = false;
                    guna2ToggleSwitch8.Enabled = false;
                    Properties.Settings.Default.report_admin = false;
                    Properties.Settings.Default.delete = false;
                }
            }
        }

        private void guna2ToggleSwitch8_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch8.Checked == true)
            {
                Properties.Settings.Default.report_admin = true;
                if (A == true | B == true | C == true | D == true | E == true )
                {
                    guna2ToggleSwitch7.Enabled = true;
                    guna2ToggleSwitch8.Enabled = true;
                }
            }
            else
            {
                Properties.Settings.Default.report_admin = false;
                Properties.Settings.Default.Save();
                if (A == false & B == false & C == false & D == false & E == false )
                {
                    if (guna2ToggleSwitch7.Checked == true) { guna2ToggleSwitch7.Checked = false; }
                    if (guna2ToggleSwitch8.Checked == true) { guna2ToggleSwitch8.Checked = false; }
                    guna2ToggleSwitch7.Enabled = false;
                    guna2ToggleSwitch8.Enabled = false;
                    Properties.Settings.Default.report_admin = false;
                    Properties.Settings.Default.delete = false;
                }
            }
        }

        private void guna2ToggleSwitch9_CheckedChanged(object sender, EventArgs e)
        {
            if(guna2ToggleSwitch9.Checked==true)
            {
                guna2ToggleSwitch10.Checked = true;
                guna2ToggleSwitch11.Checked = true;
            }
            else
            {
                guna2ToggleSwitch10.Checked = false;
                guna2ToggleSwitch11.Checked = false;
            }
        }

        private void guna2ToggleSwitch10_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch10.Checked == true)
            {
                Properties.Settings.Default.data_amar = true;
                guna2ToggleSwitch11.Enabled = true;
                
            }
            else
            {
                Properties.Settings.Default.data_amar = false;
                if (guna2ToggleSwitch11.Checked == true)
                {
                    guna2ToggleSwitch11.Checked = false;
                }
                guna2ToggleSwitch11.Enabled = false;
            }
        }

        private void guna2ToggleSwitch11_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch11.Checked == true)
            {
                Properties.Settings.Default.report_amar = true;

            }
            else
            {
                Properties.Settings.Default.report_amar = false;
                
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            this.Close();
            MessageBox.Show("اطلاعات ذخیره شده و بعد از راه اندازی مجدد نرمفزار اجرا میشوند","اطلاع",MessageBoxButtons.OK);
        }
    }
}
