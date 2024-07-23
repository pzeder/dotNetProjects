"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

function scrollDown() {
    window.scrollTo({
        top: document.body.scrollHeight,
        behavior: 'smooth'
    });
}
connection.on("ReceiveMessage", function (user, message) {
    const li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    const cleanUser = DOMPurify.sanitize(user);
    const cleanMessage = DOMPurify.sanitize(message);
    li.innerHTML = `<span style="font-weight: bold; font-size: 1.2vw">${cleanUser}</span>: &nbsp; ${cleanMessage}`; 
    scrollDown();
});

connection.on("ReceiveWave", user => {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    const sanitizedUser = DOMPurify.sanitize(user);
    li.innerHTML = `<span style="font-weight: bold; font-size: 1.2vw">${user}</span>: &nbsp; &#x1F44B;`; 
    scrollDown();
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

function handleSending(event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    // clear input
    document.getElementById("messageInput").value = "";
    // handling costum input methods
    if (message === "/clear") {
        document.getElementById("messagesList").innerHTML = "";
        return;
    }
    if (message === "/smile") message = "😊";
    if (message === "/sad") message = "\uD83D\uDE22";
    if (message === "/angry") message = "\uD83D\uDE21";
    if (message === "/shock") message = "\uD83D\uDE31";
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
}

document.getElementById("sendButton").addEventListener("click", handleSending);

document.getElementById("messageInput").addEventListener("keydown", function (event) {
    if (event.key === 'Enter') handleSending(event)
});

document.getElementById("waveButton").addEventListener("click", function (event) {
    const user = document.getElementById("userInput").value;
    connection.invoke("WaveHand", user).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("minusButton").addEventListener("click", function (event) {
    const user = document.getElementById("userInput").value;
    connection.invoke("UpdateWaitTime", user, -1).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("plusButton").addEventListener("click", function (event) {
    const user = document.getElementById("userInput").value;
    connection.invoke("UpdateWaitTime", user, 1).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
