/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/


HP.initializer.moreinfo = function () {

    var _submitMoreInfo = function ($button) {
        var $ta = $button.parent().find("textarea");

        $.ajax({
            type: 'POST',
            url: giveTextAnswerActionUrl,
            data: {
                FormStepId: $ta.data('form-step-id'),
                Text: $ta.val(),
                AnswerSetGuid: HP.sliders.getAnswerSetGuid(),
                Language: translator.currentFormLanguage()
            },
            success: function () {
                $ta.val("");
            }
        });
        
        HP.stepsNav.forward({
            elemsToWaitFor: HP.settings.getUndefinedVal(),
            forceStep: true
        });
    };

    (function () {
        $(".moreinfobutton").click(function () {
            _submitMoreInfo($(this));
        });
    })();
};