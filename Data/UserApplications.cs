using Dapper;
using Dapper.Contrib.Extensions;
using RINTE_Care.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RINTE_Care.Data
{
    public class UserApplications
    {
        private readonly string _connectionString;
        public UserApplications(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Get user if exist in database.
        /// If not exist return default value
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// 

        public IEnumerable<UserApplication> GetAll()
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            IEnumerable<UserApplication> list = cn.GetAll<UserApplication>();
            return list;
        }

        public UserApplication Get(int id)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            return cn.Get<UserApplication>(id);
        }

        public UserApplication Get(string username)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            string query =
                $"select TOP (1) * from UserApplication " +
                $"where username='{username}' and Active=1 ";
            IEnumerable<UserApplication> list = cn.Query<UserApplication>(query);
            if (list.Any())
            {
                return list.First();
            }
            else
            {
                return default;
            }
        }

        public UserApplication GetUser(UserCredential user)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            string query =
                $"select TOP (1) * from UserApplication " +
                $"where username='{user.Username}' and password='{user.Password}' and Active=1";
            IEnumerable<UserApplication> list = cn.Query<UserApplication>(query);
            if (list.Any())
            {
                return list.First();
            }
            else
            {
                return default;
            }
        }

        public long Add(UserApplication userApplication)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            long id = cn.Insert(userApplication);
            return id;
        }

        public bool Delete(UserApplication userApplication)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Delete<UserApplication>(userApplication);
            return success;
        }

        public bool Delete(int id)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Execute("DELETE FROM [UserApplication] WHERE id = @Id", new { Id = id }) != 0; ;
            return success;
        }

        public bool Update(UserApplication userApplication)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Update<UserApplication>(userApplication);
            return success;
        }

        internal string GetNewPassword()
        {
            return Guid.NewGuid().ToString().Substring(0, 5); 
        }
    }
}
