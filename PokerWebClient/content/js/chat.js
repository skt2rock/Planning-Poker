var RoomListTrigger = '/getroomlist/';
var TryHardCodedServer = false;
var HardCodedServerDetails = '{ "machineName": "169.254.123.121", "servers": [ { "Name": "FC", "Port": 8000, "Pid": 8640 } ] }';

var socket = null;
var clientName = null;
var clientID = null;


var clientListCache = null;
var serverMachineName = null;
var roomName = null;
var serverPort = null;
var everConnected = false;

var queryStringParams = null;

const c_room = "room";
const c_databaseName = "ppRoomChat";
var r_socketEndPoint; // Readonly, it is initialised only in Initialize window function.
var currentChatPaneId;
var currentChatPaneName;

var retryConnectTimerId = 0;
var retryInterval = 1000;

var dbRoomChat;
initializeDb();

$(document).ready(function () {
    registerStaticEvents();
    autoResizeContent();

    initializeChatWindow();

    $(window).one("focus", stopNotification);
    Notification.requestPermission();
});

function autoResizeContent() {
    $height = $(window).height() - 218;
    $('#chatList').height($height);
}

function initializeChatWindow() {
    clientName = localStorage.getItem("clientName");
    roomName = localStorage.getItem("serverName");
    serverMachineName = localStorage.getItem("machineName");
    serverPort = localStorage.getItem("port");
    currentChatPaneId = localStorage.getItem("serverName");
    currentChatPaneName = localStorage.getItem("serverName");
    r_socketEndPoint = 'ws://' + serverMachineName + ':' + serverPort + '/';

    $('#currentUser').html(clientName);
    $("#withUserOrRoom").text(currentChatPaneName);

    setupSocket(r_socketEndPoint);
}

function initializeDb() {
    if (!("indexedDB" in window)) { alert("Browser not supported.") };

    var openRequest = indexedDB.open(c_databaseName, 1);

    openRequest.onupgradeneeded = function (e) {
        var thisDB = e.target.result;

        if (!thisDB.objectStoreNames.contains(c_databaseName)) {
            thisDB.createObjectStore(c_databaseName);
        }
    }

    openRequest.onsuccess = function (e) {
        dbRoomChat = e.target.result;
    }

    openRequest.onerror = function (e) {
        alert("Couldnot load database, please refresh the page.");
    }
}

function registerStaticEvents() {

    // resize elements on auto resize
    $(window).resize(function () {
        autoResizeContent();
    });

    // Send button click event
    $('#send').click(function () {
        var chatMessage = $('#textToSend').val();
        var command = currentChatPaneName == roomName ? "PostChatToRoom" : "PostChatToPerson";
        send(command, [clientID, clientName, currentChatPaneId, currentChatPaneName, chatMessage]);
        $('#textToSend').val("");
    });

    // "Enter" handlers - these just trigger a 'Click' event.
    $('#textToSend').keypress(function (e) {
        if (e.which == 13) {
            $('#send').trigger('click');
        }
    });

    // Username clicked in user list.
    $(document).on("click", "#userLists li", function () {
        $("#chatList").html("");

        currentChatPaneId = this.id;
        currentChatPaneName = this.getAttribute("name");

        if ($(this).hasClass('offline')) {
            $('#textToSend').prop('disabled', true);
            $('#send').prop('disabled', true);
        }
        else {
            $('#textToSend').prop('disabled', false);
            $('#send').prop('disabled', false);
        }

        $(this).removeClass("new-message");
        $('#userLists li').removeClass("active");
        $(this).addClass("active");

        $("#withUserOrRoom").text(currentChatPaneName);

        if (this.id.toUpperCase() == roomName.toUpperCase()) {
            reloadChatRoomDataFromDb();
        }

        else {
            reloadChatFromDataFromDb(currentChatPaneName);
        }
    });
}

function setupSocket(socketEndPoint) {
    socket = new WebSocket(socketEndPoint);
    socket.onopen = function () {
        // if retry connect interval has been fired.
        if (window.retryConnectTimerId) {
            // Stopr trying to connect to the server.
            window.clearInterval(window.retryConnectTimerId);
            window.retryConnectTimerId = 0;
        }
        everConnected = true;
        $("#currentUser").removeClass("offline");
//        $('#chatList').empty();
        send("RegisterClient", [clientName, 0]);
        // Request to return all chat database for current user from server.
        send("GetAllChatFor", [clientName, roomName]);
        $('#disconnected').hide();
    };
    socket.onmessage = function (event) {
        processIncoming(JSON.parse(event.data));
    };
    socket.onclose = function () {
        if (everConnected) {
            $('#disconnected').html('You are offline. Read chats, while we connect you back.');
        }
        else {
            $('#disconnected').html('Unable to enter the room.');
        }
        $('#disconnected').show();

        $("#currentUser").addClass("offline");
        $("#clientList").empty();
        updateOfflineUserListFromDb();

        // Avoid firing a new setInterval, after one has been done
        if (!window.retryConnectTimerId) {
            // Fire in 1 second then in 6 second then in 11 seeconds, till every 5 minutes.
            window.retryConnectTimerId = setInterval(function () { setupSocket(r_socketEndPoint) }, retryInterval += retryInterval > 300000 ? 0 : 5000);
        }
    };
}

