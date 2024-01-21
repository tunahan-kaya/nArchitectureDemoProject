using Application.Features.Brands.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Brands.Rules;

public class BrandBusinessRules : BaseBusinessRules
{
    private readonly IBrandRepository _brandRepository;

    public BrandBusinessRules(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public Task BrandShouldExistWhenSelected(Brand? brand)
    {
        if (brand == null)
            throw new BusinessException(BrandsBusinessMessages.BrandNotExists);
        return Task.CompletedTask;
    }

    public async Task BrandIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Brand? brand = await _brandRepository.GetAsync(
            predicate: b => b.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await BrandShouldExistWhenSelected(brand);
    }
}