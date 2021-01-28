using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using RINTE_Care.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RINTE_Care.Data
{
    public class MyGoogleCalendar
    {
        public void CreateAppointment(DateTime dateTime, Slot slot, Doctor doctor, Patient patient)
        {
                string[] Scopes = { CalendarService.Scope.Calendar, CalendarService.Scope.CalendarReadonly };
                var ev = new Event();
                EventDateTime start = new EventDateTime
                {
                    DateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,
                                            slot.StartHour.Hours, slot.StartHour.Minutes, 0),
                    TimeZone = "Europe/Lisbon"
                };

                EventDateTime end = new EventDateTime
                {
                    DateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,
                                            slot.EndHour.Hours, slot.EndHour.Minutes, 0),
                    TimeZone = "Europe/Lisbon"
                };


                ev.Start = start;
                ev.End = end;
                ev.Summary = "Appointment";
                ev.Description =  $"Appointment\n" +
                                       $"- Doctor: {doctor.FirstName} {doctor.LastName}\n" +
                                       $"- Day: {dateTime:dd-MM-yyyy}\n" +
                                       $"- Period:{slot.StartHour} to {slot.EndHour} \n" +
                                       $"\nYou can reschedule or cancel If you unable attend to attend the appointment.";

                var calendarId = "primary";

                string ApplicationName = "RINTECare";
                Google.Apis.Auth.OAuth2.UserCredential credential;

                using (var stream =
                    new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                {
                    // The file token.json stores the user's access and refresh tokens, and is created
                    // automatically when the authorization flow completes for the first time.                    
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "Managment",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }

                // Create Google Calendar API service.
                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });
                ev.Attendees = new List<EventAttendee>() {
                    new EventAttendee()
                    {
                        Email=patient.Username
                    },
                    new EventAttendee()
                    {
                        Email=doctor.Username
                    },
                    new EventAttendee()
                    {
                        Email=EMails.EmailClinic
                    }
                };
                Event recurringEvent = service.Events.Insert(ev, calendarId).Execute();
        }
    }
}
