using BussinessObject.Models;
using Repository.Dtos;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class Deliverymanagement : IDeliverymanagement
    {
        private readonly IDeliverManagementRepo _repo;
        

 

        public Deliverymanagement(IDeliverManagementRepo repo)
        {
            _repo = repo;
           
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

        public async Task<IEnumerable<Delivery>> GetAllAsyncShipper(Guid shipperId)
        {
            return  await _repo.GetAllAsyncShipper(shipperId);
        }

        public  async Task<Delivery> GetByIdAsync(string id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<List<User>> GetManagerList()
        {
            return await _repo.GetManagerList();
        }

        public async Task<List<Order>> GetOrderList()
        {
            return await _repo.GetOrderList();
        }

        public async Task<List<User>> GetShipperList()
        {
            return await _repo.GetShipperList();
        }

        public async Task<Delivery> UpdateAsync(Delivery entity)
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
            item.Shiper = entity.Shiper ?? item.Shiper;
            item.Status = entity.Status;
            //item.Shiper.Email = entity.Shiper.Email;
            item.Order = entity.Order ?? item.Order;

            return await _repo.UpdateAsync(item);
        }

        public async Task<Delivery> UpdateStatus(UpdateStatusByShippersDTO entity)
        {
         var delivery =   await GetByIdAsync(entity.DeliveryId.ToString());
            delivery.Status = entity.Status;
            return await _repo.UpdateAsync(delivery);
        }
    }
}
