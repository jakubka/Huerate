/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/


HP.settings = (function () {

    var _returnObj = {};

    _returnObj.getTrueVal = function () { return 'True'; };
    _returnObj.getFalseVal = function () { return 'False'; };
    _returnObj.getUndefinedVal = function() { return 'undefined'; };

    //
    // hue

    var _hueSelector = 'div.hue';
    _returnObj.getHueSelector = function () { return _hueSelector; };
    
    //
    // Steps
    _returnObj.getElemCompletedDataAttr = function () { return 'data-completed'; };

    //
    // popups
    
    var _popupClosedClass = 'closed';
    _returnObj.getPopupClosedClass = function() { return _popupClosedClass; };
    
    var _popupClickHolderClass = 'onclick-popup';
    _returnObj.getClickPopupClass = function() { return _popupClickHolderClass; };

    //
    // slider
    
    var _sliderSelector = 'div.slider';
    _returnObj.getSliderSelector = function () { return _sliderSelector; };

    var _sliderHandlerSelector = 'a.ui-slider-handle';
    _returnObj.getSliderHandlerSelector = function () { return _sliderHandlerSelector; };

    var _sliderHandlerDotInsideSelector = 'i.handler-inside';
    _returnObj.getSliderHandlerDotInsideSelector = function () { return _sliderHandlerDotInsideSelector; };

    var _sliderPopupSelector = '.popup-wrapper';
    _returnObj.getSliderPopupSelector = function() { return _sliderPopupSelector; };

    var _sliderAverageValueSelector = 'div.average-value';
    _returnObj.getSliderAverageValueSelector = function() { return _sliderAverageValueSelector; };

    var _sliderAverageValueAttribute = 'data-average-vote-val';
    _returnObj.getSliderAverageValueAttribute = function() { return _sliderAverageValueAttribute; };

    var _sliderUserHasVotedAttribute = 'data-user-has-voted';
    _returnObj.getSliderUserHasVotedAttribute = function() { return _sliderUserHasVotedAttribute; };

    var _sliderUserLastVoteAttribute = 'data-user-last-vote-val';
    _returnObj.getSliderUserLastVoteAttribute = function() { return _sliderUserLastVoteAttribute; };

    var _sliderQuestionIdAttribute = 'data-question-id-val';
    _returnObj.getSliderQuestionIdAttribute = function () { return _sliderQuestionIdAttribute; };

    var _sliderColorStart =  { red: 234, green: 20, blue: 20 };
    _returnObj.getSliderColorStart = function() { return _sliderColorStart; };
    
    var _sliderColorMid = { red: 187, green: 34, blue: 104 };
    _returnObj.getSliderColorMid = function() { return _sliderColorMid; };

    var _sliderColorEnd = { red: 50, green: 234, blue: 50 };
    _returnObj.getSliderColorEnd = function() { return _sliderColorEnd; };

    var _sliderDefaultValue = 50;
    _returnObj.getSliderDefaultValue = function() { return _sliderDefaultValue; };

    //
    // composer
    
    var _bodyComposerSelector = 'div#compose-question-body';
    _returnObj.getBodyComposerSelector = function() { return _bodyComposerSelector; };

    var _popupComposerId = 'popup-composer-wrapper';
    _returnObj.getPopupComposerId = function() { return _popupComposerId; };

    var _popupComposerButtonSelector = 'div#compose-question-popup-button';
    _returnObj.getPopupComposerButtonSelector = function() { return _popupComposerButtonSelector; };
    
    var _popupComposerWrapperHtml = '<div class="popup-dialog-wrapper"></div>';
    _returnObj.getPopupComposerWrapperHtml = function() { return _popupComposerWrapperHtml; };
    
    var _composerTextCounterSelector = 'span.text-counter';
    _returnObj.getComposerTextCounterSelector = function() { return _composerTextCounterSelector; };
    
    var _composerSubmitButtonSelector = 'input.submit';
    _returnObj.getComposerSubmitButtonSelector = function() { return _composerSubmitButtonSelector; };

    var _composerTagsInputSelector = 'input#Tags';
    _returnObj.getComposerTagsInputSelector = function() { return _composerTagsInputSelector; };



    return _returnObj;
})();