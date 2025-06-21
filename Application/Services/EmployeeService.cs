using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{

    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return null;

            var dto = _mapper.Map<EmployeeDto>(employee);
            dto.PositionName = employee.Position?.PositionName;
            dto.ManagerName = employee.Manager?.FullName;

            return dto;
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync(true);
            return employees.Select(e =>
            {
                var dto = _mapper.Map<EmployeeDto>(e);
                dto.PositionName = e.Position?.PositionName;
                dto.ManagerName = e.Manager?.FullName;
                return dto;
            }).ToList();
        }

        public async Task<List<EmployeeDto>> GetEmployeesByManagerAsync(int managerId)
        {
            var employees = await _employeeRepository.GetEmployeesByManagerAsync(managerId);
            return _mapper.Map<List<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);

            if (!string.IsNullOrEmpty(employeeDto.Password))
            {
                employee.Password = _passwordHasher.HashPassword(employeeDto.Password);
            }

            await _employeeRepository.AddAsync(employee);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeDto employeeDto)
        {
            if (id != employeeDto.EmployeeId)
                throw new ArgumentException("ID mismatch");

            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null)
                throw new KeyNotFoundException("Employee not found");

            _mapper.Map(employeeDto, existingEmployee);
            await _employeeRepository.UpdateAsync(existingEmployee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                throw new KeyNotFoundException("Employee not found");

            await _employeeRepository.DeleteAsync(employee);
        }
    }
}
