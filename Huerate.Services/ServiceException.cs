/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;

namespace Huerate.Services
{
    public class ServiceException : Exception
    {
        public ServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ServiceException(string message) : base(message)
        {
        }
    }

    public class RestaurantNotFoundServiceException : ServiceException
    {
        public RestaurantNotFoundServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public RestaurantNotFoundServiceException(string message) : base(message)
        {
        }
    }
}