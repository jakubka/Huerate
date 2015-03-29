/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace Huerate.Services
{
    public static class HuerateDependencyResolver
    {
        public static T Get<T>()
        {
            return NinjectKernelFactory.GetKernel().Get<T>();
        }

        public static void SetSpecificImplementation<T>(T instance)
        {
            NinjectKernelFactory.GetKernel().Rebind<T>().ToConstant(instance);
        }
    }
}
