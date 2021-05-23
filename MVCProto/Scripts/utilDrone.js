let submitIdBtn = $("#submitIdBtn");
let idField = $("#idField");

$(function () {

    // Ссылка на автоматически-сгенерированный прокси хаба  
    let droneHub = $.connection.droneHub;

    $.connection.hub.start()
        .done(function () {
            droneHub.server.announce("Connected!");
            droneHub.server.connect("1;2;3");

        })
        .fail(function () { alert("SOMETHING WENT WRONG!") })



    droneHub.client.announce = function (message) {
        console.log(message);
    }

    droneHub.client.sendId = function (id) {
        let transValue;
        if (idField.val() && idField.val() !== 0) {
            $("#ids").append(idField.val());
            transValue = idField.val();
        }
        transValue = idField.val();
        //console.log(transValue);
        console.log(id);      
    }

    submitIdBtn.click(function () {
        let idValue = Number(idField.val());
        droneHub.server.sendId(idValue);
    });
})






