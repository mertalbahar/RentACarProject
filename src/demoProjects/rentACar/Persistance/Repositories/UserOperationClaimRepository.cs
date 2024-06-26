﻿using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistance.Contexts;

namespace Persistance.Repositories;

public class UserOperationClaimRepository : RepositoryBase<UserOperationClaim, BaseDbContext>, IUserOperationClaimRepository
{
    public UserOperationClaimRepository(BaseDbContext context) : base(context)
    {
    }
}
