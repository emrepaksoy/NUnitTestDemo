﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestDemo.Services
{
    public class IdentityValidator : IIdentityValidator
    {
        public string Country => throw new NotImplementedException();

        public ICountryProvider CountryProvider => throw new NotImplementedException();

        public bool CheckConnectionToRemoteServer()
        {
            throw new NotImplementedException();
        }

        public bool IsValid(string identityNumber)
        {
            return true;
        }
    }
}
