using BE_KFC;
using BL_KFC;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;



namespace KFC
{
    public partial class Form1 : Form
    {
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
        public static bool ADMIN_LOGN = false;
        string time_vorod;
        public Form1()
        {
            InitializeComponent();
            // this.FormBorderStyle = FormBorderStyle.None;
            // Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            contextMenuStrip1.Items[4].Visible = false;      
            Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHl2AD0gPVknKsaW0un+3PuM6TTcPMUAWEURKXNso0e5OFPaZYasFtsxNoDemsFOXbvf7SIcnyAkFX/4u37NTfx7g+0IqLXw6QIPolr1PvCSZz8Z5wjBNakeCVozGGOiuCOQDy60XNqfbgrOjxgQ5y/u54K4g7R/xuWmpdx5OMAbUbcy3WbhPCbJJYTI5Hg8C/gsbHSnC2EeOCuyA9ImrNyjsUHkLEh9y4WoRw7lRIc1x+dli8jSJxt9C+NYVUIqK7MEeCmmVyFEGN8mNnqZp4vTe98kxAr4dWSmhcQahHGuFBhKQLlVOdlJ/OT+WPX1zS2UmnkTrxun+FWpCC5bLDlwhlslxtyaN9pV3sRLO6KXM88ZkefRrH21DdR+4j79HA7VLTAsebI79t9nMgmXJ5hB1JKcJMUAgWpxT7C7JUGcWCPIG10NuCd9XQ7H4ykQ4Ve6J2LuNo9SbvP6jPwdfQJB6fJBnKg4mtNuLMlQ4pnXDc+wJmqgw25NfHpFmrZYACZOtLEJoPtMWxxwDzZEYYfT";
            time_vorod = PersianDateTime.Now.ToString();
            label31.Text = PersianDateTime.Now.ToString("hh:mm");
            label33.Text =  "جناب آقای " + BLL.name_manager().ToString();
            label15.Text = BLL.day_money(Int64.Parse(PersianDateTime.Now.ToString("yyyyMMddhhmmss"))) + " تومان";
            label6.Text = (BLL.user_work(username, Int64.Parse(PersianDateTime.Now.ToString("yyyyMMddhhmmss")))).ToString("##:##");
            label29.Text = "به سیستم خوش آمدید ساعت ورود شما ثبت شد لطفا بعد از اتمام کار و پایان شیفت کاری دکمه (پایان شیفت )را بزنید.  نگران نباشید در صورت فراموش کردن، تایم کاری شما با حداکثر اختلاف 5دقیقه ذخیره میشود ";
            point = (label29.Text.Length * 10) * -1;
            timer7.Start();
            clocs.Start();
        }
        
        public static int ID=1;
        public Form form1 = new Form();


        public static string username="s";
        
        OpenFileDialog f = new OpenFileDialog();
        BL BLL = new BL();
        food fod = new food();
        public int n = 0;
        bool update = false;
        bool register_personel = true;
        public int timer_data = 0;
        int Combobox1_Chart = 10;
        
