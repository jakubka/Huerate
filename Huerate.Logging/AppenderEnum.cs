﻿/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;

namespace Huerate.Logging
{
    [Flags]
    public enum AppenderEnum
    {
        Console = 1,
        RollingFile = 2,
        TableStorage = 4,
    }
}