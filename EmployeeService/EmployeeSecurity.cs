using DataAccess;
using System;
using System.Linq;

namespace EmployeeService
{
    public class EmployeeSecurity
    {
        public static bool Login(string username, string password)
        {
            using (empEntities entities = new empEntities())
            {
                return entities.tbl_users.Any(user =>
                       user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password);
            }
        }
    }
}