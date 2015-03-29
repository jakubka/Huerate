/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Globalization;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;
using Huerate.WebViewModels;
using Huerate.WebViewModels.Form;

namespace Huerate.Services.RestaurantAccounts
{
    public interface IRestaurantAccountService : IDataService<IRestaurantAccountRepository, RestaurantAccount, int>
    {
        RestaurantAccount CreateRestaurantAccount(string displayName, string email, string password, string currentLanguage);

        void ChangePasswordToGenerated(string restaurantAccountEmail);
        void ChangePassword(RestaurantAccount account, string newPassword);
        void ChangePassword(RestaurantAccount account, string newPassword, string oldPassword);
        void LockRestaurantAccount(RestaurantAccount user);
        void UnlockRestaurantAccount(RestaurantAccount user);
        bool ValidateRestaurantAccount(string codeNameOrEmail, string plainTextPassword);
        bool ValidateRestaurantAccount(string codeNameOrEmail, string plainTextPassword, out RestaurantAccount user);
        FormModel GetRestaurantAccountFormModel(string restaurantCodeName);
    }
}
