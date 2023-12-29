using AutoMapper;
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

namespace JewsMaterial.Services.Implementations
{
    public class MaterialService : IMaterialService, IServiceMarker
    {

        private readonly IMaterialReadRepository MaterialReadRepository;
        private readonly IMaterialWriteRepository MaterialWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidator validatorService;

        public MaterialService(IMaterialReadRepository MaterialReadRepository,
            IMaterialWriteRepository MaterialWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IServiceValidator validatorService)
        {
            this.MaterialReadRepository = MaterialReadRepository;
            this.MaterialWriteRepository = MaterialWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        async Task<MaterialModel> IMaterialService.AddAsync(MaterialModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();

            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<Material>(model);
            MaterialWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<MaterialModel>(item);
        }

        async Task IMaterialService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetCinema = await MaterialReadRepository.GetByIdAsync(id, cancellationToken);

            if (targetCinema == null)
            {
                throw new EntityNtFoundException<Material>(id);
            }

            MaterialWriteRepository.Delete(targetCinema);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<MaterialModel> IMaterialService.EditAsync(MaterialModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetMaterial = await MaterialReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetMaterial == null)
            {
                throw new EntityNtFoundException<Material>(source.Id);
            }

            targetMaterial = mapper.Map<Material>(source);
            MaterialWriteRepository.Update(targetMaterial);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<MaterialModel>(targetMaterial);
        }

        async Task<IEnumerable<MaterialModel>> IMaterialService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await MaterialReadRepository.GetAllAsync(cancellationToken);
            return result.Select(bruh => mapper.Map<MaterialModel>(bruh));
        }

        async Task<MaterialModel?> IMaterialService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await MaterialReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new EntityNtFoundException<Material>(id);
            }

            return mapper.Map<MaterialModel>(item);
        }

    }
}
