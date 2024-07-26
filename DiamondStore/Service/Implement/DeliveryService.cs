using BussinessObject.DTO;
using BussinessObject.Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Service.Interface;
using System.Data;
using System.Linq.Expressions;

namespace Service.Implement
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IBaseCRUD<Delivery> _repo;
        private readonly IBaseCRUD<Order> _order;
        private readonly IBaseCRUD<User> _user;
        private readonly IBaseCRUD<OrderItem> _item;
        private readonly DiamondStoreContext _context;

        public DeliveryService(IBaseCRUD<Delivery> repo, IBaseCRUD<Order> order, IBaseCRUD<User> user, IBaseCRUD<OrderItem> item, DiamondStoreContext diamondStoreContext)
        {
            _repo = repo;
            _order = order;
            _user = user;
            _item = item;
            _context = diamondStoreContext;
        }

        public async Task<Delivery> AddAsync(Delivery entity)
        {
            return await _repo.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<Delivery>> FindAsync(Expression<Func<Delivery, bool>> predicate)
        {
            return await _repo.FindAsync(predicate);
        }

        public async Task<IEnumerable<Delivery>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Delivery> GetByIdAsync(string id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<List<DeliveryResponse>> GetDeliveryResponsesByAdmin()
        {
            var deliveries = await _repo.GetAllAsync();
            List<DeliveryResponse> deliveryResponses = new List<DeliveryResponse>();

            foreach (var delivery in deliveries)
            {
                List<string> products = new List<string>();

                DeliveryResponse deliveryResponse = new DeliveryResponse();
                deliveryResponse.DeliveryId = delivery.DeliveryId;

                var user = await _user.GetByIdAsync(delivery.Order.UserId.ToString());
                deliveryResponse.UserName = user.Username;
                deliveryResponse.Email = user.Email;

                var manager = await _user.GetByIdAsync(delivery.ManagerId.ToString());
                deliveryResponse.ManagerName = manager.Username;

                var order = await _order.GetByIdAsync(delivery.OrderId.ToString());
                deliveryResponse.ProductPrice = order.TotalAmount;
                deliveryResponse.EndPrice = order.TotalPrice;
                deliveryResponse.OrderDate = order.OrderDate;

                var orderItem = await _item.FindAsync(x => x.OrderId == order.OrderId);

                foreach (var item in orderItem)
                {
                    products.Add(item.Product.Name);
                }

                deliveryResponse.Product = products;
                deliveryResponse.DeliveryStatus = delivery.Status;
                deliveryResponses.Add(deliveryResponse);
            }


            return deliveryResponses;
        }

        public async Task UpdateAsync(Delivery entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Delivery entity cannot be null.");
            }

            var item = await _repo.GetByIdAsync(entity.DeliveryId.ToString());

            if (item == null)
            {
                throw new KeyNotFoundException($"Delivery with ID {entity.DeliveryId} not found.");
            }

            //_context.Attach(item).State = EntityState.Modified;
            //item.Shiper = entity.Shiper ?? item.Shiper;
            //item.Status = entity.Status;
            //item.Shiper.Email = entity.Shiper.Email ?? item.Shiper.Email;
            //item.Order = entity.Order ?? item.Order;

            await _repo.UpdateAsync(item);
        }

        [HttpGet]
        public async Task<FileResult> ExportRevenue()
        {
            var deliveryResponses = await GetDeliveryResponsesByAdmin();
            var FileName = "DiamondStoreRevenue.xlsx";
            return GenerateExcel(FileName, deliveryResponses);
        }

        private FileResult GenerateExcel(string fileName, List<DeliveryResponse> deliveryResponses)
        {
            DataTable dt = new DataTable("DeliveryResponse");
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("DeliveryId"),
                new DataColumn("UserName"),
                new DataColumn("Email"),
                new DataColumn("ManagerName"),
                new DataColumn("ProductPrice"),
                new DataColumn("EndPrice"),
                new DataColumn("OrderDate"),
                new DataColumn("Product"),
                new DataColumn("DeliveryStatus")
            });

            

            foreach (var item in deliveryResponses) {
                string product = "";
                foreach (var delivery in deliveryResponses)
                {
                    product = string.Join(",", delivery.Product);
                }

                dt.Rows.Add(item.DeliveryId, item.UserName, item.Email, item.ManagerName,
                    item.ProductPrice, item.EndPrice, item.OrderDate,product, item.DeliveryStatus);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    FileContentResult file = new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = fileName
                    };
                    return file;
                }
            }
        }

    }
}
