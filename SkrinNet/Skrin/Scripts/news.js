/// <reference path="jquery-1.10.2.intellisense.js" />
/// <reference path="jquery-1.10.2.js" />


(function () {

    $(document).ready(function () {

        $('#archive_toggle').click(function(evt){
            toggle_archive(evt);
        })

        $('.news_header').click(function (evt) {
            toggle_news(evt);
        })

        if (selected_id != 0) {
            $('#news_header_' + selected_id).click();
            var top = document.getElementById("news_block_" + selected_id).offsetTop;
            window.scrollTo(0, top);
        }

    });


    var toggle_archive = function (evt) {
        var $btn = $(evt.target);
        var is_open = $btn.data("isopen") || false;
        if (is_open) {
            $(".archive").addClass("invisible");
            $btn.text("Архив новостей");
        } else {
            $(".archive").removeClass("invisible");
            $btn.text("Скрыть архив");
        }
        $btn.data("isopen", !is_open);
    }

    var toggle_news = function (evt) {
        var id = Number(evt.target.id.replace("news_header_", ""));
        var $container = $("#news_block_" + id);
        var is_open = $container.data("isopen") || false;
        var $text_block = $('#text_block_' + id);
        if (!is_open) {            
            if ($text_block.length == 0) {
                $text_block = $('<div class="text_block" id="text_block_' + id + '">');
                $.get("/News/GetNews", { id: id }, function (data) {
                    $text_block.html(data);
                })
                $container.append($text_block);
            }
            $text_block.show();
            $('#news_header_' + id + ' span').removeClass('header_down').removeClass('icon-angle-down').addClass("header_up").addClass("icon-angle-up");
        } else {
            $('#news_header_' + id + ' span').removeClass('header_up').removeClass('icon-angle-up').addClass("header_down").addClass("icon-angle-down");
            $text_block.hide();
        }
        $container.data("isopen", !is_open);
    }

})();