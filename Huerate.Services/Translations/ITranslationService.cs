/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Collections.Generic;
using System.Globalization;
using Huerate.Domain.Entities;

namespace Huerate.Services.Translations
{
    public interface ITranslationService
    {
        string Translate(string resourceCodeName, string language);
        string TranslateFormat(string resourceCodeName, string language, object[] formatArguments);
        IDictionary<string, string> GetAllDefaultResources(string language);
        Dictionary<string, Dictionary<string, string>> GetAllCustomResources(int restaurantAccountId);
        void CreateOrUpdateCustomTranslation(string codeName, string value, string language, RestaurantAccount restaurantAccount);
        string CreateCustomTranslation(string language, string value, RestaurantAccount restaurantAccount);
        string CreateCustomResourcesInAllLanguages(string sourceCodeName, RestaurantAccount restaurantAccount, params object[] formatArguments);
    }
}