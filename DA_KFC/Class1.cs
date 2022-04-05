using BE_KFC;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;



namespace DA_KFC
{

    public class db : DbContext
    {
        public db() : base("name=KFC") { }
        public DbSet<User> Users { get; set; }
        public DbSet<food> foods { get; set; }
        public DbSet<vorod_khorog> vorod_Khorogs { get; set; }
        public DbSet<time> times { get; set; }
        public DbSet<personel> personels { get; set; }
        public DbSet<personel_History> personel_Histories { get; set; }

    }

    public class da_kfc
    {
        db db1 = new db();
        food dd = new food();
        public void Login(User u)
        {

            db1.Users.Add(u);
            db1.SaveChanges();

        }
        public void fod(food u)
        {

            db1.foods.Add(u);
            db1.SaveChanges();
        }
        public bool vorod(User r)
        {
            foreach (var item in db1.Users)
            {
                if (item.password == r.password && item.username == r.username)
                {
                    return true;
                }
            }
            return false;

        }
        public List<food> ss()
        {
            db db2 = new db();
            return db2.foods.ToList();
        }
        public void regster(food f)
        {
            db1.foods.Add(f);
            db1.SaveChanges();
        }
        public void RG_Time(time f)
        {
            db1.times.Add(f);
            db1.SaveChanges();
        }
        public food readID(int f)
        {
            db db2 = new db();
            return (from i in db2.foods where i.id == f select i).Single();
        }
        public List<string> roz(int id)
        {
            db db2 = new db();
            List<string> aas = new List<string>();



            foreach (var item in db2.times)
            {
                if (item.food_id == id)
                {
                    if (item.saturday == 1) { aas.Add("A"); }
                    if (item.sunday == 1) { aas.Add("B"); }
                    if (item.monday == 1) { aas.Add("C"); }
                    if (item.tuesday == 1) { aas.Add("D"); }
                    if (item.wednesday == 1) { aas.Add("E"); }
                    if (item.thursday == 1) { aas.Add("F"); }
                    if (item.friday == 1) { aas.Add("G"); }
                }
            }

            return aas;
            //return aas.ToString();
        }
        public void update(food f, int ID)
        {

            //var q= from i in db1.foods where i.id == f.id select i;
            foreach (var item in db1.foods)
            {
                if (item.id == ID)
                {

                    item.name = f.name;
                    item.tarkib = f.tarkib;
                    item.money = f.money;
                    item.picture = f.picture;


                }
            }
            db1.SaveChanges();
            //db1.SaveChanges();


        }
        public void update_time(time f, int ID)
        {
            db db2 = new db();
            foreach (var item in db2.times)
            {
                if (item.food_id == ID)
                {
                    item.saturday = f.saturday;
                    item.sunday = f.sunday;
                    item.monday = f.monday;
                    item.tuesday = f.tuesday;
                    item.wednesday = f.wednesday;
                    item.thursday = f.thursday;
                    item.friday = f.friday;

                }
            }
            db2.SaveChanges();
        }
        public List<food> filter(string ftr)
        {


            List<int> C_F_F = new List<int>();
            List<food> food_filter = new List<food>();
            if (ftr == "شنبه")
            {
                foreach (var item in db1.times)
                {
                    if (item.saturday == 1)
                    {
                        C_F_F.Add(item.food_id);
                    }
                }
            }
            if (ftr == "یک شنبه")
            {
                foreach (var item in db1.times)
                {
                    if (item.sunday == 1)
                    {
                        C_F_F.Add(item.food_id);
                    }
                }
            }
            if (ftr == "دو شنبه")
            {
                foreach (var item in db1.times)
                {
                    if (item.monday == 1)
                    {
                        C_F_F.Add(item.food_id);
                    }
                }
            }
            if (ftr == "سه شنبه")
            {
                foreach (var item in db1.times)
                {
                    if (item.tuesday == 1)
                    {
                        C_F_F.Add(item.food_id);
                    }
                }
            }
            if (ftr == "چهار شنبه")
            {
                foreach (var item in db1.times)
                {
                    if (item.wednesday == 1)
                    {
                        C_F_F.Add(item.food_id);
                    }
                }
            }
            if (ftr == "پنج شنبه")
            {
                foreach (var item in db1.times)
                {
                    if (item.thursday == 1)
                    {
                        C_F_F.Add(item.food_id);
                    }
                }
            }
            if (ftr == "جمعه")
            {
                foreach (var item in db1.times)
                {
                    if (item.friday == 1)
                    {
                        C_F_F.Add(item.food_id);
                    }
                }
            }
            if (ftr == "همه")
            {
                foreach (var item in db1.times)
                {

                    C_F_F.Add(item.food_id);

                }
            }
            //
            if (ftr == "شنبه")
            {
                foreach (var item in C_F_F)
                {
                    foreach (var item1 in db1.foods)
                    {

                        if (item1.id == item)
                        {
                            food_filter.Add(item1);
                        }
                    }
                }
            }
            if (ftr == "یک شنبه")
            {
                foreach (var item in C_F_F)
                {
                    foreach (var item1 in db1.foods)
                    {

                        if (item1.id == item)
                        {
                            food_filter.Add(item1);
                        }
                    }
                }
            }
            if (ftr == "دو شنبه")
            {
                foreach (var item in C_F_F)
                {
                    foreach (var item1 in db1.foods)
                    {

                        if (item1.id == item)
                        {
                            food_filter.Add(item1);
                        }
                    }
                }
            }
            if (ftr == "سه شنبه")
            {
                foreach (var item in C_F_F)
                {
                    foreach (var item1 in db1.foods)
                    {

                        if (item1.id == item)
                        {
                            food_filter.Add(item1);
                        }
                    }
                }
            }
            if (ftr == "چهار شنبه")
            {
                foreach (var item in C_F_F)
                {
                    foreach (var item1 in db1.foods)
                    {

                        if (item1.id == item)
                        {
                            food_filter.Add(item1);
                        }
                    }
                }
            }
            if (ftr == "پنج شنبه")
            {
                foreach (var item in C_F_F)
                {
                    foreach (var item1 in db1.foods)
                    {

                        if (item1.id == item)
                        {
                            food_filter.Add(item1);
                        }
                    }
                }
            }
            if (ftr == "جمعه")
            {
                foreach (var item in C_F_F)
                {
                    foreach (var item1 in db1.foods)
                    {

                        if (item1.id == item)
                        {
                            food_filter.Add(item1);
                        }
                    }
                }
            }
            if (ftr == "همه")
            {
                foreach (var item in C_F_F)
                {
                    foreach (var item1 in db1.foods)
                    {

                        if (item1.id == item)
                        {
                            food_filter.Add(item1);
                        }
                    }
                }
            }
            // //if (bbn == "شنبه")



            //var q1 = from i in db1.foods where i.id == 1 select i;

            return food_filter;

        }
        public List<food> search_food(string f)
        {
            List<food> search = new List<food>();
            foreach (var item in db1.foods)
            {
                if (item.name.Contains(f) || item.tarkib.Contains(f) || item.money.ToString().Contains(f))
                {
                    search.Add(item);
                }
            }
            return search;
        }
        public food food_search_text(string f)
        {
            foreach (var item in db1.foods)
            {
                if (item.name.Contains(f))
                {
                    return item;
                }
            }
            return null;

        }