function stopNotification() {
    clearTimeout(flashTimer);
}
var flashTimer;
function notifyUserForNewChat(from, message) {
    flashTimer = setTimeout(function () {
        $("#notifyIcon").href = $("#notifyIcon").href == 'ChatLogo.png' ? 'PokerLogo.png' : 'ChatLogo.png';
    }, 1000);

    new Notification("Message from: " + from, {
        body: message
    });
}

function processIncoming(msgData) {
    if (typeof (msgData.Error) !== 'undefined' && !msgData.Error) {
        switch (msgData.Command) {
            case 'ReceiveChat':
                notifyUserForNewChat(msgData.FromName, msgData.Message);
                updateChatRoomDb(msgData.When, msgData.FromId, msgData.FromName, msgData.ToId, msgData.ToName, msgData.Message);
                // show new message notification for all users, if not in there pane
                if (msgData.ToName.toUpperCase() != roomName.toUpperCase() &&
                    msgData.FromName.toUpperCase() != currentChatPaneName.toUpperCase()) {
                    $('[name="' + msgData.FromName + '"]').addClass("new-message");
                }
                // show new message notification in room chat if not in room pane
                if (currentChatPaneName.toUpperCase() != roomName.toUpperCase() && msgData.ToName.toUpperCase() == roomName.toUpperCase()) {
                    $('[name="' + roomName + '"]').addClass("new-message");
                }
                // update chat if in current pane.
                if (currentChatPaneName.toUpperCase() == msgData.FromName.toUpperCase() || currentChatPaneName.toUpperCase() == msgData.ToName.toUpperCase()) {
                    if (msgData.ToName.toUpperCase() != roomName.toUpperCase()) {
                        updateChatRoomHtml(msgData.When, msgData.FromName, msgData.Message);
                    }
                    else if (currentChatPaneName.toUpperCase() == roomName.toUpperCase()) {
                        updateChatRoomHtml(msgData.When, msgData.FromName, msgData.Message);
                    }
                }
                break;
            case 'ReceiveAllChat':
                clearChatRoomDb(function () {
                    msgData.ChatList.forEach(function (chat) {
                        updateChatRoomDb(chat.When, chat.FromId, chat.FromName, chat.ToId, chat.ToName, chat.Message);
                    });
                });
                reloadChatRoomDataFromDb();
                $('[name="' + currentChatPaneName + '"]').click();
                break;
            case 'ClientList':
                updateClientList(msgData.Clients);
                break;
        }
    }
}

// used by other methods, modify with caution.
function updateChatRoomHtml(when, from, message) {
    // if current user has posted, dont show name in bubble
    if (from.toUpperCase() == clientName.toUpperCase()) {
        $("#chatList").append(
            $('<li>').addClass("me").append($("<div>").addClass("message").append(message)));
    }
    // show name in bubble for other users post
    else {
        $("#chatList").append(
            $('<li>').append($('<div>').addClass("sender").append(from)).append($("<div>").addClass("message").append(message)));
    }

    // Scroll to bottom when new message arrives.    
    $("#chatList").parent(".card-body").scrollTop(999999999999999);
}


// clear chatroom db
function clearChatRoomDb(successCallBack) {
    var transaction = dbRoomChat.transaction([c_databaseName], "readwrite");
    var store = transaction.objectStore(c_databaseName);
    var request = store.clear();
    request.onsuccess = successCallBack;
    request.onerror = function (e) {
        console.log("Error", e.target.error.name);
    }
}

//updateChatRoomDb(msgData.When, msgData.FromId, msgData.FromName, msgData.ToId, msgData.ToName, msgData.Message);
function updateChatRoomDb(when, fromId, fromName, toId, toName, message) {

    var transaction = dbRoomChat.transaction([c_databaseName], "readwrite");
    var store = transaction.objectStore(c_databaseName);
    var chatData = { "when": when, "fromId": fromId, "fromName": fromName, "toId": toId, "toName": toName, "message": message };
    var request = store.add(chatData, when);
    request.onerror = function (e) {
        console.log("Error", e.target.error.name);
    }
}

