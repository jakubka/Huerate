/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

function SetTranslation(codeName, value, language) {
    return $.post(actions.setTranslation, { codeName: codeName, value: value, language: language }, null, "json")
        .fail(function(data) {
            alert("Error: " + data.responseText);
        });
}

function SetLanguages(languages) {
    return $.ajax({
        type: "POST",
        url: actions.setLanguages,
        data: { languages: languages },
        traditional: true
    }).fail(function(data) {
        alert("Error: " + data.responseText);
    });
}

function GetCustomTranslations() {
    return $.get(actions.getCustomTranslations, null, null, "json")
        .fail(function(data) {
            alert("Error: " + data.responseText);
        });
}

function CreateQuestion(formStepId) {
    return $.post(actions.createQuestion, { formStepId: formStepId })
        .fail(function(data) {
            alert("Error: " + data.responseText);
        });
}

function CreateFormStep(formStepType) {
    return $.post(actions.createFormStep, { formStepType: formStepType })
        .fail(function(data) {
            alert("Error: " + data.result);
        });
}

function UpdateNumberOfDisplayedQuestions(formStepId, numberOfDisplayedQuestions) {
    return $.post(actions.updateNumberOfDisplayedQuestions, { formStepId: formStepId, numberOfDisplayedQuestions: numberOfDisplayedQuestions })
        .fail(function(data) {
            alert("Error: " + data.responseText);
        });
}

function DeleteQuestion(questionId) {
    return $.post(actions.deleteQuestion, { questionId: questionId })
        .fail(function(data) {
            alert("Error: " + data.responseText);
        });
}

function DeleteFormStep(formStepId) {
    return $.post(actions.deleteFormStep, { formStepId: formStepId })
        .fail(function(data) {
            alert("Error: " + data.responseText);
        });
}

function SwapFormStepsOrder(firstFormStepId, secondFormStepId) {
    return $.post(actions.swapFormStepsOrder, { firstFormStepId: firstFormStepId, secondFormStepId: secondFormStepId })
        .fail(function(data) {
            alert("Error: " + data.responseText);
        });
}

function SwapQuestionsOrder(firstQuestionId, secondQuestionId) {
    return $.post(actions.swapQuestionsOrder, { firstQuestionId: firstQuestionId, secondQuestionId: secondQuestionId })
        .fail(function(data) {
            alert("Error: " + data.responseText);
        });
}

function CustomTranslation(resourceCodeName, translator) {
    var self = this;

    self.tr = ko.observable(translator.getCustomValue(resourceCodeName));

    translator.currentFormLanguage.subscribe(function() {
        self.tr(translator.getCustomValue(resourceCodeName));
    });

    self.save = function() {
        if (translator.getCustomValue(resourceCodeName) != self.tr()) {
            return SetTranslation(resourceCodeName, self.tr(), translator.currentFormLanguage()).then(function() {
                translator.setCustomValue(resourceCodeName, self.tr());
            });
        }
        return jQuery.Deferred().resolve("OK").promise();
    };

    self.reset = function() {
        self.tr(translator.getCustomValue(resourceCodeName));
    };
}

function QuestionModel(questionAspModel) {
    var self = this;
    self.questionId = questionAspModel.QuestionId;
    self.text = new CustomTranslation(questionAspModel.QuestionText, translator);

    self.isLoading = ko.observable(false);

    self.save = function() {
        return self.text.save();
    };
}

