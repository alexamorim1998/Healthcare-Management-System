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
    public class Slots
    {
        private readonly string _connectionString;
        public Slots(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Slot> GetAll()
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            IEnumerable<Slot> list = cn.GetAll<Slot>();
            return list;
        }

        public Slot Get(int id)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            return cn.Get<Slot>(id);
        }

        public long Add(Slot doctor)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            long id = cn.Insert(doctor);
            return id;
        }

        public bool Delete(long id)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Execute("DELETE FROM [Slots] WHERE id = @Id", new { Id = id }) != 0; ;
            return success;
        }

        public bool Delete(Slot slot)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Delete<Slot>(slot);
            return success;
        }

        public bool Update(Slot slot)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Update<Slot>(slot);
            return success;
        }
    }
}
