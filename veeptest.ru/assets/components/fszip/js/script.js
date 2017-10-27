$(function() {
    var elIndicator = $('.js-indicator');
    var elResult = $('.js-result');
    var elActionLinks = $('.js-action');

    // клик по кнопкам действий
    elActionLinks.click(function() {
        var action = $(this).data('action');

        elIndicator.show();
        elActionLinks.addClass('js-disabled');

        $.ajax({
            url: '/assets/components/fszip/connector.php',
            type: 'post',
            data: {
                action: action
            },
            success: function(response) {
                elResult.html(response);

                // если нажали кнопку "сделать всё", то отправляем клик по кнопке скачивания
                if (action == 'make-all') {
                    window.location.href = $('.js-download-link').attr('href');
                }
            },
            error: function(xhr, textStatus) {
                elResult.html(textStatus);
            },
            complete: function() {
                elIndicator.hide();
                elResult.show();
                elActionLinks.removeClass('js-disabled');
            }
        });
    });
});
