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
            CreateMap<CreateTaskDto, EmployeesList>()
               .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.TaskName))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.AssignedToEmployeeId, opt => opt.MapFrom(src => src.AssignedToEmployeeId))
               .ForMember(dest => dest.RelatedProject, opt => opt.MapFrom(src => src.RelatedProject))
               .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
               // Bỏ qua các field không có trong DTO hoặc sẽ set sau
               .ForMember(dest => dest.TaskId, opt => opt.Ignore())
               .ForMember(dest => dest.CreationDate, opt => opt.Ignore())
               .ForMember(dest => dest.CompletionDate, opt => opt.Ignore())
               .ForMember(dest => dest.Status, opt => opt.Ignore())
               .ForMember(dest => dest.AssignedToEmployee, opt => opt.Ignore());
            CreateMap<EmployeesList, TaskDto>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId));
            CreateMap<WorkSchedule, WorkScheduleDto>()
                .ForMember(dest => dest.EmployeeName,
                    opt => opt.MapFrom(src => src.Employee.FullName))
                .ForMember(dest => dest.ScheduleDate,
                    opt => opt.MapFrom(src => src.ScheduleDate.Date));

            CreateMap<CreateWorkScheduleDto, WorkSchedule>()
                .ForMember(dest => dest.WorkScheduleId, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.ScheduleDate,
                    opt => opt.MapFrom(src => src.ScheduleDate.Date));

            CreateMap<UpdateWorkScheduleDto, WorkSchedule>()
                .ForMember(dest => dest.WorkScheduleId, opt => opt.Ignore())
                .ForMember(dest => dest.EmployeeId, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.ScheduleDate,
                    opt => opt.MapFrom(src => src.ScheduleDate.Value.Date))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
