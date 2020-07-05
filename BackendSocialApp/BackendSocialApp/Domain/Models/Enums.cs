using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Domain.Models
{
    public enum CoffeeFortuneTellingStatus
    {
        [Description("Yeni")]
        Draft = 0,
        [Description("Gönderildi")]
        SubmittedByUser = 1,
        [Description("Bakıldı")]
        SubmittedByFortuneTeller = 2
    }

    public enum CoffeeFortuneTellingType
    {
        [Description("Genel")]
        General = 0,
        [Description("Para")]
        Money = 1,
        [Description("İş")]
        Business = 2,
        [Description("Aşk")]
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
}
