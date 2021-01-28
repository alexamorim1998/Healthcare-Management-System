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
    public class WeekDays
    {
        private readonly string _connectionString;
        public WeekDays(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<WeekDayClass> GetAll()

        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            IEnumerable<WeekDayClass> list = cn.GetAll<WeekDayClass>();
            return list;
        }

        public WeekDayClass Get(int id)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            return cn.Get<WeekDayClass>(id);
        }

        public long Add(WeekDayClass week)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            long id = cn.Insert(week);
            return id;
        }

        public bool Delete(long id)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Execute("DELETE FROM [WeekDay] WHERE id = @Id", new { Id = id }) != 0; ;
            return success;
        }

        public bool Delete(WeekDayClass week)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Delete<WeekDayClass>(week);
            return success;
        }

        public bool Update(WeekDayClass week)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Update<WeekDayClass>(week);
            return success;
        }
    }
}
