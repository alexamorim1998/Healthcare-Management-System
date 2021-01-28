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
    public class Specializations
    {
        private readonly string _connectionString;
        public Specializations(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Specialization> GetAll()
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            IEnumerable<Specialization> list = cn.GetAll<Specialization>();
            return list;
        }

        public Specialization Get(int id)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            return cn.Get<Specialization>(id);
        }

        public long Add(Specialization doctor)
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
            bool success = cn.Execute("DELETE FROM [Specialization] WHERE idSpecialization = @Id", new { Id = id }) != 0; ;
            return success;
        }

        public bool Delete(Specialization specialization)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Delete<Specialization>(specialization);
            return success;
        }

        public bool Update(Specialization doctor)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Update<Specialization>(doctor);
            return success;
        }
    }
}
