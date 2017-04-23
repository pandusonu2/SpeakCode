using SQLAccess.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SQLAccess
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string strconn = "Server=tcp:imagine13.database.windows.net,1433;Initial Catalog=users;" +
                "Persist Security Info=False;User ID=speakcode;Password=Imagine@13;" +
                "MultipleActiveResultSets=False;Encrypt=True;" +
                "TrustServerCertificate=False;Connection Timeout=30;";
        public Boolean AddLang(UserLang l)
        {
            int newRows = 0;
            using(SqlConnection sqlcon = new SqlConnection(strconn))
            {
                sqlcon.Open();

                SqlCommand createCommand = new SqlCommand("", sqlcon)
                {
                    CommandType = CommandType.Text,
                    CommandText = $"insert into userlang values('{l.user}','{l.lang}','{l.integer}','{l.strings}','{l.ifelse}','{l.looper}','{l.printer}','{l.input}','{l.breaker}','{l.continuex}');"
                };
                newRows = createCommand.ExecuteNonQuery();
            }
            if (newRows != 1)
                return false;
            else return true;
        }

        public Boolean UpdProgress(User u)
        {
            int newRows = 0;
            using(SqlConnection sqlcon = new SqlConnection(strconn))
            {
                sqlcon.Open();

                SqlCommand updateCommand = new SqlCommand("", sqlcon)
                {
                    CommandType = CommandType.Text,

                    CommandText = $"update usertable set sol = {u.sol}, acc = {u.acc}, ques = '{u.ques}' where \"username\" = '{u.user}';"
                };
                newRows = updateCommand.ExecuteNonQuery();
            }
            if (newRows != 1)
                return false;
            else return true;
        }

        public Boolean AddUser(string user, string pass)
        {
            int newRows = 0;
            using(SqlConnection sqlcon = new SqlConnection(strconn))
            {
                sqlcon.Open();

                SqlCommand insertCommand = new SqlCommand("", sqlcon)
                {
                    CommandType = CommandType.Text,

                    CommandText = $"insert into usertable values ('{user}',  0, 0, '0000000000', '{pass}');"
                };
                newRows = insertCommand.ExecuteNonQuery();
            }
            if (newRows != 1)
                return false;
            else return true;
        }

        public IList<User> QueryUser()
        {
            List<User> userlist = new List<User>();
            using (SqlConnection sqlcon = new SqlConnection(strconn))
            {
                sqlcon.Open();
                using (SqlCommand sqlcmd = new SqlCommand("select * from usertable", sqlcon))
                {
                    SqlDataReader read = sqlcmd.ExecuteReader();
                    while (read.Read())
                    {
                        string user = read.GetString(0);
                        int sol = read.GetInt32(1);
                        int acc = read.GetInt32(2);
                        string ques = read.GetString(3);
                        string password = read.GetString(4);
                        userlist.Add(new User()
                        {
                            user = user,
                            sol = sol,
                            acc = acc,
                            ques = ques,
                            password = password
                        });

                    }
                }
            }
            return userlist;
        }

        public IList<UserLang> GetLangs(string user)
        {
            List<UserLang> langs = new List<UserLang>();
            using (SqlConnection sqlcon = new SqlConnection(strconn))
            {
                sqlcon.Open();
                using (SqlCommand sqlcmd = new SqlCommand("select * from userlang where \"user\" = '" + user +"'", sqlcon))
                {
                    SqlDataReader read = sqlcmd.ExecuteReader();
                    while (read.Read())
                    {
                        langs.Add(new UserLang()
                        {
                            user = read.GetString(0),
                            lang = read.GetString(1),
                            integer = read.GetString(2),
                            strings = read.GetString(3),
                            ifelse = read.GetString(4),
                            looper = read.GetString(5),
                            printer = read.GetString(6),
                            input = read.GetString(7),
                            breaker = read.GetString(8),
                            continuex = read.GetString(9)
                        });
                    }
                }
            }
            return langs;
        }
    }
}
