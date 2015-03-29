/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Huerate.Domain;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;
using Huerate.Logging;
using Huerate.Services.Questions;

namespace Huerate.Services.NewsletterSubscriptions
{
    internal class NewsletterSubscriptionService : DataServiceBase<NewsletterSubscription, int>, INewsletterSubscriptionService
    {
        public NewsletterSubscriptionService(IUnitOfWork unitOfWork, ILogService logService) : base(unitOfWork, logService)
        {
        }
    }
}