function FormStepModel(formStepAspModel, translator) {
    var self = this;

    self.translator = translator;

    self.formStepId = formStepAspModel.FormStepId;
    self.formStepType = formStepAspModel.FormStepTypeString;

    self.displayName = translator.tr('FormStepType_' + self.formStepType);
    self.description = translator.tr('FormStepType_' + self.formStepType + '_Description');

    if (formStepAspModel.hasOwnProperty('DisplayQuestionsCount')) {
        self.displayQuestionsCount = ko.observable(formStepAspModel.DisplayQuestionsCount);
    }
    if (formStepAspModel.PercentQuestions) {
        self.questions = ko.observableArray(ko.utils.arrayMap(formStepAspModel.PercentQuestions, function(q) {
            return new QuestionModel(q);
        }));
    } else if (formStepAspModel.YesNoQuestions) {
        self.questions = ko.observableArray(ko.utils.arrayMap(formStepAspModel.YesNoQuestions, function(q) {
            return new QuestionModel(q);
        }));
    }
    if (formStepAspModel.hasOwnProperty('TitleText')) {
        self.title = new CustomTranslation(formStepAspModel.TitleText, translator);
    }
    if (formStepAspModel.hasOwnProperty('InfoText')) {
        self.info = new CustomTranslation(formStepAspModel.InfoText, translator);
    }
    if (formStepAspModel.hasOwnProperty('SubmitButtonText')) {
        self.submitButton = new CustomTranslation(formStepAspModel.SubmitButtonText, translator);
    }

    self.deleteQuestion = function(question) {
        question.isLoading(true);
        if (!confirm(translator.tr('Admin_ConfirmDeleteQuestion'))) {
            question.isLoading(false);
            return;
        }
        DeleteQuestion(question.questionId).done(function() {
            self.questions.remove(question);
            alert(translator.tr('Admin_QuestionDeleted'));
        }).always(function() {
            question.isLoading(false);
        });
    };
    self.swapQuestions = function(firstQuestionIndex, secondQuestionIndex) {
        if (firstQuestionIndex > secondQuestionIndex) {
            var tmp = firstQuestionIndex;
            firstQuestionIndex = secondQuestionIndex;
            secondQuestionIndex = tmp;
        }

        var firstQuestion = self.questions()[firstQuestionIndex];
        var secondQuestion = self.questions()[secondQuestionIndex];
        ;

        firstQuestion.isLoading(true);
        secondQuestion.isLoading(true);
        SwapQuestionsOrder(firstQuestion.questionId, secondQuestion.questionId).always(function() {
            self.questions.replace(secondQuestion, firstQuestion);
            self.questions.replace(firstQuestion, secondQuestion);
            firstQuestion.isLoading(false);
            secondQuestion.isLoading(false);
        });
    };

    self.moveUpQuestion = function(question) {
        var sourceIndex = self.questions.indexOf(question);
        if (sourceIndex <= 0) {
            return;
        }

        self.swapQuestions(sourceIndex, sourceIndex - 1);
    };

    self.moveDownQuestion = function(question) {
        var sourceIndex = self.questions.indexOf(question);
        if (sourceIndex >= self.questions().length - 1) {
            return;
        }

        self.swapQuestions(sourceIndex, sourceIndex + 1);
    };

    self.isLoading = ko.observable(false);

    self.isCreatingQuestion = ko.observable(false);

    self.createQuestion = function() {
        if (!self.hasOwnProperty('questions')) {
            return;
        }

        self.isCreatingQuestion(true);
        CreateQuestion(self.formStepId).done(function(data) {
            self.questions.push(new QuestionModel(data));
        }).always(function() {
            self.isCreatingQuestion(false);
        });
    };

    self.saveAll = function() {
        var defferrers = [];

        self.isLoading(true);

        $.each(self, function(i, member) {
            if ((typeof member) === "object" && member.hasOwnProperty('save')) {
                defferrers.push(member.save());
            }
        });

        if (self.questions) {
            $.each(self.questions(), function(i, member) {
                if ((typeof member) === "object" && member.hasOwnProperty('save')) {
                    defferrers.push(member.save());
                }
            });
        }

        if (self.hasOwnProperty('displayQuestionsCount')) {
            defferrers.push(UpdateNumberOfDisplayedQuestions(self.formStepId, self.displayQuestionsCount()));
        }

        $.when.apply($, defferrers).done(function() {
            alert(self.translator.tr('Admin_FormStepChanged'));
        }).fail(function() {
            alert(self.translator.tr('Admin_FormStepErrorOnSave'));
        }).always(function() {
            self.isLoading(false);
        });
    };

    self.getTemplateName = function() {
        switch (self.formStepType) {
        case "PercentQuestions":
            return "percent-questions-form-step-template";
        case "YesNoQuestions":
            return "yes-no-questions-form-step-template";
        case "TextQuestion":
            return "text-question-form-step-template";
        case "TextInfo":
            return "text-info-form-step-template";
        case "Confirmation":
            return "confirmation-form-step-template";
        case "LanguageSelection":
            return "language-selection-form-step-template";
        }
        return null;
    };
}


