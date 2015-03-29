/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Huerate.Domain;
using Huerate.Logging;

namespace Huerate.Services
{
    internal abstract class ServiceBase
    {
        protected IUnitOfWork UnitOfWork { get; private set; }
        protected ILogService LogService { get; private set; }

        protected ServiceBase(IUnitOfWork unitOfWork, ILogService logService)
        {
            UnitOfWork = unitOfWork;
            LogService = logService;
        }
    }
}