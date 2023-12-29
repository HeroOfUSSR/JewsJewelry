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
    public class WorkshopService : IWorkshopService, IServiceMarker
    {

        private readonly IWorkshopReadRepository WorkshopReadRepository;
        private readonly IWorkshopWriteRepository WorkshopWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidator validatorService;

        public WorkshopService(IWorkshopReadRepository WorkshopReadRepository,
            IWorkshopWriteRepository WorkshopWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IServiceValidator validatorService)
        {
            this.WorkshopReadRepository = WorkshopReadRepository;
            this.WorkshopWriteRepository = WorkshopWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        async Task<WorkshopModel> IWorkshopService.AddAsync(WorkshopModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();

            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<Workshop>(model);
            WorkshopWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<WorkshopModel>(item);
        }

        async Task IWorkshopService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetCinema = await WorkshopReadRepository.GetByIdAsync(id, cancellationToken);

            if (targetCinema == null)
            {
                throw new EntityNtFoundException<Workshop>(id);
            }

            WorkshopWriteRepository.Delete(targetCinema);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<WorkshopModel> IWorkshopService.EditAsync(WorkshopModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetWorkshop = await WorkshopReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetWorkshop == null)
            {
                throw new EntityNtFoundException<Workshop>(source.Id);
            }

            targetWorkshop = mapper.Map<Workshop>(source);
            WorkshopWriteRepository.Update(targetWorkshop);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<WorkshopModel>(targetWorkshop);
        }

        async Task<IEnumerable<WorkshopModel>> IWorkshopService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await WorkshopReadRepository.GetAllAsync(cancellationToken);
            return result.Select(bruh => mapper.Map<WorkshopModel>(bruh));
        }

        async Task<WorkshopModel?> IWorkshopService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await WorkshopReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new EntityNtFoundException<Workshop>(id);
            }

            return mapper.Map<WorkshopModel>(item);
        }

    }
}
