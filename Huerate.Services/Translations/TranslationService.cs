/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Huerate.Domain;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;
using Huerate.Logging;

namespace Huerate.Services.Translations
{
    internal class TranslationService : DataServiceBase<ICustomTranslationRepository, CustomTranslation, int>, ITranslationService
    {
        private class CustomTranslationService
        {
            private readonly IDictionary<string, Dictionary<string, string>> _languageDictionaryCache;

            public CustomTranslationService(IDictionary<string, Dictionary<string, string>> customTranslations)
            {
                _languageDictionaryCache = customTranslations;
            }

            private IDictionary<string, string> GetLanguageDictionary(string languageString)
            {
                if (!_languageDictionaryCache.ContainsKey(languageString))
                {
                    _languageDictionaryCache[languageString] = new Dictionary<string, string>();
                }

                return _languageDictionaryCache[languageString];
            }

            public string Translate(string language, string codeName)
            {
                string result;
                if (GetLanguageDictionary(language).TryGetValue(codeName, out result))
                {
                    return result;
                }

                return null;
            }

            public void Add(CustomTranslation customTranslation)
            {
                GetLanguageDictionary(customTranslation.Language)[customTranslation.CodeName] = customTranslation.Value;
            }
        }

        private static CustomTranslationService _customTranslationService;

        private CustomTranslationService GetCustomTranslationService()
        {
            return _customTranslationService ?? (_customTranslationService = new CustomTranslationService(Repository.GetAllByLanguageAndCodeName()));
        }

        public TranslationService(IUnitOfWork unitOfWork, ILogService logService) : base(unitOfWork, logService)
        {
        }

        public string Translate(string resourceCodeName, string language)
        {
            string customTranslation = GetCustomTranslationService().Translate(language, resourceCodeName);

            if (customTranslation != null)
            {
                return customTranslation;
            }

            return WebUI.Resources.Translations.ResourceManager.GetString(resourceCodeName, new CultureInfo(language)) ?? string.Format("XXX{0}XXX", resourceCodeName);
        }

        public string TranslateFormat(string resourceCodeName, string language, object[] formatArguments)
        {
            return string.Format(Translate(resourceCodeName, language), formatArguments);
        }

        public IDictionary<string, string> GetAllDefaultResources(string language)
        {
            return WebUI.Resources.Translations.ResourceManager.GetResourceSet(new CultureInfo(language), true, true).Cast<DictionaryEntry>().ToDictionary(i => i.Key.ToString(), i => i.Value.ToString());
        }

        public Dictionary<string, Dictionary<string, string>> GetAllCustomResources(int restaurantAccountId)
        {
            return Repository.GetAllByRestaurant(restaurantAccountId).GroupBy(cr => cr.Language).ToDictionary(g => g.Key, g => g.ToDictionary(cr => cr.CodeName, cr => cr.Value));
        }

        public void CreateOrUpdateCustomTranslation(string codeName, string value, string language, RestaurantAccount restaurantAccount)
        {
            var customTranslation = Repository.GetByLanguageAndCodeName(language, codeName);

            if (customTranslation != null)
            {
                if (customTranslation.RestaurantAccountId != restaurantAccount.Id)
                {
                    throw new ServiceException("Owning restaurant account does not match");
                }
                customTranslation.Value = value;
                GetCustomTranslationService().Add(customTranslation);
            }
            else
            {
                AddCustomTranslation(codeName, value, language, restaurantAccount);
            }

            UnitOfWork.Save();
        }

        private void AddCustomTranslation(string codeName, string value, string language, RestaurantAccount restaurantAccount)
        {
            var customTranslation = new CustomTranslation()
                                        {
                                            CodeName = codeName,
                                            Language = language,
                                            Value = value,
                                            RestaurantAccountId = restaurantAccount.Id,
                                        };
            Repository.Add(customTranslation);

            GetCustomTranslationService().Add(customTranslation);
        }

        public string CreateCustomTranslation(string language, string value, RestaurantAccount restaurantAccount)
        {
            string codeName = Guid.NewGuid().ToString();

            AddCustomTranslation(codeName, value, language, restaurantAccount);

            UnitOfWork.Save();

            return codeName;
        }

        public string CreateCustomResourcesInAllLanguages(string sourceCodeName, RestaurantAccount restaurantAccount, params object[] formatArguments)
        {
            string customResourceCodeName = Guid.NewGuid().ToString();

            foreach (var language in restaurantAccount.LanguagesSet)
            {
                AddCustomTranslation(customResourceCodeName, TranslateFormat(sourceCodeName, language, formatArguments), language, restaurantAccount);
            }

            return customResourceCodeName;
        }
    }
}