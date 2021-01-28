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
    public class Patients
    {
        private readonly string _connectionString;
        public Patients(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Patient> GetAll()
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            IEnumerable<Patient> list = cn.GetAll<Patient>();
            return list;
        }

        public IEnumerable<Patient> GetAll(string nameToFind)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            //vamos verificar se este doctor já existe com userapplication
            string query = " SELECT  * " +
                            "FROM [dbo].[Patient] " +
                            $" where LOWER([FirstName])  Like LOWER('%{nameToFind}%') or LOWER([LastName])  Like LOWER('%{nameToFind}%')";
            IEnumerable<Patient> list = cn.Query<Patient>(query);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idSpecialization"></param>
        /// <returns></returns>
        public IEnumerable<Doctor> GetAllByDoctor(long idDoctor)
        {
            return null;
        }

        public Patient Get(long id)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            return cn.Get<Patient>(id);
        }

        public Patient Get(string username)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            //vamos verificar se este doctor já existe com userapplication
            string query = " SELECT Top 1 * " +
                            "FROM [dbo].[Patient] " +
                            $" where Username = '{username}'";
            IEnumerable<Patient> list = cn.Query<Patient>(query);
            return list.FirstOrDefault();
        }

        public Patient GetUsingTaxNumber(int taxNumber)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            //vamos verificar se este doctor já existe com userapplication
            string query = " SELECT Top 1 * " +
                            "FROM [dbo].[Patient] " +
                            $" where TaxNumber = {taxNumber}";
            IEnumerable<Patient> list = cn.Query<Patient>(query);
            return list.FirstOrDefault();
        }

        public long Add(Patient patient)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            //vamos verificar se este doctor já existe com userapplication
            string query = $"select * from UserApplication where username='{patient.Username}'";
            IEnumerable<UserApplication> list = cn.Query<UserApplication>(query);
            if (list.Any())
            {
                long id = cn.Insert(patient);
                return id;
            }
            else
            {
                UserApplication userApplication = new UserApplication()
                {
                    Username = patient.Username,
                    Role = UserApplication.Patient,
                    Password = UserApplication.DefaultPassword
                };
                long id = cn.Insert(userApplication);
                id = cn.Insert(patient);
                return id;
            }
        }

        public bool Delete(Patient patient)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Delete<Patient>(patient);
            return success;
        }

        public bool Delete(string username)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Execute("DELETE FROM [Patient] WHERE username = @Id", new { Id = username }) != 0;
            if (!success)
                return false;
            success = cn.Execute("DELETE FROM [UserApplication] WHERE username = @Id", new { Id = username }) != 0; ;
            return success;
        }

        public bool Update(Patient patient)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Update<Patient>(patient);
            return success;
        }
    }
}
