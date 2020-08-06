using challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace challenge.Services
{
    public interface ICompensationService
    {
        Compensation GetById(String id);
        Compensation Create(Compensation compensation);
    }
}
