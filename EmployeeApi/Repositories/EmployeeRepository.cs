using Microsoft.Data.SqlClient;
using System.Data;
using EmployeeApi.Models;

namespace EmployeeApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration
                .GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetEmployees", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    await con.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            employees.Add(new Employee
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Department = reader["Department"].ToString(),
                                Salary = Convert.ToDecimal(reader["Salary"])
                            });
                        }
                    }
                }
            }

            return employees;
        }

        public async Task AddAsync(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_AddEmployee", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);

                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}