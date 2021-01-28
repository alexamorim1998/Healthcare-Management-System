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
    public class DoctorAppointments
    {
        public const double PricePerHour = 30.0;
        private readonly string _connectionString;
        public DoctorAppointments(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<DoctorAppointment> GetAll()
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            IEnumerable<DoctorAppointment> list = cn.GetAll<DoctorAppointment>();
            return list;
        }

        internal IEnumerable<DoctorAppointmentView> GetAllFriendlyShow()
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            string query =
                "SELECT DoctorAppointment.*," +
                          "[dbo].[Doctor].FirstName + ' ' + [dbo].[Doctor].LastName DoctorName," +
                          "[dbo].[Patient].FirstName + ' ' + [dbo].[Patient].LastName PatientName," +
                          "[dbo].[WeekDay].Name WeekdayString," +
                          "CONCAT([dbo].[Slots].StartHour, '-', [dbo].[Slots].EndHour) SlotString " +
                "FROM " +
                          "[dbo].[DoctorAppointment]," +
                          "[dbo].[DoctorAvailability]," +
                          "[dbo].[Doctor],[dbo].[Patient]," +
                          "[dbo].[WeekDay]," +
                          "[dbo].[Slots] " +
                "where " +
                    "[DoctorAppointment].idDoctorAvailability = DoctorAvailability.id and " +
                    "[DoctorAvailability].idDoctor = [dbo].[Doctor].idDoctor and " +
                    "[DoctorAppointment].idPatient = [dbo].[Patient].idPatient and " +
                    "[DoctorAvailability].idWeekDay = [dbo].[WeekDay].idWeekDay and " +
                    "[DoctorAvailability].idSlot = [dbo].[Slots].idSlot";
            IEnumerable<DoctorAppointmentView> list = cn.Query<DoctorAppointmentView>(query);
            return list;
        }


        internal IEnumerable<DoctorAppointmentView> GetAllFriendlyShowPatient(int idPatient)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            string query =
                "SELECT DoctorAppointment.*," +
                          "[dbo].[Doctor].FirstName + ' ' + [dbo].[Doctor].LastName DoctorName," +
                          "[dbo].[Patient].FirstName + ' ' + [dbo].[Patient].LastName PatientName," +
                          "[dbo].[WeekDay].Name WeekdayString," +
                          "CONCAT([dbo].[Slots].StartHour, '-', [dbo].[Slots].EndHour) SlotString " +
                "FROM " +
                          "[dbo].[DoctorAppointment]," +
                          "[dbo].[DoctorAvailability]," +
                          "[dbo].[Doctor],[dbo].[Patient]," +
                          "[dbo].[WeekDay]," +
                          "[dbo].[Slots] " +
                "where " +
                    $"[Patient].idPatient = {idPatient} and " +
                    "[DoctorAppointment].idDoctorAvailability = DoctorAvailability.id and " +
                    "[DoctorAvailability].idDoctor = [dbo].[Doctor].idDoctor and " +
                    "[DoctorAppointment].idPatient = [dbo].[Patient].idPatient and " +
                    "[DoctorAvailability].idWeekDay = [dbo].[WeekDay].idWeekDay and " +
                    "[DoctorAvailability].idSlot = [dbo].[Slots].idSlot";
            IEnumerable<DoctorAppointmentView> list = cn.Query<DoctorAppointmentView>(query);
            return list;
        }

        internal IEnumerable<DoctorAppointment> GetFutureAppoimentsForAvailability(long id)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            string query =
                "SELECT  * " +
                " FROM [dbo].[DoctorAppointment] " +
                " where " +
                $" idDoctorAvailability = {id} and " +
                $"State = {(int)State.Create} and " +
                "DateAppointment > GETDATE()";
            IEnumerable<DoctorAppointment> list = cn.Query<DoctorAppointment>(query);
            return list;
        }

        internal IEnumerable<DoctorAppointmentView> GetAllFriendlyShowPatient(string username)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            string query =
                "SELECT DoctorAppointment.*," +
                          "[dbo].[Doctor].FirstName + ' ' + [dbo].[Doctor].LastName DoctorName," +
                          "[dbo].[Patient].FirstName + ' ' + [dbo].[Patient].LastName PatientName," +
                          "[dbo].[WeekDay].Name WeekdayString," +
                          "CONCAT([dbo].[Slots].StartHour, '-', [dbo].[Slots].EndHour) SlotString " +
                "FROM " +
                          "[dbo].[DoctorAppointment]," +
                          "[dbo].[DoctorAvailability]," +
                          "[dbo].[Doctor],[dbo].[Patient]," +
                          "[dbo].[WeekDay]," +
                          "[dbo].[Slots] " +
                "where " +
                    $"[Patient].username = '{username}' and " +
                    "[DoctorAppointment].idDoctorAvailability = DoctorAvailability.id and " +
                    "[DoctorAvailability].idDoctor = [dbo].[Doctor].idDoctor and " +
                    "[DoctorAppointment].idPatient = [dbo].[Patient].idPatient and " +
                    "[DoctorAvailability].idWeekDay = [dbo].[WeekDay].idWeekDay and " +
                    "[DoctorAvailability].idSlot = [dbo].[Slots].idSlot";
            IEnumerable<DoctorAppointmentView> list = cn.Query<DoctorAppointmentView>(query);
            return list;
        }

        internal IEnumerable<DoctorAppointmentView> GetAllFriendlyShowDoctor(int idDoctor)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            string query =
                "SELECT DoctorAppointment.*," +
                          "[dbo].[Doctor].FirstName + ' ' + [dbo].[Doctor].LastName DoctorName," +
                          "[dbo].[Patient].FirstName + ' ' + [dbo].[Patient].LastName PatientName," +
                          "[dbo].[WeekDay].Name WeekdayString," +
                          "CONCAT([dbo].[Slots].StartHour, '-', [dbo].[Slots].EndHour) SlotString " +
                "FROM " +
                          "[dbo].[DoctorAppointment]," +
                          "[dbo].[DoctorAvailability]," +
                          "[dbo].[Doctor],[dbo].[Patient]," +
                          "[dbo].[WeekDay]," +
                          "[dbo].[Slots] " +
                "where " +
                    $"[DoctorAppointment].idDoctorAvailability ={idDoctor} and " +
                    "[DoctorAppointment].idDoctorAvailability = DoctorAvailability.id and " +
                    "[DoctorAvailability].idDoctor = [dbo].[Doctor].idDoctor and " +
                    "[DoctorAppointment].idPatient = [dbo].[Patient].idPatient and " +
                    "[DoctorAvailability].idWeekDay = [dbo].[WeekDay].idWeekDay and " +
                    "[DoctorAvailability].idSlot = [dbo].[Slots].idSlot";
            
            IEnumerable<DoctorAppointmentView> list = cn.Query<DoctorAppointmentView>(query);
            return list;
        }

        internal IEnumerable<DoctorAppointmentView> GetAllFriendlyShowDoctor(string username)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            string query =
                "SELECT DoctorAppointment.*," +
                          "[dbo].[Doctor].FirstName + ' ' + [dbo].[Doctor].LastName DoctorName," +
                          "[dbo].[Patient].FirstName + ' ' + [dbo].[Patient].LastName PatientName," +
                          "[dbo].[WeekDay].Name WeekdayString," +
                          "CONCAT([dbo].[Slots].StartHour, '-', [dbo].[Slots].EndHour) SlotString " +
                "FROM " +
                          "[dbo].[DoctorAppointment]," +
                          "[dbo].[DoctorAvailability]," +
                          "[dbo].[Doctor],[dbo].[Patient]," +
                          "[dbo].[WeekDay]," +
                          "[dbo].[Slots] " +
                "where " +
                    $"[Doctor].username ='{username}' and " +
                    "[DoctorAppointment].idDoctorAvailability = DoctorAvailability.id and " +
                    "[DoctorAvailability].idDoctor = [dbo].[Doctor].idDoctor and " +
                    "[DoctorAppointment].idPatient = [dbo].[Patient].idPatient and " +
                    "[DoctorAvailability].idWeekDay = [dbo].[WeekDay].idWeekDay and " +
                    "[DoctorAvailability].idSlot = [dbo].[Slots].idSlot";
            IEnumerable<DoctorAppointmentView> list = cn.Query<DoctorAppointmentView>(query);
            return list;
        }




        public DoctorAppointment Get(int id)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            return cn.Get<DoctorAppointment>(id);
        }

        public long Add(DoctorAppointment doctor)
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
            bool success = cn.Execute("DELETE FROM [DoctorAppointment] WHERE idSpecialization = @Id", new { Id = id }) != 0; 
            return success;
        }

        internal bool CancelApppointByAdministrator(int id)
        {
            return CancelApppoint(id, State.Canceled_by_Administrator);
        }

        internal bool AccomplishApppointByAdministrator(int id)
        {
            return AccomplishApppoint(id, State.Accomplish);
        }
        

        internal bool CancelApppointByDoctor(int id)
        {
            return CancelApppoint(id, State.Canceled_by_Doctor);
        }

        internal bool CancelApppointByPatient(int id)
        {
            return CancelApppoint(id, State.Canceled_by_Client);
        }

        private bool CancelApppoint(int id, State state)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = 
                cn.Execute(
                "UPDATE [dbo].[DoctorAppointment]" +
                    " SET [State] = @State " +
                "WHERE [id] = @Id",
                new { Id = id, State= state }) != 0;
            return success;
        }

        private bool AccomplishApppoint(int id, State state)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success =
                cn.Execute(
                "UPDATE [dbo].[DoctorAppointment]" +
                    " SET [State] = @State " +
                "WHERE [id] = @Id",
                new { Id = id, State = state }) != 0;
            return success;
        }


        public bool Delete(DoctorAppointment doctorAppointment)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Delete<DoctorAppointment>(doctorAppointment);
            return success;
        }



        public bool Update(DoctorAppointment doctorAppointment)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);
            cn.Open();
            bool success = cn.Update<DoctorAppointment>(doctorAppointment);
            return success;
        }
    }
}
