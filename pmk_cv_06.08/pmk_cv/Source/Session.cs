using System.Collections.Generic;

namespace HRCloud.Source
{
    public struct UserSessData
    {
        public int id;
        public string username;
        public string name;
        public string email;
        public int kategoria;
        public int jogosultsag;
        public int validitas;
        public string belepve;
        public string reg_datum;
    }
    class Session
    {
        private static List<UserSessData> UserDatas;
        public List<UserSessData> UserData { get { return UserDatas; } set { UserDatas = value; } }

        private static string Tartomanyi;
        public string tartomanyi { get { return Tartomanyi; } set { Tartomanyi = value; } }
    }
}
