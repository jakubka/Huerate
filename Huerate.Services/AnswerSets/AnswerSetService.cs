/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Huerate.Domain;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;
using Huerate.Logging;
using Huerate.WebViewModels;
using Huerate.WebViewModels.Admin;

namespace Huerate.Services.AnswerSets
{
    internal class AnswerSetService : DataServiceBase<IAnswerSetRepository, AnswerSet, Guid>, IAnswerSetService
    {
        public AnswerSetService(IUnitOfWork unitOfWork, ILogService logService)
            : base(unitOfWork, logService)
        {
        }

        public AnswerSet GetOrCreateAnswerSet(Guid guid, int restaurantAccountId)
        {
            AnswerSet answerSet = Repository.GetByGuid(guid, restaurantAccountId);

            if (answerSet == null)
            {
                answerSet = new AnswerSet
                                {
                                    Id = guid,
                                    Created = DateTime.UtcNow,
                                    RestaurantAccountId = restaurantAccountId,
                                };

                Create(answerSet);
            }

            return answerSet;
        }
    }
}