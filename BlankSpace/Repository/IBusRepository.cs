using BlankSpace.Models;
using BlankSpace.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.Repository
{
    public interface IBusRepository
    {
         void AddNewBus(BusVm BusVm);
         List<Buses> GetAllBuses();
         void UpdateBus(BusVm BusVm);
         void DeleteBus(BusVm id);
         BusVm GetBus(int id);



    }
}
