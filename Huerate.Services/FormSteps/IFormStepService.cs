/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;

namespace Huerate.Services.FormSteps
{
    public interface IFormStepService : IDataService<IFormStepRepository, FormStep, int>
    {
        void SwapFormStepsOrder(int firstFormStepId, int secondFormStepId);
        FormStep CreateFormStep(FormStepType formStepType, RestaurantAccount restaurantAccount);
    }
}
