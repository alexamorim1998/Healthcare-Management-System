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
    public class Administrators
    {
        private readonly string _connectionString;
        public Administrators(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Administrator> GetAll()
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            IEnumerable<Administrator> list = cn.GetAll<Administrator>();
            return list;
        }

        public Administrator Get(int id)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            return cn.Get<Administrator>(id);
        }

        public long Add(Administrator administrator)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            //vamos verificar se este doctor já existe com userapplication
            string query = $"select * from UserApplication where username='{administrator.Username}'";
            IEnumerable<UserApplication> list = cn.Query<UserApplication>(query);
            if (list.Any())
            {
                long id = cn.Insert(administrator);
                return id;
            }
            else
            {
                UserApplication userApplication = new UserApplication()
                {
                    Username = administrator.Username,
                    Role = UserApplication.Administrator,
                    Password = UserApplication.DefaultPassword
                };
                long id = cn.Insert(userApplication);
                id = cn.Insert(administrator);
                return id;
            }
        }

        //public bool Delete(long id)
        //{
        //    using SqlConnection cn = new SqlConnection(_connectionString);
        //    cn.Open();
        //    bool success = cn.Execute("DELETE FROM [Doctor] WHERE idDoctor = @Id", new { Id = id }) != 0; ;
        //    return success;
        //}

        public bool Delete(Administrator administrator)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Delete<Administrator>(administrator);
            return success;
        }

        public bool Update(Administrator administrator)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Update<Administrator>(administrator);
            return success;
        }
    }
}
