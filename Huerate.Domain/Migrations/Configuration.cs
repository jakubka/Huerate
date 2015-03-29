/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Collections.Generic;
using Huerate.Domain.Entities;

namespace Huerate.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HuerateContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HuerateContext context)
        {
            SeedActivityTypes(context);

            //SeedEmailTemplates(context);

            SeedCommonCustomTranslations(context);
        }

        private void SeedCommonCustomTranslations(HuerateContext context)
        {
            context.CustomTranslations.AddOrUpdate(ct => new { ct.CodeName, ct.Language },
                new CustomTranslation()
                {
                    CodeName = "Form_TheWorst",
                    Language = "cs",
                    Value = "Nejhorší",
                });
            context.CustomTranslations.AddOrUpdate(ct => new { ct.CodeName, ct.Language },
                new CustomTranslation()
                {
                    CodeName = "Form_TheWorst",
                    Language = "en",
                    Value = "Worst",
                });
            context.CustomTranslations.AddOrUpdate(ct => new { ct.CodeName, ct.Language },
                new CustomTranslation()
                {
                    CodeName = "Form_TheBest",
                    Language = "cs",
                    Value = "Nejlepší",
                });
            context.CustomTranslations.AddOrUpdate(ct => new { ct.CodeName, ct.Language },
                new CustomTranslation()
                {
                    CodeName = "Form_TheBest",
                    Language = "en",
                    Value = "Best",
                });
            context.CustomTranslations.AddOrUpdate(ct => new { ct.CodeName, ct.Language },
                new CustomTranslation()
                {
                    CodeName = "Form_No",
                    Language = "cs",
                    Value = "Ne",
                });
            context.CustomTranslations.AddOrUpdate(ct => new { ct.CodeName, ct.Language },
                new CustomTranslation()
                {
                    CodeName = "Form_No",
                    Language = "en",
                    Value = "No",
                });
            context.CustomTranslations.AddOrUpdate(ct => new { ct.CodeName, ct.Language },
                new CustomTranslation()
                {
                    CodeName = "Form_Yes",
                    Language = "cs",
                    Value = "Ano",
                });
            context.CustomTranslations.AddOrUpdate(ct => new { ct.CodeName, ct.Language },
                new CustomTranslation()
                {
                    CodeName = "Form_Yes",
                    Language = "en",
                    Value = "Yes",
                });
        }

        private static void SeedEmailTemplates(HuerateContext context)
        {
            #region "LostPassword"

            context.EmailTemplates.AddOrUpdate(et => new { et.CodeName, et.Language }, new EmailTemplate()
            {
                CodeName = "LostPassword",
                Language = "cs",
                SubjectTemplate = "Nové heslo na službě huerate",
                HtmlBodyTemplate = @"<p>Dobrý den,</p>
            
            <p>nové heslo pro restauraci @ViewBag.RestaurantName je @ViewBag.NewPassword. </p>
            
            <p>Příhlásit se můžete na stránce @ViewBag.SignInPage pod Vaším emailem @ViewBag.Email a novým heslem <b>@ViewBag.NewPassword</b>.</p>
            
            <p>Děkujeme za využívání služby Huerate.cz.</p>
            
            <p>S pozdravem,</p>
            
            <p>Jakub<br />
            provozovatel</p>",
                PlainTextBodyTemplate = @"Dobrý den,
            
nové heslo pro restauraci @ViewBag.RestaurantName je @ViewBag.NewPassword. 
            
Příhlásit se můžete na stránce @ViewBag.SignInPage pod Vaším emailem @ViewBag.Email a novým heslem @ViewBag.NewPassword.
            
Děkujeme za využívání služby Huerate.cz.
            
S pozdravem,
            
Jakub
provozovatel",
            });

            #endregion


            #region "SignUp"

            context.EmailTemplates.AddOrUpdate(et => new { et.CodeName, et.Language }, new EmailTemplate()
            {
                CodeName = "SignUp",
                Language = "cs",
                SubjectTemplate = "Potvrzení registrace na službě huerate",
                HtmlBodyTemplate = @"<p>Dobrý den,</p>
                
                <p>děkujeme za registraci restaurace <b>@ViewBag.RestaurantName</b> na službě Huerate. </p>
                
                <p>Přihlásit se můžete na adrese @ViewBag.SignInPage pod Vaším emailem @ViewBag.Email a heslem, které jste si zvolili při registraci.</p>
                
                <p>V případě zapomenutí hesla si je můžete obnovit na adrese @ViewBag.LostPasswordPage.</p>
                
                <p>Rád Vám poskytnu veškerou asistenci při používání služby. Stačí se obrátit na email jakub@huerate.cz nebo telefon 739661033.</p>
                
                
                <p>S pozdravem,</p>
                
                <p>Jakub<br />
                provozovatel</p>",
                PlainTextBodyTemplate = @"Dobrý den,
                                                             
děkujeme za registraci restaurace @ViewBag.RestaurantName na službě Huerate. 
                                                             
Přihlásit se můžete na adrese @ViewBag.SignInPage pod Vaším emailem @ViewBag.Email a heslem, které jste si zvolili při registraci.
                                                             
V případě zapomenutí hesla si je můžete obnovit na adrese @ViewBag.LostPasswordPage.
                                                             
Rád Vám poskytnu veškerou asistenci při používání služby. Stačí se obrátit na email jakub@huerate.cz nebo telefon 739661033.
                                                             
                                                             
S pozdravem,
                                                             
Jakub
provozovatel",
            });

            #endregion


            #region "Invitation"

            context.EmailTemplates.AddOrUpdate(et => new { et.CodeName, et.Language }, new EmailTemplate()
            {
                CodeName = "Invitation",
                Language = "cs",
                SubjectTemplate = "Prosba o spolupráci při tvorbě diplomové práce",
                HtmlBodyTemplate = @"<p>Dobrý den,</p>
                 
                            <p>jmenuji se Jakub Kadlubiec a jsem studentem pátého ročníku Fakulty Informačních Technologií na VUT v Brně.</p>
                 
                            <p>Rád bych Vás tímto požádal o pomoc s mou diplomovou prací.</p>
                 
                            <p>Jako diplomovou práci tvořím službu pro restaurace pro sběr názorů a zpětné vazby hostů pomocí mobilních telefonů. Cílem je, aby vedení restaurace zjistilo, co hostům vadí a co se jim líbí. Na základě těchto informací je pak možné zlepšit služby v těch nejdůležitějších oblastech a tím nalákat více návštěvníků.</p>
                 
                            <p>Obracím se na Vás se dvěma prosbami:</p>
                            <ol>
                            <li>Velmi mě zajímá Váš názor jakožto potencionálního uživatele služby, a proto bych Vás rád poprosil o vyplnění krátkého dotazníku. Na základě výsledků budu dále směřovat svou práci. Vyplnění dotazníku zabere 5-10 minut. Najdete ho na adrese:
                            <a href=""https://docs.google.com/forms/d/1RlOgza42drfOLIrZ6XOSBqRcr78XScnRDd2AjtQohpI/viewform"">https://docs.google.com/forms/d/1RlOgza42drfOLIrZ6XOSBqRcr78XScnRDd2AjtQohpI/viewform</a>
                            </li> 
                            <li>Rád bych Vám nabídnul spolupráci ve formě zkušebního provozu ve Vaší restauraci zdarma. Další informace o tom, jak to vlastně funguje, a jak to můžete vyzkoušet, najdete níže v tomto mailu.</li>
                            </ol>
                 
                            <p>V systému Huerate (název mé diplomové práce) hosté odesílají zpětnou vazbu majiteli restaurace pomocí svých mobilních telefonů. V restauraci se umístí na stoly (například do jídelního lístku nebo jako samostatné letáky) www adresu. Host si tuto adresu otevře ve svém telefonu, zobrazí se mu dotazník a vyplní informace, které chce sdělit majiteli.</p>
                 
                            <p>Pokud máte jakékoliv dotazy ohledně fungování služby, tak mě neváhejte kontaktovat na jakub@huerate.cz.</p>
                 
                            <p>Pokud máte zájem o zprovoznění ve Vaší restauraci, tak se můžete zdarma zaregistrovat na stránce <a href=""http://huerate.cz/signup?ref=@ViewBag.ReceiverEmailAddress"">http://huerate.cz/signup</a> a pak postupovat podle návodu na <a href=""http://huerate.cz/help?ref=@ViewBag.ReceiverEmailAddress"">http://huerate.cz/help</a>. Všechno by mělo fungovat, ale pokud by byl přece jenom nějaký problém nebo by bylo potřeba asistence, tak mi neváhejte napsat na jakub@huerate.cz nebo zavolat na 739 661 033.</p>
                
                            <p></p> 
                            <p>Předem mockrát děkuji za spolupráci,</p>
                 
                            <p>Jakub Kadlubiec<br />
                            jakub@huerate.cz</p>",
                PlainTextBodyTemplate = @"Dobrý den,
                 
jmenuji se Jakub Kadlubiec a jsem studentem pátého ročníku Fakulty Informačních Technologií na VUT v Brně.
                 
Rád bych Vás tímto požádal o pomoc s mou diplomovou prací.
                 
Jako diplomovou práci tvořím službu pro restaurace pro sběr názorů a zpětné vazby hostů pomocí mobilních telefonů. Cílem je, aby vedení restaurace zjistilo, co hostům vadí a co se jim líbí. Na základě těchto informací je pak možné zlepšit služby v těch nejdůležitějších oblastech a tím nalákat více návštěvníků.
                 
Obracím se na Vás se dvěma prosbami:
1. Velmi mě zajímá Váš názor jakožto potencionálního uživatele služby, a proto bych Vás rád poprosil o vyplnění krátkého dotazníku. Na základě výsledků budu dále směřovat svou práci. Vyplnění dotazníku zabere 5-10 minut. Najdete ho na adrese: https://docs.google.com/forms/d/1RlOgza42drfOLIrZ6XOSBqRcr78XScnRDd2AjtQohpI/viewform

2. Rád bych Vám nabídnul spolupráci ve formě zkušebního provozu ve Vaší restauraci zdarma. Další informace o tom, jak to vlastně funguje, a jak to můžete vyzkoušet, najdete níže v tomto mailu.

                 
V systému Huerate (název mé diplomové práce) hosté odesílají zpětnou vazbu majiteli restaurace pomocí svých mobilních telefonů. V restauraci se umístí na stoly (například do jídelního lístku nebo jako samostatné letáky) www adresu. Host si tuto adresu otevře ve svém telefonu, zobrazí se mu dotazník a vyplní informace, které chce sdělit majiteli.
                 
Pokud máte jakékoliv dotazy ohledně fungování služby, tak mě neváhejte kontaktovat na jakub@huerate.cz.
                 
Pokud máte zájem o zprovoznění ve Vaší restauraci, tak se můžete zdarma zaregistrovat na stránce http://huerate.cz/signup a pak postupovat podle návodu na http://huerate.cz/help. Všechno by mělo fungovat, ale pokud by byl přece jenom nějaký problém nebo by bylo potřeba asistence, tak mi neváhejte napsat na jakub@huerate.cz nebo zavolat na 739 661 033.
                
                             
Předem mockrát děkuji za spolupráci,
                 
Jakub Kadlubiec
jakub@huerate.cz",
            });

            #endregion


            #region "Error"

            context.EmailTemplates.AddOrUpdate(et => et.CodeName, new EmailTemplate()
            {
                CodeName = "Error",
                SubjectTemplate = "Error on Huerate",
                HtmlBodyTemplate = @"<h1>Error on huerate</h1>
<p><b>Message:</b> @ViewBag.Message</p>
<p><b>Deployment:</b> @ViewBag.Deployment</p>
<p><b>Instance:</b> @ViewBag.Instance</p>
<p><b>Time:</b> @DateTime.UtcNow</p>
<p><b>Exception:</b>
@if (ViewBag.Exception == null)
{
    No exception
}
else
{
    <pre>
        @ViewBag.Exception.ToString()
    </pre>
}",
            });

            #endregion


            var scheduledEmailsWithoutTemplate = context.ScheduledOutgoingEmails.Where(e => e.EmailTemplate == null).ToList();

            if (scheduledEmailsWithoutTemplate.Count > 0)
            {
                context.SaveChanges();

                var invitationTemplate = context.EmailTemplates.Single(et => et.CodeName == "Invitation");

                scheduledEmailsWithoutTemplate.ForEach(e => e.EmailTemplate = invitationTemplate);

                context.SaveChanges();
            }
        }

        private static void SeedActivityTypes(HuerateContext context)
        {
            var activityTypes = Enum.GetValues(typeof(ActivityTypeEnum)).Cast<ActivityTypeEnum>().Select(activityTypeEnum => new ActivityType()
            {
                Id = activityTypeEnum,
                Points = 3,
                Name = activityTypeEnum.ToString(),
            }).ToArray();

            context.ActivityTypes.AddOrUpdate(at => at.Id, activityTypes);
        }
    }
}