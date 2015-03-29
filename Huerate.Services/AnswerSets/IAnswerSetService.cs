/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;
using Huerate.WebViewModels;
using Huerate.WebViewModels.Admin;

namespace Huerate.Services.AnswerSets
{
    public interface IAnswerSetService : IDataService<IAnswerSetRepository, AnswerSet, Guid>
    {
        AnswerSet GetOrCreateAnswerSet(Guid guid, int restaurantAccountId);
    }
}
