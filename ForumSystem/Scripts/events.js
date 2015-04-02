// If the button of the form for asking a question is clicked, a form is show with fadeIn effect and the button hides.

$('#open-post-form').click(function () {
    $('#post')
        .fadeIn("fast");

    $('#open-post-form')
        .hide();
});

// Showing the comment for the wanted answer. 
// Every answer has a link that has his own class that differs him from the other comment links.
// The same class is set to the form container of every answer.
// So, if link with class '0commentNo' is clicked, the form container (for the answer) with that class is shown with slideDown effect :).

$('.comment').click(function () {
    var elementClass = $(this).attr('class'),
        blankSpaceIndex = elementClass.indexOf(' '),
        identifierClass = "";

    for (var i = blankSpaceIndex + 1; i < elementClass.length; i++) {
        identifierClass += elementClass[i];
    }

    $('div.comment-form.' + identifierClass).slideDown('fast');
});