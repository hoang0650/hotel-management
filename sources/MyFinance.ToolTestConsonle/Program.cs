using MyFinance.Data;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Entities;
using MyFinance.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.ToolTestConsonle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Đồng bộ data theo từng khách sạn ?");
            mirateData(true, null);

            Console.ReadLine();
        }

        static bool mirateData(bool byHotel,int? hotelId)
        {
            MyFinanceContext _context = new MyFinanceContext();
            var orders= _context.Orders.Where(a => a.OrderStatus == (int)OrderStatus.CheckIn && a.OrderServices.Count>0 && !a.Hotel.IsDeleted).Select(a=>new {
                Id=a.Id,
                hotelId=a.HotelId
            }).ToList();
            foreach (var item in orders)
            {
                var services = _context.OrderService.Where(a => a.OrderId == item.Id).Select(b=> new  ServiceRowModel
                {
                    Id = b.Service.Id,
                    Name = b.Service.Name,
                    Price = b.Service.PricePaid,
                    Quantity = b.Quantity
                }).ToList();
                /// add to order detail
                /// 
                foreach (var service in services)
                {
                    int shiftId = 0;
                    var shift = _context.Shift.Where(a => a.HotelId == item.hotelId && a.EndTime.HasValue).FirstOrDefault();
                    if (shift == null)
                    {
                        shift = _context.Shift.Where(a => a.HotelId == item.hotelId).OrderByDescending(a=>a.Id).FirstOrDefault();
                       
                    }
                    shiftId = shift.Id;
                    var row=new OrderDetail()
                    {
                        Title=service.Name,
                        SubAmount=service.Price* service.Quantity,
                        DetailTypeId=(int)CategoryInvoice.Service,
                        CreatedDate=DateTime.Now,
                        OrderId=item.Id,
                        RelatedId= service.Id,
                        ShiftId= shiftId,
                        Price= service.Price,
                        Quantity=service.Quantity

                    };
                    _context.OrderDetails.Add(row);

                }
                _context.SaveChanges();
            }

            return true;
        }
    }
}
