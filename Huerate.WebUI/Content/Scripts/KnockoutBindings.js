/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/


ko.bindingHandlers.loadingWhen = {
    init: function (element) {
        var $element = $(element);
        var currentPosition = $element.css("position");

        var $loader = $("<div>").addClass("loader").hide();

        //add the loader
        $element.append($loader);

        //make sure that we can absolutely position the loader against the original element
        if (currentPosition == "auto" || currentPosition == "static") {
            $element.css("position", "relative");
        }

        //center the loader
        //$loader.css({
        //    position: "absolute",
        //    top: "50%",
        //    left: "50%",
        //    "margin-left": -($loader.width() / 2) + "px",
        //    "margin-top": -($loader.height() / 2) + "px"
        //});
    },
    update: function (element, valueAccessor) {
        var isLoading = ko.utils.unwrapObservable(valueAccessor());
        var $element = $(element);
        //var $childrenToHide = $element.children(":not(div.loader)");
        var $loader = $element.children("div.loader");

        if (isLoading) {
            //$childrenToHide.css("visibility", "hidden").attr("disabled", "disabled");
            $loader.show();
        } else {
            $loader.fadeOut("fast");
            //$childrenToHide.css("visibility", "visible").removeAttr("disabled");
        }
    }
};