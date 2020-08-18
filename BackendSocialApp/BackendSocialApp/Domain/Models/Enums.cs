using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Domain.Models
{
    public enum CoffeeFortuneTellingStatus
    {
        Draft = 0,
        SubmittedByUser = 1,
        SubmittedByFortuneTeller = 2
    }

    public enum CoffeeFortuneTellingType
    {
        General = 0,
        Money = 1,
        Business = 2,
        Love = 3
    }

    public enum UserStatus
    {
        Deactive = 0,
        Active = 1
    }

    public enum ConnectionStatus
    {
        Offline = 0,
        Online = 1
    }

    public enum GenderType
    {
        Male = 0,
        Female = 1,
    }
}
