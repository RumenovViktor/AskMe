$('#open-post-form').click(function () {
    $('#post')
        .fadeIn("fast");

    $('#open-post-form')
        .hide();
});

$('.comment').click(function () {
    var elementClass = $(this).attr('class');
    var blankSpaceIndex = elementClass.indexOf(' ');
    var identifierClass = "";

    for (var i = blankSpaceIndex + 1; i < elementClass.length; i++) {
        identifierClass += elementClass[i];
    }

    $('div.comment-form.' + identifierClass).slideDown('fast');
});