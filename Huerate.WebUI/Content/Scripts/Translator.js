/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

function Translator(classicTranslations, customTranslations, currentLanguage) {
    var self = this;

    self.currentLanguage = currentLanguage;
    self.currentFormLanguage = ko.observable(currentLanguage);
    self.classicTranslations = classicTranslations;
    self.customTranslations = customTranslations;

    self.tr = function(resourceCodeName) {
        var translation = self.classicTranslations[self.currentLanguage][resourceCodeName];

        if (!translation) {
            return 'XXX' + resourceCodeName + 'XXX';
        }

        return translation;
    };

    self.nativeLanguageNames =
    {
        cs: 'Čeština',
        en: 'English',
        de: 'Deutsch',
        fr: 'Français',
        pl: 'Polski',
        sk: 'Slovenčina',
        ru: 'Pyccĸий'
    };

    self.getLanguageName = function(languageCode) {
        if (self.nativeLanguageNames[languageCode]) {
            return self.nativeLanguageNames[languageCode];
        }

        return languageCode;
    };

    self.getCustomValue = function(resourceCodeName) {
        var cul = self.currentFormLanguage();

        if (self.customTranslations[cul] && self.customTranslations[cul][resourceCodeName]) {
            return self.customTranslations[cul][resourceCodeName];
        }

        return '';
    };

    self.setCustomValue = function(resourceCodeName, value) {
        self.customTranslations[self.currentFormLanguage()][resourceCodeName] = value;
    };
}