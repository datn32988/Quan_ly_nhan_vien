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
            CreateMap<DailyWorkSummary, DailyWorkSummaryDto>()
                .ForMember(dest => dest.EmployeeName,
                    opt => opt.MapFrom(src => src.Employee.FullName));
            CreateMap<WorkPlan, WorkPlanDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FullName));
            CreateMap<CreateWorkPlanDto, WorkPlan>();
            CreateMap<UpdateWorkPlanDto, WorkPlan>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            //DailyReport
            CreateMap<DailyReport, DailyReportDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FullName))
                .ForMember(dest => dest.CompletedTasks, opt => opt.MapFrom(src => src.DailyReportCompletedTasks))
                .ForMember(dest => dest.PlannedTasks, opt => opt.MapFrom(src => src.DailyReportPlannedTasks));
            CreateMap<DailyReportCreateDto, DailyReport>();
            CreateMap<DailyReportCompletedTask, CompletedTaskDetailDto>()
                .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.Task.TaskName))
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId))
                .ForMember(dest => dest.CompletionDescription, opt => opt.MapFrom(src => src.CompletionDescription));

            CreateMap<CompletedTaskDto, DailyReportCompletedTask>();
            CreateMap<DailyReportPlannedTask, PlannedTaskDetailDto>()
                .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.Task.TaskName))
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId))
                .ForMember(dest => dest.PlannedDescription, opt => opt.MapFrom(src => src.PlannedDescription));

            CreateMap<PlannedTaskDto, DailyReportPlannedTask>();
            //Task
            CreateMap<EmployeesList, AvailableTaskDto>();
            CreateMap<EmployeesList, AvailableTaskDto>();
            CreateMap<CreateTaskDto, EmployeesList>();
            CreateMap<UpdateTaskDto, EmployeesList>();
            CreateMap<DailyReport, DailyReportDto>();

        }

    }
}

