/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using Huerate.Domain;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;
using Huerate.Logging;
using Huerate.Services.DefaultData;

namespace Huerate.Services.FormSteps
{
    internal class FormStepService : DataServiceBase<IFormStepRepository, FormStep, int>, IFormStepService
    {
        private IDefaultDataService DefaultDataService { get; set; }

        public FormStepService(IDefaultDataService defaultDataService, IUnitOfWork unitOfWork, ILogService logService)
            : base(unitOfWork, logService)
        {
            DefaultDataService = defaultDataService;
        }


        public void SwapFormStepsOrder(int firstFormStepId, int secondFormStepId)
        {
            var firstFormStep = Repository.GetById(firstFormStepId);
            var secondFormStep = Repository.GetById(secondFormStepId);

            var tmp = firstFormStep.Order;
            firstFormStep.Order = secondFormStep.Order;
            secondFormStep.Order = tmp;

            Save();
        }

        public FormStep CreateFormStep(FormStepType formStepType, RestaurantAccount restaurantAccount)
        {
            var formStep = DefaultDataService.GetFormStep(formStepType, restaurantAccount);

            Create(formStep);

            return formStep;
        }
    }
}