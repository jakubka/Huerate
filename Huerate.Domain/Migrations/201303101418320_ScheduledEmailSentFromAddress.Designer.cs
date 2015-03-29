﻿/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

// <auto-generated />
namespace Huerate.Domain.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    public sealed partial class ScheduledEmailSentFromAddress : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(ScheduledEmailSentFromAddress));
        
        string IMigrationMetadata.Id
        {
            get { return "201303101418320_ScheduledEmailSentFromAddress"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
