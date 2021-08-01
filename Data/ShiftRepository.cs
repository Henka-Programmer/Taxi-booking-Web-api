using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueJSDotnet51.Data.DTO;
using VueJSDotnet51.Models;

namespace VueJSDotnet51.Data
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly ApiContext _ctx;
        private readonly IMapper _mapper;

        public ShiftRepository(ApiContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }


        public async Task Addshift(Shift shift)
        {
            await _ctx.shfits.AddAsync(shift);
        }

        public void DeleteShift(int shiftId)
        {
            var shift = _ctx.shfits.Find(shiftId);

            _ctx.shfits.Remove(shift);
        }

        public Driver GetDriverById(int driverid)
        {
            return _ctx.drivers.Find(driverid);
        }

      

        public List<ShiftforShiftDTO> Getshifts()
        {
            var shifts = _ctx.shfits.Include(a => a.vehicle).Include(x => x.Driver).ToList();
            var response = _mapper.Map<List<ShiftforShiftDTO>>(shifts);
            return response;
            
        }

        public List<ShiftforShiftDTO> GetshiftsByDriverId(int driverid)
        {


            var source = _ctx.shfits.Where(x => x.DriverId == driverid).Include(a=>a.vehicle).Include(a=>a.Driver).ToList();
            var res = _mapper.Map<List<ShiftforShiftDTO>>(source);

            return res;
        }

       

        public Vehicle GetVehicleById(int id)
        {
            var cab = _ctx.vehicles.Find(id);

            return cab;
        }

        public async Task<int> SaveChanges()
        {
            return await _ctx.SaveChangesAsync();
        }

       
    }
}
