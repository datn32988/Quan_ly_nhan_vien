using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class Mapper : Profile
    {
        public Mapper() 
        {
            CreateMap<Position, PositionDto>();
            CreateMap<PositionDto, Position>();
            // Employee
            CreateMap<Employee, EmployeeDto>()
          .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position.PositionName))
          .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Manager.FullName));

            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.Position, opt => opt.Ignore())
                .ForMember(dest => dest.Manager, opt => opt.Ignore());
            // Checkin mapping
            CreateMap<Checkin, CheckinDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FullName));
            CreateMap<CheckinDto, Checkin>();

            // DailyWorkSummary mapping
            CreateMap<DailyWorkSummary, DailyWorkSummaryDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FullName));
            CreateMap<DailyWorkSummaryDto, DailyWorkSummary>();
            // DailyReport mappings
            CreateMap<DailyReport, DailyReportDto>();
            CreateMap<DailyReportDto, DailyReport>()
                .ForMember(dest => dest.DailyReportCompletedTasks, opt => opt.Ignore())
                .ForMember(dest => dest.DailyReportPlannedTasks, opt => opt.Ignore());

            CreateMap<DailyReportCompletedTask, CompletedTaskDto>();
            CreateMap<DailyReportPlannedTask, PlannedTaskDto>();
            CreateMap<EmployeesList, TaskDto>();
        }
    }
}
