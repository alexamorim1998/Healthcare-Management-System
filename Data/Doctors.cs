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
    public class Doctors
    {
        private readonly string _connectionString;
        public Doctors(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Doctor> GetAll()
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            IEnumerable<Doctor> list = cn.GetAll<Doctor>();
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idSpecialization"></param>
        /// <returns></returns>
        public IEnumerable<Doctor> GetAllBySpeciality(long idSpecialization)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            string query = $"select * from Doctor where idSpecialization={idSpecialization}";
            IEnumerable<Doctor> list = cn.Query<Doctor>(query);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of doctor</param>
        /// <returns></returns>
        internal IEnumerable<DoctorAvailabilityView> GetAvailabilityOfDoctor(int id)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            string query = 
                "SELECT [id],[DoctorAvailability].[idDoctor],[Doctor].FirstName + ' ' + [Doctor].LastName as DoctorName" +
                        ",[DoctorAvailability].[idWeekDay],[WeekDay].Name as  'WeekDay'"+
                        ",[DoctorAvailability].[idSlot],CONCAT([Slots].StartHour, '-', [Slots].EndHour) as SlotInformation"+
                        ",[DayStart],[DayEnd],[active] "+
            " FROM " +
                        " [DoctorAvailability],[WeekDay],[Slots],[Doctor] "+
             "where " +
                    "[DoctorAvailability].idDoctor =[dbo].[Doctor].idDoctor AND "+
                    "[DoctorAvailability].idSlot =[dbo].[Slots].idSlot AND "+
                    "[DoctorAvailability].idWeekDay =[dbo].[WeekDay].idWeekDay AND "+
                    $"[DoctorAvailability].idDoctor = {id}";
            IEnumerable<DoctorAvailabilityView> list = cn.Query<DoctorAvailabilityView>(query);
            return list;
        }




        /// <summary>
        /// Partimos da hipótese que para um determinado dia da semana
        /// não existem mais availibities activas para um medico
        /// num determinado periodo
        /// </summary>
        /// <param name="idDoctor"></param>
        /// <param name="idWeekDay"></param>
        /// <returns></returns>
        internal IEnumerable<DoctorAvailabilityView> GetAllFutureAvailabilities(
            long idDoctor, long idWeekDay)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            string query =
                "SELECT [id],[DoctorAvailability].[idDoctor],[Doctor].FirstName + ' ' + [Doctor].LastName as DoctorName" +
                        ",[DoctorAvailability].[idWeekDay],[WeekDay].Name as  'WeekDay'" +
                        ",[DoctorAvailability].[idSlot],CONCAT([Slots].StartHour, '-', [Slots].EndHour) as SlotInformation" +
                        ",[DayStart],[DayEnd],[active] " +
            " FROM " +
                        " [DoctorAvailability],[WeekDay],[Slots],[Doctor] " +
            " where " +
                    "[dbo].[DoctorAvailability].active= 'true' and " +
                    "GETDATE() BETWEEN[dbo].[DoctorAvailability].DayStart and[dbo].[DoctorAvailability].DayEnd and " +
                    "[DoctorAvailability].idDoctor =[dbo].[Doctor].idDoctor AND " +
                    "[DoctorAvailability].idSlot =[dbo].[Slots].idSlot AND " +
                    "[DoctorAvailability].idWeekDay =[dbo].[WeekDay].idWeekDay AND " +
                    $"[DoctorAvailability].idDoctor = {idDoctor} and" +
                    $"[DoctorAvailability].idWeekDay={idWeekDay}";
            IEnumerable<DoctorAvailabilityView> list = cn.Query<DoctorAvailabilityView>(query);
            return list;
        }

        internal IEnumerable<DoctorAvailabilityView> GetAllFutureAvailabilities(
            string usernameDoctor, long idWeekDay)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            string query =
                "SELECT [id],[DoctorAvailability].[idDoctor],[Doctor].FirstName + ' ' + [Doctor].LastName as DoctorName" +
                        ",[DoctorAvailability].[idWeekDay],[WeekDay].Name as  'WeekDay'" +
                        ",[DoctorAvailability].[idSlot],CONCAT([Slots].StartHour, '-', [Slots].EndHour) as SlotInformation" +
                        ",[DayStart],[DayEnd],[active] " +
            " FROM " +
                        " [DoctorAvailability],[WeekDay],[Slots],[Doctor] " +
            " where " +
                    "[dbo].[DoctorAvailability].active= 'true' and " +
                    "GETDATE() BETWEEN[dbo].[DoctorAvailability].DayStart and[dbo].[DoctorAvailability].DayEnd and " +
                    "[DoctorAvailability].idDoctor =[dbo].[Doctor].idDoctor AND " +
                    "[DoctorAvailability].idSlot =[dbo].[Slots].idSlot AND " +
                    "[DoctorAvailability].idWeekDay =[dbo].[WeekDay].idWeekDay AND " +
                    $"[Doctor].username = '{usernameDoctor}' and" +
                    $"[DoctorAvailability].idWeekDay={idWeekDay}";
            IEnumerable<DoctorAvailabilityView> list = cn.Query<DoctorAvailabilityView>(query);
            return list;
        }



        /// <summary>
        /// Devolve os weekdays possiveis para este médico
        /// </summary>
        /// <param name="idDoctor"></param>
        /// <returns></returns>
        internal IEnumerable<WeekDayClass> GetAllWeekDays(long idDoctor)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            string query =
                "SELECT distinct [dbo].[WeekDay].* " +
                "FROM " +
                "[dbo].[DoctorAvailability], [dbo].[WeekDay] " +
                "where " +
                "[dbo].[DoctorAvailability].active = 'true' and " +
                "GETDATE() BETWEEN[dbo].[DoctorAvailability].DayStart and[dbo].[DoctorAvailability].DayEnd and " +
                "[dbo].[DoctorAvailability].idWeekDay = [dbo].[WeekDay].idWeekDay and " +
                "idDoctor = " + idDoctor;
            IEnumerable<WeekDayClass> list = cn.Query<WeekDayClass>(query);
            return list;
        }


        internal IEnumerable<WeekDayClass> GetAllWeekDays(string username)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            string query =
                "SELECT distinct [dbo].[WeekDay].* " +
                "FROM " +
                "[dbo].[DoctorAvailability], [dbo].[WeekDay]," +
                "[dbo].[Doctor] " +
                "where " +
                "[dbo].[Doctor].idDoctor=[dbo].[DoctorAvailability].idDoctor and " +
                "[dbo].[DoctorAvailability].active = 'true' and " +
                "GETDATE() BETWEEN[dbo].[DoctorAvailability].DayStart and[dbo].[DoctorAvailability].DayEnd and " +
                "[dbo].[DoctorAvailability].idWeekDay = [dbo].[WeekDay].idWeekDay and " +
                $"username = '{username}'" ;
            IEnumerable<WeekDayClass> list = cn.Query<WeekDayClass>(query);
            return list;
        }



        internal IEnumerable<DoctorAvailabilityView> GetAvailabilityOfDoctorTrue(int id)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            string query =
                "SELECT [id],[DoctorAvailability].[idDoctor],[Doctor].FirstName + ' ' + [Doctor].LastName as DoctorName" +
                        ",[DoctorAvailability].[idWeekDay],[WeekDay].Name as  'WeekDay'" +
                        ",[DoctorAvailability].[idSlot],CONCAT([Slots].StartHour, '-', [Slots].EndHour) as SlotInformation" +
                        ",[DayStart],[DayEnd],[active] " +
            "FROM" +
                        "[DoctorAvailability],[WeekDay],[Slots],[Doctor] " +
            "where " +
                    "[DoctorAvailability].idDoctor =[dbo].[Doctor].idDoctor AND " +
                    "[DoctorAvailability].idSlot =[dbo].[Slots].idSlot AND " +
                    "[DoctorAvailability].idWeekDay =[dbo].[WeekDay].idWeekDay AND " +
                    $"[DoctorAvailability].idDoctor = {id} AND" +
                    $"[DoctorAvailability].idSlot =[dbo].[DoctorAvailability].active";
            IEnumerable<DoctorAvailabilityView> list = cn.Query<DoctorAvailabilityView>(query);
            return list;
        }

        public Doctor Get(int id)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            return cn.Get<Doctor>(id);
        }

        public Doctor Get(string username)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            //vamos verificar se este doctor já existe com userapplication
            string query = $"select top 1 * from Doctor where username='{username}'";
            return cn.Query<Doctor>(query).FirstOrDefault();
        }

        public long Add(Doctor doctor)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            //vamos verificar se este doctor já existe com userapplication
            string query = $"select * from UserApplication where username='{doctor.Username}'";
            IEnumerable<UserApplication> list = cn.Query<UserApplication>(query);
            if (list.Any())
            {
                long id = cn.Insert(doctor);
                return id;
            }
            else
            {
                UserApplication userApplication = new UserApplication()
                {
                    Username = doctor.Username,
                    Role=UserApplication.Doctor,
                    Password=UserApplication.DefaultPassword
                };
                long id = cn.Insert(userApplication);
                id = cn.Insert(doctor);
                return id;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of availability</param>
        /// <returns></returns>
        internal DoctorAvailabilityView GetAvailability(int id)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            string query =
                "SELECT Top 1 [id],[DoctorAvailability].[idDoctor],[Doctor].FirstName + ' ' + [Doctor].LastName as DoctorName" +
                        ",[DoctorAvailability].[idWeekDay],[WeekDay].Name as  'WeekDay'" +
                        ",[DoctorAvailability].[idSlot],CONCAT([Slots].StartHour, '-', [Slots].EndHour) as SlotInformation" +
                        ",[DayStart],[DayEnd],[active] " +
            "FROM" +
                        "[DoctorAvailability],[WeekDay],[Slots],[Doctor] " +
            "where " +
                    "[DoctorAvailability].idDoctor =[dbo].[Doctor].idDoctor AND " +
                    "[DoctorAvailability].idSlot =[dbo].[Slots].idSlot AND " +
                    "[DoctorAvailability].idWeekDay =[dbo].[WeekDay].idWeekDay AND " +
                    $"[DoctorAvailability].id = {id}";
            IEnumerable<DoctorAvailabilityView> list = cn.Query<DoctorAvailabilityView>(query);
            return list.FirstOrDefault();
        }

        public bool Delete(long id)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Execute("DELETE FROM [Doctor] WHERE idDoctor = @Id", new { Id = id }) != 0; ;
            return success;
        }

        public bool Delete(Doctor doctor)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Delete<Doctor>(doctor);
            return success;
        }

        public bool Update(Doctor doctor)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Update<Doctor>(doctor);
            return success;
        }
    }
}
