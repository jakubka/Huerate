/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/


// jQuery plugin for popups

// OPTIONS:
// --------
// dataElementId: Id of the element that contains
//             the data to be shown.
//
// wrapperElementId: Id of the wrapper element 
//             (grey shadow layer between the whole page and the popup).
//             If not specified, no wrapper will be used.
//
// eventType: event type ('click', 'onmouseon', ...)

(function($) {
    function HPPopup(el, options) {
        this.$toggleButton = $(el);

        this.requiredOptions = [
            'dataElementId'
        ];

        this.options = {
            eventType: 'click',
        };

        this.setOptions(options);
        this.initialize();
    };

    HPPopup.prototype = {
        
        killerFn: null,
        
        initialize: function() {
            var _me = this;
            
            // Initialize killerFn on _me
            this.killerFn = function (e) {
                var _$target = $(e.target);
                if (_$target.attr('id') !== _me.options.dataElementId &&
                    _$target.parents('#' + _me.options.dataElementId).length === 0) {
                    _me.$wrapperElement.hide();
                    $('body').removeClass('modal-enabled');
                    _me.disableKillerFn();
                }
            };
            
            // Get the data element. If this element
            // does not exist, throw an error.
            var _$dataElement = $('#' + this.options.dataElementId);
            if (_$dataElement.length < 1) {
                throw '#' + this.options.dataElementId + ' does not exist';
            }
            
            // If wrapperElementId is defined, then get this element
            if (this.options.wrapperElementId !== undefined) {
                this.$wrapperElement = $('#' + this.options.wrapperElementId);

                // If the element does not exist, throw an error
                if (this.$wrapperElement.length < 1) {
                    throw '#' + this.options.wrapperElementId + ' does not exist';
                }

                // If this element does not contain the data element
                // defined earlier, throw an error
                if (this.$wrapperElement.find('#' + this.options.dataElementId).length === 0) {
                    throw 'DataElement #' + this.options.dataElementId +
                        ' must be inside the wrapperElement #' + this.options.wrapperElementId;
                }
            }
            
            // Else dataElement is the wrapperElement
            // (and will be used to show/hide)
            else {
                this.$wrapperElement = _$dataElement;
            }

            // Bind the show/hide functionality
            this.$toggleButton.bind(_me.options.eventType, function (e) {
                _me.$wrapperElement.toggle();
                $('body').toggleClass('modal-enabled');
                
                if (_me.$wrapperElement.is(":visible")) {
                    e.stopImmediatePropagation();
                    _me.enableKillerFn();
                }
                else {
                    _me.disableKillerFn();
                }
            });


            var _hashCode = this.options.hashCode;
            
            if (_hashCode && window.location.hash == "#" + _hashCode) {
                _me.$wrapperElement.show();
                $('body').class('modal-enabled');

                _me.enableKillerFn();
            }
        },
        

        // Check if all required options are defined 
        // and extend default options with options specified by the user
        setOptions: function (options) {

            // Check if all required options are specified
            for (var _i = 0; _i < this.requiredOptions.length; _i++) {
                if (options[this.requiredOptions[_i]] === undefined) {
                    throw 'Required option ' + this.requiredOptions[_i] + ' is undefined.';
                }
            }
            
            var _o = this.options;
            $.extend(_o, options);
        },
        
        enableKillerFn: function () {
            var _me = this;
            $(document).bind(_me.options.eventType, _me.killerFn);
        },

        disableKillerFn: function () {
            var _me = this;
            $(document).unbind(_me.options.eventType, _me.killerFn);
        },
    };
    

    $.fn.HPPopup = function (options) {
        if (this.length > 0) {
            return new HPPopup(this.get(0), options);
        }

        return this;
    };

})(jQuery);