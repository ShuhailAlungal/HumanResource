using Dapper;
using HumanResourceMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HumanResourceMVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
                return View(DapperORM.GetList<EmployeeModel>("GetAllEmployee"));
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            if (id==0)
                return View();
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                return View(DapperORM.GetList<EmployeeModel>("GetEmployeeById", param).FirstOrDefault< EmployeeModel>());
            }
            
        }

        [HttpPost]
        public ActionResult AddOrEdit(EmployeeModel model)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", model.Id);
            param.Add("@FirstName", model.FirstName);
            param.Add("@SecondName", model.SecondName);
            param.Add("@DateOfBirth", model.DateOfBirth);
            param.Add("@Department", model.Department);
            param.Add("@Status", model.Status);
            param.Add("@EmployeeNumber", model.EmployeeNumber);
            param.Add("@Email", model.Email);
            DapperORM.ExecuteWithOutReturn("EmpAddOrEdit", param);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            DapperORM.ExecuteWithOutReturn("DeleteEmployeeById", param);
            return RedirectToAction("Index");
        }
    }
}