using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BE_KFC
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string picture_User { get; set; }
        public bool manager { get; set; }
    }
    public class food
    {
        public int id { get; set; }
        public string name { get; set; }
        public string tarkib { get; set; }
        public int money { get; set; }
        public string picture { get; set; }
        

    }
    public class time
    {
        public int id { get; set; }
        public int food_id { get; set; }
        public int saturday { get; set; }
        public int sunday { get; set; }
        public int monday { get; set; }
        public int tuesday { get; set; }
        public int wednesday { get; set; }
        public int thursday { get; set; }
        public int friday { get; set; }

    }
    public class personel
    {
        public int id { get; set; }
        public int eshterak { get; set; }
        public string name { get; set; }
        public string adres { get; set; }
    }
    public class personel_History
    {
        public int id { get; set; }
        public int personel_id { get; set; }
        public string name_food { get; set; }
        public int money { get; set; }
        public int tedad { get; set; }
        public int money_Final { get; set; }
        public Int64 time { get; set; }
    }
    public class History
    {
        public int id { get; set; }
        public Int64 Time { get; set; }
        public int money { get; set; }
    }
    public class vorod_khorog
    {
        public int id { get; set; }
        public string username { get; set; }
        public Int64 vorod { get; set; }
        public Int64 khoroj { get; set; }
    }
    public class day_work
    {
        public Int64 day { get; set; }
        public int work { get; set; }
    }
}
