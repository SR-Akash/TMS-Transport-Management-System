using BlankSpace.Models;
using BlankSpace.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.Helper
{
    public static class GenarateTicket
    {
        public static List<TicketReservation> Ticket(int Seat, int BusScheduleId,DateTime Date)
        {
            List<TicketReservation> BusTicketBooking = new List<TicketReservation>();
            int c = 1;
            var SeatText = 65;
            for (int i = 0; i < Seat; i++)
            {
                
                TicketReservation vs = new TicketReservation()
                {
                    ConfirmStatus = false,
                    BusScheduleId = BusScheduleId,
                    Date = Date,
                    AgentId = 1,
                    PassengerId = 1,
                    TicketReservationId = 0
                };
                int k = i;
                if (k % 4 == 0 && k>0)
                {
                    SeatText++;
                    c = 1;
                }
                var t = (char)SeatText;
                
                var o=t.ToString();
                vs.SeatNumber = o +" "+c;

                BusTicketBooking.Add(vs);
                c++;
               
            }
            return BusTicketBooking;

        }
    }
}

