// Header dropdown closure
(function () {
    $(document).on('mouseleave', '.header-navigation .dropdown', function () {
        $(this).removeClass('open');
    });
}());

// Alert closure
(function () {
    $('.alerts .alert').each(function () {
        var alert = $(this);

        if (alert.data('timeout')) {
            setTimeout(function () {
                alert.fadeTo(300, 0).slideUp(300, function () {
                    $(this).remove();
                });
            }, alert.data('timeout'));
        }
    });

    $(document).on('click', '.alert .close', function () {
        $(this.parentNode).fadeTo(300, 0).slideUp(300, function () {
            $(this).remove();
        });
    });
}());

// JQuery dialog overlay binding
(function () {
    $(document).on('click', '.ui-widget-overlay', function () {
        $('.ui-dialog:visible .ui-dialog-titlebar-close').trigger('click');
    });
}());

// Globalized validation binding
(function () {
    $.validator.methods.date = function (value, element) {
        return this.optional(element) || Globalize.parseDate(value);
    };

    $.validator.methods.number = function (value, element) {
        var pattern = new RegExp('^(?=.*\\d+.*)[-+]?\\d*[' + Globalize.culture().numberFormat['.'] + ']?\\d*$');

        return this.optional(element) || pattern.test(value);
    };

    $.validator.methods.min = function (value, element, param) {
        return this.optional(element) || Globalize.parseFloat(value) >= parseFloat(param);
    };

    $.validator.methods.max = function (value, element, param) {
        return this.optional(element) || Globalize.parseFloat(value) <= parseFloat(param);
    };

    $.validator.methods.range = function (value, element, param) {
        return this.optional(element) || (Globalize.parseFloat(value) >= parseFloat(param[0]) && Globalize.parseFloat(value) <= parseFloat(param[1]));
    };

    $.validator.addMethod('greater', function (value, element, param) {
        return this.optional(element) || Globalize.parseFloat(value) > parseFloat(param);
    });
    $.validator.unobtrusive.adapters.add("greater", ["min"], function (options) {
        options.rules["greater"] = options.params.min;
        if (options.message) {
            options.messages["greater"] = options.message;
        }
    });

    $.validator.addMethod('integer', function (value, element) {
        return this.optional(element) || /^[+-]?\d+$/.test(value);
    });
    $.validator.unobtrusive.adapters.addBool("integer");

    $(document).on('change', '.datalist-hidden-input', function () {
        var validator = $(this).parents('form').validate();

        if (validator) {
            var datalistInput = $(this).prevAll('[data-datalist-for="' + this.id + '"]');
            if (validator.element('#' + this.id)) {
                datalistInput.removeClass('input-validation-error');
            } else {
                datalistInput.addClass('input-validation-error');
            }
        }
    });
    $('form').on('invalid-form', function (form, validator) {
        var datalists = $(this).find('.datalist-input');
        for (var i = 0; i < datalists.length; i++) {
            var datalistInput = $(datalists[i]);
            var hiddenInputId = datalistInput.attr('data-datalist-for');

            if (validator.invalid[hiddenInputId]) {
                datalistInput.addClass('input-validation-error');
            } else {
                datalistInput.removeClass('input-validation-error');
            }
        }
    });
    $(document).on('ready', function () {
        var hiddenDatalistInputs = $('.datalist-hidden-input.input-validation-error');
        for (var i = 0; i < hiddenDatalistInputs.length; i++) {
            var hiddenInput = $(hiddenDatalistInputs[i]);
            hiddenInput.prevAll('[data-datalist-for="' + hiddenDatalistInputs[i].id + '"]').addClass('input-validation-error');
        }
    });

    var currentIgnore = $.validator.defaults.ignore;
    $.validator.setDefaults({
        ignore: function () {
            return $(this).is(currentIgnore) && !$(this).hasClass('datalist-hidden-input');
        }
    });
    
    var lang = $('html').attr('lang');

    Globalize.cultures.en = null;
    Globalize.addCultureInfo(lang, window.cultures.globalize[lang]);
    Globalize.culture(lang);
}());

// Datepicker binding
(function () {
    var options = {
        beforeShow: function (e) {
            return !$(e).attr('readonly');
        },
        onSelect: function () {
            $(this).focusout();
        }
    };

    if ($.fn.datepicker) {
        var lang = $('html').attr('lang');
        $(".datepicker").datepicker(options);
        $.datepicker.setDefaults(window.cultures.datepicker[lang]);
    }

    if ($.fn.timepicker) {
        $(".datetimepicker").datetimepicker(options);
        $.timepicker.setDefaults(window.cultures.timepicker[lang]);
    }
}());

// JsTree binding
(function () {
    var jsTrees = $('.js-tree-view');
    for (var i = 0; i < jsTrees.length; i++) {
        var jsTree = $(jsTrees[i]).jstree({
            'core': {
                'themes': {
                    'icons': false
                }
            },
            'plugins': [
                'checkbox'
            ],
            'checkbox': {
                'keep_selected_style': false
            }
        });

        jsTree.on('ready.jstree', function (e, data) {
            var selectedNodes = $(this).prev('.js-tree-view-ids').children();
            for (var j = 0; j < selectedNodes.length; j++) {
                data.instance.select_node(selectedNodes[j].value, false, true);
            }

            data.instance.open_node($.makeArray(jsTree.find('> ul > li')), null, null);
            data.instance.element.show();
        });
    }

    $(document).on('submit', 'form', function () {
        var jsTrees = $(this).find('.js-tree-view');
        for (var i = 0; i < jsTrees.length; i++) {
            var jsTree = $(jsTrees[i]).jstree();
            var treeIdSpan = jsTree.element.prev('.js-tree-view-ids');

            treeIdSpan.empty();
            var selectedNodes = jsTree.get_selected();
            for (var j = 0; j < selectedNodes.length; j++) {
                var node = jsTree.get_node(selectedNodes[j]);
                if (node.li_attr.id) {
                    treeIdSpan.append('<input type="hidden" value="' + node.li_attr.id + '" name="' + jsTree.element.attr('for') + '" />');
                }
            }
        }
    });
}());

// Mvc.Grid binding
(function () {
    if ($.fn.mvcgrid) {
        $('.mvc-grid').mvcgrid();
        $.fn.mvcgrid.lang = window.cultures.grid[$('html').attr('lang')];

        if (MvcGridNumberFilter) {
            MvcGridNumberFilter.prototype.isValid = function (value) {
                var pattern = new RegExp('^(?=.*\\d+.*)[-+]?\\d*[' + Globalize.culture().numberFormat['.'] + ']?\\d*$');

                return value == '' || pattern.test(value);
            }
        }
    }
}());

// Datalist binding
(function () {
    if ($.fn.datalist) {
        $.fn.datalist.lang = window.cultures.datalist[$('html').attr('lang')];
    }
}());

// Read only binding
(function () {
    $(document).on('click', 'input:checkbox[readonly],input:radio[readonly]', function () {
        return false;
    });
}());

// Input focus binding
(function () {
    var invalidInput = $('.content-container .input-validation-error:visible:not([readonly],.datepicker,.datetimepicker):first');
    if (invalidInput.length > 0) {
        invalidInput.focus();
        invalidInput.val(invalidInput.val());
    } else {
        var input = $('.content-container input:text:visible:not([readonly],.datepicker,.datetimepicker):first');
        if (input.length > 0) {
            input.focus();
            input.val(input.val());
        }
    }
}());

// Bootstrap binding
(function () {
    $('[data-toggle=tooltip]').tooltip();
}());
