using AutoMapper;
using JewsJewelry.Common.Entity.DBInterface;
using JewsJewelry.Context.Contracts.Models;
using JewsJewelry.Repositories.Contracts.Interface;
using JewsJewelry.Repositories.Contracts.WriteReposInterface;
using JewsJewelry.Services.Contracts.Exceptions;
using JewsJewelry.Services.Contracts.Interface;
using JewsJewelry.Services.Contracts.Models;
using JewsJewelry.Services.Contracts.ModelsRequest;
using JewsJewelry.Services.Markers;
using JewsJewelry.Services.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Implementations
{
    public class OrderService : IOrderService, IServiceMarker
    {

        private readonly IOrderReadRepository OrderReadRepository;
        private readonly IOrderWriteRepository OrderWriteRepository;
        private readonly IJewelryReadRepository jewelryReadRepository;
        private readonly ICustomerReadRepository customerReadRepository;
        private readonly IWorkshopReadRepository workshopReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidator validatorService;

        public OrderService(IOrderReadRepository OrderReadRepository,
            IOrderWriteRepository OrderWriteRepository,
            IJewelryReadRepository jewelryReadRepository,
            ICustomerReadRepository customerReadRepository,
            IWorkshopReadRepository workshopReadRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IServiceValidator validatorService)
        {
            this.OrderReadRepository = OrderReadRepository;
            this.OrderWriteRepository = OrderWriteRepository;
            this.jewelryReadRepository= jewelryReadRepository;
            this.customerReadRepository = customerReadRepository;
            this.workshopReadRepository = workshopReadRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        async Task<OrderModel> IOrderService.AddAsync(OrderRequestModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();

            await validatorService.ValidateAsync(model, cancellationToken);

            var order = mapper.Map<Order>(model);
            OrderWriteRepository.Add(order);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return await GetTicketModelOnMapping(order, cancellationToken);
        }

        async Task IOrderService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetOrder = await OrderReadRepository.GetByIdAsync(id, cancellationToken);

            if (targetOrder == null)
            {
                throw new EntityNtFoundException<Order>(id);
            }

            OrderWriteRepository.Delete(targetOrder);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<OrderModel> IOrderService.EditAsync(OrderRequestModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var order = await OrderReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (order == null)
            {
                throw new EntityNtFoundException<Order>(source.Id);
            }

            order = mapper.Map<Order>(source);
            OrderWriteRepository.Update(order);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return await GetTicketModelOnMapping(order, cancellationToken);
        }

        async Task<IEnumerable<OrderModel>> IOrderService.GetAllAsync(CancellationToken cancellationToken)
        {
            var orders = await OrderReadRepository.GetAllAsync(cancellationToken);
            
            var jewelries = await jewelryReadRepository
                .GetByIdsAsync(orders.Select(x => x.JewelryId).Distinct(), cancellationToken);

            var customers = await customerReadRepository
                .GetByIdsAsync(orders.Select(x => x.CustomerId).Distinct(), cancellationToken);

            var workshops = await workshopReadRepository
                .GetByIdsAsync(orders.Select(x => x.WorkshopId).Distinct(), cancellationToken);

            var result = new List<OrderModel>();

            foreach (var order in orders)
            {
                if (!jewelries.TryGetValue(order.JewelryId, out var jewelry) ||
                !customers.TryGetValue(order.CustomerId, out var customer) ||
                !workshops.TryGetValue(order.WorkshopId, out var workshop))
                {
                    continue;
                }
                else
                {
                    var orderModel = mapper.Map<OrderModel>(order);

                    orderModel.Jewelry = mapper.Map<JewelryModel>(jewelry);
                    orderModel.Customer = mapper.Map<CustomerModel>(customer);
                    orderModel.Workshop = mapper.Map<WorkshopModel>(workshop);

                    result.Add(orderModel);
                }
            }

            return result;
        }

        async Task<OrderModel?> IOrderService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await OrderReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new EntityNtFoundException<Order>(id);
            }

            return mapper.Map<OrderModel>(item);
        }

        async private Task<OrderModel> GetTicketModelOnMapping(Order order, CancellationToken cancellationToken)
        {
            var orderModel = mapper.Map<OrderModel>(order);
            orderModel.Jewelry = mapper.Map<JewelryModel>(await OrderReadRepository.GetByIdAsync(order.JewelryId, cancellationToken));
            orderModel.Customer = mapper.Map<CustomerModel>(await customerReadRepository.GetByIdAsync(order.CustomerId, cancellationToken));
            orderModel.Workshop = mapper.Map<WorkshopModel>(await workshopReadRepository.GetByIdAsync(order.WorkshopId, cancellationToken));

            return orderModel;
        }
    }
}
