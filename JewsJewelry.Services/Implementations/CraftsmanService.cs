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
    public class CraftsmanService : ICraftsmanService, IServiceMarker
    {

        private readonly ICraftsmanReadRepository craftsmanReadRepository;
        private readonly ICraftsmanWriteRepository craftsmanWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidator validatorService;

        public CraftsmanService(ICraftsmanReadRepository craftsmanReadRepository, 
            ICraftsmanWriteRepository craftsmanWriteRepository, 
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IServiceValidator validatorService)
        {
            this.craftsmanReadRepository = craftsmanReadRepository;
            this.craftsmanWriteRepository = craftsmanWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        async Task<CraftsmanModel> ICraftsmanService.AddAsync(CraftsmanModel model, CancellationToken cancellationToken)
        {
            model.Id= Guid.NewGuid();

            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<Craftsman>(model);
            craftsmanWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<CraftsmanModel>(item);
        }

        async Task ICraftsmanService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetCinema = await craftsmanReadRepository.GetByIdAsync(id, cancellationToken);

            if (targetCinema == null)
            {
                throw new EntityNtFoundException<Craftsman>(id);
            }

            craftsmanWriteRepository.Delete(targetCinema);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<CraftsmanModel> ICraftsmanService.EditAsync(CraftsmanModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetCraftsman = await craftsmanReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetCraftsman == null)
            {
                throw new EntityNtFoundException<Craftsman>(source.Id);
            }

            targetCraftsman = mapper.Map<Craftsman>(source);
            craftsmanWriteRepository.Update(targetCraftsman);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<CraftsmanModel>(targetCraftsman);
        }

        async Task<IEnumerable<CraftsmanModel>> ICraftsmanService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await craftsmanReadRepository.GetAllAsync(cancellationToken);
            return result.Select(bruh => mapper.Map<CraftsmanModel>(bruh));
        }

        async Task<CraftsmanModel?> ICraftsmanService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await craftsmanReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new EntityNtFoundException<Craftsman>(id);
            }

            return mapper.Map<CraftsmanModel>(item);
        }

    }
}
