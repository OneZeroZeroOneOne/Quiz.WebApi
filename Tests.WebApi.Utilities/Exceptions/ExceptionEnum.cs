﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.WebApi.Utilities.Exceptions
{
    public enum ExceptionEnum
    {
        InvalidCredentials = 1,
        SecurityKeyIsNull = 2,
        AuthorizationHeaderNotExist = 3,
        Unauthorized = 4,
        EmployeeIsNotYours = 5,
        EmployeeNotFound = 6,
    }
}