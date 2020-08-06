using challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace challenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation GetById(String id);
        Compensation Add(Compensation compensation); 
        Task SaveAsync();
    }
}