function EditFormModel(aspModel, translator, actions) {
    var self = this;
    self.translator = translator;
    self.actions = actions;
    self.languages = ko.observableArray(aspModel.Languages);
    self.formSteps = ko.observableArray(ko.utils.arrayMap(aspModel.FormSteps, function(formStepAspModel) {
        return new FormStepModel(formStepAspModel, translator);
    }));

    self.newFormStepType = ko.observable();
    self.formStepTypes = aspModel.FormStepTypes;
    self.isCreatingFormStep = ko.observable(false);

    self.languages.subscribe(function() {
        if (self.languages.indexOf(self.translator.currentFormLanguage()) < 0) {
            self.translator.currentFormLanguage(self.languages()[0]);
        }
    });

    self.createFormStep = function() {
        self.isCreatingFormStep(true);
        CreateFormStep(self.newFormStepType).done(function(formStep) {
            GetCustomTranslations().done(function(customTranslations) {
                self.translator.customTranslations = customTranslations;
                self.formSteps.push(new FormStepModel(formStep, self.translator));
            }).always(function() {
                self.isCreatingFormStep(false);
            });
        }).fail(function() {
            self.isCreatingFormStep(false);
        });
    };

    self.swapFormSteps = function(firstFormStepIndex, secondFormStepIndex) {
        if (firstFormStepIndex > secondFormStepIndex) {
            var tmp = firstFormStepIndex;
            firstFormStepIndex = secondFormStepIndex;
            secondFormStepIndex = tmp;
        }

        var firstFormStep = self.formSteps()[firstFormStepIndex];
        var secondFormStep = self.formSteps()[secondFormStepIndex];
        ;

        firstFormStep.isLoading(true);
        secondFormStep.isLoading(true);
        SwapFormStepsOrder(firstFormStep.formStepId, secondFormStep.formStepId).always(function() {
            self.formSteps.replace(secondFormStep, firstFormStep);
            self.formSteps.replace(firstFormStep, secondFormStep);
            firstFormStep.isLoading(false);
            secondFormStep.isLoading(false);
        });
    };

    self.moveUpFormStep = function(sourceFormStep) {
        var sourceIndex = self.formSteps.indexOf(sourceFormStep);
        if (sourceIndex <= 0) {
            return;
        }

        self.swapFormSteps(sourceIndex, sourceIndex - 1);
    };

    self.moveDownFormStep = function(sourceFormStep) {
        var sourceIndex = self.formSteps.indexOf(sourceFormStep);
        if (sourceIndex >= self.formSteps().length - 1) {
            return;
        }

        self.swapFormSteps(sourceIndex, sourceIndex + 1);
    };

    self.deleteFormStep = function(formStep) {
        if (!confirm(translator.tr('Admin_ConfirmDeleteFormStep'))) {
            return;
        }
        formStep.isLoading(true);
        DeleteFormStep(formStep.formStepId).done(function() {
            formStep.isLoading(false);
            self.formSteps.remove(formStep);
        });
    };

    self.getAvailableLanguages = function() {
        return ['cs', 'en', 'de', 'fr', 'pl', 'sk', 'ru'].filter(function(e) {
            return self.languages().indexOf(e) < 0;
        });
    };

    self.newLanguage = ko.observable();

    self.availableLanguages = ko.observableArray(self.getAvailableLanguages());

    self.availableLanguages.subscribe(function() {
        self.newLanguage(undefined);
    });
    
    self.languagesInEditMode = ko.observableArray(self.languages().slice(0));

    self.isChangingLanguage = ko.observable(false);

    self.translator.currentFormLanguage.subscribe(function () {
        self.isChangingLanguage(true);

        setTimeout(function() {
            self.isChangingLanguage(false);
        }, 500);
    });

    self.addLanguage = function() {
        self.languagesInEditMode.push(self.newLanguage());
        self.availableLanguages.remove(self.newLanguage());
    };

    self.deleteLanguage = function(language) {
        self.languagesInEditMode.remove(language);
        self.availableLanguages.push(language);
    };

    self.initEditing = function() {
        self.languagesInEditMode(self.languages().slice(0));
        self.availableLanguages(self.getAvailableLanguages());
    };

    self.isSavingLanguages = ko.observable(false);

    self.saveEditedLanguages = function() {
        self.isSavingLanguages(true);
        return SetLanguages(self.languagesInEditMode()).done(function() {
            self.languages(self.languagesInEditMode().slice());
        }).always(function() {
            self.isSavingLanguages(false);
        });
    };

    self.cancelEditingLanguages = function() {
        self.languagesInEditMode(self.languages().slice(0));
    };
}