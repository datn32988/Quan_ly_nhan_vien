using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CheckinService : ICheckinService
    {
        private readonly ICheckinRepository _checkinRepository;
        private readonly IMapper _mapper;

        public CheckinService(ICheckinRepository checkinRepository, IMapper mapper)
        {
            _checkinRepository = checkinRepository;
            _mapper = mapper;
        }

        public async Task<CheckinDto?> GetCheckinByIdAsync(long id)
        {
            var checkin = await _checkinRepository.GetByIdAsync(id);
            return _mapper.Map<CheckinDto>(checkin);
        }

        public async Task<List<CheckinDto>> GetCheckinsByEmployeeAsync(long employeeId)
        {
            var checkins = await _checkinRepository.GetByEmployeeIdAsync(employeeId);
            return _mapper.Map<List<CheckinDto>>(checkins);
        }

        public async Task<List<CheckinDto>> GetCheckinsByEmployeeAndDateAsync(long employeeId, DateTime date)
        {
            var checkins = await _checkinRepository.GetByEmployeeAndDateAsync(employeeId, date);
            return _mapper.Map<List<CheckinDto>>(checkins);
        }

        public async Task<List<CheckinDto>> GetCheckinsByEmployeeAndMonthAsync(long employeeId, int year, int month)
        {
            var checkins = await _checkinRepository.GetByEmployeeAndMonthAsync(employeeId, year, month);
            return _mapper.Map<List<CheckinDto>>(checkins);
        }

        public async Task<CheckinDto> CreateCheckinAsync(CheckinDto checkinDto)
        {
            var checkin = _mapper.Map<Checkin>(checkinDto);
            checkin.CheckinTime = DateTime.Now;
            await _checkinRepository.AddAsync(checkin);
            return _mapper.Map<CheckinDto>(checkin);
        }

        public async Task DeleteCheckinAsync(long id)
        {
            var checkin = await _checkinRepository.GetByIdAsync(id);
            if (checkin != null)
            {
                await _checkinRepository.DeleteAsync(checkin);
            }
        }
    }
}
