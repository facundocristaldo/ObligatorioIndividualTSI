"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
console.log("carga el archivo char");
connection.on("ReceiveMessage", function (user, message) {
    console.log(message);
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    
});
$(document).ready(function () {
connection.invoke("SendMessage", "Nuevo usuario", "conectado.").catch(function (err) {
            return console.error(err.toString());
        });

    $("#sendButton").click(function () {
        var user = document.getElementById("userInput").value;
        var message = document.getElementById("messageInput").value;

        console.log(user + " " + message);
        connection.invoke("SendMessage", user, message).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });
});