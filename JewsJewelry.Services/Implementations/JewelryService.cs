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
    public class JewelryService : IJewelryService, IServiceMarker
    {

        private readonly IJewelryReadRepository JewelryReadRepository;
        private readonly IJewelryWriteRepository JewelryWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidator validatorService;

        public JewelryService(IJewelryReadRepository JewelryReadRepository,
            IJewelryWriteRepository JewelryWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IServiceValidator validatorService)
        {
            this.JewelryReadRepository = JewelryReadRepository;
            this.JewelryWriteRepository = JewelryWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        async Task<JewelryModel> IJewelryService.AddAsync(JewelryModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();

            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<Jewelry>(model);
            JewelryWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<JewelryModel>(item);
        }

        async Task IJewelryService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetCinema = await JewelryReadRepository.GetByIdAsync(id, cancellationToken);

            if (targetCinema == null)
            {
                throw new EntityNtFoundException<Jewelry>(id);
            }

            JewelryWriteRepository.Delete(targetCinema);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<JewelryModel> IJewelryService.EditAsync(JewelryModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetJewelry = await JewelryReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetJewelry == null)
            {
                throw new EntityNtFoundException<Jewelry>(source.Id);
            }

            targetJewelry = mapper.Map<Jewelry>(source);
            JewelryWriteRepository.Update(targetJewelry);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<JewelryModel>(targetJewelry);
        }

        async Task<IEnumerable<JewelryModel>> IJewelryService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await JewelryReadRepository.GetAllAsync(cancellationToken);
            return result.Select(bruh => mapper.Map<JewelryModel>(bruh));
        }

        async Task<JewelryModel?> IJewelryService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await JewelryReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new EntityNtFoundException<Jewelry>(id);
            }

            return mapper.Map<JewelryModel>(item);
        }

    }
}