function reloadChatFromDataFromDb(fromName) {
    var transaction = dbRoomChat.transaction([c_databaseName]);
    var store = transaction.objectStore(c_databaseName);
    store.openCursor().onsuccess = function (event) {
        var cursor = event.target.result;
        if (cursor) {
            // Display only chats with the chosen user.
            if ((cursor.value.fromName.toUpperCase() == fromName.toUpperCase() && cursor.value.toName.toUpperCase() == clientName.toUpperCase()) || (cursor.value.toName.toUpperCase() == fromName.toUpperCase() && cursor.value.fromName.toUpperCase() == clientName.toUpperCase())) {
                updateChatRoomHtml(cursor.value.when, cursor.value.fromName, cursor.value.message);
            }
            cursor.continue();
        }
    }
}

function reloadChatRoomDataFromDb() {
    var transaction = dbRoomChat.transaction([c_databaseName]);
    var store = transaction.objectStore(c_databaseName);
    store.openCursor().onsuccess = function (event) {
        var cursor = event.target.result;
        if (cursor) {
            // Display only chats in the current room.
            if (cursor.value.toName == roomName) {
                updateChatRoomHtml(cursor.value.when, cursor.value.fromName, cursor.value.message);
            }
            cursor.continue();
        }
    }
}

function updateOfflineUserListFromDb() {

    $('#offlineClientList').empty();

    // add to clinet list only if it is not already there
    if ($("#userLists").find($('[name="' + roomName + '"]')).length <= 0) {
        // Register thge room to offline list.
        var userElement = $('<li>');
        userElement.addClass("fa fa-home offline");
        userElement.append(roomName);
        userElement.attr("id", "offline-" + roomName);
        userElement.attr("name", roomName);
        $('#offlineClientList').append(userElement);
    }

    var transaction = dbRoomChat.transaction([c_databaseName]);
    var store = transaction.objectStore(c_databaseName);
    store.openCursor().onsuccess = function (event) {
        var cursor = event.target.result;
        if (cursor) {
            // find all chats sent to current user.            
            if (cursor.value.toName == clientName) {
                // add to clinet list only if it is not already there
                if ($("#userLists").find($('[name="' + cursor.value.fromName + '"]')).length <= 0) {
                    var userElement = $('<li>');
                    userElement.addClass("fa fa-user-o offline");
                    userElement.append(cursor.value.fromName);
                    userElement.attr("id", "offline-" + cursor.value.fromName);
                    userElement.attr("name", cursor.value.fromName);
                    $('#offlineClientList').append(userElement);
                }
            }
            // find all chats sent by the current user.
            else if (cursor.value.fromName == clientName) {
                // add to clinet list only if it is not already there
                if ($("#userLists").find($('[name="' + cursor.value.toName + '"]')).length <= 0) {
                    var userElement = $('<li>');
                    userElement.addClass("fa fa-user-o offline");
                    userElement.append(cursor.value.toName);
                    userElement.attr("id", "offline-" + cursor.value.toName);
                    userElement.attr("name", cursor.value.toName);
                    $('#offlineClientList').append(userElement);
                }
            }
            cursor.continue();
        }
        // Cursor complete
        else {
            $('[name="' + currentChatPaneName + '"]').addClass("active");
        }
    }


}

function isObjectEmpty(object) {
    if (object == 'undefined' || object == undefined || object == null) {
        return true;
    }
    else {
        return false;
    }
}

/*
Sample client data:
[
    {
        ID: <guid>,
        Name: 'Bob',
        IsSpectator: false,
        IsAdmin: false
    },
    {
        ID: <guid>,
        Name: 'Adam',
        IsSpectator: true,
        IsAdmin: true
    }
]
*/
function updateClientList(data) {
    // Update the cache first
    clientListCache = data;

    $('#clientList').empty();
    $('#offlineClientList').empty();

    var userElement = $('<li>');
    userElement.addClass("fa fa-home");
    userElement.append(roomName);
    userElement.attr("id", roomName);
    userElement.attr("name", roomName);
    $('#clientList').append(userElement);

    data.forEach(function (e) {

        // Updates the ID of the current logged in user.
        if (e.Name.toUpperCase() == clientName.toUpperCase()) {
            clientID = e.ID;
        }
        else {
            var userElement = $('<li>');
            userElement.addClass("fa fa-user-o");
            userElement.append(e.Name);
            userElement.attr("id", e.ID);
            userElement.attr("name", e.Name);
            $('#clientList').append(userElement);
        }
    });

    updateOfflineUserListFromDb();

    // on initial load, show room as active
    if ($('#clientList li.active').length <= 0) {
        $('#clientList li:first').click();

    }
}

// JSON-serializes an object (this is just a quick wrapper to make other source easier to read).
function send(name, args) {
    if (typeof (name) !== 'string' || name === null) {
        throw 'Name was not a string (in function send()).';
    }
    if (typeof (args) === 'undefined' || args === null || typeof (args) !== 'object') {
        args = [];
    }
    socket.send(JSON.stringify({ MethodName: name, MethodArguments: args }));
}