/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Security.Cryptography;
using System.Text;
using Huerate.Domain;
using Huerate.Domain.Entities;
using Huerate.Logging;
using Huerate.Services.Names;

namespace Huerate.Services.Membership
{
    internal class MembershipService : ServiceBase, IMembershipService
    {
        private INameService NameService { get; set; }
        private IMembershipPersistenceService MembershipPersistenceService { get; set; }

        public MembershipService(INameService nameService, IMembershipPersistenceService membershipPersistenceService, IUnitOfWork unitOfWork, ILogService logService) : base(unitOfWork, logService)
        {
            NameService = nameService;
            MembershipPersistenceService = membershipPersistenceService;
        }


        public void SignIn(RestaurantAccount restaurantAccount, bool rememberMe = false)
        {
            restaurantAccount.LastLoginDate = DateTime.UtcNow;

            UnitOfWork.Save();

            MembershipPersistenceService.StoreUserId(restaurantAccount.Id, rememberMe);

            LogService.DebugExecution("Login, id " + restaurantAccount.Id);
        }

        public void SignOut()
        {
            MembershipPersistenceService.Clear();

            LogService.DebugExecution("Logout");
        }


        public RestaurantAccount CurrentRestaurantAccount
        {
            get
            {
                int? id = MembershipPersistenceService.GetStoredUserId();

                if (id == null)
                {
                    return null;
                }

                var restaurantAccount = UnitOfWork.RestaurantAccountRepository.GetById(id.Value);

                if (restaurantAccount == null || restaurantAccount.IsLockedOut)
                {
                    return null;
                }

                return restaurantAccount;
            }
        }

        public bool IsRestaurantAccountOnline
        {
            get { return CurrentRestaurantAccount != null; }
        }
    }
}