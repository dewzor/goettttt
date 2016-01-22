$(document).ready(function () {
    $.ajax({

        URL: 'api/Wall/0',
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            alert("Loaded successfully");
        }
    })
})



//var uri = 'api/Wall/0';

//$(document).ready(function () {
//    $.getJSON(uri).done(function (data) {
//            $.each(data, function (key, post) {
//                $('<li>', { text: insertitem(item) },'</li>').appendTo($('#wallposts'));
//            });
//        });
//});

//function insertitem(post) {
//    return post.post + ': ' + post.authorid;
//}

//$(document).ready(function () {
//    $.getJSON("Api/Wall/0",
//        function (data) {

//            $.each(data, function (index, value) {
//                var text1 = "<p>" +value.authorid +": " + value.post + "</p>";
//                $("#wallposts").prepend(text1);


//                senderId = value.authorid;
//                receiverId = value.receiverId;
//                text = value.post;
//            })
//        })
//});
