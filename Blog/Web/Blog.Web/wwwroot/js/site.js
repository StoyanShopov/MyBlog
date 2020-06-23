function sendEmail() {
    var token = $("#emailForm input[name=__RequestVerificationToken]").val();

    var json =
    {
        name: $("#name").val(),
        email: $("#email").val(),
        subject: $("#subject").val(),
        message: $("#message").val()
    };

    $.ajax({
        url: '/api/emails',
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'RequestVerificationToken': token },
        success: function (result) {
            console.log(result);
        }
    });

    //var modal = document.getElementById("myModal");
    //var btn = document.getElementById("sendBtn");
    //var span = document.getElementsByClassName("close")[0];

    //btn.onclick = function () {
    //    modal.style.display = "block";
    //}

    //span.onclick = function () {
    //    modal.style.display = "none";
    //}

    //window.onclick = function (event) {
    //    if (event.target == modal) {
    //        modal.style.display = "none";
    //    }
    //}
};

