/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Globalization;
using Huerate.Domain.Entities;

namespace Huerate.Services.DefaultData
{
    public interface IDefaultDataService
    {
        void FillNewRestauratAccount(RestaurantAccount restaurantAccount);
        TFormStep GetFormStep<TFormStep>(RestaurantAccount restaurantAccount, int? order = null) where TFormStep : FormStep;
        FormStep GetFormStep(FormStepType formStepType, RestaurantAccount restaurantAccount, int? order = null);
    }
}