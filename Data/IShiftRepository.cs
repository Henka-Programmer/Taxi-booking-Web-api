using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueJSDotnet51.Data.DTO;
using VueJSDotnet51.Models;

namespace VueJSDotnet51.Data
{
    public interface IShiftRepository
    {
        Task Addshift(Shift shift);

        Driver GetDriverById(int driverid);

        Vehicle GetVehicleById(int id);
        Task<int> SaveChanges();

        List<ShiftforShiftDTO> Getshifts();

        List<ShiftforShiftDTO> GetshiftsByDriverId(int driverid);

        void DeleteShift(int shiftId);
    }
}
