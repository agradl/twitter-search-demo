ko.bindingHandlers.radio = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        // This will be called when the binding is first applied to an element
        // Set up any initial state, event handlers, etc. here
        $(element).on('click', '.btn', function () {
            valueAccessor()($(this).val());
        });
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        // This will be called once when the binding is first applied to an element,
        // and again whenever the associated observable changes value.
        // Update the DOM element based on the supplied values here.
        var value = ko.utils.unwrapObservable(valueAccessor());
        $(element).find('.active').removeClass('active');
        $(element).find('[value="' + value + '"]').addClass('active');
    }
};

ko.bindingHandlers.enterKeyPress = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        // This will be called when the binding is first applied to an element
        // Set up any initial state, event handlers, etc. here
        $(element).on('keyup', function (evt) {
            if (evt.keyCode === 13)
                ko.utils.unwrapObservable(valueAccessor())(evt);
        });
    }
};