﻿using Application.Features.Users.Queries.GetById;
using Core.CrossCuttingConcerns.Exceptions.Types;
using StarterProject.Application.Tests.Mocks.FakeDatas;
using StarterProject.Application.Tests.Mocks.Repositories;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Application.Features.Users.Queries.GetById.GetByIdUserQuery;

namespace StarterProject.Application.Tests.Features.Users.Queries.GetById;

public class GetByIdUserTests : UserMockRepository
{
    private readonly GetByIdUserQuery _query;
    private readonly GetByIdUserQueryHandler _handler;

    public GetByIdUserTests(UserFakeData fakeData, GetByIdUserQuery query)
        : base(fakeData)
    {
        _query = query;
        _handler = new GetByIdUserQueryHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    [Fact]
    public async Task GetByIdUserShouldSuccessfully()
    {
        _query.Id = 1;

        GetByIdUserResponse result = await _handler.Handle(_query, CancellationToken.None);

        Assert.Equal(expected: "example@email.com", result.Email);
    }

    [Fact]
    public async Task UserIdNotExistsShouldReturnError()
    {
        _query.Id = 6;

        async Task Action() => await _handler.Handle(_query, CancellationToken.None);

        await Assert.ThrowsAsync<BusinessException>(Action);
    }
}
