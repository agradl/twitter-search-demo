$(function () {
    $('.btn-group[data-toggle-name]').on('click', 'button', function () {
        var button = $(this),
            group = button.closest('div.btn-group'),
            name = group.attr('data-toggle-name'),
            hidden = group.closest('form').find('input[name="' + name + '"]');

        hidden.val(button.val());
        group.find('button').removeClass('active');
        button.addClass('active');
    });
});