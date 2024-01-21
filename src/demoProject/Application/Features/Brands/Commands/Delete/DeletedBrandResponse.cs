using Core.Application.Responses;

namespace Application.Features.Brands.Commands.Delete;

public class DeletedBrandResponse : IResponse
{
    public Guid Id { get; set; }
}