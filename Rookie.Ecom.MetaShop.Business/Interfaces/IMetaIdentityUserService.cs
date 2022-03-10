﻿using Microsoft.AspNetCore.Identity;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Auth;
using System;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Business.Interfaces
{
    public interface IMetaIdentityUserService
    {
        Task<IdentityResult> Register(UserRegistrationDto request, string role);

        Task<MetaIdentityUserDto> GetById(string id);

    }
}