        public void rejester_History(List<personel_History> ss)
        {
            foreach (var item in ss)
            {
                db1.personel_Histories.Add(item);
                db1.SaveChanges();
            }
        }
        public int Auto_eshterak()
        {
            int s = 1000;
            List<int> vs = new List<int>();
            foreach (var item in db1.personels)
            {
                vs.Add(item.eshterak);
            }

            var q = from i in vs where i >= 1000 orderby i ascending select i;
            foreach (var item in q)
            {
                if (item == s)
                {
                    s++;
                }
                else
                {
                    return s;
                }
            }
            return s;
        }//ساخت اشتراک

        public bool check_exist_eshterak(int s)
        {
            foreach (var item in db1.personels)
            {
                if (item.eshterak == s)
                {
                    return true;
                }

            }
            return false;
        }//برسی موجودیت اشتراک
        public void Register_Personel(personel f)
        {
            db1.personels.Add(f);
            db1.SaveChanges();
        }//ثبت کاربر
        public personel search_Personel_eshterak(int f)
        {
            db db2 = new db();
            foreach (var item in db2.personels)
            {
                if (item.eshterak == f)
                {
                    return item;
                }
            }
            return null;
        }//جست و جوی اشتراک
        public void Edit_Personel(personel f)
        {
            foreach (var item in db1.personels)
            {
                if (item.id == f.id)
                {
                    item.eshterak = f.eshterak;
                    item.name = f.name;
                    item.adres = f.adres;


                }
            }
            db1.SaveChanges();
        }//ویرایش کاربر
        public int ID_personel(int eshterak)
        {
            foreach (var item in db1.personels)
            {
                if (item.eshterak == eshterak)
                {
                    return item.id;
                }
            }

            return 0;
        }//پیدا کردن ایدی با اشتراک
        public List<History> histories(int cd)
        {
            List<History> cc = new List<History>();
            List<Int64> xx = new List<long>();



            var q1 = (from i in db1.personels where i.eshterak == cd select i.id).Single();



            foreach (var item in db1.personel_Histories)
            {
                if (item.personel_id == q1)
                {
                    if (b(item.time) == false)
                    {
                        xx.Add(item.time);
                    }

                }
            }
            bool b(Int64 c)
            {
                var q = from i in xx where i == c select i;
                if (q.Count() >= 1)
                {
                    return true;
                }
                return false;
            }

            Int64 T = 0;
            int M = 0;

            xx.Add(1);
            foreach (var item1 in xx.ToList())
            {
                foreach (var item in db1.personel_Histories)
                {
                    if (item.time == item1)
                    {
                        T = item.time;
                        if (item.time == T)
                        {

                            M = item.money_Final + M;
                        }

                    }
                    else
                    {
                        if (M != 0)
                        {
                            History g = new History();
                            g.Time = T;
                            g.money = M;
                            cc.Add(g);
                            M = 0;
                        }
                    }
                }
            }


            if (cc != null)
            {
                return cc;
            }
            else
            {
                History m = new History();
                m.money = 0;
                m.Time = 0;
                cc.Add(m);
                return cc;
            }


        }//تاریخچه اشتراک برای دیتا گرید 4
        public List<personel_History> _Histories(Int64 x)
        {
            return (from i in db1.personel_Histories where i.time == x select i).ToList();

        }//گرفتن سابقه غذا با تاریخ
        //
        public List<int> sell()
        {

            List<double> Z = new List<double>();
            foreach (var item in db1.personel_Histories)
            {
                if (b(item.time) == false)
                {
                    Z.Add(double.Parse(item.time.ToString().Substring(0, 8)));
                }
            }
            bool b(Int64 c)
            {
                var q = from i in Z where i == double.Parse(c.ToString().Substring(0, 8)) select i;
                if (q.Count() >= 1)
                {
                    return true;
                }
                return false;
            }

            List<int> w = new List<int>();
            int one = 0;
            int S = 0;

            foreach (var item in db1.personel_Histories)
            {
                foreach (var item1 in Z)
                {
                    if (Z.Count() == 1)
                    {
                        if (item1 == double.Parse(item.time.ToString().Substring(0, 8)))
                        {
                            one = item.money_Final + one;
                        }
                    }
                    else
                    {
                        if (double.Parse(item.time.ToString().Substring(0, 8)) == item1)
                        {
                            S = item.money_Final + S;
                        }
                        else
                        {
                            w.Add(S);
                            one = 0;
                        }
                    }
                }
            }

            if (one.ToString().Count() >= 2)
            {
                w.Add(one);
                return w;
            }
            return w;
        }
        public List<History> cdz()
        {

            List<History> sx = new List<History>();
            List<History> sx1 = new List<History>();
            List<int> tt = new List<int>();
            int r = 0;



            foreach (var item in db1.personel_Histories)
            {
                tt.Add(int.Parse(item.time.ToString().Substring(0, 8)));
            }

            foreach (var item in tt.Distinct().OrderByDescending(i => i).Take(730))
            {
                if (r == 0)
                {
                    tt.Clear();
                    r++;
                }
                tt.Add(item);
            }

            bool b(int c)
            {
                var q = from i in tt where i == int.Parse(c.ToString().Substring(0, 8)) select i;
                if (q.Count() >= 1)
                {
                    return true;
                }
                return false;
            }
            int ID = 0;
            foreach (var item in db1.personel_Histories)
            {
                if (b(int.Parse(item.time.ToString().Substring(0, 8))) == true)
                {
                    History history = new History();
                    history.money = item.money_Final;
                    history.Time = Int64.Parse(item.time.ToString().Substring(0, 8));
                    sx.Add(history);
                }


            }
            foreach (var item in tt)
            {
                foreach (var item1 in sx)
                {
                    if (int.Parse(item1.Time.ToString()) == item)
                    {
                        sx1.Add(item1);
                    }
                }
            }
            foreach (var item in sx1)
            {

                item.id = ID++;
            }

            int T = 0;
            int M = 0;
            int cou = sx1.Count();
            sx.Clear();
            foreach (var item in tt)
            {
                foreach (var item1 in sx1)
                {
                    if (item == int.Parse(item1.Time.ToString()))
                    {
                        if (item1.id == cou - 1)
                        {
                            T = int.Parse(item1.Time.ToString());
                            M = item1.money + M;

                            History history = new History();
                            history.money = M;
                            history.Time = Int64.Parse(T.ToString());
                            sx.Add(history);
                            M = 0;
                            T = 0;
                        }
                        else
                        {

                            T = int.Parse(item1.Time.ToString());
                            M = item1.money + M;
                        }


                    }
                    else
                    {
                        if (T != 0)
                        {
                            History history = new History();
                            history.money = M;
                            history.Time = Int64.Parse(T.ToString());
                            sx.Add(history);
                            M = 0;
                            T = 0;
                        }
                    }
                }
            }


            r = 0;
            if (sx.Count() != 0)
            {
                return sx;
            }
            else
            {
                return null;
            }
        }
        public bool Search_Username(string Username)
        {
            var q = from i in db1.Users where i.username == Username select i;
            if (q.Count() >= 1)
            {
                return true;
            }
            return false;
        }
        public User search_user_id(int id)
        {
            db db2 = new db();
            var q = from i in db2.Users where i.id == id select i;
            return q.Single();
        }
        public List<User> All_User()
        {
            return db1.Users.ToList();
        }
        public List<personel> All_Personels()
        {
            db db2 = new db();
            return db2.personels.ToList();
        }
        public List<vorod_khorog> All_vorod_khorog()
        {
            db db2 = new db();
            return db2.vorod_Khorogs.ToList();
        }
        public void Edite_User(User Edite)
        {
            db db2 = new db();
            foreach (var item in db2.Users)
            {
                if(item.id==Edite.id)
                {
                    item.name = Edite.name;
                    item.username = Edite.username;
                    item.password = Edite.password;
                    item.picture_User = Edite.picture_User;
                }
            }
            db2.SaveChanges();
        }
        public User search_user_Username(string Username)
        {
            db db2 = new db();
            var q = from i in db2.Users where i.username == Username select i;
            if(q.Count()==1)
            {
                return q.Single();
            }
            return null;
        }
        public void Register_vorod_khorog(vorod_khorog V_K)
        {
            db1.vorod_Khorogs.Add(V_K);
            db1.SaveChanges();
        }
        public vorod_khorog Search_vorod_Khorog(int id)
        {
            var q = from i in db1.vorod_Khorogs where i.id == id select i;
            return q.Single();
        }
        public void Edite_vorod_Khorog(vorod_khorog Edit)
        {
            db db2 = new db();
            foreach (var item in db2.vorod_Khorogs)
            {
                if(item.id==Edit.id)
                {
                    item.username = Edit.username;
                    item.vorod = Edit.vorod;
                    item.khoroj = Edit.khoroj;
                }
            }
            db2.SaveChanges();
        }
        public List<day_work> All_day_Work()
        {
            List<day_work> Histore_day_Works = new List<day_work>();
            db db2 = new db();
            foreach (var item in db2.vorod_Khorogs)
            {
                day_work d_w = new day_work();
                string work_V = item.vorod.ToString("##:##").Substring(8, 5);
                DateTime work_1 = DateTime.Parse(work_V);
                string work_KH = item.khoroj.ToString("##:##").Substring(8, 5);
                DateTime work_2 = DateTime.Parse(work_KH);
                TimeSpan final = work_2 - work_1;
                var final_filter=final.ToString().Substring(0,5);
                var remov = new string[] { "/", " ", "_", ":" };
                foreach (var item1 in remov) { final_filter = final_filter.Replace(item1, string.Empty); }
                d_w.work = int.Parse(final_filter);
                d_w.day = Int64.Parse(item.vorod.ToString().Substring(0, 8));
                ///////////////////////////////////////////////////////////////////////
                if(check(d_w.day)==true)
                {
                    foreach (var item2 in Histore_day_Works)
                    {
                        if(item2.day==d_w.day)
                        {
                            item2.work = item2.work + d_w.work;
                        }
                        
                    }
                }
                else if (check(d_w.day) == false)
                {
                    Histore_day_Works.Add(d_w);
                }

            }
            bool check(Int64 day)
            {
                var q = from i in Histore_day_Works where i.day == day select i;
                if(q.Count()==1)
                {
                    return true;
                }
                return false;
            }
            return Histore_day_Works;
        }
        public void Delete_Vorod_Khorog(int id)
        {
            db db2 = new db();
            foreach (var item in db2.vorod_Khorogs)
            {
                if (item.id == id) { db2.vorod_Khorogs.Remove(item); }
            }
            db2.SaveChanges();
        }
        public void Delete_food(int id)
        {
            db db2 = new db();
            foreach (var item in db2.foods)
            {
                if (item.id == id) { db2.foods.Remove(item); }
            }
            db2.SaveChanges();
        }
        public void Delete_personels(int id)
        {
            db db2 = new db();
            foreach (var item in db2.personels)
            {
                if (item.id == id) { db2.personels.Remove(item); }
            }
            db2.SaveChanges();
        }
        public void Delete_user(int id)
        {
            db db2 = new db();
            foreach (var item in db2.Users)
            {
                if (item.id == id) { db2.Users.Remove(item); }
            }
            db2.SaveChanges();
        }
        public void Delete_All_Vorod_Khorog()
        {
            db db2 = new db();
            foreach (var item in db2.vorod_Khorogs)
            {
                 db2.vorod_Khorogs.Remove(item); 
            }
            db2.SaveChanges();
        }
        public void Delete_All_food()
        {
            db db2 = new db();
            foreach (var item in db2.foods)
            {
                 db2.foods.Remove(item); 
            }
            db2.SaveChanges();
        }
        public void Delete_All_personels()
        {
            db db2 = new db();
            foreach (var item in db2.personels)
            {
                db2.personels.Remove(item); 
            }
            db2.SaveChanges();
        }
        public void Delete_All_user()
        {
            db db2 = new db();
            foreach (var item in db2.Users)
            {
                 db2.Users.Remove(item); 
            }
            db2.SaveChanges();
        }
        public List<User> search_User(string text)
        {
            db db2 = new db();
            List<User> search = new List<User>();
            foreach (var item in db1.Users)
            {
                if (item.username.Contains(text) || item.name.Contains(text) )
                {
                    search.Add(item);
                }
            }
            return search;
        }
        public List<personel> search_personel(string text)
        {
            db db2 = new db();
            List<personel> search = new List<personel>();
            foreach (var item in db1.personels)
            {
                if (item.name.Contains(text) || item.adres.Contains(text) || item.eshterak.ToString().Contains((text)))
                {
                    search.Add(item);
                }
            }
            return search;
        }
        public List<vorod_khorog> search_Vorod_Khorog(string text)
        {
            db db2 = new db();
            List<vorod_khorog> search = new List<vorod_khorog>();
            foreach (var item in db1.vorod_Khorogs)
            {
                if (item.username.Contains(text) || item.vorod.ToString().Contains(text) || item.khoroj.ToString().Contains(text))
                {
                    search.Add(item);
                }
            }
            return search;
        }
        public List<food> day_food(byte day)
        {
            db db2 = new db();
            List<int> Time_Food_Filter = new List<int>();
            List<food> FOOD = new List<food>();
           
            if (day == 1)
            {
                foreach (var item in db2.times)
                {
                    if (item.saturday == 1 & item.sunday == 0 & item.monday == 0 & item.thursday == 0 & item.wednesday == 0 & item.thursday == 0 & item.friday == 0)
                    {
                        Time_Food_Filter.Add(item.food_id);
                    }
                }
                foreach (var item in db2.foods)
                {
                    foreach (var item1 in Time_Food_Filter)
                    {
                        if (CHECK(item.id) == false)
                        {
                            if (item.id == item1)
                            {
                                FOOD.Add(item);
                            }
                        }
                    }
                }
            }
            if (day == 2)
            {
                foreach (var item in db2.times)
                {
                    if (item.saturday == 0 & item.sunday == 1 & item.monday == 0 & item.thursday == 0 & item.wednesday == 0 & item.thursday == 0 & item.friday == 0)
                    {
                        Time_Food_Filter.Add(item.food_id);
                    }
                }
                foreach (var item in db2.foods)
                {
                    foreach (var item1 in Time_Food_Filter)
                    {
                        if (CHECK(item.id) == false)
                        {
                            if (item.id == item1)
                            {
                                FOOD.Add(item);
                            }
                        }
                    }
                }
            }
            if (day == 3)
            {
                foreach (var item in db2.times)
                {
                    if (item.saturday == 0 & item.sunday == 0 & item.monday == 1 & item.thursday == 0 & item.wednesday == 0 & item.thursday == 0 & item.friday == 0)
                    {
                        Time_Food_Filter.Add(item.food_id);
                    }
                }
                foreach (var item in db2.foods)
                {
                    foreach (var item1 in Time_Food_Filter)
                    {
                        if (CHECK(item.id) == false)
                        {
                            if (item.id == item1)
                            {
                                FOOD.Add(item);
                            }
                        }
                    }
                }
            }
            if (day == 4)
            {
                foreach (var item in db2.times)
                {
                    if (item.saturday == 0 & item.sunday == 0 & item.monday == 0 & item.thursday == 1 & item.wednesday == 0 & item.thursday == 0 & item.friday == 0)
                    {
                        Time_Food_Filter.Add(item.food_id);
                    }
                }
                foreach (var item in db2.foods)
                {
                    foreach (var item1 in Time_Food_Filter)
                    {
                        if (CHECK(item.id) == false)
                        {
                            if (item.id == item1)
                            {
                                FOOD.Add(item);
                            }
                        }
                    }
                }
            }
            if (day == 5)
            {
                foreach (var item in db2.times)
                {
                    if (item.saturday == 0 & item.sunday == 0 & item.monday == 0 & item.thursday == 0 & item.wednesday == 1 & item.thursday == 0 & item.friday == 0)
                    {
                        Time_Food_Filter.Add(item.food_id);
                    }
                }
                foreach (var item in db2.foods)
                {
                    foreach (var item1 in Time_Food_Filter)
                    {
                        if (CHECK(item.id) == false)
                        {
                            if (item.id == item1)
                            {
                                FOOD.Add(item);
                            }
                        }
                    }
                }
            }
            if (day == 6)
            {
                foreach (var item in db2.times)
                {
                    if (item.saturday == 0 & item.sunday == 0 & item.monday == 0 & item.thursday == 0 & item.wednesday == 0 & item.thursday == 1 & item.friday == 0)
                    {
                        Time_Food_Filter.Add(item.food_id);
                    }
                }
                foreach (var item in db2.foods)
                {
                    foreach (var item1 in Time_Food_Filter)
                    {
                        if (CHECK(item.id) == false)
                        {
                            if (item.id == item1)
                            {
                                FOOD.Add(item);
                            }
                        }
                    }
                }
            }
            if (day==7)
            {
                foreach (var item in db2.times)
                {
                    if (item.saturday == 0 & item.sunday == 0 & item.monday == 0 & item.thursday == 0 & item.wednesday == 0 & item.thursday == 0 & item.friday == 1)
                    {
                        Time_Food_Filter.Add(item.food_id);
                    }  
                }
                foreach (var item in db2.foods)
                {
                    foreach (var item1 in Time_Food_Filter)
                    {
                        if (CHECK(item.id) == false)
                        {
                            if (item.id == item1)
                            {
                                FOOD.Add(item);
                            }
                        }
                    }
                }
            }
            if(day==8)
            {
                foreach (var item in db2.times)
                {
                    if(item.saturday==1& item.sunday == 1 & item.monday == 1 & item.thursday == 1 & item.wednesday == 1 & item.thursday == 1 & item.friday == 1)
                    {
                        Time_Food_Filter.Add(item.food_id);
                    }
                }
                foreach (var item in db2.foods)
                {
                    foreach (var item1 in Time_Food_Filter)
                    {
                        if(CHECK(item.id)==false)
                        {
                            if(item.id==item1)
                            {
                                FOOD.Add(item);
                            }
                        }
                    }
                }
            }    
            if (day==9)
            {
                return db2.foods.ToList();
            }
            bool CHECK(int id)
            {
                var q = from i in FOOD where i.id == id select i;
                if (q.Count() == 1) { return true; }
                return false;
            }
            return FOOD;
        }
        public food  search_food_id(int id)
        {
            db db2 = new db();
            return  (from i in db2.foods where i.id == id select i).Single();
        }
        public List<string> send_name_food()
        {
            db db2 = new db();
            List<string> food = new List<string>();
            foreach (var item in db2.foods)
            {
                food.Add(item.name);
            }
            return food;
        }
        public bool check_manager(string username)
        {
            db db2 = new db();
            var q = from i in db2.Users where i.username == username select i.manager;
            if(q.Any()==true)
            {
                return true;
            }
            return false;
        }
        public string day_money(Int64 time)
        {
           
            db db2 = new db();
            List<int> money = new List<int>();
            int money_day_now = 0;
            foreach (var item in db2.personel_Histories)
            {
                if(item.time==time)
                {
                    money.Add(item.money_Final);
                }
            }
            foreach (var item in money)
            {
                money_day_now += item;
            }
            return money_day_now.ToString();
        }
        public int user_work(string username,Int64 time_now)
        {
            db db2 = new db();
            int work = 0;
            foreach (var item in db2.vorod_Khorogs)
            {
                if(item.username==username & item.vorod.ToString().Substring(0,7)==time_now.ToString().Substring(0,7))
                {
                    DateTime V = DateTime.Parse(item.vorod.ToString("##:##").Substring(8,5));
                    DateTime KH = DateTime.Parse(item.khoroj.ToString("##:##").Substring(8,5));
                    TimeSpan  work_user=  KH - V;
                    var final_filter = work_user.ToString().Substring(0, 5);
                    var remov = new string[] { "/", " ", "_", ":" };
                    foreach (var item1 in remov) { final_filter = final_filter.Replace(item1, string.Empty); }
                    work += int.Parse(final_filter.ToString());
                }
            }
            return work;
        }
        public string name_manager()
        {
            var q = from i in db1.Users where i.manager == true select i.name;

            return q.Single();
        }
        public User Search_user_Username(string username)
        {
            var q = from i in db1.Users where i.username == username select i;
            return q.Single();
        }
    }

}
