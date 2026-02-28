using Microsoft.AspNetCore.Mvc;
using EmployeeApi.Repositories;
using EmployeeApi.Models;

namespace EmployeeApi.Controllers
{
    [Route("api/Emp")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;

        public EmployeesController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _repository.GetAllAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Employee employee)
        {
            await _repository.AddAsync(employee);
            return Ok("Employee Added Successfully");
        }
    }
}
