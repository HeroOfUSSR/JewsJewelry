using AutoMapper;
using JewsJewelry.Common.Entity.DBInterface;
using JewsJewelry.Context.Contracts.Models;
using JewsJewelry.Repositories.Contracts.Interface;
using JewsJewelry.Repositories.Contracts.WriteReposInterface;
using JewsJewelry.Services.Contracts.Exceptions;
using JewsJewelry.Services.Contracts.Interface;
using JewsJewelry.Services.Contracts.Models;
using JewsJewelry.Services.Markers;
using JewsJewelry.Services.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Implementations
{
    public class CustomerService : ICustomerService, IServiceMarker
    {

        private readonly ICustomerReadRepository CustomerReadRepository;
        private readonly ICustomerWriteRepository CustomerWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidator validatorService;

        public CustomerService(ICustomerReadRepository CustomerReadRepository,
            ICustomerWriteRepository CustomerWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IServiceValidator validatorService)
        {
            this.CustomerReadRepository = CustomerReadRepository;
            this.CustomerWriteRepository = CustomerWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        async Task<CustomerModel> ICustomerService.AddAsync(CustomerModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();

            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<Customer>(model);
            CustomerWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<CustomerModel>(item);
        }

        async Task ICustomerService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetCinema = await CustomerReadRepository.GetByIdAsync(id, cancellationToken);

            if (targetCinema == null)
            {
                throw new EntityNtFoundException<Customer>(id);
            }

            CustomerWriteRepository.Delete(targetCinema);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<CustomerModel> ICustomerService.EditAsync(CustomerModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetCustomer = await CustomerReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetCustomer == null)
            {
                throw new EntityNtFoundException<Customer>(source.Id);
            }

            targetCustomer = mapper.Map<Customer>(source);
            CustomerWriteRepository.Update(targetCustomer);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<CustomerModel>(targetCustomer);
        }

        async Task<IEnumerable<CustomerModel>> ICustomerService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await CustomerReadRepository.GetAllAsync(cancellationToken);
            return result.Select(bruh => mapper.Map<CustomerModel>(bruh));
        }

        async Task<CustomerModel?> ICustomerService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await CustomerReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new EntityNtFoundException<Customer>(id);
            }

            return mapper.Map<CustomerModel>(item);
        }

    }
}
