using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlankSpace.Database;
using BlankSpace.Models;
using BlankSpace.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlankSpace.Repository
{
    public class BusRepository : IBusRepository
    {
        private readonly DatabaseContext _context;
        [TempData]
        public string msg { get; set; }
        public BusRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void AddNewBus(BusVm BusVm)
        {
            var e = _context.Buses.Where(x => x.BusNumber == BusVm.BusNumber).FirstOrDefault();
            Buses bus = new Buses() { 
            BusId=BusVm.BusId,
            BusNumber=BusVm.BusNumber,
            CoachName=BusVm.CoachName,
            TotalSeat=BusVm.TotalSeat,
            };
            if (e == null)
            {
                _context.Buses.Add(bus);
                
                _context.SaveChanges();
                msg = "Bus Added Successfully";

            }
            else
                msg = "Bus Already Exist!";
            
        }

        public BusVm GetBus(int id)
        {
            var i = _context.Buses.AsNoTracking().Where(s => s.BusId == id).FirstOrDefault();
            if (i == null)
            {
                i = new Buses();
            }
            BusVm sr = new BusVm 
	    { 
            BusId=i.BusId,
            BusNumber=i.BusNumber,
            TotalSeat=i.TotalSeat,
            CoachName=i.CoachName,
            };

            return sr;
        }

        public List<Buses> GetAllBuses()
        {
            return _context.Buses.AsNoTracking().ToList();
        }

       

        public void UpdateBus(BusVm BusVm)
        {
            Buses bus = new Buses()
            {
                BusId = BusVm.BusId,
                BusNumber = BusVm.BusNumber,
                CoachName = BusVm.CoachName,
                TotalSeat = BusVm.TotalSeat,
            };
            _context.Buses.Update(bus);
            _context.SaveChanges();
            
        }

        public void DeleteBus(BusVm id)
        {
            var i= _context.Buses.AsNoTracking().Where(s => s.BusId == id.BusId).FirstOrDefault();
            if(i!=null)
            {
                _context.Buses.Remove(i);
                _context.SaveChanges();
            }
           

        }
    }
}
