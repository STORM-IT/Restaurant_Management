using BE_KFC;
using DA_KFC;
using System;
using System.Collections.Generic;

namespace BL_KFC
{
    public class BL
    {
        db db1 = new db();
        da_kfc d = new da_kfc();
        public void User_rejster(User u)
        {
            d.Login(u);
        }
        public void fod(food v)
        {
            d.fod(v);
        }
        public List<food> ss()
        {

            return d.ss();
        }
        public bool vorod(User v)
        {

            return d.vorod(v);


        }
        public void regster(food f)
        {
            d.regster(f);
        }
        public void RG_Time(time f)
        {
            d.RG_Time(f);
        }
        public food readid(int f)
        {
            return d.readID(f);
        }
        public List<string> roz(int id)
        {
            return d.roz(id);
        }

        public void update(food f, int ID)
        {
            d.update(f, ID);
        }
        public void update_time(time f, int ID)
        {
            d.update_time(f, ID);
        }
        public List<food> filter(string ftr)
        {
            return d.filter(ftr);
        }
        public List<food> food_search(string f)
        {
            return d.search_food(f);
        }
        public food food_search_text(string f)
        {
            return d.food_search_text(f);
        }
        public void rejester_History(List<personel_History> ss)
        {
            d.rejester_History(ss);
        }
        public int Auto_eshterak()
        {
            return d.Auto_eshterak();
        }
        public bool check_exist_eshterak(int f)
        {
            return d.check_exist_eshterak(f);
        }
        public void Register_Personel(personel f)
        {
            d.Register_Personel(f);
        }
        public personel search_Personel_eshterak(int f)
        {
            return d.search_Personel_eshterak(f);
        }
        public void Edit_Personel(personel f)
        {
            d.Edit_Personel(f);
        }
        public int ID_personel(int eshterak)
        {
            return d.ID_personel(eshterak);
        }

        public List<History> histories(int cd)
        {
            return d.histories(cd);
        }
        public List<personel_History> _Histories(Int64 x)//گرفتن سابقه غذا با تاریخ
        {
            return d._Histories(x);
        }
        //
        public List<int> sell()
        {
            return d.sell();
        }
        public List<History> cdz()
        {
            return d.cdz();
        }
        public bool Search_Username(string username)
        {
            return d.Search_Username(username);
        }
        public List<User> All_User()
        {
            return d.All_User();
        }
        public List<personel> All_Personels()
        {
            return d.All_Personels();
        }
        public List<vorod_khorog> All_vorod_khorog()
        {
            return d.All_vorod_khorog();
        }
        public User search_user_id(int id)
        {
            return d.search_user_id(id);
        }
        public void Edite_User(User Edite)
        {
            d.Edite_User(Edite);
        }
        public User search_user_Username(string Username)
        {
            return d.search_user_Username(Username);
        }
        public void Register_vorod_khorog(vorod_khorog V_K)
        {
            d.Register_vorod_khorog(V_K);
        }
        public vorod_khorog Search_vorod_Khorog(int id)
        {
            return d.Search_vorod_Khorog(id);
        }
        public void Edite_vorod_Khorog(vorod_khorog Edit)
        {
            d.Edite_vorod_Khorog(Edit);
        }
        public List<day_work> All_day_Work()
        {
            return d.All_day_Work();
        }
        public void Delete_Vorod_Khorog(int id)
        {
            d.Delete_Vorod_Khorog(id);
        }
        public void Delete_food(int id)
        {
            d.Delete_food(id);
        }
        public void Delete_personels(int id)
        {
            d.Delete_personels(id);
        }
        public void Delete_user(int id)
        {
            d.Delete_user(id);
        }
        public void Delete_All_Vorod_Khorog()
        {
            d.Delete_All_Vorod_Khorog();
        }
        public void Delete_All_food() 
        { 
            d.Delete_All_food();
        }
        public void Delete_All_personels()
        {
            d.Delete_All_personels();
        }
        public void Delete_All_user() 
        {
            d.Delete_All_user();
        }
        public List<vorod_khorog> search_Vorod_Khorog(string text)
        {
            return d.search_Vorod_Khorog(text);
        }
        public List<personel> search_personel(string text)
        {
            return d.search_personel(text);
        }
        public List<User> search_User(string text)
        {
            return d.search_User(text);
        }
        public List<food> day_food(byte day)
        {
            return d.day_food(day);
        }
        public food search_food_id(int id)
        {
            return d.search_food_id(id);
        }
        public List<string> send_name_food()
        {
            return d.send_name_food();
        }
        public bool check_manager(string username)
        {
            return d.check_manager(username);

        }
        public string day_money(Int64 time)
        {
            return d.day_money(time);

        }
        public int user_work(string username, Int64 time_now)
        {
            return d.user_work(username, time_now);
        }
        public string name_manager()
        {
            return d.name_manager();
        }
        public User Search_user_Username(string username)
        {
            return d.search_user_Username(username);
        }
    }
}
