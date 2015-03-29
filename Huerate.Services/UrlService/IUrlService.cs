﻿/*
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

namespace Huerate.Services.UrlService
{
    public interface IUrlService
    {
        string Action(string actionName, string controllerName, object routeValues = null, string protocol = null);

        string GetAbsolutePathToWebObject(string relativePath);
    }
}
