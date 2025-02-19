﻿using BookManagement.Domain.Entities;

namespace BookManagement.Application.Identity.Services;

public interface IPasswordGeneratorService
{
    string GeneratePassword();

    string GetValidatedPassword(string password, User user);
}