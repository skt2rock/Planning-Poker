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
var currentChatPaneId;
var currentChatPaneName;

var dbRoomChat;

$(function () {
    registerStaticEvents();
});

function initializeChatWindow(clntName, romName, mchnName, port) {
    clientName = clntName;
    roomName = romName;
    serverMachineName = mchnName;
    serverPort = port;
    currentChatPaneId = romName;
    currentChatPaneName = romName;
    
    $('#currentUser').html(clientName);
    $("#withUserOrRoom").text(currentChatPaneName);

    socket = new WebSocket('ws://' + serverMachineName + ':' + serverPort + '/');
    setupSocket();

    initializeDb();
    reloadChatRoomDataFromDb();    
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

function registerStaticEvents()
{
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
}

function setupSocket() {
    socket.onopen = function () {
        send("RegisterClient", [clientName, 0]);
    };
    socket.onmessage = function (event) {
        processIncoming(JSON.parse(event.data));
    };
    socket.onclose = function () {

        if (everConnected) {
            $('#disconnected').html('The room has terminated the connection.');
        }
        else {
            $('#disconnected').html('Unable to enter the room.');
        }
        $('#disconnected').show();
        everConnected = false;
    };
}

function processIncoming(msgData) {
    if (typeof (msgData.Error) !== 'undefined' && !msgData.Error) {
        switch (msgData.Command) {
            //case 'ReceiveChatForRoom':
            //    updateChatRoomDb(msgData.When, msgData.FromId, msgData.FromName, msgData.ToId, msgData.ToName, msgData.Message);
            //    if (currentChatPaneName == roomName) {
            //        updateChatRoomHtml(msgData.When, msgData.FromName, msgData.Message);
            //    }
            //    break;
            //case 'ReceiveChatFromUser':
            case 'ReceiveChat':
                updateChatRoomDb(msgData.When, msgData.FromId, msgData.FromName, msgData.ToId, msgData.ToName, msgData.Message);
                if (currentChatPaneName == msgData.FromName || currentChatPaneName == msgData.ToName) {
                    updateChatRoomHtml(msgData.When, msgData.FromName, msgData.Message);
                }
                break;
            case 'ClientList':
                updateClientList(msgData.Clients);
                break;
        }
    }
}

// used by other methods, modify with caution.
function updateChatRoomHtml(when, from, message) {
    $("#chatList").append(
        $('<li>').append(from + ':' + message));
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
            if ((cursor.value.fromName == fromName && cursor.value.toName == clientName) || (cursor.value.toName == fromName && cursor.value.fromName == clientName)) {
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

    var userElement = $('<li>');
    userElement.append(roomName);
    userElement.attr("id", roomName);
    $('#clientList').append(userElement);

    data.forEach(function (e) {

        // Updates the ID of the current logged in user.
        if (e.Name == clientName) {
            clientID = e.ID;
        }
        else {
            var userElement = $('<li>');
            userElement.append(e.Name);
            userElement.attr("id", e.ID);
            $('#clientList').append(userElement);
        }
    });

    $('#clientList li').click(function () {
        $("#chatList").html("");

        currentChatPaneId = this.id;
        currentChatPaneName = this.innerText;

        $("#withUserOrRoom").text(currentChatPaneName);

        if (this.id == roomName) {
            reloadChatRoomDataFromDb();
        }

        else {
            reloadChatFromDataFromDb(currentChatPaneName);
        }

    });
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