/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/


HP.initializer.sliders = function () {

    // function for slider initialization
    var _initializeSlider = function ($slider) {
        $slider.slider({
            create: function (event, ui) {
                var $input = $(this);
                var $sliderDiv = $input.siblings('.ui-slider');

                $input.bind('change', function () {
                    _onSliderSlide($sliderDiv, $input.val());
                });

                $input.on('slidestop', function() {
                    _onSliderChange($input.val(), $input.attr(HP.settings.getSliderQuestionIdAttribute()), $sliderDiv, $input);
                });
            }
        });
    };

    // called when slider is created
    //var _onSliderCreate = function ($slider) {
    //};
    
    // called on every mouse move during the slide
    var _onSliderSlide = function ($slider, value) {
        $slider.css('background-color', _getColorByValue(value));
    };
    
    // called on slide stop
    var _onSliderChange = function (value, questionId, $sliderDiv, $input) {
        $sliderDiv.css('background-color', _getColorByValue(value));

        $.post(voteActionUrl, {
            QuestionId: questionId,
            Value: value,
            AnswerSetGuid: _guid,
            Language: translator.currentFormLanguage()
        });

        $sliderDiv.attr('data-completed', 'True');
        $input.attr('data-completed', 'True');
        HP.stepsNav.forward({
            elemsToWaitFor: ['div.ui-slider']
        });
    };
    
    // function invoked on ajax vote request success
    //var _onVoteRequestSuccess = function ($slider, $dotInsideSlider) {
    //    var _color = $slider.css('background-color');
    //    $dotInsideSlider.css('background-color', _color);
    //};
    
    // returns color based on value (range 0 - 100)
    var _getColorByValue = function (value) {
        var _ratio = value / 50;

        var _sliderColorStart = HP.settings.getSliderColorStart();
        var _sliderColorMid = HP.settings.getSliderColorMid();
        var _sliderColorEnd = HP.settings.getSliderColorEnd();

        var _finalColor = { red: 0, green: 0, blue: 0 };
        if (_ratio < 1) {
            _finalColor.red = _sliderColorStart.red * (1.0 - _ratio) + _sliderColorMid.red * _ratio;
            _finalColor.green = _sliderColorStart.green * (1.0 - _ratio) + _sliderColorMid.green * _ratio;
            _finalColor.blue = _sliderColorStart.blue * (1.0 - _ratio) + _sliderColorMid.blue * _ratio;
        }
        else {
            _ratio -= 1;
            _finalColor.red = _sliderColorMid.red * (1.0 - _ratio) + _sliderColorEnd.red * _ratio;
            _finalColor.green = _sliderColorMid.green * (1.0 - _ratio) + _sliderColorEnd.green * _ratio;
            _finalColor.blue = _sliderColorMid.blue * (1.0 - _ratio) + _sliderColorEnd.blue * _ratio;
        }

        // validate/cleanup values
        _finalColor.red = _finalColor.red > 255 ? 255 : _finalColor.red;
        _finalColor.green = _finalColor.green > 255 ? 255 : _finalColor.green;
        _finalColor.blue = _finalColor.blue > 255 ? 255 : _finalColor.blue;

        return 'rgb(' + Math.floor(_finalColor.red) + ','
                      + Math.floor(_finalColor.green) + ','
                      + Math.floor(_finalColor.blue) + ')';
    };

    // identifier for specific "user"
    var _guid = HP.functions.generateGuid();

    // run sliders initialization
    (function (sliderSelector) {
        $(sliderSelector).each(function (idx, element) {
            _initializeSlider($(element));
        });
    })('.mobileslider');

    // return object with some
    // usefull pointers to closures
    return {
        //initializeNewSliders: function () {
        //    var _$sliders = $(HP.settings.getSliderSelector()).filter(':not(.ui-slider)');
        //    _$sliders.each(function (index, element) {
        //        var _$elem = $(element);
        //        _initializeSlider(_$elem, _guid);
        //    });
        //},

        getAnswerSetGuid: function () {
            return _guid;
        }
    };
};