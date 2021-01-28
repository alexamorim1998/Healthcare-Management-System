using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RINTE_Care.Data;
using RINTE_Care.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace RINTE_Care.Controllers
{
    public class AppointmentController : Controller
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
        private readonly EMails emails;
        private readonly ILogger<AppointmentController> _logger;
        readonly IGeneratePdf _generatePdf;
        
        public AppointmentController(
            ILogger<AppointmentController> logger,
            IConfiguration configuration,
            IGeneratePdf generatePdf)
        {
            _generatePdf = generatePdf;
            _logger = logger;
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("ConnectionStrings:RinteCare");
            doctors = new Doctors(_connectionString);
            weekDays = new WeekDays(_connectionString);
            slots = new Slots(_connectionString);
            doctorAvailabilitys = new DoctorAvailabilitys(_connectionString);
            doctorAppointments = new DoctorAppointments(_connectionString);
            patients = new Patients(_connectionString);
            emails = new EMails();
            listOfweekdays = weekDays.GetAll();
            listOfSlots = slots.GetAll();
        }

        // GET: AppointmentController
        public ActionResult IndexAdministrator()
        {
            return View(doctorAppointments.GetAllFriendlyShow());
        }

        public ActionResult IndexDoctor()
        {
            string username = HttpContext.Session.GetString(SessionInfo.UsernameKey);
            return View(doctorAppointments.GetAllFriendlyShowDoctor(username));
        }

        public ActionResult IndexPatient()
        {
            string username = HttpContext.Session.GetString(SessionInfo.UsernameKey);
            return View(doctorAppointments.GetAllFriendlyShowPatient(username));
        }

        // GET: AppointmentController/Details/5
        public ActionResult Details(int id)
        {
            return View(doctorAppointments.Get(id));
        }

        // GET: AppointmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppointmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAdministrator));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppointmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(doctorAppointments.Get(id));
        }

        // POST: AppointmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAdministrator));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CancelAdministrator(int id, IFormCollection collection)
        {
            try
            {
                DoctorAppointment doctorAppointment = doctorAppointments.Get(id);
                if (doctorAppointment.State != (int)State.Create)
                {
                    return RedirectToAction(nameof(IndexAdministrator));
                }
                doctorAppointments.CancelApppointByAdministrator(id);
                return RedirectToAction(nameof(IndexAdministrator));
            }
            catch(Exception ex)
            {
                ViewBag.Error = $"For appointment {id} :" + ex.Message;
                return View();
            }
        }

        public ActionResult AccomplishAdministrator(int id, IFormCollection collection)
        {
            try
            {
                DoctorAppointment doctorAppointment = doctorAppointments.Get(id);
                if (doctorAppointment.State != (int) State.Create)
                {
                    return RedirectToAction(nameof(IndexAdministrator));
                }
                doctorAppointments.AccomplishApppointByAdministrator(id);
                Patient patient = patients.Get(doctorAppointment.idPatient);
                DoctorAvailability doctorAvailability = doctorAvailabilitys.Get(doctorAppointment.idDoctorAvailability);
                Slot slot = slots.Get(doctorAvailability.idSlot);
                Doctor doctor = doctors.Get(doctorAvailability.idDoctor);
                var redFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "assets", "logo.png");
                /*Envio de email*/
                try
                {
                    
                    string fileCreated = "Invoice.pdf";
                    
                    

                    string image = $"https://localhost:{this.HttpContext.Connection.LocalPort}/assets/img/logo.png";
                    // versao 1 - Html inside

                           var html = $@"<!DOCTYPE html>
                           <html>
                           <img src ='{image}'  alt ='image' style='width: 100 %;' />
                           <h2>Invoice</h2>
                           <body> 
                           <p>Dear {patient.FirstName} {patient.LastName},</p>
                            <p>Once again thank you for choosing our services. All the appointment details are described below:</p>
                            <h3>- Patient details</h3>
                                <table>
                                  <tr>
                                    <th>Name</th>
                                    <th>Tax Number</th>
                                    <th>Phone Number</th>
                                    <th>Address</th>
                                    <th>Zip Code</th>
                                  </tr>
                                  <tr>
                                    <td>{patient.FirstName} {patient.LastName}</td>
                                    <td>{patient.TaxNumber}</td>
                                    <td>{patient.PhoneNumber}</td>
                                    <td>{patient.Address}, {patient.Location}</td>
                                    <td>{patient.ZipCode}</td>
                                  </tr>
                                </table>
                            <h3>- Appointment details</h3>
                                <table>
                                  <tr>
                                    <th>Doctor</th>
                                    <th>Tax Number</th>
                                    <th>License Number</th>
                                    <th>Phone Number</th>
                                    <th>Description</th>
                                    <th>Date/Hour</th>
                                  </tr>
                                  <tr>
                                    <td>{doctor.FirstName} {doctor.LastName}</td>
                                    <td>{doctor.TaxNumber}</td>
                                    <td>{doctor.ProfessionalLicenseNumber}</td>
                                    <td>{doctor.PhoneNumber}</td>
                                    <td>{patient.Description}</td>
                                    <td>{doctorAppointment.DateAppointment:dd-MM-yyyy} | {slot.StartHour} - {slot.EndHour}</td>
                                  </tr>
                                </table>
                            <h3>- Payment details</h3>
                                <table>
                                  <tr>
                                    <th>Entity</th>
                                    <th>Reference</th>
                                    <th>Total cost</th>
                                    <th>Deadline</th>
                                  </tr>
                                  <tr>
                                    <td>10451</td>
                                    <td>520644463</td>
                                    <td>{DoctorAppointments.PricePerHour}€</td>
                                    <td>{doctorAppointment.DateAppointment.AddDays(5)} | {slot.StartHour} - {slot.EndHour}</td>
                                  </tr>
                                </table>
                            </table>
                            <p>Best regards,</p>
                            <p>RINTECare Porto</p>
                            <p>Contacts: 224890213 | rintecare@gmail.com</p>
                        </body>";

                    //https://github.com/fpanaccia/Wkhtmltopdf.NetCore.Example/blob/master/Rotativa/Controllers/TestViewsController.cs
                    //https://github.com/fpanaccia/Wkhtmltopdf.NetCore
                    System.IO.File.WriteAllBytes(fileCreated, _generatePdf.GetPDF(html));
                    
                    //AppointmentPDF appointmentPDF = new AppointmentPDF()
                    //{
                    //    PatientFirstName = patient.FirstName
                    //};
                    //System.IO.File.WriteAllBytes(fileCreated, _generatePdf.GetByteArray("Views/Appointment/AccomplishAdministrator.cshtml", appointmentPDF).Result);


                    emails.SendEmail(
                            new Email()
                            {
                                SenderEmail = EMails.EmailClinic,
                                Body = $"\nDear {patient.FirstName} {patient.LastName},\n" +
                                       $"\nThe following appointment has been issued:\n " +
                                       $"\n- Patient Information: Tax Number - {patient.TaxNumber} | Address - {patient.Address} | Zip Code - {patient.ZipCode} | Location - {patient.Location}\n" +
                                       $"\n- Doctor Information: Name - {doctor.FirstName} {doctor.LastName} | Tax Number - {doctor.TaxNumber} |Professional License Number - {doctor.ProfessionalLicenseNumber} \n" +
                                       $"\n- Day: {doctorAppointment.DateAppointment:dd-MM-yyyy}\n" +
                                       $"\n- Period: {slot.StartHour} to {slot.EndHour} \n" +
                                       $"\nOur details for the payment are:\n" +
                                       $"\n- Entity: 10451\n" +
                                       $"\n- Reference: 520644463\n" +
                                       $"\n- Price: {DoctorAppointments.PricePerHour}€\n" +
                                       $"\n- Payment Deadline: {doctorAppointment.DateAppointment.AddDays(5)} {slot.EndHour}\n" +
                                       $"\nKeep the transaction receipt with the invoice/receipt as proof of payment.\n" +
                                       $"\nBest regards, \n" +
                                       $"\nRINTECare Porto",
                                Subject = "RINTECare Porto | Invoice",
                                DestinationName1 = patient.FirstName + " " + patient.LastName,
                                DestinationEmail1 = patient.Username,
                                FileAttachment = fileCreated
                            }
                            ); ;
                }
                catch (Exception ex)
                {
                    _logger.LogDebug(ex.StackTrace);
                }
                return RedirectToAction(nameof(IndexAdministrator));
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"For appointment {id} :" + ex.Message;
                return View();
            }
        }
        

        public ActionResult CancelDoctor(int id, IFormCollection collection)
        {
            try
            {
                DoctorAppointment doctorAppointment = doctorAppointments.Get(id);
                if (doctorAppointment.State != (int)State.Create)
                {
                    return RedirectToAction(nameof(IndexDoctor));
                }
                doctorAppointments.CancelApppointByDoctor(id);
                return RedirectToAction(nameof(IndexDoctor));
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"For appointment {id} :" + ex.Message;
                return View();
            }
        }

        public ActionResult CancelPatient(int id, IFormCollection collection)
        {
            try
            {
                DoctorAppointment doctorAppointment = doctorAppointments.Get(id);
                if (doctorAppointment.State != (int)State.Create)
                {
                    return RedirectToAction(nameof(IndexPatient));
                }
                doctorAppointments.CancelApppointByPatient(id);
                return RedirectToAction(nameof(IndexPatient));
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"For appointment {id} :" + ex.Message;
                return View();
            }
        }


        // GET: AppointmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(doctorAppointments.Get(id));
        }

        // POST: AppointmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAdministrator));
            }
            catch
            {
                return View();
            }
        }
    }
}