        string savepic(string cod)
        {

            if (update == true)
            {

                FileInfo info = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + @"\Pictures\" + cod + ".jpg");


                if (info.Exists)
                {
                    info.Delete();
                }

            }

            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Pictures\";
            if (Directory.Exists(appPath) == false)
            {
                Directory.CreateDirectory(appPath);
            }
            string iName = cod + ".jpg";
            try
            {
                string filepath = f.FileName;
                File.Copy(filepath, appPath + iName, true);

            }
            catch (Exception)
            {

                MessageBox.Show("محصول بدون تصویر ذخیره شد");
            }

            return appPath + iName;


        }

        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }
        private void button3_Click(object sender, EventArgs e)
        {

            Image pic;
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox8.Image != null)
                {
                    pictureBox8.Image.Dispose();
                    pictureBox8.Image = null;
                }
                pic = Image.FromFile(f.FileName);
                pictureBox8.Image = pic;
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            checkedListBox1.Visible = true;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            checkedListBox1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {




            if (update == false)
            {



                time tim = new time();

                fod.name = textBox5.Text;
                fod.tarkib = textBox6.Text;
                fod.money = int.Parse(textBox7.Text);
                if (pictureBox8.Image != null) { fod.picture = savepic(fod.name); }


                BLL.fod(fod);
                tim.food_id = fod.id;

                if (radioButton2.Checked == true)
                {
                    tim.saturday = 1;
                    tim.sunday = 1;
                    tim.monday = 1;
                    tim.tuesday = 1;
                    tim.wednesday = 1;
                    tim.thursday = 1;
                    tim.friday = 1;
                }
                else
                {
                    foreach (var item in checkedListBox1.CheckedItems)
                    {
                        if (item.ToString() == "شنبه") { tim.saturday = 1; }
                        if (item.ToString() == "یک شنبه") { tim.sunday = 1; }
                        if (item.ToString() == "دو شنبه") { tim.monday = 1; }
                        if (item.ToString() == "سه شنبه") { tim.tuesday = 1; }
                        if (item.ToString() == "چهار شنبه") { tim.wednesday = 1; }
                        if (item.ToString() == "پنج شنبه") { tim.thursday = 1; }
                        if (item.ToString() == "جمعه") { tim.friday = 1; }
                    }
                }
                BLL.RG_Time(tim);
                pictureBox8.Image = BackgroundImage;
                textBox5.Text = null;
                textBox6.Text = null;
                textBox7.Text = null;
            }
            else if (update == true)
            {
                time time_update = new time();
                food food_update = new food();
                food_update.name = textBox5.Text;
                food_update.tarkib = textBox6.Text;
                food_update.money = int.Parse(textBox7.Text);


                food_update.picture = savepic(food_update.name);



                time_update.food_id = ID;

                if (radioButton2.Checked == true)
                {
                    time_update.saturday = 1;
                    time_update.sunday = 1;
                    time_update.monday = 1;
                    time_update.tuesday = 1;
                    time_update.wednesday = 1;
                    time_update.thursday = 1;
                    time_update.friday = 1;
                }
                else
                {
                    foreach (var item in checkedListBox1.CheckedItems)
                    {
                        if (item.ToString() == "شنبه") { time_update.saturday = 1; }
                        if (item.ToString() == "یک شنبه") { time_update.sunday = 1; }
                        if (item.ToString() == "دو شنبه") { time_update.monday = 1; }
                        if (item.ToString() == "سه شنبه") { time_update.tuesday = 1; }
                        if (item.ToString() == "چهار شنبه") { time_update.wednesday = 1; }
                        if (item.ToString() == "پنج شنبه") { time_update.thursday = 1; }
                        if (item.ToString() == "جمعه") { time_update.friday = 1; }
                    }
                    checkedListBox1.SetItemChecked(1, false);
                }




                BLL.update_time(time_update, ID);
                BLL.update(food_update, ID);
                update = false;

            }
            clear();
            dataGridView2.DataSource = BLL.ss();
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = BLL.ss();
            dataGridView2.Columns[4].Visible = false;


        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            //Graphics gr = panel1.CreateGraphics();
            //Pen p = new Pen(Color.Black);
            //gr.DrawLine(p, 370, 432, 1100, 432);

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = (int)(dataGridView2.Rows[e.RowIndex].Cells[0].Value);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (BLL.food_search(textBox8.Text).Count >= 1)
            {

                dataGridView2.DataSource = BLL.food_search(textBox8.Text);
            }
        }





        

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = (int)(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
        }



        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() == "شنبه")
            {
                dataGridView2.DataSource = BLL.filter("شنبه");
            }
            if (comboBox2.SelectedItem.ToString() == "یک شنبه")
            {
                dataGridView2.DataSource = BLL.filter("یک شنبه");
            }
            if (comboBox2.SelectedItem.ToString() == "دو شنبه")
            {
                dataGridView2.DataSource = BLL.filter("دو شنبه");
            }
            if (comboBox2.SelectedItem.ToString() == "سه شنبه")
            {
                dataGridView2.DataSource = BLL.filter("سه شنبه");
            }
            if (comboBox2.SelectedItem.ToString() == "چهار شنبه")
            {
                dataGridView2.DataSource = BLL.filter("چهار شنبه");
            }
            if (comboBox2.SelectedItem.ToString() == "پنج شنبه")
            {
                dataGridView2.DataSource = BLL.filter("پنج شنبه");
            }
            if (comboBox2.SelectedItem.ToString() == "جمعه")
            {
                dataGridView2.DataSource = BLL.filter("جمعه");
            }
            if (comboBox2.SelectedItem.ToString() == "همه")
            {
                dataGridView2.DataSource = BLL.filter("همه");
            }

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Text.Count() >= 1)
            {
                comboBox2.SelectedItem = "همه";
                comboBox2.Enabled = false;
            }
            else
            {
                comboBox2.Enabled = true;
            }
        }



        private void textBox5_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            if (textBox5.Text.Count() <= 1)
            {
                errorProvider1.SetError(textBox5, "لطفا نام را وارد کنید");
            }
        }



        private void timer1_Tick(object sender, EventArgs e)
        {

           // tim();
            timer1.Stop();
            // errorProvider1.Clear();
        }


        private void textBox6_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            if (textBox6.Text.Count() <= 1)
            {
                errorProvider1.SetError(textBox6, "لطفا ترکیبات را وارد کنید");
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            if (textBox7.Text.Count() <= 1)
            {
                errorProvider1.SetError(textBox7, "لطفا قیمت را وارد کنید");
            }
        }

        public void clear()
        {
            pictureBox8.Image = null;
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemCheckState(i, false ? CheckState.Checked : CheckState.Unchecked);
            radioButton2.Checked = true;
            button2.Text = "ثبت اطلاعات";

        }
        private void button6_Click(object sender, EventArgs e)
        {
            update = false;


            clear();


            button6.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        //public void tim()
        //{
        //    PersianCalendar p = new PersianCalendar();
        //    DateTime dt = DateTime.Now;

        //    int y, m, d;
        //    y = p.GetYear(dt);
        //    m = p.GetMonth(dt);
        //    d = p.GetDayOfMonth(dt);
        //    label1.Text = y.ToString() + "/"
        //    + m.ToString() + "/"
        //    + d.ToString();
        //    timer1.Start();
        //}
        List<personel_History> p_h = new List<personel_History>();
        public int v = 0;
        private void button8_Click(object sender, EventArgs e)
        {
            if (guna2TextBox6.Text.Length >= 1 & guna2TextBox7.Text.Length >= 1)
            {
                personel_History P_H = new personel_History();
                P_H.id = v++;
                P_H.name_food = guna2TextBox6.Text;
                P_H.money = int.Parse(guna2TextBox7.Text);
                P_H.tedad = int.Parse(numericUpDown1.Value.ToString());
                P_H.money_Final = int.Parse(guna2TextBox8.Text);
                p_h.Add(P_H);
                dataGridView1.DataSource = p_h.ToList();
                guna2TextBox6.Clear();
                guna2TextBox7.Clear();
                guna2TextBox8.Clear();
                richTextBox1.Clear();
                numericUpDown1.Value = 1;
            }
            else
            {
                button8.BackColor = Color.Red;
                button8.Text = "لطفا مشخصات محصول را به صورت کامل وارد کنید";
                timer11.Start();
               

            }

            //dataGridView1.ClearSelection();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (p_h.Count() >= 1)
            {
                v = 0;

                int ID_Personel = 0;
                var q = PersianDateTime.Now.ToString("yyyyMMddhhmmss");

                if (textBox4.Text.Count() == 4)
                {
                    if (BLL.check_exist_eshterak(int.Parse(textBox4.Text)) == true)
                    {
                        ID_Personel = BLL.ID_personel(int.Parse(textBox4.Text));
                    }
                }
                foreach (var item in p_h)
                {
                    item.time = Int64.Parse(q);
                    item.personel_id = ID_Personel;
                }
                BLL.rejester_History(p_h);



                p_h.Clear();
                dataGridView1.DataSource = p_h;
                button1.BackColor = Color.Lime;
                button1.Text = "باموفقیت ثبت شد";
                timer11.Start();
                //MessageBox.Show("ثبت سفارش انجام شد");
            }
            else
            {
                button1.BackColor = Color.Red;
                button1.Text = "لطفا محصولی به فاکتور اضافه کنید";
                timer11.Start();
               
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Count() == 4)
            {

                if (BLL.check_exist_eshterak(int.Parse(textBox4.Text)) == false)
                {
                    textBox11.Enabled = true;
                    textBox12.Enabled = true;
                    register_personel = true;
                    button13.Visible = false;

                    button11.Text = "ثبت کاربر";
                }
                else
                {
                    // personel P_S= new personel();
                    personel P_S = BLL.search_Personel_eshterak(int.Parse(textBox4.Text));
                    textBox12.Text = P_S.name;
                    textBox11.Text = P_S.adres;
                    register_personel = false;
                    button11.Text = "ویرایش";
                    button9.Visible = true;

                }
            }
            else
            {
                button9.Visible = false;
                textBox11.Clear();
                textBox12.Clear();
                button11.Text = "ثبت کاربر";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //if (textBox2.Text.Count() >= 1)
            //{
            //    int x = int.Parse(textBox2.Text);
            //    int h = int.Parse(numericUpDown1.Value.ToString());
            //    textBox1.Text = (x * h).ToString();
            //}
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (guna2TextBox7.Text.Length >= 1)
            {
                
                int x = int.Parse(guna2TextBox7.Text);
                int h = int.Parse(numericUpDown1.Value.ToString());
                guna2TextBox8.Text = (x * h).ToString();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }



        private void button11_Click(object sender, EventArgs e)
        {

            if (textBox11.Text.Count() >= 1 && textBox12.Text.Count() >= 1)
            {
                if (register_personel == true)
                {
                    personel P_S = new personel();
                    P_S.eshterak = int.Parse(textBox4.Text);
                    P_S.name = textBox12.Text;
                    P_S.adres = textBox11.Text;
                    BLL.Register_Personel(P_S);
                    button11.Text = "ویرایش";
                    textBox11.Enabled = false;
                    textBox12.Enabled = false;
                    register_personel = false;
                    button9.Visible = true;

                }
                else
                {
                    if (textBox11.Enabled == false)
                    {
                        button11.Text = "ثبت ویرایش";
                        textBox11.Enabled = true;
                        textBox12.Enabled = true;
                    }
                    else
                    {
                        int eshterak = int.Parse(textBox4.Text);

                        personel Edit_P = BLL.search_Personel_eshterak(int.Parse(textBox4.Text));
                        if (int.Parse(textBox4.Text) != eshterak)
                        {
                            var q = BLL.check_exist_eshterak(int.Parse(textBox4.Text));
                            if (q == false)
                            {
                                Edit_P.eshterak = int.Parse(textBox4.Text);
                            }
                            else
                            {
                                MessageBox.Show("A");
                            }
                        }
                        Edit_P.eshterak = int.Parse(textBox4.Text);
                        Edit_P.name = textBox12.Text;
                        Edit_P.adres = textBox11.Text;
                        BLL.Edit_Personel(Edit_P);

                        textBox11.Enabled = false;
                        textBox12.Enabled = false;
                        //register_personel = true;
                        button11.Text = "ویرایش";





                    }

                }
            }
            else
            {
                button9.Enabled = true;
                MessageBox.Show("لطفا مشخصات را پر کنید");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox11.Clear();
            textBox12.Clear();
            textBox4.Clear();
            textBox4.Text = BLL.Auto_eshterak().ToString();
            register_personel = true;


        }


        private void button9_Click(object sender, EventArgs e)
        {
            if (BLL.histories(int.Parse(int.Parse(textBox4.Text).ToString())).Count() >= 1)
            {
                button13.Visible = true;
                dataGridView4.DataSource = BLL.histories(int.Parse(textBox4.Text));
                label5.Text = dataGridView4.RowCount.ToString() +  " مورد یافت شد  "  ;
                label5.Visible = true;
                timer2.Start();

            }
            else
            {
                label5.Visible = true;
                label5.Text = "موردی یافت نشد";
                timer2.Start();
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label5.Text = "";
            label5.Visible = false;
            timer2.Stop();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            
            //stiReport1.Load("I:/backup/New folder/KFC/KFC/bin/panel1_histore_user.mrt");
            var startupPath = System.Windows.Forms.Application.StartupPath;
            int A = startupPath.Length - 6;
            stiReport1.Load(startupPath.Substring(0, A) + @"\panel1_histore_user.mrt");
            var q = PersianDateTime.Now.ToString("hh:MM:ss");
            var q1 = PersianDateTime.Now.ToString("yy/MM/dd");
            stiReport1.Dictionary.Variables["Time_2"].Value = q;
            stiReport1.Dictionary.Variables["Time_1"].Value = q1;
            stiReport1.Dictionary.Variables["Eshterak"].Value = textBox4.Text;
            stiReport1.Dictionary.Variables["name"].Value = textBox12.Text;
            stiReport1.Dictionary.Variables["Address"].Value = textBox11.Text;
            Cause cause = new Cause();
            cause.ShowDialog();
            if (cause.richTextBox1.Text.Count() > 0)
            {
                stiReport1.Dictionary.Variables["Cause"].Value = cause.richTextBox1.Text;

            }
            else
            {
                stiReport1.Dictionary.Variables["Cause"].Value = "ندارد";
            }

            stiReport1.RegBusinessObject("L", BLL.histories(int.Parse(textBox4.Text)));

            stiReport1.Render();
            stiReport1.Show();

        }



        private void چاپToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var startupPath = System.Windows.Forms.Application.StartupPath;
            int A = startupPath.Length - 6;
            stiReport1.Load(startupPath.Substring(0, A) + @"\User_Histore_Zoom.mrt");
            var q1 = PersianDateTime.Now.ToString("hh:MM:ss");
            var q = PersianDateTime.Now.ToString("yy/MM/dd");

            stiReport1.Dictionary.Variables["Time_1"].Value = q;
            stiReport1.Dictionary.Variables["Time_2"].Value = q1;
            stiReport1.Dictionary.Variables["Eshterak"].Value = textBox4.Text;
            stiReport1.Dictionary.Variables["name"].Value = textBox12.Text;
            stiReport1.Dictionary.Variables["Address"].Value = textBox11.Text;
            Cause cause = new Cause();
            cause.ShowDialog();
            if (cause.richTextBox1.Text.Count() > 0)
            {
                stiReport1.Dictionary.Variables["Cause"].Value = cause.richTextBox1.Text;

            }
            else
            {
                stiReport1.Dictionary.Variables["Cause"].Value = "ندارد";
            }

            stiReport1.RegBusinessObject("M", BLL._Histories(Int64.Parse(dataGridView4.CurrentRow.Cells[1].Value.ToString())));
            stiReport1.Render();
            stiReport1.Show();
        }


        #region ثبت اطلاعات خرید در دیتا گرید 1 با اینتر
        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (guna2TextBox6.Text.Count() >= 1 & guna2TextBox7.Text.Count() >= 1)
                {
                    personel_History P_H = new personel_History();
                    P_H.name_food = guna2TextBox6.Text;
                    P_H.money = int.Parse(guna2TextBox7.Text);
                    P_H.tedad = int.Parse(numericUpDown1.Value.ToString());
                    P_H.money_Final = int.Parse(guna2TextBox8.Text);
                    p_h.Add(P_H);
                    dataGridView1.DataSource = p_h.ToList();
                    guna2TextBox6.Clear();
                    guna2TextBox7.Clear();
                    numericUpDown1.Value = 1;
                }
                else
                {
                    button8.BackColor = Color.Red;
                    button8.Text = "لطفا مشخصات محصول را به صورت کامل وارد کنید";
                    timer11.Start();
                }
            }
        }
        #endregion

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in p_h.ToList())
            {
                if (item.id == int.Parse(dataGridView1.CurrentRow.Cells[1].Value.ToString()))
                {
                    p_h.Remove(item);
                    //MessageBox.Show("A");
                    dataGridView1.DataSource = p_h.ToList();
                    dataGridView1.ClearSelection();
                    break;
                }
            }
            //MessageBox.Show(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            //MessageBox.Show(dataGridView1.CurrentRow.Cells[1].Value.ToString());
            //MessageBox.Show(dataGridView1.CurrentRow.Cells[2].Value.ToString());

            //MessageBox.Show(dataGridView1.CurrentRow.Cells[3].Value.ToString());
            //MessageBox.Show(dataGridView1.CurrentRow.Cells[4].Value.ToString());
            //MessageBox.Show(dataGridView1.CurrentRow.Cells[5].Value.ToString());
            //MessageBox.Show(dataGridView1.CurrentRow.Cells[6].Value.ToString());
            //MessageBox.Show(dataGridView1.CurrentRow.Cells[7].Value.ToString());
            // item.name_food == dataGridView1.CurrentRow.Cells[3].Value.ToString() && item.money == int.Parse(dataGridView1.CurrentRow.Cells[4].Value.ToString()) && item.tedad == int.Parse(dataGridView1.CurrentRow.Cells[5].Value.ToString()) && item.money_Final == int.Parse(dataGridView1.CurrentRow.Cells[6].Value.ToString())
        }

        

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = panel4.CreateGraphics();
            Pen p = new Pen(Color.Blue);
            gr.DrawLine(p, 40, 307, 1068, 307);
        }








        #region control chart typr
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var item in chart1.Series)
            {

                item.ChartType = SeriesChartType.Point;
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var item in chart1.Series)
            {
                item.ChartType = SeriesChartType.Spline;
            }

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var item in chart1.Series)
            {
                item.ChartType = SeriesChartType.Column;
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var item in chart1.Series)
            {
                item.ChartType = SeriesChartType.Line;
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var item in chart1.Series)
            {
                item.ChartType = SeriesChartType.SplineArea;
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var item in chart1.Series)
            {
                item.ChartType = SeriesChartType.StackedArea;
            }
        }
        #endregion

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton14.Checked == true)
            {
                Filter_Sell_Chart = true;


                int d = Combobox1_Chart;

                if (Combobox1_Chart == 0)
                {
                    comboBox1.SelectedIndex = 1;
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                    comboBox1.SelectedIndex = 0;
                    comboBox1.SelectedIndex = d;
                }




            }

        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton13.Checked == true)
            {
                Filter_Sell_Chart = false;


                int d = Combobox1_Chart;
                if (Combobox1_Chart == 0)
                {
                    comboBox1.SelectedIndex = 1;
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                    comboBox1.SelectedIndex = 0;
                    comboBox1.SelectedIndex = d;
                }

            }
        }

        List<History> Not_Sell = new List<History>();
        List<History> Yes_Sell = new List<History>();
        bool Filter_Sell_Chart = false;
        int notSell = 0;
        int sell = 0;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            solidGauge3.To = 0;
            solidGauge3.Value = 0;
            int money = 0;
            notSell = 0;
            sell = 0;
            int Money_Average = 0;
            Not_Sell.Clear();
            Yes_Sell.Clear();
            History history_High = new History();
            History history_Low = new History();




            if (comboBox1.SelectedItem.ToString() == "گزارش هفت روز گذشته")
            {
                foreach (var item in chart1.Series)
                {
                    item.Points.Clear();
                }
                List<int> LAst = new List<int>();
                for (int i = 0; i < 7; i++)
                {
                    var Last = PersianDateTime.Now.AddDays(-i);
                    var L = Last.Date.ToString().Substring(0, 4);
                    var L1 = Last.Date.ToString().Substring(5, 2);
                    var L2 = Last.Date.ToString().Substring(8, 2);
                    var L4 = int.Parse(L + "" + L1 + "" + L2);
                    LAst.Add(L4);
                }

                foreach (var item in LAst)
                {
                    if (Check(item) == true)
                    {
                        sell++;
                        var q = from i in BLL.cdz() where i.Time == item select i;
                        q.Single();
                        Yes_Sell.Add(q.Single());
                        #region
                        if (history_Low.money == 0)
                        {
                            history_Low.money = q.Single().money;
                            history_Low.Time = q.Single().Time;
                        }
                        else if (q.Single().money <= history_Low.money)
                        {
                            history_Low.money = q.Single().money;
                            history_Low.Time = q.Single().Time;
                        }
                        #endregion// کمترین درآمد
                        money = q.Single().money + money;
                        if (q.Single().money > history_High.money)
                        {
                            history_High.money = q.Single().money;
                            history_High.Time = q.Single().Time;
                        }
                        chart1.Series["sell"].Points.AddXY(q.Single().Time.ToString("####/##/##"), q.Single().money.ToString());
                    }
                    if (Check(item) == false)
                    {
                        History H = new History();
                        H.money = 0;
                        H.Time = item;
                        Not_Sell.Add(H);
                    }
                    if (Check(item) == false & Filter_Sell_Chart == false)
                    {
                        notSell++;
                        chart1.Series["sell"].Points.AddXY(item.ToString("####/##/##"), "0");
                    }
                }


                for (int i = 6; i < 13; i++)
                {
                    var Last = PersianDateTime.Now.AddDays(-i);
                    var L = Last.Date.ToString().Substring(0, 4);
                    var L1 = Last.Date.ToString().Substring(5, 2);
                    var L2 = Last.Date.ToString().Substring(8, 2);
                    var L4 = int.Parse(L + "" + L1 + "" + L2);
                    //LAst.Add(L4);

                    if (Check(L4) == true)
                    {
                        var q = from I in BLL.cdz() where I.Time == L4 select I;
                        Money_Average = q.Single().money + Money_Average;
                    }
                }

                bool Check(int C)
                {
                    var Ch = from i in BLL.cdz() where i.Time == C select i;

                    if (Ch.Count() >= 1)
                    {
                        return true;
                    }
                    return false;

                }
                Combobox1_Chart = 0;

            }
            //////////////////////////////////////////////////////////////////////////////////
            if (comboBox1.SelectedItem.ToString() == "گزارش ماه گذشته")
            {
                foreach (var item in chart1.Series)
                {
                    item.Points.Clear();
                }
                List<int> LAst = new List<int>();
                for (int i = 0; i < 30; i++)
                {
                    var Last = PersianDateTime.Now.AddDays(-i);
                    var L = Last.Date.ToString().Substring(0, 4);
                    var L1 = Last.Date.ToString().Substring(5, 2);
                    var L2 = Last.Date.ToString().Substring(8, 2);
                    var L4 = int.Parse(L + "" + L1 + "" + L2);
                    // LAst.Add(L4);



                    //foreach (var item in LAst)
                    //{
                    if (Check(L4) == true)
                    {
                        sell++;
                        var q = from Y in BLL.cdz() where Y.Time == L4 select Y;
                        q.Single();
                        Yes_Sell.Add(q.Single());
                        money = q.Single().money + money;

                        #region
                        if (history_Low.money == 0)
                        {
                            history_Low.money = q.Single().money;
                            history_Low.Time = q.Single().Time;
                        }
                        else if (q.Single().money <= history_Low.money)
                        {
                            history_Low.money = q.Single().money;
                            history_Low.Time = q.Single().Time;
                        }
                        #endregion// کمترین درآمد

                        if (q.Single().money > history_High.money)
                        {
                            history_High.money = q.Single().money;
                            history_High.Time = q.Single().Time;
                        }
                        chart1.Series["sell"].Points.AddXY(q.Single().Time.ToString("####/##/##"), q.Single().money.ToString());

                    }
                    if (Check(L4) == false)
                    {
                        History H = new History();
                        H.money = 0;
                        H.Time = L4;
                        Not_Sell.Add(H);
                    }
                    if (Check(L4) == false & Filter_Sell_Chart == false)
                    {
                        notSell++;
                        chart1.Series["sell"].Points.AddXY(L4.ToString("####/##/##"), "0");
                    }
                    //}
                }
                for (int i = 30; i < 60; i++)
                {
                    var Last = PersianDateTime.Now.AddDays(-i);
                    var L = Last.Date.ToString().Substring(0, 4);
                    var L1 = Last.Date.ToString().Substring(5, 2);
                    var L2 = Last.Date.ToString().Substring(8, 2);
                    var L4 = int.Parse(L + "" + L1 + "" + L2);
                    //LAst.Add(L4);

                    if (Check(L4) == true)
                    {
                        var q = from I in BLL.cdz() where I.Time == L4 select I;
                        Money_Average = q.Single().money + Money_Average;
                    }
                }

                bool Check(int C)
                {
                    var Ch = from i in BLL.cdz() where i.Time == C select i;

                    if (Ch.Count() >= 1)
                    {
                        return true;
                    }
                    return false;

                }



                Combobox1_Chart = 1;
            }
            //////////////////////////////////////////////////////////////////////////////////
            if (comboBox1.SelectedItem.ToString() == "گزارش سه ماه گذشته")
            {
                foreach (var item in chart1.Series)
                {
                    item.Points.Clear();
                }
                List<int> LAst = new List<int>();
                for (int i = 0; i < 90; i++)
                {
                    var Last = PersianDateTime.Now.AddDays(-i);
                    var L = Last.Date.ToString().Substring(0, 4);
                    var L1 = Last.Date.ToString().Substring(5, 2);
                    var L2 = Last.Date.ToString().Substring(8, 2);
                    var L4 = int.Parse(L + "" + L1 + "" + L2);
                    LAst.Add(L4);
                }
                foreach (var item in LAst)
                {
                    if (Check(item) == true)
                    {
                        sell++;
                        var q = from i in BLL.cdz() where i.Time == item select i;
                        q.Single();
                        money = q.Single().money + money;
                        Yes_Sell.Add(q.Single());
                        #region
                        if (history_Low.money == 0)
                        {
                            history_Low.money = q.Single().money;
                            history_Low.Time = q.Single().Time;
                        }
                        else if (q.Single().money <= history_Low.money)
                        {
                            history_Low.money = q.Single().money;
                            history_Low.Time = q.Single().Time;
                        }
                        #endregion// کمترین درآمد

                        if (q.Single().money > history_High.money)
                        {
                            history_High.money = q.Single().money;
                            history_High.Time = q.Single().Time;
                        }
                        chart1.Series["sell"].Points.AddXY(q.Single().Time.ToString("####/##/##"), q.Single().money.ToString());
                    }
                    if (Check(item) == false)
                    {
                        History H = new History();
                        H.money = 0;
                        H.Time = item;
                        Not_Sell.Add(H);
                    }
                    if (Check(item) == false & Filter_Sell_Chart == false)
                    {
                        notSell++;
                        chart1.Series["sell"].Points.AddXY(item.ToString("####/##/##"), "0");
                    }
                }

                for (int i = 90; i < 180; i++)
                {
                    var Last = PersianDateTime.Now.AddDays(-i);
                    var L = Last.Date.ToString().Substring(0, 4);
                    var L1 = Last.Date.ToString().Substring(5, 2);
                    var L2 = Last.Date.ToString().Substring(8, 2);
                    var L4 = int.Parse(L + "" + L1 + "" + L2);
                    //LAst.Add(L4);

                    if (Check(L4) == true)
                    {
                        var q = from I in BLL.cdz() where I.Time == L4 select I;
                        Money_Average = q.Single().money + Money_Average;
                    }
                }

                bool Check(int C)
                {
                    var Ch = from i in BLL.cdz() where i.Time == C select i;

                    if (Ch.Count() >= 1)
                    {
                        return true;
                    }
                    return false;

                }
                Combobox1_Chart = 2;
            }
            //////////////////////////////////////////////////////////////////////////////////
            if (comboBox1.SelectedItem.ToString() == "گزارش سال گذشته")
            {
                foreach (var item in chart1.Series)
                {
                    item.Points.Clear();
                }
                List<int> LAst = new List<int>();
                for (int i = 0; i < 365; i++)
                {
                    var Last = PersianDateTime.Now.AddDays(-i);
                    var L = Last.Date.ToString().Substring(0, 4);
                    var L1 = Last.Date.ToString().Substring(5, 2);
                    var L2 = Last.Date.ToString().Substring(8, 2);
                    var L4 = int.Parse(L + "" + L1 + "" + L2);
                    LAst.Add(L4);
                }
                foreach (var item in LAst)
                {
                    if (Check(item) == true)
                    {
                        sell++;
                        var q = from i in BLL.cdz() where i.Time == item select i;
                        q.Single();
                        money = q.Single().money + money;
                        Yes_Sell.Add(q.Single());
                        #region
                        if (history_Low.money == 0)
                        {
                            history_Low.money = q.Single().money;
                            history_Low.Time = q.Single().Time;
                        }
                        else if (q.Single().money <= history_Low.money)
                        {
                            history_Low.money = q.Single().money;
                            history_Low.Time = q.Single().Time;
                        }
                        #endregion// کمترین درآمد

                        if (q.Single().money > history_High.money)
                        {
                            history_High.money = q.Single().money;
                            history_High.Time = q.Single().Time;
                        }
                        chart1.Series["sell"].Points.AddXY(q.Single().Time.ToString("####/##/##"), q.Single().money.ToString());
                    }
                    if (Check(item) == false)
                    {
                        History H = new History();
                        H.money = 0;
                        H.Time = item;
                        Not_Sell.Add(H);
                    }
                    if (Check(item) == false & Filter_Sell_Chart == false)
                    {
                        notSell++;
                        chart1.Series["sell"].Points.AddXY(item.ToString("####/##/##"), "0");
                    }
                }

                for (int i = 365; i < 730; i++)
                {
                    var Last = PersianDateTime.Now.AddDays(-i);
                    var L = Last.Date.ToString().Substring(0, 4);
                    var L1 = Last.Date.ToString().Substring(5, 2);
                    var L2 = Last.Date.ToString().Substring(8, 2);
                    var L4 = int.Parse(L + "" + L1 + "" + L2);
                    //LAst.Add(L4);

                    if (Check(L4) == true)
                    {
                        var q = from I in BLL.cdz() where I.Time == L4 select I;
                        Money_Average = q.Single().money + Money_Average;
                    }
                }

                bool Check(int C)
                {
                    var Ch = from i in BLL.cdz() where i.Time == C select i;

                    if (Ch.Count() >= 1)
                    {
                        return true;
                    }
                    return false;

                }
                Combobox1_Chart = 3;
            }

            label26.Text = "0000/00/00";
            label28.Text = "0000/00/00";
            solidGauge3.To = history_High.money;
            solidGauge3.Value = history_High.money;
            if (history_High.money > 0)
            {
                label26.Text = history_High.Time.ToString("####/##/##");
            }
            solidGauge2.To = history_High.money;
            if (money != 0)
            {
                solidGauge2.Value = money / sell;
            }
            else
            {
                solidGauge2.Value = 0;
            }
            solidGauge1.To = history_High.money;
            solidGauge1.Value = history_Low.money;
            if (history_Low.money > 0)
            {
                label28.Text = history_Low.Time.ToString("####/##/##");
            }

            if (Money_Average == 0)
            {
                solidGauge4.To = 0;
                solidGauge4.Value = 0;
            }
            else
            {
                if (money / Money_Average > 4)
                {
                    solidGauge4.To = money / Money_Average + 1;
                    solidGauge4.Value = money / Money_Average;
                }
                else
                {
                    solidGauge4.Value = money / Money_Average;
                }
            }


            R = 0;
            timer4.Start();
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
        }

        private void radioButton15_CheckedChanged(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].Area3DStyle.Enable3D = false;
        }

        private void solidGauge2_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            //colorDialog1.ShowDialog();


            //chart1.Series["sell"].Color = colorDialog1.Color;
            timer10.Start();
        }


        int R = 0;
        private void timer4_Tick(object sender, EventArgs e)
        {
            R = R + 1;
            byte Finish = 0;
            if (R <= notSell + sell)
            {
                label20.Text = R.ToString();
            }
            else
            {
                Finish++;
            }
            if (R <= notSell)
            {
                label22.Text = R.ToString();
            }
            else
            {
                Finish++;
            }
            if (R <= sell)
            {
                label24.Text = R.ToString();
            }
            else
            {
                if (sell == 0)
                {
                    label24.Text = "0";
                }
                Finish++;
            }
            if (Finish == 3)
            {

                timer4.Start();
            }


        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void solidGauge1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        byte Report = 0;
        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {

        }
        string Type;
        private void button18_Click(object sender, EventArgs e)
        {

            var startupPath = System.Windows.Forms.Application.StartupPath;
            int A = startupPath.Length - 6;
            stiReport1.Load(startupPath.Substring(0, A) + @"\HISTORE_AMAR_PANEL3.mrt");

            var q = PersianDateTime.Now.ToString("hh:MM:ss");
            var q1 = PersianDateTime.Now.ToString("yy/MM/dd");
            stiReport1.Dictionary.Variables["Time_2"].Value = q.ToString();
            stiReport1.Dictionary.Variables["Time_1"].Value = q1.ToString();
            stiReport1.Dictionary.Variables["Type"].Value = Type;
            Cause cause = new Cause();
            cause.ShowDialog();
            if (cause.richTextBox1.Text.Count() > 0)
            {
                stiReport1.Dictionary.Variables["Cause"].ValueObject = cause.richTextBox1.Text;


            }
            else
            {
                stiReport1.Dictionary.Variables["Cause"].ValueObject = "ندارد";
            }

            stiReport1.RegBusinessObject("HISTORY", dataGridView3.DataSource);



            stiReport1.Render();
            stiReport1.Show();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            Type = comboBox1.Text.ToString();
            if (comboBox3.SelectedItem.ToString() == "مشاهده روز های بدون فروش")
            {
                dataGridView3.DataSource = Not_Sell;
                Report = 2;
            }
            if (comboBox3.SelectedItem.ToString() == "مشاهده روز های فروش")
            {
                dataGridView3.DataSource = Yes_Sell;
                Report = 1;
            }
            if (comboBox3.SelectedItem.ToString() == "مشاهده تمام روز ها")
            {
                byte P = 0;
                Report = 3;
                List<History> H = new List<History>();
                foreach (var item in Yes_Sell)
                {
                    H.Add(item);
                }
                foreach (var item in Not_Sell)
                {
                    H.Add(item);
                }
                foreach (var item in H.OrderByDescending(i => i.Time))
                {
                    if (P == 0)
                    {
                        P++;
                        H.Clear();
                    }
                    H.Add(item);
                }
                dataGridView3.DataSource = H;
            }

        }
        // //////////////////////////////////////////////////////////////



        private void button14_Click(object sender, EventArgs e)
        {






            //ToolTip tooltip = new ToolTip();
            //Point? clickPosition = null;

            //void chart1_MouseMove(object A, MouseEventArgs E)
            //{
            //    if (clickPosition.HasValue && E.Location != clickPosition)
            //    {
            //        tooltip.RemoveAll();
            //        clickPosition = null;
            //    }
            //}

            //void chart1_MouseClick(object A, MouseEventArgs E)
            //{
            //    var pos = E.Location;
            //    clickPosition = pos;
            //    var results = chart1.HitTest(pos.X, pos.Y, false,
            //                                 ChartElementType.PlottingArea);
            //    foreach (var result in results)
            //    {
            //        if (result.ChartElementType == ChartElementType.PlottingArea)
            //        {
            //            var xVal = result.ChartArea.AxisX.PixelPositionToValue(pos.X);
            //            var yVal = result.ChartArea.AxisY.PixelPositionToValue(pos.Y);

            //            tooltip.Show("X=" + xVal + ", Y=" + yVal,
            //                         this.chart1, E.Location.X, E.Location.Y - 15);
            //        }
            //    }
            //}
            chart1.Series["Series1"].ToolTip = "#VALY, #VALX";
        }

        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            // chart1.Series["Series1"].ToolTip = "#VALY, #VALX";
        }
        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();
        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = chart1.HitTest(pos.X, pos.Y, false,
        ChartElementType.PlottingArea);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.PlottingArea)
                {
                    chart1.Series["sell"].ToolTip = "Y=#VALY";
                }
            }
        }

        byte A = 0;
        int G_5 = 365;
        int G_6 = 365;
        int G_7 = 365;
        int G_8 = 365;
        int G_9 = 365;
        int p = 16;
        int l = 1603;
        int Space = 0;
        string Text10 = "";
        int guna_buton1 = 519;
        int guna_buton2 = 519;
        int guna_buton3 = 519;
        int guna_buton4 = 392;
        private void timer5_Tick(object sender, EventArgs e)
        {

            if (A == 5)
            {
                if (G_5 >= 600)
                {
                    timer5.Stop();
                }
                else
                {
                    if (G_6 != 365) { G_6 = G_6 - 20; dataGridView6.Size = new Size(240, G_6); }
                    if (G_7 != 365) { G_7 = G_7 - 20; dataGridView7.Size = new Size(287, G_7); }
                    if (G_8 != 365) { G_8 = G_8 - 20; dataGridView8.Size = new Size(223, G_8); }
                    if (G_9 != 365) { G_9 = G_9 - 20; dataGridView9.Size = new Size(160, G_9); }
                    G_5 = G_5 + 20; dataGridView5.Size = new Size(174, G_5);
                }
            }
            if (A == 6)
            {
                if (G_6 >= 600)
                {
                    timer5.Stop();
                }
                else
                {
                    if (G_5 != 365) { G_5 = G_5 - 20; dataGridView5.Size = new Size(174, G_5); }
                    if (G_7 != 365) { G_7 = G_7 - 20; dataGridView7.Size = new Size(287, G_7); }
                    if (G_8 != 365) { G_8 = G_8 - 20; dataGridView8.Size = new Size(223, G_8); }
                    if (G_9 != 365) { G_9 = G_9 - 20; dataGridView9.Size = new Size(160, G_9); }
                    G_6 = G_6 + 20; dataGridView6.Size = new Size(240, G_6);
                }


            }
            if (A == 7)
            {
                if (G_7 >= 600)
                {
                    timer5.Stop();
                }
                else
                {
                    if (G_5 != 365) { G_5 = G_5 - 20; dataGridView5.Size = new Size(174, G_5); }
                    if (G_6 != 365) { G_6 = G_6 - 20; dataGridView6.Size = new Size(240, G_6); }
                    if (G_8 != 365) { G_8 = G_8 - 20; dataGridView8.Size = new Size(223, G_8); }
                    if (G_9 != 365) { G_9 = G_9 - 20; dataGridView9.Size = new Size(160, G_9); }
                    G_7 = G_7 + 20; dataGridView7.Size = new Size(287, G_7);
                }


            }
            if (A == 8)
            {
                if (G_8 >= 600)
                {
                    timer5.Stop();
                }
                else
                {
                    if (G_5 != 365) { G_5 = G_5 - 20; dataGridView5.Size = new Size(174, G_5); }
                    if (G_6 != 365) { G_6 = G_6 - 20; dataGridView6.Size = new Size(240, G_6); }
                    if (G_7 != 365) { G_7 = G_7 - 20; dataGridView7.Size = new Size(287, G_7); }
                    if (G_9 != 365) { G_9 = G_9 - 20; dataGridView9.Size = new Size(160, G_9); }
                    G_8 = G_8 + 20; dataGridView8.Size = new Size(223, G_8);
                }


            }
            if (A == 9)
            {
                if (G_9 >= 600)
                {
                    timer5.Stop();
                }
                else
                {
                    if (G_5 != 365) { G_5 = G_5 - 20; dataGridView5.Size = new Size(174, G_5); }
                    if (G_6 != 365) { G_6 = G_6 - 20; dataGridView6.Size = new Size(240, G_6); }
                    if (G_7 != 365) { G_7 = G_7 - 20; dataGridView7.Size = new Size(287, G_7); }
                    if (G_8 != 365) { G_8 = G_8 - 20; dataGridView8.Size = new Size(223, G_8); }
                    G_9 = G_9 + 20; dataGridView9.Size = new Size(160, G_9);
                }


            }
            if (A == 1)
            {
                if (p >= 1700)
                {
                    timer5.Stop();
                }
                else
                {
                    p = p + 10;
                    l = l - 20;
                    guna2Separator2.Size = new Size(p, 10);
                }
            }
            if (A == 2)
            {
                if (guna_gradient == true)
                {
                    if (guna_buton1 >= 585)
                    {
                        guna2GradientCircleButton1.Text = ">";
                        timer5.Stop();
                    }
                    else
                    {
                        guna_buton1++;
                        guna_buton1++;
                        guna2GradientCircleButton1.Location = new Point(1049, guna_buton1);
                    }
                }
                if (guna_gradient == false)
                {
                    if (guna_buton1 <= 519)
                    {
                        guna2GradientCircleButton1.Text = "?";
                        timer5.Stop();
                    }
                    else
                    {
                        guna_buton1--;
                        guna_buton1--;
                        guna2GradientCircleButton1.Location = new Point(1049, guna_buton1);
                    }
                }

            }
            if (A == 3)
            {
                if (guna_gradient == true)
                {
                    if (guna_buton2 >= 585)
                    {
                        guna2GradientCircleButton2.Text = ">";
                        timer5.Stop();
                    }
                    else
                    {
                        guna_buton2++;
                        guna_buton2++;
                        guna2GradientCircleButton2.Location = new Point(811, guna_buton2);
                    }
                }
                if (guna_gradient == false)
                {
                    if (guna_buton2 <= 519)
                    {
                        guna2GradientCircleButton2.Text = "?";
                        timer5.Stop();
                    }
                    else
                    {
                        guna_buton2--;
                        guna_buton2--;
                        guna2GradientCircleButton2.Location = new Point(811, guna_buton2);
                    }
                }

            }
            if (A == 4)
            {
                if (guna_gradient == true)
                {
                    if (guna_buton3 >= 585)
                    {
                        guna2GradientCircleButton3.Text = ">";
                        timer5.Stop();
                    }
                    else
                    {
                        guna_buton3++;
                        guna_buton3++;
                        guna2GradientCircleButton3.Location = new Point(481, guna_buton3);
                    }
                }
                if (guna_gradient == false)
                {
                    if (guna_buton3 <= 519)
                    {
                        guna2GradientCircleButton3.Text = "?";
                        timer5.Stop();
                    }
                    else
                    {
                        guna_buton3--;
                        guna_buton3--;
                        guna2GradientCircleButton3.Location = new Point(481, guna_buton3);
                    }
                }

            }
            if (A == 10)
            {
                if (guna_gradient == true)
                {
                    if (guna_buton4 >= 512)
                    {
                        guna2GradientCircleButton4.Text = ">";
                        timer5.Stop();
                    }
                    else
                    {
                        guna_buton4 += 3;
                        guna2GradientCircleButton4.Location = new Point(12, guna_buton4);
                    }
                }
                if (guna_gradient == false)
                {
                    if (guna_buton4 <= 392)
                    {
                        guna2GradientCircleButton4.Text = "?";
                        timer5.Stop();
                    }
                    else
                    {
                        guna_buton4 -= 3;
                        guna2GradientCircleButton4.Location = new Point(12, guna_buton4);
                    }
                }

            }



        }

        private void timer6_Tick(object sender, EventArgs e)
        {

            if (A == 0)
            {
                if(G_button1<= 27 & G_button2<= 27 & G_button3 <= 27 & G_button4<= 27 & G_button5 <= 27)
                {
                    timer6.Stop();
                }
                else
                {
                   //if (G_button1 <= 40) { } else { G_button1 = G_button1 - 5; guna2GradientButton1.Size = new Size(G_button1, 43); }

                    if (G_button1 <= 27) { } else { G_button1 = G_button1 - 5; guna2GradientButton1.Width = G_button1; }
                    if (G_button2 <= 27) { } else { G_button2 = G_button2 - 5; guna2GradientButton2.Width = G_button2; }
                    if (G_button3 <= 27) { } else { G_button3 = G_button3 - 5; guna2GradientButton3.Width = G_button3; }
                    if (G_button4 <= 27) { } else { G_button4 = G_button4 - 5; guna2GradientButton4.Width = G_button4; }
                    if (G_button5 <= 27) { } else { G_button5 = G_button5 - 5; guna2GradientButton5.Width = G_button5; }
                    // if (G_button2 <= 40) { } else { G_button2 = G_button2 - 5; guna2GradientButton2.Size = new Size(G_button2, 43); }
                }
                //if (G_5 != 365) { G_5 = G_5 - 20; dataGridView5.Size = new Size(174, G_5); }
                //if (G_6 != 365) { G_6 = G_6 - 20; dataGridView6.Size = new Size(240, G_6); }
                //if (G_7 != 365) { G_7 = G_7 - 20; dataGridView7.Size = new Size(287, G_7); }
                //if (G_8 != 365) { G_8 = G_8 - 20; dataGridView8.Size = new Size(223, G_8); }
                //if (G_9 != 365) { G_9 = G_9 - 20; dataGridView9.Size = new Size(160, G_9); }
               
            }






        }

        private void dataGridView5_MouseMove(object sender, MouseEventArgs e)
        {
            A = 5;
            tableLayoutPanel7.Enabled = false;
            timer5.Start();
        }

        private void dataGridView5_MouseLeave(object sender, EventArgs e)
        {
            A = 0;
            timer6.Start();
        }

        private void dataGridView6_MouseMove(object sender, MouseEventArgs e)
        {
            A = 6;
            timer5.Start();
        }

        private void dataGridView6_MouseLeave(object sender, EventArgs e)
        {
            A = 0;
            timer6.Start();
        }

        private void dataGridView7_MouseMove(object sender, MouseEventArgs e)
        {
            A = 7;
            timer5.Start();
        }

        private void dataGridView7_MouseLeave(object sender, EventArgs e)
        {
            A = 0;
            timer6.Start();
        }

        private void dataGridView8_MouseMove(object sender, MouseEventArgs e)
        {
            A = 8;
            timer5.Start();
        }

        private void dataGridView8_MouseLeave(object sender, EventArgs e)
        {
            A = 0;
            timer6.Start();
        }

        private void dataGridView9_MouseMove(object sender, MouseEventArgs e)
        {
            A = 9;
            timer5.Start();
        }

        private void dataGridView9_MouseLeave(object sender, EventArgs e)
        {
            A = 0;
            timer6.Start();
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            label29.Text = @"یوزرها                                       اشتراک ها                                               غذاها                                         ساعت های ورود و خروج                        تایم های کاری";

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            

        }
        int point = 0;
        int a = 0;
        private void timer7_Tick(object sender, EventArgs e)
        {

            if (point >= 1800)
            {
                point = 3;
                label29.Visible = false;
                timer7.Stop();
            }
            else
            {
                point = point + 2;
                label29.Location = new Point(point, 15);


            }
        }

        private void timer8_Tick(object sender, EventArgs e)
        {
            if (point >=
                1100)
            {
                a = label29.Text.Length;
                if (a != 0)
                {

                    label29.Text = label29.Text.ToString().Substring(1, a - 1);
                }

            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A");
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A");
        }

        private void button15_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button14_Click_2(object sender, EventArgs e)
        {


        }

        private void dataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = (int)(dataGridView7.Rows[e.RowIndex].Cells[0].Value);
        }

        private void guna2RatingStar3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        bool guna_gradient = true;
        private void guna2GradientCircleButton1_Click(object sender, EventArgs e)
        {
            if (guna2GradientCircleButton1.Text == "?")
            {
                guna_gradient = true;
            }
            else if (guna2GradientCircleButton1.Text == ">")
            {
                guna_gradient = false;
            }
            A = 2;
            timer5.Start();
            timer9.Start();
        }



        private void guna2GradientCircleButton2_Click(object sender, EventArgs e)
        {
            if (guna2GradientCircleButton2.Text == "?")
            {
                guna_gradient = true;
            }
            else if (guna2GradientCircleButton2.Text == ">")
            {
                guna_gradient = false;
            }
            A = 3;
            timer5.Start();
            timer9.Start();
        }

        private void guna2GradientCircleButton3_Click(object sender, EventArgs e)
        {
            if (guna2GradientCircleButton3.Text == "?")
            {
                guna_gradient = true;
            }
            else if (guna2GradientCircleButton3.Text == ">")
            {
                guna_gradient = false;
            }
  
            A = 4;
            timer5.Start();
            timer9.Start();
        }
       
        private void timer9_Tick(object sender, EventArgs e)
        {

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Edite_Admin.update_1 = false;
            Edite_Admin edite_Admin = new Edite_Admin();
            edite_Admin.Size = new Size(414, 638);
            edite_Admin.panel1.Visible = true;
            edite_Admin.panel1.Dock = DockStyle.Fill;
            edite_Admin.ShowDialog();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Edite_Admin.update_1 = false;
            Edite_Admin edite_Admin = new Edite_Admin();
            edite_Admin.Size = new Size(414, 581);
            edite_Admin.panel2.Visible = true;
            edite_Admin.panel2.Dock = DockStyle.Fill;
            edite_Admin.ShowDialog();


        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Edite_Admin edite_Admin = new Edite_Admin();
            Edite_Admin.update_1 = false;
            edite_Admin.Size = new Size(414, 581);
            edite_Admin.panel3.Visible = true;
            edite_Admin.panel3.Dock = DockStyle.Fill;
            edite_Admin.ShowDialog();


        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            Edite_Admin.update_1 = false;
            Edite_Admin edite_Admin = new Edite_Admin();
            edite_Admin.Size = new Size(414, 581);
            edite_Admin.panel4.Visible = true;
            edite_Admin.panel4.Dock = DockStyle.Fill;
            edite_Admin.ShowDialog();


        }

        private void guna2GradientCircleButton4_Click(object sender, EventArgs e)
        {
            if (guna2GradientCircleButton4.Text == "?")
            {
                guna_gradient = true;
            }
            else if (guna2GradientCircleButton4.Text == ">")
            {
                guna_gradient = false;
            }
            A = 10;
            timer5.Start();
            timer9.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        
        byte datagrid=0;
        //public static int ID;
        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(datagrid==6)
            {
                
                Edite_Admin.update_1 = true;
                Edite_Admin edite_Admin = new Edite_Admin();
                vorod_khorog V_K = new vorod_khorog();
                V_K = BLL.Search_vorod_Khorog(ID);
                edite_Admin.guna2TextBox13.Text = V_K.username;
                edite_Admin.maskedTextBox1.Text = V_K.vorod.ToString();
                edite_Admin.maskedTextBox2.Text = V_K.khoroj.ToString();
                edite_Admin.Size = new Size(414, 581);
                edite_Admin.panel4.Dock = DockStyle.Fill;
                edite_Admin.panel4.Visible = true;
                edite_Admin.guna2GradientButton4.Text = "ویرایش اطلاعات";
                edite_Admin.ShowDialog();
            }
            if (datagrid == 7)
            {
                Edite_Admin edite_Admin = new Edite_Admin();
                Edite_Admin.chang = false;
                Edite_Admin.update_1 = true;
                time tim = new time();
                food ff = new food();
                ff = BLL.readid(ID);
                edite_Admin.guna2TextBox8.Text = ff.name;
                Edite_Admin.name = ff.name;
                Edite_Admin.picture = ff.picture;
                edite_Admin.Size = new Size(414, 581);
                edite_Admin.panel3.Dock = DockStyle.Fill;
                edite_Admin.panel3.Visible = true;
                edite_Admin.guna2TextBox9.Text = ff.tarkib;
                edite_Admin.guna2TextBox10.Text = (ff.money).ToString();

                if (ff.picture != null)
                {
                    edite_Admin.pictureBox3.Image = Image.FromFile(ff.picture);
                }


                List<string> roz_CLB = new List<string>();

                foreach (var item in BLL.roz(ID))
                {
                    if (item.ToString() == "A") { n++; roz_CLB.Add("A"); }
                    if (item.ToString() == "B") { n++; roz_CLB.Add("B"); }
                    if (item.ToString() == "C") { n++; roz_CLB.Add("C"); }
                    if (item.ToString() == "D") { n++; roz_CLB.Add("D"); }
                    if (item.ToString() == "E") { n++; roz_CLB.Add("E"); }
                    if (item.ToString() == "F") { n++; roz_CLB.Add("F"); }
                    if (item.ToString() == "G") { n++; roz_CLB.Add("G"); }
                }
                if (n == 7)
                {

                    edite_Admin.radioButton2.Checked = true;
                }
                else
                {
                    foreach (var item in roz_CLB)
                    {
                        edite_Admin.radioButton1.Checked = true;
                        if (item.ToString() == "A") { edite_Admin.checkedListBox1.SetItemChecked(0, true); }
                        if (item.ToString() == "B") { edite_Admin.checkedListBox1.SetItemChecked(1, true); }
                        if (item.ToString() == "C") { edite_Admin.checkedListBox1.SetItemChecked(2, true); }
                        if (item.ToString() == "D") { edite_Admin.checkedListBox1.SetItemChecked(3, true); }
                        if (item.ToString() == "E") { edite_Admin.checkedListBox1.SetItemChecked(4, true); }
                        if (item.ToString() == "F") { edite_Admin.checkedListBox1.SetItemChecked(5, true); }
                        if (item.ToString() == "G") { edite_Admin.checkedListBox1.SetItemChecked(6, true); }
                    }
                }
                n = 0;

                edite_Admin.guna2GradientButton3.Text = "ویرایش اطلاعات";
                edite_Admin.ShowDialog();
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////
            if(datagrid==8)
            {
                Edite_Admin.update_1 = true;
                personel Edit_P = BLL.search_Personel_eshterak(Edite_Admin.eshterak);
                Edite_Admin edite_Admin = new Edite_Admin();
                edite_Admin.Size = new Size(414, 581);
                edite_Admin.panel2.Visible = true;
                edite_Admin.panel2.Dock = DockStyle.Fill;
                edite_Admin.guna2TextBox5.Text = Edit_P.name;
                edite_Admin.guna2TextBox6.Text = Edit_P.adres;
                edite_Admin.guna2TextBox7.Text = Edit_P.eshterak.ToString();
                edite_Admin.guna2TextBox7.Enabled = false;
                edite_Admin.guna2GradientButton2.Text = "ویرایش اطلاعات";
                edite_Admin.ShowDialog();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////
            if (datagrid == 9)
            {
                Edite_Admin.update_1 = true;
                Edite_Admin.chang = false;
                User user_Edit = BLL.search_user_id(ID);
                Edite_Admin edite_Admin = new Edite_Admin();
                edite_Admin.Size = new Size(414, 638);
                edite_Admin.panel1.Visible = true;
                edite_Admin.panel1.Dock = DockStyle.Fill;
                edite_Admin.pictureBox1.ImageLocation = user_Edit.picture_User;
                edite_Admin.guna2TextBox1.Text = user_Edit.name;
                edite_Admin.guna2TextBox2.Text = user_Edit.username;
                Edite_Admin.username= user_Edit.username;
                edite_Admin.guna2TextBox3.Text = user_Edit.password;
                edite_Admin.guna2TextBox4.Text = user_Edit.password;
                Edite_Admin.picture = user_Edit.picture_User;
                edite_Admin.guna2GradientButton1.Text= "ویرایش اطلاعات";
                edite_Admin.ShowDialog();
            }
        }

        private void dataGridView7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                ID = (int)(dataGridView7.Rows[e.RowIndex].Cells[0].Value);
                datagrid = 7;
            }
           
        }

        

        private void dataGridView8_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        Int64 TIME;
        string NAME;
        string TARKIB;
        private void dataGridView8_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // dataGridView8.ColumnCount = 3;
            if (e.RowIndex != -1)
            {
                if (personel_show == true)
                {


                    Edite_Admin.eshterak = (int)(dataGridView8.Rows[e.RowIndex].Cells[1].Value);
                    NAME = (dataGridView8.Rows[e.RowIndex].Cells[2].Value).ToString();
                    TARKIB = (dataGridView8.Rows[e.RowIndex].Cells[3].Value).ToString();



                }
                else
                {
                    TIME = (Int64)(dataGridView8.Rows[e.RowIndex].Cells[1].Value);
                }

                ID = (int)(dataGridView8.Rows[e.RowIndex].Cells[0].Value);
                datagrid = 8;

            }
            

        }

        private void dataGridView9_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex !=-1)
            {
                ID = (int)(dataGridView9.Rows[e.RowIndex].Cells[0].Value);
                datagrid = 9;

                //MessageBox.Show(((int)(dataGridView9.Rows[e.RowIndex].Cells[5].Value)).ToString());
            }
            
        }

        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                ID = (int)(dataGridView6.Rows[e.RowIndex].Cells[0].Value);
                datagrid = 6;
            }
           
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            var startupPath = System.Windows.Forms.Application.StartupPath;
            int A = startupPath.Length - 6;
            stiReport1.Load(startupPath.Substring(0, A) + @"\VOROD_KHOROGS.mrt");

            var q = PersianDateTime.Now.ToString("hh:mm:ss");
            var q1 = PersianDateTime.Now.ToString("yy/MM/dd");
            stiReport1.Dictionary.Variables["Time_2"].Value = q.ToString();
            stiReport1.Dictionary.Variables["Time_1"].Value = q1.ToString();
            stiReport1.Dictionary.Variables["Type"].Value = "اطلاعات یوزر های سیستم";
            stiReport1.Dictionary.Variables["shomare"].Value = 2222.ToString(); ;
            stiReport1.Dictionary.Variables["manager"].Value = "رضا علیپور";
            Cause cause = new Cause();
            cause.ShowDialog();
            if (cause.richTextBox1.Text.Count() > 0)
            {
                stiReport1.Dictionary.Variables["Cause"].ValueObject = cause.richTextBox1.Text;


            }
            else
            {
                stiReport1.Dictionary.Variables["Cause"].ValueObject = "ندارد";
            }

            stiReport1.RegBusinessObject("vorod_khorogs".ToUpper(), dataGridView6.DataSource);



            stiReport1.Render();
            stiReport1.Show();
        }

        private void حذفToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(datagrid==6)
            {
                if (MessageBox.Show("!آیا این مورد حذف شود", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    BLL.Delete_Vorod_Khorog(ID);
                }
            }
            if (datagrid == 7)
            {
                if (MessageBox.Show("!آیا این مورد حذف شود", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    BLL.Delete_food(ID);
                }
            }
            if (datagrid == 8)
            {
                if (MessageBox.Show("!آیا این مورد حذف شود", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    BLL.Delete_personels(ID);
                }
            }
            if (datagrid == 9)
            {
                if (MessageBox.Show("!آیا این مورد حذف شود", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    BLL.Delete_user(ID);
                }
            }
            dataGridView5.DataSource = BLL.All_day_Work();
            dataGridView6.DataSource = BLL.All_vorod_khorog();
            dataGridView7.DataSource = BLL.ss();
            dataGridView8.DataSource = BLL.All_Personels();
            dataGridView9.DataSource = BLL.All_User();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("!آیا تمامی اطلاعات این جدول پاک شود", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                BLL.Delete_All_Vorod_Khorog();
                dataGridView6.DataSource = BLL.All_vorod_khorog();
            }
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("!آیا تمامی اطلاعات این جدول پاک شود", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                BLL.Delete_All_food();
                dataGridView7.DataSource = BLL.ss();
            }
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("!آیا تمامی اطلاعات این جدول پاک شود", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                BLL.Delete_All_personels();
                dataGridView8.DataSource = BLL.All_Personels();
            }
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("!آیا تمامی اطلاعات این جدول پاک شود", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                BLL.Delete_All_user();
                dataGridView9.DataSource = BLL.All_User();
            }
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            if(guna2TextBox2.Text.Length !=0)
            {
                dataGridView7.DataSource = BLL.food_search(guna2TextBox2.Text);
            }
            else
            {
                
                dataGridView7.DataSource = BLL.ss();
               
                
            }
           
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox3.Text.Length != 0)
            {
                dataGridView9.DataSource = BLL.search_User(guna2TextBox3.Text);
            }
            else
            {
                dataGridView9.DataSource = BLL.All_User();
            }

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text.Length != 0)
            {
                dataGridView8.DataSource = BLL.search_personel(guna2TextBox1.Text);
            }
            else
            {
                dataGridView8.DataSource = BLL.All_Personels();
            }
            
        }

        

        private void guna2TextBox4_TextChanged_1(object sender, EventArgs e)
        {
            if (guna2TextBox4.Text.Length != 0)
            {
                dataGridView6.DataSource = BLL.search_Vorod_Khorog(guna2TextBox4.Text);
            }
            else
            {
                dataGridView6.DataSource = BLL.All_vorod_khorog();
            }
        }

        private void guna2Button15_Click(object sender, EventArgs e)
        {
            var startupPath = System.Windows.Forms.Application.StartupPath;
            int A = startupPath.Length - 6;
            stiReport1.Load(startupPath.Substring(0, A) + @"\USERS.mrt");

            var q = PersianDateTime.Now.ToString("hh:mm:ss");
            var q1 = PersianDateTime.Now.ToString("yy/MM/dd");
            stiReport1.Dictionary.Variables["Time_2"].Value = q.ToString();
            stiReport1.Dictionary.Variables["Time_1"].Value = q1.ToString();
            stiReport1.Dictionary.Variables["Type"].Value = "اطلاعات یوزر های سیستم";
            stiReport1.Dictionary.Variables["shomare"].Value = 2222.ToString(); ;
            stiReport1.Dictionary.Variables["manager"].Value = "رضا علیپور";
            Cause cause = new Cause();
            cause.ShowDialog();
            if (cause.richTextBox1.Text.Count() > 0)
            {
                stiReport1.Dictionary.Variables["Cause"].ValueObject = cause.richTextBox1.Text;


            }
            else
            {
                stiReport1.Dictionary.Variables["Cause"].ValueObject = "ندارد";
            }

            stiReport1.RegBusinessObject("USER", dataGridView9.DataSource);



            stiReport1.Render();
            stiReport1.Show();
        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            
            if(personel_show==true)
            {
                var startupPath = System.Windows.Forms.Application.StartupPath;
                int A = startupPath.Length - 6;
                stiReport1.Load(startupPath.Substring(0, A) + @"\PERSONEL.mrt");

                var q = PersianDateTime.Now.ToString("hh:mm:ss");
                var q1 = PersianDateTime.Now.ToString("yy/MM/dd");
                stiReport1.Dictionary.Variables["Time_2"].Value = q.ToString();
                stiReport1.Dictionary.Variables["Time_1"].Value = q1.ToString();
                stiReport1.Dictionary.Variables["Type"].Value = "اطلاعات یوزر های سیستم";
                stiReport1.Dictionary.Variables["shomare"].Value = 2222.ToString(); 
                stiReport1.Dictionary.Variables["manager"].Value = "رضا علیپور";
                Cause cause = new Cause();
                cause.ShowDialog();
                if (cause.richTextBox1.Text.Count() > 0)
                {
                    stiReport1.Dictionary.Variables["Cause"].ValueObject = cause.richTextBox1.Text;


                }
                else
                {
                    stiReport1.Dictionary.Variables["Cause"].ValueObject = "ندارد";
                }

                stiReport1.RegBusinessObject("PERSONEL", dataGridView8.DataSource);



                stiReport1.Render();
                stiReport1.Show();
            }
            else if(personel_show==false)
            {
                var startupPath = System.Windows.Forms.Application.StartupPath;
                int A = startupPath.Length - 6;
                stiReport1.Load(startupPath.Substring(0, A) + @"\Panel3_Amar.mrt");

                var q = PersianDateTime.Now.ToString("hh:MM:ss");
                var q1 = PersianDateTime.Now.ToString("yy/MM/dd");
                stiReport1.Dictionary.Variables["Time-2"].Value = q.ToString();
                stiReport1.Dictionary.Variables["Time-1"].Value = q1.ToString();
                Cause cause = new Cause();
                cause.ShowDialog();
                if (cause.richTextBox1.Text.Count() > 0)
                {
                    stiReport1.Dictionary.Variables["Cause"].ValueObject = cause.richTextBox1.Text;


                }
                else
                {
                    stiReport1.Dictionary.Variables["Cause"].ValueObject = "ندارد";
                }

                stiReport1.RegBusinessObject("as", dataGridView3.DataSource);



                stiReport1.Render();
                stiReport1.Show();
            }
        }
           

        private void guna2Button16_Click(object sender, EventArgs e)
        {
            var startupPath = System.Windows.Forms.Application.StartupPath;
            int A = startupPath.Length - 6;
            stiReport1.Load(startupPath.Substring(0, A) + @"\DAY_WORK.mrt");

            var q = PersianDateTime.Now.ToString("hh:mm:ss");
            var q1 = PersianDateTime.Now.ToString("yy/MM/dd");
            stiReport1.Dictionary.Variables["Time_2"].Value = q.ToString();
            stiReport1.Dictionary.Variables["Time_1"].Value = q1.ToString();
            stiReport1.Dictionary.Variables["Type"].Value = "اطلاعات یوزر های سیستم";
            stiReport1.Dictionary.Variables["shomare"].Value = 2222.ToString(); ;
            stiReport1.Dictionary.Variables["manager"].Value = "رضا علیپور";
            Cause cause = new Cause();
            cause.ShowDialog();
            if (cause.richTextBox1.Text.Count() > 0)
            {
                stiReport1.Dictionary.Variables["Cause"].ValueObject = cause.richTextBox1.Text;


            }
            else
            {
                stiReport1.Dictionary.Variables["Cause"].ValueObject = "ندارد";
            }

            stiReport1.RegBusinessObject("DAY_WORK", dataGridView8.DataSource);



            stiReport1.Render();
            stiReport1.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            guna2TextBox3.Clear();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Clear();
            if(data8_C_O==true)
            {
                dataGridView8.Dock = DockStyle.Bottom;
                data8_C_O = false;
                personel_show = true;
                timer_datagrid8.Start();
            }
            

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            guna2TextBox2.Clear();
        }

        private void guna2Button17_Click(object sender, EventArgs e)
        {
            guna2TextBox4.Clear();
        }

        private void guna2Button18_Click(object sender, EventArgs e)
        {
            guna2TextBox5.Clear();
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            var startupPath = System.Windows.Forms.Application.StartupPath;
            int A = startupPath.Length - 6;
            stiReport1.Load(startupPath.Substring(0, A) + @"\FOOD.mrt");

            var q = PersianDateTime.Now.ToString("hh:mm:ss");
            var q1 = PersianDateTime.Now.ToString("yy/MM/dd");
            stiReport1.Dictionary.Variables["Time_2"].Value = q.ToString();
            stiReport1.Dictionary.Variables["Time_1"].Value = q1.ToString();
            stiReport1.Dictionary.Variables["Type"].Value = "اطلاعات یوزر های سیستم";
            stiReport1.Dictionary.Variables["shomare"].Value = 2222.ToString(); ;
            stiReport1.Dictionary.Variables["manager"].Value = "رضا علیپور";
            Cause cause = new Cause();
            cause.ShowDialog();
            if (cause.richTextBox1.Text.Count() > 0)
            {
                stiReport1.Dictionary.Variables["Cause"].ValueObject = cause.richTextBox1.Text;


            }
            else
            {
                stiReport1.Dictionary.Variables["Cause"].ValueObject = "ندارد";
            }

            stiReport1.RegBusinessObject("FOOD", dataGridView8.DataSource);



            stiReport1.Render();
            stiReport1.Show();
        }

        

        private void guna2Button19_Click(object sender, EventArgs e)
        {
            //dataGridView8.Dock = DockStyle.Top;
            //dataGridView8.Columns["name_8"].Visible = false;
            //dataGridView8.Columns["adres_8"].Visible = false;
            //dataGridView8.Columns["eshterak_8"].Visible = false;
            //dataGridView8.Columns["Time_8"].Visible = true;
            //dataGridView8.Columns["money_8"].Visible = true;
            //data8_C_O = false;
            //personel_show = true;
            //timer_datagrid8.Start();
            contextMenuStrip1.Items[1].Visible = false;
        }

        private void مشاهدهتاریخچهخریدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Edite_Admin.eshterak ==0)
            {
                MessageBox.Show("لطفا موردی را انتخاب کنید");
            }
            else
            {
                dataGridView8.Dock = DockStyle.None;
                data8_C_O = false;
                personel_show = false;
                
                timer_datagrid8.Start();
            }
           
        }
        int datagrid8_size = 377;
        int datagrid_8_H;
        bool data8_C_O;
        bool personel_show=true;
        private void timer10_Tick(object sender, EventArgs e)
        {
            datagrid_8_H = int.Parse((dataGridView8.GetPreferredSize(dataGridView8.Size)).ToString().Substring(7, 3));
            if (personel_show == true)
            {
                if (data8_C_O == true)
                {
                    if (datagrid8_size >= 500)
                    {
                        dataGridView8.Dock = DockStyle.Fill;

                        dataGridView8.DataSource = BLL.All_Personels();
                        
                        
                        timer_datagrid8.Stop();
                    }
                    else
                    {
                        datagrid8_size +=10;
                        dataGridView8.Size = new Size(datagrid_8_H, datagrid8_size);
                    }
                }
                if (data8_C_O == false)
                {

                    if (datagrid8_size <= 0)
                    {
                        #region Personel_All
                        dataGridView8.Columns.Clear();
                        dataGridView8.Columns.Add("id_8", "id");
                        dataGridView8.Columns["id_8"].DataPropertyName = "id";
                        dataGridView8.Columns["id_8"].Visible = false;
                        dataGridView8.Columns.Add("name_8", "نام");
                        dataGridView8.Columns["name_8"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView8.Columns["name_8"].FillWeight = 60;
                        dataGridView8.Columns["name_8"].MinimumWidth = 30;
                        dataGridView8.Columns["name_8"].Width = 52;
                        dataGridView8.Columns["name_8"].DataPropertyName = "name";
                        dataGridView8.Columns["name_8"].ReadOnly = true;
                        dataGridView8.Columns.Add("adres_8", "آدرس");
                        dataGridView8.Columns["adres_8"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView8.Columns["adres_8"].FillWeight = 129;
                        dataGridView8.Columns["adres_8"].MinimumWidth = 117;
                        dataGridView8.Columns["adres_8"].Width = 117;
                        dataGridView8.Columns["adres_8"].DataPropertyName = "adres";
                        dataGridView8.Columns["adres_8"].ReadOnly = true;
                        dataGridView8.Columns.Add("eshterak_8", "اشتراک");
                        dataGridView8.Columns["eshterak_8"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView8.Columns["eshterak_8"].FillWeight = 45;
                        dataGridView8.Columns["eshterak_8"].MinimumWidth = 45;
                        dataGridView8.Columns["eshterak_8"].Width = 45;
                        dataGridView8.Columns["eshterak_8"].DataPropertyName = "eshterak";
                        dataGridView8.Columns["eshterak_8"].ReadOnly = true;
                        dataGridView8.DataSource = BLL.All_Personels();
                        #endregion
                        contextMenuStrip1.Items[3].Visible = true;
                        contextMenuStrip1.Items[4].Visible = false;
                        dataGridView8.Dock = DockStyle.Top;
                        guna2Button1.FillColor = ColorTranslator.FromHtml("94, 148, 255");
                        guna2Button1.Text = "پاک کردن";
                        data8_C_O = true;
                    }
                    else
                    {
                        datagrid8_size -= 10;
                        dataGridView8.Size = new Size(datagrid_8_H, datagrid8_size);
                    }
                }
            }
                if (personel_show == false)
                {
                    if (data8_C_O == true)
                    {
                        if (datagrid8_size >= 500)
                        {
                            dataGridView8.Dock = DockStyle.Fill;

                        

                        dataGridView8.DataSource = BLL.histories(Edite_Admin.eshterak);
                        
                            
                        timer_datagrid8.Stop();
                        }
                        else
                        {
                            datagrid8_size += 10;
                            dataGridView8.Size = new Size(datagrid_8_H, datagrid8_size);
                        }
                    }
                    if (data8_C_O == false)
                    {

                        if (datagrid8_size <= 0)
                        {
                        #region Money_Time
                        dataGridView8.Columns.Clear();
                        dataGridView8.Columns.Add("id_8", "id");
                        dataGridView8.Columns["id_8"].DataPropertyName = "id";
                        dataGridView8.Columns["id_8"].Visible = false;
                        dataGridView8.Columns.Add("Time_8", "تاریخ");
                        dataGridView8.Columns["Time_8"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView8.Columns["Time_8"].FillWeight = 112;
                        dataGridView8.Columns["Time_8"].MinimumWidth = 139;
                        dataGridView8.Columns["Time_8"].Width = 139;
                        dataGridView8.Columns["Time_8"].DataPropertyName = "Time";
                        dataGridView8.Columns["Time_8"].ReadOnly = true;
                        dataGridView8.Columns["Time_8"].DefaultCellStyle.Format = "####/##/##     ##:##:##";
                        dataGridView8.Columns.Add("money_8", "میزان خرید");
                        dataGridView8.Columns["money_8"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView8.Columns["money_8"].FillWeight = 70;
                        dataGridView8.Columns["money_8"].MinimumWidth = 87;
                        dataGridView8.Columns["money_8"].Width = 87;
                        dataGridView8.Columns["money_8"].DataPropertyName = "money";
                        dataGridView8.Columns["money_8"].ReadOnly = true;
                        dataGridView8.Columns["money_8"].DefaultCellStyle.Format = "###,###,###,###";
                        dataGridView8.DataSource = BLL.histories(Edite_Admin.eshterak);
                        #endregion
                        data8_C_O = true;
                        contextMenuStrip1.Items[3].Visible = false;
                        contextMenuStrip1.Items[4].Visible = true;
                        guna2Button1.FillColor = Color.Orange;
                        guna2Button1.Text = "بازگشت";
                        dataGridView8.Dock = DockStyle.Top;
                    }
                        else
                        {
                        
                            datagrid8_size -= 10;
                            dataGridView8.Size = new Size(datagrid_8_H, datagrid8_size);
                        }
                    }
                }
            
        }

        private void مشاهدهجزئیاتوچاپToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(TIME !=0)
            {
                var startupPath = System.Windows.Forms.Application.StartupPath;
                int A = startupPath.Length - 6;
                stiReport1.Load(startupPath.Substring(0, A) + @"\User_Histore_Zoom.mrt");
                var q1 = PersianDateTime.Now.ToString("hh:MM:ss");
                var q = PersianDateTime.Now.ToString("yy/MM/dd");

                stiReport1.Dictionary.Variables["Time_1"].Value = q;
                stiReport1.Dictionary.Variables["Time_2"].Value = q1;
                stiReport1.Dictionary.Variables["Eshterak"].Value = Edite_Admin.eshterak.ToString();
                stiReport1.Dictionary.Variables["name"].Value = NAME;
                stiReport1.Dictionary.Variables["Address"].Value = TARKIB;
                Cause cause = new Cause();
                cause.ShowDialog();
                if (cause.richTextBox1.Text.Count() > 0)
                {
                    stiReport1.Dictionary.Variables["Cause"].Value = cause.richTextBox1.Text;

                }
                else
                {
                    stiReport1.Dictionary.Variables["Cause"].Value = "ندارد";
                }

                stiReport1.RegBusinessObject("M", BLL._Histories(Int64.Parse(TIME.ToString())));
                stiReport1.Render();
                stiReport1.Show();
            }
            else
            {
                MessageBox.Show("لطفا موردی را انتخاب کنید");
            }
            
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {

        }

        private void guna2Button19_MouseMove(object sender, MouseEventArgs e)
        {
            guna2Button19.FillColor = Color.Red;
        }

        private void guna2Button19_MouseLeave(object sender, EventArgs e)
        {
            guna2Button19.FillColor = Color.Orange;
        }

        private void guna2Button22_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"
                                  !آیا تمام اطلاعات جدول ها حذف شوند
درصورت حذف اطلاعات امکان بازنشانی اطلاعات وجود ندارد", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                BLL.Delete_All_Vorod_Khorog();
                BLL.Delete_All_user();
                BLL.Delete_All_personels();
                BLL.Delete_All_food();
                dataGridView7.DataSource = BLL.ss();
                dataGridView8.DataSource = BLL.All_Personels();
                dataGridView9.DataSource = BLL.All_User();
                dataGridView6.DataSource = BLL.All_vorod_khorog();
            }
        }
       
       

       

        private void guna2Button20_Click(object sender, EventArgs e)
        {
            byte All=0;
            option_admin option_Admin = new option_admin();
            if (Properties.Settings.Default.datagrid_9 == true) { option_Admin.guna2ToggleSwitch2.Checked = true;  All++; }
            if (Properties.Settings.Default.datagrid_8 == true) { option_Admin.guna2ToggleSwitch3.Checked = true; All++; }
            if (Properties.Settings.Default.datagrid_7 == true) { option_Admin.guna2ToggleSwitch4.Checked = true; All++; }
            if (Properties.Settings.Default.datagrid_6 == true) { option_Admin.guna2ToggleSwitch5.Checked = true; All++; }
            if (Properties.Settings.Default.datagrid_5 == true) { option_Admin.guna2ToggleSwitch6.Checked = true; All++; }
            if (Properties.Settings.Default.delete == true) { option_Admin.guna2ToggleSwitch7.Checked = true; All++; }
            if (Properties.Settings.Default.report_admin == true) { option_Admin.guna2ToggleSwitch8.Checked = true; All++; }
            if(All==7)
            {
                option_Admin.guna2ToggleSwitch1.Checked = true;
                
            }
            All = 0;
            if (Properties.Settings.Default.data_amar == true) { option_Admin.guna2ToggleSwitch10.Checked = true; All++; }
            if (Properties.Settings.Default.report_amar == true) { option_Admin.guna2ToggleSwitch11.Checked = true; All++; }

            if (a==2) { option_Admin.guna2ToggleSwitch9.Checked = true; }
           


            option_Admin.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
             

        }

        private void guna2Button23_Click(object sender, EventArgs e)
        {
           

        }
        bool panel_rejster;
        int Vseparator = 0;
        int separator3_4 = 0;
        int separator5_6 = 0;
        int Vseparator10_11_12 = 0;
        byte next=0;
        private void timer10_Tick_1(object sender, EventArgs e)
        {
            if (panel_rejster == true)
            {
                if(separator5_6>=1750)
                {
                    separator5_6 = 0;
                    panel_rejster = false;
                    timer10.Stop();
                }
                else
                {
                    separator5_6 += 10;
                    guna2Separator5.Size = new Size(separator5_6, 10);
                    guna2Separator6.Size = new Size(separator5_6, 10);
                }
            }
            else
            {
                if (next == 0)
                {
                    if (Vseparator >= 950)
                    {
                        next = 1;
                    }
                    else
                    {
                        Vseparator += 17;
                        guna2VSeparator9.Size = new Size(7, Vseparator);
                    }
                }
                if (next == 1)
                {
                    if (separator3_4 >= 1350)
                    {
                        next = 2;
                    }
                    else
                    {
                        separator3_4 += 15;
                        guna2Separator3.Size = new Size(separator3_4, 2);
                        guna2Separator4.Size = new Size(separator3_4, 2);

                    }
                }
                if (next == 2)
                {
                    if (Vseparator10_11_12 >= 255)
                    {
                        Vseparator = 0;
                        separator3_4 = 0;
                        Vseparator10_11_12 = 0;
                        next = 0;
                        timer10.Stop();
                    }
                    else
                    {
                        Vseparator10_11_12 += 10;
                        guna2VSeparator10.Size = new Size(33, Vseparator10_11_12);
                        guna2VSeparator11.Size = new Size(33, Vseparator10_11_12);
                        guna2VSeparator12.Size = new Size(33, Vseparator10_11_12);
                    }
                }
                if (next == 2)
                {
                    if (Vseparator10_11_12 == 255)
                    {
                        Vseparator = 0;
                        separator3_4 = 0;
                        Vseparator10_11_12 = 0;
                        next = 0;
                        timer10.Stop();
                    }
                    else
                    {
                        Vseparator10_11_12 += 5;
                        guna2VSeparator10.Size = new Size(33, Vseparator10_11_12);
                        guna2VSeparator11.Size = new Size(33, Vseparator10_11_12);
                        guna2VSeparator12.Size = new Size(33, Vseparator10_11_12);
                    }
                }
            }
           
        }

        private void guna2TextBox6_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyData == Keys.Enter)
            //{
            //    if (guna2TextBox6.Text.Length >= 1 & guna2TextBox7.Text.Length >= 1)
            //    {
            //        personel_History P_H = new personel_History();
            //        P_H.name_food = guna2TextBox6.Text;
            //        P_H.money = int.Parse(guna2TextBox7.Text);
            //        P_H.tedad = int.Parse(numericUpDown1.Value.ToString());
            //        P_H.money_Final = int.Parse(guna2TextBox8.Text);
            //        p_h.Add(P_H);
            //        dataGridView1.DataSource = p_h.ToList();
            //    }
            //    else
            //    {
            //        label13.Text = "لطفا مشخصات محصول را به صورت کامل وارد کنید";
            //        label13.Visible = true;
            //        timer3.Start();
            //    }
            //}
        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {
            food ff;
            if (guna2TextBox6.Text.Length >= 2)
            {
                ff = BLL.food_search_text(guna2TextBox6.Text);
                if (ff != null)
                {

                    guna2TextBox7.Text = ff.money.ToString();
                    richTextBox1.Text = ff.tarkib;
                    if (ff.picture != null)
                    {
                        pictureBox9.Image = Image.FromFile(ff.picture);
                    }
                }
            }
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox7.Text.Length >= 1)
            {
                int x = int.Parse(guna2TextBox7.Text);
                int h = int.Parse(numericUpDown1.Value.ToString());
                guna2TextBox8.Text = (x * h).ToString();
            }
        }

        private void guna2TextBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (guna2TextBox6.Text.Length >= 1 & guna2TextBox7.Text.Length >= 1)
                {
                    personel_History P_H = new personel_History();
                    P_H.name_food = guna2TextBox6.Text;
                    P_H.money = int.Parse(guna2TextBox7.Text);
                    P_H.tedad = int.Parse(numericUpDown1.Value.ToString());
                    P_H.money_Final = int.Parse(guna2TextBox8.Text);
                    p_h.Add(P_H);
                    dataGridView1.DataSource = p_h.ToList();
                }
                else
                {
                    button8.BackColor = Color.Red;
                    button8.Text = "لطفا مشخصات محصول را به صورت کامل وارد کنید";
                    timer11.Start();
                }
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(guna2ComboBox1.SelectedIndex==0)
            {
                var day=PersianDateTime.Now.DayOfWeek.ToString();
                if (byte.Parse(day) == 0) { dataGridView10.DataSource = BLL.day_food(1); }
                if (byte.Parse(day) == 1) { dataGridView10.DataSource = BLL.day_food(2); }
                if (byte.Parse(day) == 2) { dataGridView10.DataSource = BLL.day_food(3); }
                if (byte.Parse(day) == 3) { dataGridView10.DataSource = BLL.day_food(4); }
                if (byte.Parse(day) == 4) { dataGridView10.DataSource = BLL.day_food(5); }
                if (byte.Parse(day) == 5) { dataGridView10.DataSource = BLL.day_food(6); }
                if (byte.Parse(day) == 6) { dataGridView10.DataSource = BLL.day_food(7); }

            }
            if (guna2ComboBox1.SelectedIndex == 1)
            {
                dataGridView10.DataSource = BLL.day_food(1);
            }
            if (guna2ComboBox1.SelectedIndex == 2)
            {
                dataGridView10.DataSource = BLL.day_food(2);
            }
            if (guna2ComboBox1.SelectedIndex == 3)
            {
                dataGridView10.DataSource = BLL.day_food(3);
            }
            if (guna2ComboBox1.SelectedIndex == 4)
            {
                dataGridView10.DataSource = BLL.day_food(4);
            }
            if (guna2ComboBox1.SelectedIndex == 5)
            {
                dataGridView10.DataSource = BLL.day_food(5);
            }
            if (guna2ComboBox1.SelectedIndex == 6)
            {
                dataGridView10.DataSource = BLL.day_food(6);
            }
            if (guna2ComboBox1.SelectedIndex == 7)
            {
                dataGridView10.DataSource = BLL.day_food(7);
            }
            if (guna2ComboBox1.SelectedIndex == 8)
            {
                dataGridView10.DataSource = BLL.day_food(8);
            }
            if (guna2ComboBox1.SelectedIndex == 9)
            {
                dataGridView10.DataSource = BLL.day_food(9);
            }
           
        }

        int index_datagrid10_food_id;
        private void dataGridView10_DoubleClick(object sender, EventArgs e)
        {
            food food = BLL.search_food_id(index_datagrid10_food_id);
            guna2TextBox6.Text = food.name;
            guna2TextBox7.Text =food.money.ToString();
            richTextBox1.Text = food.tarkib;
            if(food.picture !=null)
            {
                pictureBox9.ImageLocation = food.picture;
            }
        }

        private void dataGridView10_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index_datagrid10_food_id = (int)(dataGridView10.Rows[e.RowIndex].Cells[0].Value);
        }

        private void guna2TextBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button14_Click_3(object sender, EventArgs e)
        {
            

            
        }

        private void guna2TextBox6_Load(object sender, EventArgs e)
        {
           
        }

        private void guna2TextBox6_MouseClick(object sender, MouseEventArgs e)
        {
            //List<string> name_food = BLL.send_name_food();
            //var q = new AutoCompleteStringCollection();
            
            //foreach (var item in name_food)
            //{
            //    q.Add(item);

            //}
            

            //guna2TextBox6.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //guna2TextBox6.AutoCompleteCustomSource = q;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void button14_Click_4(object sender, EventArgs e)
        {
            var q=int.Parse((guna2ShadowPanel1.GetPreferredSize(MaximumSize)).ToString().Substring(7, 4));
            MessageBox.Show((q/=2).ToString());
        }

        private void timer11_Tick(object sender, EventArgs e)
        {
            button1.BackColor = Color.Tan;
            button1.Text = "ثبت سفارش";
            button8.BackColor = Color.Chartreuse;
            button8.Text = "ثبت";
            timer11.Stop();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar));
        }

        private void dataGridView5_MouseMove_1(object sender, MouseEventArgs e)
        {
          
        }

        private void dataGridView5_MouseClick(object sender, MouseEventArgs e)
        {
            //contextMenuStrip1.Items.ind
        }

        private void dataGridView5_MouseClick_1(object sender, MouseEventArgs e)
        {
           
        }

        private void dataGridView5_MouseClick_2(object sender, MouseEventArgs e)
        {
            contextMenuStrip1.Items[3].Visible = false;
            contextMenuStrip1.Items[4].Visible = false;
        }

        private void button14_Click_5(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void clocs_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Parse(PersianDateTime.Now.ToString());
            DateTime last = DateTime.Parse(time_vorod.ToString());
            TimeSpan work_user= now - last;
            var final_filter = work_user.ToString().Substring(0, 5);
            var remov = new string[] { "/", " ", "_", ":" };
            foreach (var item1 in remov) { final_filter = final_filter.Replace(item1, string.Empty); }

            label6.Text= (int.Parse(final_filter.ToString())+ BLL.user_work(username, Int64.Parse(PersianDateTime.Now.ToString("yyyyMMddhhmmss")))).ToString("##:##");
            label2.Text = PersianDateTime.Now.ToString("hh:mm:ss");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
          
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button24_Click(object sender, EventArgs e)
        {
           // this.guna2Button28 = StyleChanged.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        private void guna2GradientButton1_MouseMove(object sender, MouseEventArgs e)
        {
            select_button = 1;
            timer6.Stop();
            timer3.Start();
        }
        byte select_button;
        int G_button1 = 27;
        int G_button2 = 27;
        int G_button3 = 27;
        int G_button4 = 27;
        int G_button5 = 27;

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (select_button == 1)
            {
                if (G_button1 >= 150&G_button2<= 27 & G_button3 <= 27 & G_button4 <= 27 & G_button5 <= 27)
                {
                    timer3.Stop();
                }
                else
                {
                    if (G_button2 <= 27) { } else { G_button2 = G_button2 - 5; guna2GradientButton2.Width = G_button2; }
                    if (G_button3 <= 27) { } else { G_button3 = G_button3 - 5; guna2GradientButton3.Width = G_button3; }
                    if (G_button4 <= 27) { } else { G_button4 = G_button4 - 5; guna2GradientButton4.Width = G_button4; }
                    if (G_button5 <= 27) { } else { G_button5 = G_button5 - 5; guna2GradientButton5.Width = G_button5; }
                    if (G_button1 >= 150) { } else { G_button1 = G_button1 + 10; guna2GradientButton1.Width = G_button1; }
                }
            }
            if (select_button == 2)
            {
                if (G_button2 >= 150 & G_button1 <= 27 & G_button3 <= 27 & G_button4 <= 27 & G_button5 <= 27)
                {
                    timer3.Stop();
                }
                else
                {
                    if (G_button1 <= 27) { } else { G_button1 = G_button1 - 5; guna2GradientButton1.Width = G_button1; }
                    if (G_button3 <= 27) { } else { G_button3 = G_button3 - 5; guna2GradientButton3.Width = G_button3; }
                    if (G_button4 <= 27) { } else { G_button4 = G_button4 - 5; guna2GradientButton4.Width = G_button4; }
                    if (G_button5 <= 27) { } else { G_button5 = G_button5 - 5; guna2GradientButton5.Width = G_button5; }
                    if (G_button2 >= 150) { } else { G_button2 = G_button2 + 10; guna2GradientButton2.Width = G_button2; }
                }
            }
            if (select_button == 3)
            {
                if (G_button3 >= 150 & G_button1 <= 27 & G_button2 <= 27 & G_button4 <= 27 & G_button5 <= 27)
                {
                    timer3.Stop();
                }
                else
                {
                    if (G_button1 <= 27) { } else { G_button1 = G_button1 - 5; guna2GradientButton1.Width = G_button1; }
                    if (G_button2 <= 27) { } else { G_button2 = G_button2 - 5; guna2GradientButton2.Width = G_button2; }
                    if (G_button4 <= 27) { } else { G_button4 = G_button4 - 5; guna2GradientButton4.Width = G_button4; }
                    if (G_button5 <= 27) { } else { G_button5 = G_button5 - 5; guna2GradientButton5.Width = G_button5; }
                    if (G_button3 >= 150) { } else { G_button3 = G_button3 + 10; guna2GradientButton3.Width = G_button3; }
                }
            }
            if (select_button == 4)
            {
                if (G_button4 >= 150 & G_button1 <= 27 & G_button2 <= 27 & G_button3 <= 27 & G_button5 <= 27)
                {
                    timer3.Stop();
                }
                else
                {
                    if (G_button1 <= 27) { } else { G_button1 = G_button1 - 5; guna2GradientButton1.Width = G_button1; }
                    if (G_button2 <= 27) { } else { G_button2 = G_button2 - 5; guna2GradientButton2.Width = G_button2; }
                    if (G_button3 <= 27) { } else { G_button3 = G_button3 - 5; guna2GradientButton3.Width = G_button3; }
                    if (G_button5 <= 27) { } else { G_button5 = G_button5 - 5; guna2GradientButton5.Width = G_button5; }
                    if (G_button4 >= 150) { } else { G_button4 = G_button4 + 10; guna2GradientButton4.Width = G_button4; }
                }
            }
            if (select_button == 5)
            {
                if (G_button5 >= 150 & G_button1 <= 27 & G_button2 <= 27 & G_button3 <= 27 & G_button4 <= 27)
                {
                    timer3.Stop();
                }
                else
                {
                    if (G_button1 <= 27) { } else { G_button1 = G_button1 - 5; guna2GradientButton1.Width = G_button1; }
                    if (G_button2 <= 27) { } else { G_button2 = G_button2 - 5; guna2GradientButton2.Width = G_button2; }
                    if (G_button3 <= 27) { } else { G_button3 = G_button3 - 5; guna2GradientButton3.Width = G_button3; }
                    if (G_button4 <= 27) { } else { G_button4 = G_button4 - 5; guna2GradientButton4.Width = G_button4; }
                    if (G_button5 >= 150) { } else { G_button5 = G_button5 + 10; guna2GradientButton5.Width = G_button5; }
                }
            }

        }

        private void guna2GradientButton2_MouseMove(object sender, MouseEventArgs e)
        {
            select_button = 2;
            timer6.Stop();
            timer3.Start();
        }

        private void guna2GradientButton1_MouseLeave(object sender, EventArgs e)
        {
            A = 0;
            timer3.Stop();
            timer6.Start();
        }

        private void guna2GradientButton2_MouseLeave(object sender, EventArgs e)
        {
            A = 0;
            timer3.Stop();
            timer6.Start();
        }

        private void guna2GradientButton3_MouseMove(object sender, MouseEventArgs e)
        {
            select_button = 3;
            timer6.Stop();
            timer3.Start();
        }

        private void guna2GradientButton3_MouseLeave(object sender, EventArgs e)
        {
            A = 0;
            timer3.Stop();
            timer6.Start();
        }

        private void guna2GradientButton4_MouseMove(object sender, MouseEventArgs e)
        {
            select_button = 4;
            timer6.Stop();
            timer3.Start();
        }

        private void guna2GradientButton4_MouseLeave(object sender, EventArgs e)
        {
            A = 0;
            timer3.Stop();
            timer6.Start();
        }

        private void guna2GradientButton5_MouseMove(object sender, MouseEventArgs e)
        {
            select_button = 5;
            timer6.Stop();
            timer3.Start();
        }

        private void guna2GradientButton5_MouseLeave(object sender, EventArgs e)
        {
            A = 0;
            timer3.Stop();
            timer6.Start();
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            #region show_panel
            panel5.Visible = true;
            panel4.Visible = false;
            panel3.Visible = false;
            panel2.Visible = false;
            #endregion
        }

        private void pictureBox17_MouseMove(object sender, MouseEventArgs e)
        {
            select_button = 1;
            timer6.Stop();
            timer3.Start();
        }

        private void pictureBox17_MouseLeave(object sender, EventArgs e)
        {
            A = 0;
            timer3.Stop();
            timer6.Start();
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            #region show_panel
            panel5.Visible = false;
            panel4.Visible = true;
            panel3.Visible = false;
            panel2.Visible = false;
            #endregion
            panel_rejster = true;
            timer10.Start();
        }

        private void pictureBox18_MouseMove(object sender, MouseEventArgs e)
        {
            select_button = 2;
            timer6.Stop();
            timer3.Start();
        }

        private void pictureBox18_MouseLeave(object sender, EventArgs e)
        {
            A = 0;
            timer3.Stop();
            timer6.Start();
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
           
            if (ADMIN_LOGN != true)
            {
                byte All = 0;
                if (Properties.Settings.Default.data_amar == true) { All++; }
                if (Properties.Settings.Default.report_amar == true) { All++; }

                if (a == 0) { MessageBox.Show("!متاسفانه این بخش برای شما فعال نیست"); }
                else
                {
                    #region show_panel
                    panel5.Visible = false;
                    panel4.Visible = false;
                    panel3.Visible = true;
                    panel2.Visible = false;
                    #endregion
                    if (Properties.Settings.Default.report_amar == false) { button18.Enabled = false; }
                }
            }
            else
            {
                #region show_panel
                panel5.Visible = false;
                panel4.Visible = false;
                panel3.Visible = true;
                panel2.Visible = false;
                #endregion
            }
        }

        private void pictureBox19_MouseMove(object sender, MouseEventArgs e)
        {
            select_button = 3;
            timer6.Stop();
            timer3.Start();
        }

        private void pictureBox19_MouseLeave(object sender, EventArgs e)
        {
            A = 0;
            timer3.Stop();
            timer6.Start();
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            
            byte All = 0;
            if (ADMIN_LOGN != true)
            {
                option_admin option_Admin = new option_admin();
                if (Properties.Settings.Default.datagrid_9 == true) { All++; }
                if (Properties.Settings.Default.datagrid_8 == true) { All++; }
                if (Properties.Settings.Default.datagrid_7 == true) { All++; }
                if (Properties.Settings.Default.datagrid_6 == true) { All++; }
                if (Properties.Settings.Default.datagrid_5 == true) { All++; }
                if (Properties.Settings.Default.delete == true) { All++; }
                if (Properties.Settings.Default.report_admin == true) { All++; }
                if (All == 0) { MessageBox.Show("!متاسفانه این بخش برای شما فعال نیست"); }
                else
                {
                    if (Properties.Settings.Default.datagrid_9 == false) { dataGridView9.Enabled = false; guna2TextBox3.Enabled = false; guna2Button3.Enabled = false; guna2Button15.Enabled = false; guna2Button11.Enabled = false; guna2Button6.Enabled = false; pictureBox15.BackColor = ColorTranslator.FromHtml("255, 128, 128"); }
                    else { dataGridView9.DataSource = BLL.All_User(); }
                    if (Properties.Settings.Default.datagrid_8 == false) { dataGridView8.Enabled = false; guna2TextBox1.Enabled = false; guna2Button1.Enabled = false; guna2Button14.Enabled = false; guna2Button10.Enabled = false; guna2Button5.Enabled = false; pictureBox14.BackColor = ColorTranslator.FromHtml("255, 128, 128"); }
                    else
                    {
                        #region Personel_All
                        dataGridView8.Columns.Clear();
                        dataGridView8.Columns.Add("id_8", "id");
                        dataGridView8.Columns["id_8"].DataPropertyName = "id";
                        dataGridView8.Columns["id_8"].Visible = false;
                        dataGridView8.Columns.Add("name_8", "نام");
                        dataGridView8.Columns["name_8"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView8.Columns["name_8"].FillWeight = 60;
                        dataGridView8.Columns["name_8"].MinimumWidth = 30;
                        dataGridView8.Columns["name_8"].Width = 52;
                        dataGridView8.Columns["name_8"].DataPropertyName = "name";
                        dataGridView8.Columns["name_8"].ReadOnly = true;
                        dataGridView8.Columns.Add("adres_8", "آدرس");
                        dataGridView8.Columns["adres_8"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView8.Columns["adres_8"].FillWeight = 129;
                        dataGridView8.Columns["adres_8"].MinimumWidth = 117;
                        dataGridView8.Columns["adres_8"].Width = 117;
                        dataGridView8.Columns["adres_8"].DataPropertyName = "adres";
                        dataGridView8.Columns["name_8"].ReadOnly = true;
                        dataGridView8.Columns.Add("eshterak_8", "اشتراک");
                        dataGridView8.Columns["eshterak_8"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridView8.Columns["eshterak_8"].FillWeight = 45;
                        dataGridView8.Columns["eshterak_8"].MinimumWidth = 45;
                        dataGridView8.Columns["eshterak_8"].Width = 45;
                        dataGridView8.Columns["eshterak_8"].DataPropertyName = "eshterak";
                        dataGridView8.Columns["name_8"].ReadOnly = true;
                        dataGridView8.DataSource = BLL.All_Personels();
                        #endregion }
                    }
                    if (Properties.Settings.Default.datagrid_7 == false) { dataGridView7.Enabled = false; guna2TextBox2.Enabled = false; guna2Button2.Enabled = false; guna2Button13.Enabled = false; guna2Button9.Enabled = false; guna2Button4.Enabled = false; pictureBox13.BackColor = ColorTranslator.FromHtml("255, 128, 128"); }
                    else { dataGridView7.DataSource = BLL.ss(); }
                    if (Properties.Settings.Default.datagrid_6 == false) { dataGridView6.Enabled = false; guna2TextBox4.Enabled = false; guna2Button17.Enabled = false; guna2Button12.Enabled = false; guna2Button8.Enabled = false; guna2Button7.Enabled = false; pictureBox12.BackColor = ColorTranslator.FromHtml("255, 128, 128"); }
                    else { dataGridView6.DataSource = BLL.All_vorod_khorog(); }
                    if (Properties.Settings.Default.datagrid_5 == false) { dataGridView5.Enabled = false; guna2TextBox5.Enabled = false; guna2Button18.Enabled = false; guna2Button16.Enabled = false; pictureBox7.BackColor = ColorTranslator.FromHtml("255, 128, 128"); }
                    else { dataGridView5.DataSource = BLL.All_day_Work(); }
                    if (Properties.Settings.Default.report_admin == false) { guna2Button17.Enabled = false; guna2Button13.Enabled = false; guna2Button14.Enabled = false; guna2Button15.Enabled = false; contextMenuStrip1.Items.RemoveAt(2); }
                    if (Properties.Settings.Default.delete == false) { guna2Button8.Enabled = false; guna2Button9.Enabled = false; guna2Button10.Enabled = false; guna2Button11.Enabled = false; guna2Button22.Enabled = false; contextMenuStrip1.Items.RemoveAt(1); }
                    guna2Button23.Enabled = false; guna2Button20.Enabled = false; guna2Button19.Enabled = false;
                    #region show_panel
                    panel5.Visible = false;
                    panel4.Visible = false;
                    panel3.Visible = false;
                    panel2.Visible = true;
                    #endregion
                }
            }
            else
            {
                dataGridView9.DataSource = BLL.All_User();
                #region Personel_All
                dataGridView8.Columns.Clear();
                dataGridView8.Columns.Add("id_8", "id");
                dataGridView8.Columns["id_8"].DataPropertyName = "id";
                dataGridView8.Columns["id_8"].Visible = false;
                dataGridView8.Columns.Add("name_8", "نام");
                dataGridView8.Columns["name_8"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView8.Columns["name_8"].FillWeight = 60;
                dataGridView8.Columns["name_8"].MinimumWidth = 30;
                dataGridView8.Columns["name_8"].Width = 52;
                dataGridView8.Columns["name_8"].DataPropertyName = "name";
                dataGridView8.Columns["name_8"].ReadOnly = true;
                dataGridView8.Columns.Add("adres_8", "آدرس");
                dataGridView8.Columns["adres_8"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView8.Columns["adres_8"].FillWeight = 129;
                dataGridView8.Columns["adres_8"].MinimumWidth = 117;
                dataGridView8.Columns["adres_8"].Width = 117;
                dataGridView8.Columns["adres_8"].DataPropertyName = "adres";
                dataGridView8.Columns["name_8"].ReadOnly = true;
                dataGridView8.Columns.Add("eshterak_8", "اشتراک");
                dataGridView8.Columns["eshterak_8"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView8.Columns["eshterak_8"].FillWeight = 45;
                dataGridView8.Columns["eshterak_8"].MinimumWidth = 45;
                dataGridView8.Columns["eshterak_8"].Width = 45;
                dataGridView8.Columns["eshterak_8"].DataPropertyName = "eshterak";
                dataGridView8.Columns["name_8"].ReadOnly = true;
                dataGridView8.DataSource = BLL.All_Personels();
                #endregion }
                dataGridView7.DataSource = BLL.ss();
                dataGridView6.DataSource = BLL.All_vorod_khorog();
                dataGridView5.DataSource = BLL.All_day_Work();
                #region show_panel
                panel5.Visible = false;
                panel4.Visible = false;
                panel3.Visible = false;
                panel2.Visible = true;
                #endregion
            }


            //label29.Text = "شما در بخش مدریت قابلیت درسترسی به بخش های بیشتر سیستم دارین و میتوانید اطلاعاتی را حذف ویا اظافه کنید";
            //point = (label29.Text.Length * 9) * -1;
            //timer7.Start();
            A = 1;
            timer5.Start();

            if (All != 7 & ADMIN_LOGN != true) { MessageBox.Show("بخش مدیریت همراه با محدودیت برای شما فعال شد"); }
        }

        private void pictureBox20_MouseMove(object sender, MouseEventArgs e)
        {
            select_button = 4;
            timer6.Stop();
            timer3.Start();
        }

        private void pictureBox20_MouseLeave(object sender, EventArgs e)
        {
            A = 0;
            timer3.Stop();
            timer6.Start();
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox21_MouseMove(object sender, MouseEventArgs e)
        {
            select_button = 5;
            timer6.Stop();
            timer3.Start();
        }

        private void pictureBox21_MouseLeave(object sender, EventArgs e)
        {
            A = 0;
            timer3.Stop();
            timer6.Start();
        }

        private void guna2Button21_Click(object sender, EventArgs e)
        {
            Edite_Admin.update_1 = true;
            Edite_Admin.chang = false;
            User user_Edit = BLL.search_user_Username(username);
            Edite_Admin edite_Admin = new Edite_Admin();
            edite_Admin.Size = new Size(414, 638);
            edite_Admin.panel1.Visible = true;
            edite_Admin.panel1.Dock = DockStyle.Fill;
            edite_Admin.pictureBox1.ImageLocation = user_Edit.picture_User;
            edite_Admin.guna2TextBox1.Text = user_Edit.name;
            edite_Admin.guna2TextBox2.Text = user_Edit.username;
            Edite_Admin.username = user_Edit.username;
            edite_Admin.guna2TextBox3.Text = user_Edit.password;
            edite_Admin.guna2TextBox4.Text = user_Edit.password;
            Edite_Admin.picture = user_Edit.picture_User;
            edite_Admin.guna2GradientButton1.Text = "ویرایش اطلاعات";
            edite_Admin.ShowDialog();
        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button24_Click_1(object sender, EventArgs e)
        {
            vorod_khorog User_now = new vorod_khorog();
            User_now.username = username;
            var final_filter = time_vorod.ToString();
            var remov = new string[] { "/", " ", "_", ":" };
            foreach (var item1 in remov) { final_filter = final_filter.Replace(item1, string.Empty); }
            User_now.vorod = Int64.Parse(final_filter);
            User_now.khoroj = Int64.Parse(PersianDateTime.Now.ToString("yyyyMMddhhmmss"));
            this.Close();

        }

        private void guna2Button25_Click(object sender, EventArgs e)
        {
            vorod_khorog User_now = new vorod_khorog();
            User_now.username = username;
            var final_filter = time_vorod.ToString();
            var remov = new string[] { "/", " ", "_", ":" };
            foreach (var item1 in remov) { final_filter = final_filter.Replace(item1, string.Empty); }
            User_now.vorod = Int64.Parse(final_filter);
            //string f = PersianDateTime.Now.ToString("yyyyMMddhhmmss");
            
            User_now.khoroj = Int64.Parse(PersianDateTime.Now.ToString("yyyyMMddhhmmss"));
            Application.Restart();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}



