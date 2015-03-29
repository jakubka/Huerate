/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/


function ToEditMode($questionContainerDiv) {
    AllToReadMode();

    var $input = $questionContainerDiv.children("input");

    $input.attr("readonly", null);
    $input.removeClass("read-mode");
    $input.addClass("edit-mode");
    $input.attr("title", null);

    $questionContainerDiv.data("mode", "edit");
    SetButtonsVisibility($questionContainerDiv);
}

function ToReadMode($questionContainerDiv) {
    var $input = $questionContainerDiv.children("input");

    $input.attr("readonly", "readonly");
    $input.removeClass("edit-mode");
    $input.addClass("read-mode");
    $input.val($questionContainerDiv.data("default-text"));
    $input.attr("title", editText);

    $questionContainerDiv.data("mode", "read");
    SetButtonsVisibility($questionContainerDiv);
}

function AllToReadMode() {
    var selector = ".edit-question-container[data-mode!='read']";

    $(selector).each(function() {
        ToReadMode($(this));
    });
}

function SetButtonsVisibility($questionContainerDiv) {
    var readMode = $questionContainerDiv.data("mode") == "read";

    $questionContainerDiv.find(".edit-button").toggle(readMode);
    $questionContainerDiv.find(".delete-button").toggle(readMode);
    $questionContainerDiv.find(".save-button").toggle(!readMode);
    $questionContainerDiv.find(".cancel-button").toggle(!readMode);
}

function SaveQuestion($questionContainerDiv) {
    var newText = $questionContainerDiv.find("input").val();
    var questionId = $questionContainerDiv.data("id");

    $.post(updateQuestionUrl, { questionId: questionId, questionText: newText })
        .done(function() {
            alert(updatedText);
            $questionContainerDiv.data("default-text", newText);
            ToReadMode($questionContainerDiv);
        })
        .fail(function(data) {
            alert("Error: " + data.responseText);
        });
}

function DeleteQuestion($questionContainerDiv) {
    if (!confirm(confirmDeleteText)) {
        return;
    }
    var questionId = $questionContainerDiv.data("id");

    $.post(deleteQuestionUrl, { questionId: questionId })
        .done(function() {
            alert(deletedText);
            $questionContainerDiv.remove();
        })
        .fail(function(data) {
            alert("Error: " + data.responseText);
        });
}

function GetQuestionContainerDiv(insideControl) {
    return $(insideControl).closest(".edit-question-container");
}

window.onload = function() {
    $(".edit-button, .edit-question-container[data-mode='read'] input").click(function() {
        ToEditMode(GetQuestionContainerDiv(this));
    });

    $(".cancel-button").click(function() {
        ToReadMode(GetQuestionContainerDiv(this));
    });

    $(".save-button").click(function() {
        SaveQuestion(GetQuestionContainerDiv(this));
    });

    $(".edit-question-container input").keypress(function(event) {
        if (event.which == 13) {
            SaveQuestion(GetQuestionContainerDiv(this));
        }
    });

    $(".delete-button").click(function() {
        var $questionContainer = $(this).closest(".edit-question-container");
        $questionContainer.addClass("before-delete");
        DeleteQuestion($questionContainer);
        $questionContainer.removeClass("before-delete");
    });

    var setAltAndTitle = function(cssClass, text) {
        $("." + cssClass).attr({ title: text, alt: text });
    };

    setAltAndTitle("edit-button", editText);
    setAltAndTitle("delete-button", deleteText);
    setAltAndTitle("save-button", saveText);
    setAltAndTitle("cancel-button", cancelChangesText);

    AllToReadMode();
};