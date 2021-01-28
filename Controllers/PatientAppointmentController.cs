using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RINTE_Care.Data;
using RINTE_Care.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RINTE_Care.Controllers
{
    public class PatientAppointmentController : Controller
    {

        readonly IConfiguration _configuration;
        private readonly Doctors doctors;
        private readonly WeekDays weekDays;
        private readonly Slots slots;
        private readonly DoctorAvailabilitys doctorAvailabilitys;
        private readonly DoctorAppointments doctorAppointments;
        private readonly Patients patients;

        private readonly IEnumerable<WeekDayClass> listOfweekdays;
        private readonly IEnumerable<Slot> listOfSlots;
        private readonly string _connectionString;
        private readonly Specializations specializations;
        private readonly ILogger<PatientAppointmentController> _logger;
        private readonly EMails emails;
        private readonly MyGoogleCalendar myGoogleCalendar;


        public PatientAppointmentController(ILogger<PatientAppointmentController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("ConnectionStrings:RinteCare");
            doctors = new Doctors(_connectionString);
            weekDays = new WeekDays(_connectionString);
            slots = new Slots(_connectionString);
            doctorAvailabilitys = new DoctorAvailabilitys(_connectionString);
            doctorAppointments = new DoctorAppointments(_connectionString);
            patients = new Patients(_connectionString);
            specializations = new Specializations(_connectionString);
            emails = new EMails();
            myGoogleCalendar = new MyGoogleCalendar();

            listOfweekdays = weekDays.GetAll();
            listOfSlots = slots.GetAll();
        }

        public ActionResult GetSpeciality()
        {
            return View(specializations.GetAll());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id Specialization</param>
        /// <returns></returns>
        public ActionResult GetDoctors(long id)
        {
            return View(doctors.GetAllBySpeciality(id));
        }

        /// <summary>
        /// Recebe o doctor e devolve os week days do doctor
        /// </summary>
        /// <param name="idDoctor"></param>
        /// <returns></returns>
        public ActionResult SelectWeekDay(long idDoctor)
        {
            ViewBag.idDoctor = idDoctor;
            return View(doctors.GetAllWeekDays(idDoctor));
        }

        public ActionResult SelectAvailabity(long idWeekDay, long idDoctor)
        {
            return View(doctors.GetAllFutureAvailabilities(idDoctor, idWeekDay));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">idAvailability</param>
        public ActionResult SelectDate(long id)
        {
            var availability = doctorAvailabilitys.Get(id);
            IEnumerable<DoctorAppointment> appointments = doctorAppointments.GetFutureAppoimentsForAvailability(id);
            List<string> list = new List<string>();
            DateTime startDate = availability.DayStart > DateTime.Today ? availability.DayStart : DateTime.Today;
            for (DateTime date = startDate; date <= availability.DayEnd; date = date.AddDays(1))
            {
                if (((int)date.DayOfWeek) == availability.idWeekDay &&
                    !appointments.Select(x => x.DateAppointment).Contains(date))
                {
                    list.Add(date.ToString("d-M-yyyy"));
                }
            }

            AppoimentDateView appoimentDateView = new AppoimentDateView()
            {
                idAvailability = id,
                date = list.Any() ? DateTime.ParseExact(list.First(), "d-M-yyyy", CultureInfo.InvariantCulture) : DateTime.Today,
                datesAvailables = list.ToArray()
            };
            return View(appoimentDateView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectDate(AppoimentDateView appoimentDateView)
        {
            string usernamePatient;
            try
            {
                usernamePatient = HttpContext.Session.GetString(SessionInfo.UsernameKey);
                DoctorAppointment doctorAppointment = new DoctorAppointment()
                {
                    DateAppointment = appoimentDateView.date,
                    idDoctorAvailability = (int)appoimentDateView.idAvailability,
                    idPatient = patients.Get(usernamePatient).idPatient,
                    State = (int)State.Create
                };
                doctorAppointments.Add(doctorAppointment);
                Patient patient = patients.Get(usernamePatient);
                DoctorAvailability doctorAvailability = doctorAvailabilitys.Get((int)appoimentDateView.idAvailability);
                Slot slot = slots.Get(doctorAvailability.idSlot);
                Doctor doctor = doctors.Get(doctorAvailability.idDoctor);


                /*Envio de email*/
                try
                {
                    emails.SendEmail(
                            new Email()
                            {
                                SenderEmail = EMails.EmailClinic,
                                Body = $"\nDear {patient.FirstName} {patient.LastName}\n " +
                                       $"\nYou have the following appointment scheduled:\n " +
                                       $"\n- Doctor: {doctor.FirstName} {doctor.LastName}\n" +
                                       $"\n- Day: {appoimentDateView.date:dd-MM-yyyy}\n" +
                                       $"\n- Period: {slot.StartHour} to {slot.EndHour} \n" +
                                       $"\nYou can reschedule or cancel if you cannot attend.\n" +
                                       $"\nBest regards, \n" +
                                       $"\nRINTECare Porto",
                                Subject = "RINTECare Porto | Appointment",
                                DestinationName1 = patient.FirstName + " " + patient.LastName,
                                DestinationEmail1 = patient.Username
                            }
                            );
                }
                catch (Exception ex)
                {
                    _logger.LogDebug(ex.StackTrace);
                }

                /*Marcação no google calendar*/
                try
                {
                    myGoogleCalendar.CreateAppointment(appoimentDateView.date, slot, doctor, patient);
                }
                catch (Exception ex)
                {
                    _logger.LogDebug(ex.StackTrace);
                }
                return RedirectToAction("IndexPatient", "Appointment");
            }
            catch (Exception e)
            {
                ViewBag.Error = "Some Error existed in database...";
                _logger.LogDebug(e.StackTrace);
                return View(appoimentDateView);
            }

        }


        // GET: PatientAppointmentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PatientAppointmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PatientAppointmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientAppointmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientAppointmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PatientAppointmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientAppointmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PatientAppointmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult SmartAppointment()
        {
            return View();
        }
    }
}
