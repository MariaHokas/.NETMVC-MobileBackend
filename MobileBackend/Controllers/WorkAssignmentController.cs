using MobileBackend.DataAccess;
using MobileFronend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MobileBackend.Controllers
{
    public class WorkAssignmentController : ApiController
    {
        public string[] GetAll()
        {


            string[] assingnmentNames = null;
            TimesheetEntities entities = new TimesheetEntities();
            try
            {
                //Jos olisi lista List<string> alkuun ja ToArrayn tilaalle ToList();
                assingnmentNames = (from wa in entities.WorkAssignments
                                    where (wa.Active == true)
                                    select wa.Title).ToArray();

            }
            finally
            {

                entities.Dispose();
            }
            return assingnmentNames;
        }

        [HttpPost]
        public bool PostStatus(WorkAssigmentOperationModel input)
        {
            TimesheetEntities entities = new TimesheetEntities();
            try
            {
                //Jos olisi lista List<string> alkuun ja ToArrayn tilaalle ToLista();
                WorkAssignment assingnment = (from wa in entities.WorkAssignments
                                    where (wa.Active == true) &&
                                    (wa.Title == input.AssignmentTitle)
                                    select wa).FirstOrDefault();

                if (assingnment == null)
                {
                    return false;
                }

                if(input.Operation == "Start")
                { 

                int assignmentid = assingnment.Id_WorkAssignment;
                    Timesheet newEntry = new Timesheet()

                    {
                        id_WorkAssignment = assignmentid,
                        StartTime = DateTime.Now,
                        WorkCompleted = false,
                        Active = true,
                        CreatedAt = DateTime.Now

                };
                entities.Timesheets.Add(newEntry);
                }

                  else if (input.Operation == "Stop")
                {

                    int assignmentid = assingnment.Id_WorkAssignment;
                    Timesheet existing = (from ts in entities.Timesheets
                                            where (ts.id_WorkAssignment == assignmentid) &&
                                            (ts.Active == true) && (ts.WorkCompleted == false)
                                            orderby ts.StartTime descending
                                            select ts).FirstOrDefault();

                    if (existing != null)
                    {
                        existing.StopTime = DateTime.Now;
                        existing.WorkCompleted = true;
                        existing.LastModifiedAt = DateTime.Now;
                    }

                    else
                    {
                        return false;
                    }
                }
                entities.SaveChanges();
            }
            catch
            {
                return false;
            }
            finally
            {

                entities.Dispose();
            }
           
            return true;
        }
    }
}
