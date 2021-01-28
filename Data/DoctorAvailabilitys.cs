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
    public class DoctorAvailabilitys
    {
        private readonly string _connectionString;
        public DoctorAvailabilitys(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<DoctorAvailability> GetAll()
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            IEnumerable<DoctorAvailability> list = cn.GetAll<DoctorAvailability>();
            return list;
        }

        public DoctorAvailability Get(long id)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            return cn.Get<DoctorAvailability>(id);
        }

        public long Add(DoctorAvailability availability)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            long id = cn.Insert(availability);
            return id;
        }

        public bool Delete(long id)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Execute("DELETE FROM [DoctorAvailability] WHERE id = @Id", new { Id = id }) != 0; ;
            return success;
        }

        public bool Delete(DoctorAvailability doctorAvailability)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Delete<DoctorAvailability>(doctorAvailability);
            return success;
        }

        public bool Update(DoctorAvailability doctorAvailability)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Update<DoctorAvailability>(doctorAvailability);
            return success;
        }

        
    }
}
