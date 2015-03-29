/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/



(function ($) {

    var hpStepNav = function(object, customOptions) {

        var defaultOptions = {
            pageSwitcherSelector: 'div#page-switcher',
            buttonBackSelector: 'span.back',
            buttonForwardSelector: 'span.forward',
            percentProgressSelector: 'span.percent',
            progressBarSelector: 'div.progress',
            stepWrapperSelector: 'div.step',
            pageButtonActiveClass: 'active',
            slideSpeed: 500
        };

        // Set options to default
        var options = defaultOptions;

        // Enables sliding initiatet by different situation
        // than clicking on the front/back buttons
        var enableFluidSliding = true;

        // And merge them with the custom options
        setOptions(customOptions);
        
        // Store some objects for later user
        var $wrapperObject = $(object);
        var $pageSwitcher = $wrapperObject.find(options.pageSwitcherSelector);
        var $buttonBack = $pageSwitcher.find(options.buttonBackSelector);
        var $buttonForward = $pageSwitcher.find(options.buttonForwardSelector);
        var $percentWrapper = $pageSwitcher.find(options.percentProgressSelector);
        var $progressBar = $pageSwitcher.find(options.progressBarSelector);
        var $steps = $wrapperObject.find(options.stepWrapperSelector);

        // Some helpfull variables
        var currentStep = 0;
        var stepsCount = $steps.length;
        var stepRelativeWidth = 100 / stepsCount;
        var wrapperRelativeWidth = stepsCount * 100;
        var progressStepRelativeWidth = 100 / (stepsCount - 1);

        // Initialize the css, so the other steps would be hidden
        $wrapperObject.css('width', wrapperRelativeWidth + '%');
        $wrapperObject.children().css('width', stepRelativeWidth + '%');
        $steps.css('float', 'left');
        
        // Hide the progress bar for the beginning
        $progressBar.css('width', '0');
        
        // 'Move' to the first step
        afterMove();
        
        // The step-switch logic
        $buttonForward.click(function (e) {
            e.stopImmediatePropagation();
            stepForward();
        });
        $buttonBack.click(function (e) {
            e.stopImmediatePropagation();
            stepBackward();
        });

        // Check if the elem has the 'completed' attribute set to True
        function isCompleted(elems) {
            var result = true;
            if (elems !== 'undefined' &&
                elems !== undefined) {

                $.each(elems, function (idx, el) {
                    var $elems = $($steps[currentStep]).find(el);
                    $elems.each(function (idx, el) {
                        if ($(el).attr(HP.settings.getElemCompletedDataAttr()) !== HP.settings.getTrueVal()) {
                            result = false;
                        }
                    });
                });
            }

            return result;
        };
        
        // Step forward
        function stepForward (elemsToWaitFor) {
            if ($buttonForward.hasClass(options.pageButtonActiveClass) &&
                isCompleted(elemsToWaitFor)) {

                $($steps[currentStep]).animate({
                    'margin-left': '-' + stepRelativeWidth + '%'
                }, options.slideSpeed);
                currentStep = currentStep + 1;
                afterMove();
            }
        }
        
        // Step backward
        function stepBackward(elemsToWaitFor) {
            if ($buttonBack.hasClass(options.pageButtonActiveClass) &&
                isCompleted(elemsToWaitFor)) {

                currentStep = currentStep - 1;
                $($steps[currentStep]).animate({
                    'margin-left': '0'
                }, options.slideSpeed);
                afterMove();
            }

            enableFluidSliding = false;
        }
        
        // Update buttons
        function updateButtons() {
            if (stepsCount === 1) {
                $buttonBack.removeClass(options.pageButtonActiveClass);
                $buttonForward.removeClass(options.pageButtonActiveClass);
                return;
            }
            
            if (currentStep === 0) {
                $buttonBack.removeClass(options.pageButtonActiveClass);
                $buttonForward.addClass(options.pageButtonActiveClass);
                return;
            }
            
            if (currentStep === stepsCount - 1) {
                $buttonBack.addClass(options.pageButtonActiveClass);
                $buttonForward.removeClass(options.pageButtonActiveClass);
                return;
            }

            $buttonBack.addClass(options.pageButtonActiveClass);
            $buttonForward.addClass(options.pageButtonActiveClass);
        }
        
        // Update the progress bar width
        function updateProgress() {
            $progressBar.animate({
                'width': (currentStep * progressStepRelativeWidth) + '%'
            }, options.slideSpeed, function () {
                $percentWrapper.text(Math.round(currentStep * (100 / (stepsCount - 1))) + '%');
            });
        }
        
        // Perform actions needed to be performed after every move
        function afterMove() {
            updateButtons();
            updateProgress();
            
            var $step = $($steps[currentStep]);
            $step.trigger('visit');

            //$step.css('display', 'block');
            //setTimeout(function() {
            //    $steps.not($step).css('display', 'none');
            //}, 500);
        }
        
        // Merge current options with the custom option
        function setOptions(opts) {
            options = $.extend(options, opts);
        };

        return {
            forward: function (settings) {
                if (enableFluidSliding === true ||
                    settings.forceStep === true) {
                    return stepForward(settings.elemsToWaitFor);
                }
            },
            backward: function (settings) {
                if (enableFluidSliding === true ||
                    settings.forceStep === true) {
                    return stepBackward(settings.elemsToWaitFor);
                }
            }
            
        };

    };
    
    $.fn.hpStepNav = function (customOptions) {
        var stepNavReturn = hpStepNav(this, customOptions);
        $.extend(this, stepNavReturn);
        return this;
    };

})(jQuery);