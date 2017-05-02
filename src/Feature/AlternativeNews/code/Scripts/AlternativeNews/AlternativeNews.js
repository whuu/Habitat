(function ($) {
    //load news list
    function loadNews() {
        var container = $('#news-container');
        var parameters = {
            sc_itemid: container.parent().data('context')
        };
        $.get({
            url: "/api/News/NewsList",
            data: $.param(parameters)
        }).done(function (html) {
            container.append(html);
        })
    }

    //insert news article
    function insertNewsItem(component) {
        var parentContainer = $(component).parent();
        var contextId = parentContainer.data('context');
        var parameters = {
            title: parentContainer.find('#news-title').val(),
            content: parentContainer.find('#news-content').val()
        };
        $.post(
        {
            url: "/api/News/InsertNews?sc_itemid=" + contextId,
            data: parameters
        }).done(function (result) {
            parentContainer.append(result);
        });
    }

    //on page load
    $(function () {
        $('#news-add').on('click', function () {
            insertNewsItem(this);
        });
        loadNews();
    });
})(jQuery);

