﻿using BackendSocialApp.Domain.Models;
using BackendSocialApp.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Requests
{
    public class GetActiveFortuneTellersRequest
    {
        public FortuneTellingType FortuneTellingType { get; set; }
    }
}
