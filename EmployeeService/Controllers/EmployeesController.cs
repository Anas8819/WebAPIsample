using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;
using System.Web.Http.Cors;
using System.Threading;

namespace EmployeeService.Controllers
{
    [RequireHttps]
    [EnableCorsAttribute("*", "*", "*")]
    public class EmployeesController : ApiController
    {
        [BasicAuthentication]
        public HttpResponseMessage Get(string gender = "All")
        {
            string username = Thread.CurrentPrincipal.Identity.Name;

            using (empEntities entities = new empEntities())
            {
                switch (username.ToLower())
                {
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entities.tbl_employee.Where(e => e.Gender.ToLower() == "male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entities.tbl_employee.Where(e => e.Gender.ToLower() == "female").ToList());
                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }
        public HttpResponseMessage GetById(int id)
        {
            using (empEntities entities = new empEntities())
            {
                var entity = entities.tbl_employee.FirstOrDefault(e => e.Id == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID = " + id.ToString() + " not found");
                }
            }
        }
        [HttpPost]
        public HttpResponseMessage Insert([FromBody]tbl_employee employee)
        {
            try
            {
                using (empEntities entities = new empEntities())
                {
                    entities.tbl_employee.Add(employee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.Id.ToString());
                    return message;
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
        [DisableCors]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (empEntities entities = new empEntities())
                {
                    var entity = entities.tbl_employee.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.tbl_employee.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
        [HttpPut]
        public HttpResponseMessage Edit(int id, [FromBody] tbl_employee employee)
        {
            try
            {
                using (empEntities entities = new empEntities())
                {
                    var entity = entities.tbl_employee.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID" + id.ToString() + " not found");
                    }
                    else
                    {
                        entity = employee;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    //entity.Id = employee.Id;
                    //entity.Name = employee.Name;
                    //entity.Gender = employee.Gender;
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}