using BE_KFC;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KFC
{
    public partial class Edite_Admin : Form
    {
        public Edite_Admin()
        {
            InitializeComponent();
        }

        BL_KFC.BL BLL = new BL_KFC.BL();

        bool enabled1 = false;
        bool enabled2 = false;
        bool enabled3 = false;
        bool enabled4 = false;
        bool enabled5 = false;

        #region پنل یک ـ ثبت یوزر
        string savepic_User(string cod)
        {

            // if (update == true)
            //{

            //    FileInfo info = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + @"\Pictures_User\" + cod + ".jpg");


            //    if (info.Exists)
            //    {

            //        info.Delete();
            //    }

            //}

            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Pictures_User\";
            if (Directory.Exists(appPath) == false)
            {
                Directory.CreateDirectory(appPath);
            }
            string iName = cod + ".jpg";
            try
            {
                string filepath ;
                if (f.FileName != "")
                {
                    filepath = f.FileName;
                }
                else
                {
                    filepath = picture;
                }
                File.Copy(filepath, appPath + iName, true);
            }
            catch (Exception)
            {
                MessageBox.Show("محصول بدون تصویر ذخیره شد");
            }
            if (update_1 == true & name != cod)
            {

                FileInfo info = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + @"\Pictures_User\" + name + ".jpg");


                if (info.Exists)
                {
                    pictureBox3.Image.Dispose();
                    pictureBox3.Image = null;
                    info.Delete();
                }

            }
            return appPath + iName;
        }

        OpenFileDialog f = new OpenFileDialog();

        private void guna2ToggleSwitch1_CheckedChanged_1(object sender, EventArgs e)
        {

            if (guna2ToggleSwitch1.Checked == true)
            {
                guna2TextBox3.PasswordChar = '\0';
                guna2TextBox4.PasswordChar = '\0';
            }
            else
            {
                guna2TextBox3.PasswordChar = '*';
                guna2TextBox4.PasswordChar = '*';
            }
        }
        private void guna2Button4_Click_1(object sender, EventArgs e)
        {
            Image pic;
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }
                pic = Image.FromFile(f.FileName);
                pictureBox1.Image = pic;
                chang = true;
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (enabled1 == false | enabled2 == false | enabled3 == false | enabled4 == false | enabled5 == false)
            {
                if (enabled1 == false) { errorProvider1.SetError(guna2TextBox1, "لطفا نام خود را وارد کنید"); }
                if (guna2TextBox2.Text == "")
                {
                    errorProvider1.SetError(guna2TextBox2, "لطفا یوزر نیم را وارد کنید");
                }
                else if (enabled2 == false) { errorProvider1.SetError(guna2TextBox2, "این یوزر نیم قبلا استفاده شده"); }
                if (enabled3 == false) { errorProvider1.SetError(guna2TextBox3, "لطفا پسورد را وارد کنید"); }
                if (guna2TextBox4.Text == "")
                {
                    errorProvider1.SetError(guna2TextBox4, "لطفا تکرار پسورد را وارد کنید");
                }
                else if (guna2TextBox4.Text != guna2TextBox3.Text)
                {

                    errorProvider1.SetError(guna2TextBox4, "پسورد با پسورد وارد شده مطابقت ندارد");
                }

                if (enabled5 == false) { MessageBox.Show("لطفا تیک تایید متن بالا را فعال کنید"); }
                //MessageBox.Show("لطفا فیلدی را خالی نگذارید");
            }
            else
            {
                if (update_1 == false)
                {



                    if (guna2TextBox3.Text == guna2TextBox4.Text)
                    {
                        User user = new User();
                        user.name = guna2TextBox1.Text;
                        user.username = guna2TextBox2.Text;
                        user.password = guna2TextBox3.Text;
                        if (pictureBox1 != null)
                        {
                            user.picture_User = savepic_User(guna2TextBox1.Text + guna2TextBox2.Text);
                        }
                        BLL.User_rejster(user);
                        pictureBox1.Image = null;
                        guna2TextBox1.Text = null;
                        guna2TextBox1.BorderColor = Color.Silver;
                        guna2TextBox2.Text = null;
                        guna2TextBox2.BorderColor = Color.Silver;
                        guna2TextBox3.Text = null;
                        guna2TextBox3.BorderColor = Color.Silver;
                        guna2TextBox4.Text = null;
                        guna2TextBox4.BorderColor = Color.Silver;
                        guna2ToggleSwitch1.Checked = false;
                        guna2CustomCheckBox1.Checked = false;
                        enabled1 = false;
                        enabled2 = false;
                        enabled3 = false;
                        enabled4 = false;
                        enabled5 = false;
                        MessageBox.Show("یوزر با موفقیت ثبت شد");
                    }
                    else
                    {
                        MessageBox.Show("لطفا پسورد را باز بینی کنید");
                    }


                }
                else if (update_1 == true)
                {
                    User user_Edite = new User();
                    name = guna2TextBox1.Text;
                    user_Edite.id = Form1.ID;
                    user_Edite.name = guna2TextBox1.Text;
                    user_Edite.username = guna2TextBox2.Text;
                    user_Edite.password = guna2TextBox3.Text;
                    if (chang == true | user_Edite.name != name)
                    {
                        user_Edite.picture_User = savepic_User(user_Edite.name);
                    }
                    else
                    {
                        user_Edite.picture_User = picture;
                    }
                    BLL.Edite_User(user_Edite);

                    if (user_Edite.picture_User != null)
                    {
                        ((Form1)Application.OpenForms["Form1"]).pictureBox22.ImageLocation = user_Edite.picture_User;
                    }
                    else
                    {
                        ((Form1)Application.OpenForms["Form1"]).pictureBox22.Image = BackgroundImage;
                    }

                    this.Close();
                }
            ((Form1)Application.OpenForms["Form1"]).dataGridView9.DataSource = BLL.All_User();
            }
        }


        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "")
            {
                enabled2 = false;
                guna2TextBox2.BorderColor = Color.Red;
            }
            else
            {


                enabled2 = true;
                guna2TextBox2.BorderColor = Color.Lime;


            }
            if (enabled1 == true & enabled2 == true & enabled3 == true & enabled4 == true & enabled5 == true)
            { guna2GradientButton1.FillColor2 = Color.Green; }
            else { guna2GradientButton1.FillColor2 = Color.Red; }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "")
            {
                enabled1 = false;
                guna2TextBox1.BorderColor = Color.Red;
            }
            else
            {
                enabled1 = true;
                guna2TextBox1.BorderColor = Color.Lime;
            }
            if (enabled1 == true & enabled2 == true & enabled3 == true & enabled4 == true & enabled5 == true)
            { guna2GradientButton1.FillColor2 = Color.Green; }
            else { guna2GradientButton1.FillColor2 = Color.Red; }

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox3.Text == "")
            {
                enabled3 = false;
                guna2TextBox3.BorderColor = Color.Red;
            }
            else
            {
                enabled3 = true;
                guna2TextBox3.BorderColor = Color.Lime;
            }
            if (enabled1 == true & enabled2 == true & enabled3 == true & enabled4 == true & enabled5 == true)
            { guna2GradientButton1.FillColor2 = Color.Green; }
            else { guna2GradientButton1.FillColor2 = Color.Red; }
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

            if (guna2TextBox4.Text == "")
            {
                enabled4 = false;
                guna2TextBox4.BorderColor = Color.Red;
                errorProvider1.SetError(guna2TextBox4, "لطفا تکرار پسورد را وارد کنید");
            }
            else
            {
                if (guna2TextBox3.Text == guna2TextBox4.Text)
                {
                    enabled4 = true;
                    guna2TextBox4.BorderColor = Color.Lime;
                    errorProvider1.Clear();
                }
                else
                {
                    enabled4 = false;
                    guna2TextBox4.BorderColor = Color.Yellow;
                    errorProvider1.SetError(guna2TextBox4, "پسورد با پسورد وارد شده مطابقت ندارد");

                }
                if (enabled1 == true & enabled2 == true & enabled3 == true & enabled4 == true & enabled5 == true)
                { guna2GradientButton1.FillColor2 = Color.Green; }
                else { guna2GradientButton1.FillColor2 = Color.Red; }

            }
        }

        private void guna2CustomCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox1.Checked == true)
            {
                enabled5 = true;
            }
            else
            {
                enabled5 = false;
            }
            if (enabled1 == true & enabled2 == true & enabled3 == true & enabled4 == true & enabled5 == true)
            { guna2GradientButton1.FillColor2 = Color.Green; }
            else { guna2GradientButton1.FillColor2 = Color.Red; }
        }

        public static string username;
        private void guna2TextBox2_Leave(object sender, EventArgs e)
        {
            if (BLL.Search_Username(guna2TextBox2.Text) == true & guna2TextBox2.Text !=username)
            {
                enabled2 = false;
                guna2TextBox2.BorderColor = Color.Red;
                errorProvider1.SetError(guna2TextBox2, "این یوزر نیم قبلا استفاده شده");
            }
            else
            {
                errorProvider1.Clear();
                enabled2 = true;
                guna2TextBox2.BorderColor = Color.Lime;
            }
            if (enabled1 == true & enabled2 == true & enabled3 == true & enabled4 == true & enabled5 == true)
            { guna2GradientButton1.FillColor2 = Color.Green; }
            else { guna2GradientButton1.FillColor2 = Color.Red; }
        }
        #endregion پنل یک ـ ثبت یوزر//


        string savepic(string cod)
        {



            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Pictures_Admin\";
            if (Directory.Exists(appPath) == false)
            {
                Directory.CreateDirectory(appPath);
            }
            string iName = cod + ".jpg";
            try
            {
                string filepath;
                if (f.FileName != "")
                {
                    filepath = f.FileName;
                }
                else
                {
                    filepath = picture;
                }
                File.Copy(filepath, appPath + iName, true);

            }
            catch (Exception)
            {

                MessageBox.Show("محصول بدون تصویر ذخیره شد");
            }
            if (update_1 == true & name != cod)
            {

                FileInfo info = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + @"\Pictures_Admin\" + name + ".jpg");


                if (info.Exists)
                {
                    pictureBox3.Image.Dispose();
                    pictureBox3.Image = null;
                    info.Delete();
                }

            }
            return appPath + iName;


        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2TextBox7.Text = BLL.Auto_eshterak().ToString();
            enabled3 = true;
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox7.Text.Length == 4)
            {
                if (BLL.check_exist_eshterak(int.Parse(guna2TextBox7.Text)) == false | guna2TextBox7.Text == eshterak.ToString())
                {
                    enabled3 = true;
                    guna2TextBox7.BorderColor = Color.Lime;
                }
                else
                {
                    guna2TextBox7.BorderColor = Color.Red;
                    enabled3 = false;
                }

            }
            else
            {
                guna2TextBox7.BorderColor = Color.Red;
                enabled3 = false;
            }
            if (enabled1 == true & enabled2 == true & enabled3 == true)
            {
                guna2GradientButton2.FillColor2 = Color.Lime;
            }
            else
            {
                guna2GradientButton2.FillColor2 = Color.Red;
            }
        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox5.Text.Length >= 1)
            {
                guna2TextBox5.BorderColor = Color.Lime;
                enabled1 = true;
            }
            else
            {
                guna2TextBox5.BorderColor = Color.Red;
                enabled1 = false;
            }
            if (enabled1 == true & enabled2 == true & enabled3 == true)
            {
                guna2GradientButton2.FillColor2 = Color.Lime;
            }
            else
            {
                guna2GradientButton2.FillColor2 = Color.Red;
            }
        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox6.Text.Length >= 1)
            {
                guna2TextBox6.BorderColor = Color.Lime;
                enabled2 = true;
            }
            else
            {
                guna2TextBox6.BorderColor = Color.Red;
                enabled2 = false;
            }
            if (enabled1 == true & enabled2 == true & enabled3 == true)
            {
                guna2GradientButton2.FillColor2 = Color.Lime;
            }
            else
            {
                guna2GradientButton2.FillColor2 = Color.Red;
            }
        }

        public static int eshterak;
        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (update_1 == false)
            {
                if (enabled1 == false | enabled2 == false | enabled3 == false)
                {
                    if (enabled1 == false) { errorProvider1.SetError(guna2TextBox5, "لطفا فیلد نام را خالی نگذارید"); }
                    if (enabled2 == false) { errorProvider1.SetError(guna2TextBox6, "لطفا فیلد آدرس را خالی نگذارید"); }
                    if (enabled3 == false) { errorProvider1.SetError(guna2TextBox7, "لطفا فیلد اشتراک را به صورت صحیح پر کنید"); }
                }
                else
                {
                    personel personel = new personel();
                    personel.name = guna2TextBox5.Text;
                    personel.adres = guna2TextBox6.Text;
                    personel.eshterak = int.Parse(guna2TextBox7.Text);
                    BLL.Register_Personel(personel);
                    enabled1 = false;
                    enabled2 = false;
                    enabled3 = false;
                    guna2TextBox5.Clear();
                    guna2TextBox6.Clear();
                    guna2TextBox7.Clear();
                    errorProvider1.Clear();
                    eshterak = 0;
                    ((Form1)Application.OpenForms["Form1"]).dataGridView8.DataSource = BLL.All_Personels();
                    this.Close();
                }
            }
            else if (update_1 == true)
            {
                if (enabled1 == false | enabled2 == false | enabled3 == false)
                {
                    if (enabled1 == false) { errorProvider1.SetError(guna2TextBox5, "لطفا فیلد نام را خالی نگذارید"); }
                    if (enabled2 == false) { errorProvider1.SetError(guna2TextBox6, "لطفا فیلد آدرس را خالی نگذارید"); }
                    if (enabled3 == false) { errorProvider1.SetError(guna2TextBox7, "لطفا فیلد اشتراک را به صورت صحیح پر کنید"); }
                }
                else
                {
                    personel personel_edite = new personel();
                    personel_edite.id = Form1.ID;
                    personel_edite.name = guna2TextBox5.Text;
                    personel_edite.adres = guna2TextBox6.Text;
                    personel_edite.eshterak = int.Parse(guna2TextBox7.Text);
                    BLL.Edit_Personel(personel_edite);
                    ((Form1)Application.OpenForms["Form1"]).dataGridView8.DataSource = BLL.All_Personels();
                    this.Close();

                }
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                // action_4 = false;
                checkedListBox1.Visible = true;
                A = 1;
                timer1.Start();
            }
            if (radioButton1.Checked == false)
            {
                //action_4 = true;
                A = 2;
                timer1.Start();
            }
        }

        byte A = 0;
        int check_List = 4;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (A == 1)
            {
                if (check_List >= 144)
                {
                    timer1.Stop();
                }
                else
                {
                    check_List += 20;
                    checkedListBox1.Size = new Size(94, check_List);
                }
            }
            if (A == 2)
            {
                if (check_List <= 4)
                {
                    checkedListBox1.Visible = false;
                    timer1.Stop();
                }
                else
                {
                    check_List -= 20;
                    checkedListBox1.Size = new Size(94, check_List);
                }
            }
        }



        public static bool update_1 = false;
        //public static int ID;
        public static string name;
        public static string picture;
        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (update_1 == false)
            {
                if (action_1 == false | action_2 == false | action_3 == false)
                {
                    if (action_1 == false) { errorProvider1.SetError(guna2TextBox8, "لطفا فیلد نام محصول را خالی نگذارید"); }
                    if (action_2 == false) { errorProvider1.SetError(guna2TextBox9, "لطفا فیلد ترکیبات را خالی نگذارید"); }
                    if (action_3 == false) { errorProvider1.SetError(guna2TextBox10, "لطفا فیلد قیمت را خالی نگذارید"); }
                }
                else
                {
                    food fod = new food();
                    time tim = new time();

                    fod.name = guna2TextBox8.Text;
                    fod.tarkib = guna2TextBox9.Text;
                    fod.money = int.Parse(guna2TextBox10.Text);
                    if (pictureBox3.Image != null) { fod.picture = savepic(fod.name); }


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
                    pictureBox3.Image = BackgroundImage;
                    guna2TextBox8.Text = null;
                    guna2TextBox9.Text = null;
                    guna2TextBox10.Text = null;
                    ((Form1)Application.OpenForms["Form1"]).dataGridView7.DataSource = BLL.ss();

                }
            }
            else if (update_1 == true)
            {
                if (action_1 == false | action_2 == false | action_3 == false)
                {
                    if (action_1 == false) { errorProvider1.SetError(guna2TextBox8, "لطفا فیلد نام محصول را خالی نگذارید"); }
                    if (action_2 == false) { errorProvider1.SetError(guna2TextBox9, "لطفا فیلد ترکیبات را خالی نگذارید"); }
                    if (action_3 == false) { errorProvider1.SetError(guna2TextBox10, "لطفا فیلد قیمت را خالی نگذارید"); }
                }
                else
                {
                    time time_update = new time();
                    food food_update = new food();
                    food_update.name = guna2TextBox8.Text;
                    food_update.tarkib = guna2TextBox9.Text;
                    food_update.money = int.Parse(guna2TextBox10.Text);
                    if (chang == true | food_update.name != name)
                    {
                        food_update.picture = savepic(food_update.name);
                    }
                    else
                    {
                        food_update.picture = picture;
                    }




                    time_update.food_id = Form1.ID;

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
                        //checkedListBox1.SetItemChecked(1, false);
                    }




                    BLL.update_time(time_update, Form1.ID);
                    BLL.update(food_update, Form1.ID);
                    ((Form1)Application.OpenForms["Form1"]).dataGridView7.DataSource = BLL.ss();
                    this.Close();
                }

            }

        }
        public static bool chang = false;
        private void guna2Button5_Click(object sender, EventArgs e)
        {

            Image pic;
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox3.Image != null)
                {
                    pictureBox3.Image.Dispose();
                    pictureBox3.Image = null;
                }
                pic = Image.FromFile(f.FileName);
                pictureBox3.Image = pic;
                chang = true;
            }
        }

        bool action_1 = false;
        bool action_2 = false;
        bool action_3 = false;
        private void guna2TextBox8_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox8.Text.Length >= 1)
            {
                action_1 = true;
                if (action_1 == true & action_2 == true & action_3 == true)
                {
                    guna2GradientButton3.FillColor2 = Color.Lime;
                }
                else { guna2GradientButton3.FillColor2 = Color.Red; }

            }
            else
            {
                action_1 = false;
                if (action_1 == true & action_2 == true & action_3 == true)
                {
                    guna2GradientButton3.FillColor2 = Color.Lime;
                }
                else { guna2GradientButton3.FillColor2 = Color.Red; }
            }
        }

        private void guna2TextBox9_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox9.Text.Length >= 1)
            {
                action_2 = true;
                if (action_1 == true & action_2 == true & action_3 == true)
                {
                    guna2GradientButton3.FillColor2 = Color.Lime;
                }
                else { guna2GradientButton3.FillColor2 = Color.Red; }

            }
            else
            {
                action_2 = false;
                if (action_1 == true & action_2 == true & action_3 == true)
                {
                    guna2GradientButton3.FillColor2 = Color.Lime;
                }
                else { guna2GradientButton3.FillColor2 = Color.Red; }
            }
        }

        private void guna2TextBox10_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox10.Text.Length >= 1)
            {
                action_3 = true;
                if (action_1 == true & action_2 == true & action_3 == true)
                {
                    guna2GradientButton3.FillColor2 = Color.Lime;
                }
                else { guna2GradientButton3.FillColor2 = Color.Red; }

            }
            else
            {
                action_3 = false;
                if (action_1 == true & action_2 == true & action_3 == true)
                {
                    guna2GradientButton3.FillColor2 = Color.Lime;
                }
                else { guna2GradientButton3.FillColor2 = Color.Red; }
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_ParentChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void checkedListBox1_CausesValidationChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        private void checkedListBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void guna2GradientTileButton1_Click(object sender, EventArgs e)
        {
            
        }

        string mask1_length;
        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
             mask1_length = maskedTextBox1.Text;
            var remov = new string[] { "/", " ", "_", ":" };
            foreach (var item in remov) { mask1_length = mask1_length.Replace(item, string.Empty); }
            if (mask1_length.Length==12)
            {
                action_2 = true;
                if (action_1 == true & action_2 == true & action_3 == true)
                {
                    guna2GradientButton4.FillColor2 = Color.Lime;
                }
                else { guna2GradientButton4.FillColor2 = Color.Red; }
            }
            else
            {
                action_2 = false;
                guna2GradientButton4.FillColor2 = Color.Red;
            }
        }

        private void guna2TextBox13_TextChanged(object sender, EventArgs e)
        {
            if(BLL.search_user_Username(guna2TextBox13.Text) !=null)
            {
                User Vorod_Khorog_User = BLL.search_user_Username(guna2TextBox13.Text);
                pictureBox4.ImageLocation = Vorod_Khorog_User.picture_User;
                action_1 = true;
                if (action_1 == true & action_2 == true & action_3 == true)
                {
                    guna2GradientButton4.FillColor2 = Color.Lime;
                }
                else { guna2GradientButton4.FillColor2 = Color.Red; }
            }
            else
            {
                action_1 = false;
                pictureBox4.Image = BackgroundImage;
                guna2GradientButton4.FillColor2 = Color.Red;
            }
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            if (action_1 == false | action_2 == false | action_3 == false)
            {
                if (action_1 == false) { errorProvider1.SetError(guna2TextBox13, "یوزر نیم را وارد کنید"); }
                if (action_2 == false) { errorProvider1.SetError(maskedTextBox1, "تاریخ ورود را وارد کنید"); }
                if (action_3 == false) { errorProvider1.SetError(maskedTextBox2, "تاریخ خروج را وارد کنید"); }
            }
            else
            {
                if (update_1 == false)
                {

                        vorod_khorog V_K = new vorod_khorog();
                        V_K.username = guna2TextBox13.Text;
                        V_K.vorod = Int64.Parse(mask1_length);
                        V_K.khoroj = Int64.Parse(mask2_length);
                        BLL.Register_vorod_khorog(V_K);
                        this.Close();
                }
                else if (update_1 == true)
                {
                    vorod_khorog Edite = new vorod_khorog();
                    Edite.id = Form1.ID;
                    Edite.username = guna2TextBox13.Text;
                    Edite.vorod = Int64.Parse(mask1_length);
                    Edite.khoroj = Int64.Parse(mask2_length);
                    BLL.Edite_vorod_Khorog(Edite);
                    
                    this.Close();
                }
                ((Form1)Application.OpenForms["Form1"]).dataGridView5.DataSource = BLL.All_day_Work();
                ((Form1)Application.OpenForms["Form1"]).dataGridView6.DataSource = BLL.All_vorod_khorog();
            }


        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            
            maskedTextBox1.Text= PersianDateTime.Now.ToString("yyyyMMddHHmm"); ;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            maskedTextBox2.Text = PersianDateTime.Now.ToString("yyyyMMddHHmm"); ;
        }

        string mask2_length;
        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            mask2_length = maskedTextBox2.Text;
            var remov = new string[] { "/", " ", "_", ":" };
            foreach (var item in remov) { mask2_length = mask2_length.Replace(item, string.Empty); }
            if (mask2_length.Length == 12)
            {
                action_3 = true;
                if (action_1 == true & action_2 == true & action_3 == true)
                {
                    guna2GradientButton4.FillColor2 = Color.Lime;
                }
                else
                {
                    guna2GradientButton4.FillColor2 = Color.Red;
                }
            }
            else
            {
                action_3 = false;
                if (action_1 == true & action_2 == true & action_3 == true)
                {
                    guna2GradientButton4.FillColor2 = Color.Lime;
                }
                else
                {
                    guna2GradientButton4.FillColor2 = Color.Red;
                }
            }
        }
    }

}
