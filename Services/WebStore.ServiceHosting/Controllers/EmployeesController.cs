using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/employees")]
    [ApiController]
    [Produces("application/json")]
    public class EmployeesController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        [HttpGet, ActionName("Get")]
        public IEnumerable<Employee> GetAll()
        {
            return _EmployeesData.GetAll();
        }

        [HttpGet("{id}"), ActionName("Get")]
        public Employee GetById(int id)
        {
            return _EmployeesData.GetById(id);
        }

        [HttpPost, ActionName("Post")]
        public void AddNew([FromBody] Employee employee)
        {
            _EmployeesData.AddNew(employee);
        }

        [HttpPut("{id}"), ActionName("Put")]
        public Employee Update(int id, [FromBody] Employee employee)
        {
            return _EmployeesData.Update(id, employee);
        }

        [HttpDelete("{id}")]
        public void Delete(int id/*, [FromServices] ILogger<EmployeesController> Logger*/)
        {
            //using(Logger.BeginScope($"Удаление сотрудника с id {id}"))
            {
                _EmployeesData.Delete(id);
                //Logger.LogInformation("Сотрудник id {0} удалён усепшно", id);
            }
        }

        [NonAction]
        public void SaveChanges()
        {
            _EmployeesData.SaveChanges();
        }
    }
}