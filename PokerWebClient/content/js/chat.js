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
var currentChatPane = c_room;

var dbRoomChat;

function initializeChatWindow(cName, rName, mName, port) {
    clientName = cName;
    roomName = rName;
    serverMachineName = mName;
    serverPort = port;

    initializeDb();
    
    $('#currentUser').html(clientName);
    
    socket = new WebSocket('ws://' + serverMachineName + ':' + serverPort + '/');
    setupSocket();

    $('#send').click(function () {
        var chatMessage = $('#textToSend').val();
        send("PostChatToRoom", [clientName, chatMessage]);
    });

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
            // Handle success commands here.
            case 'PostChatToRoom':
                updateChatRoomDb(msgData.When, msgData.From, msgData.Message);
                if (currentChatPane == c_room) {
                    updateChatRoomHtml(msgData.When, msgData.From, msgData.Message);
                }
                break;
            case 'ClientList':
                updateClientList(msgData.Clients);
                break;
        }
    }
}



function updateChatRoomDb(when, from, message) {
        
    var transaction = dbRoomChat.transaction([c_databaseName], "readwrite");
    var store = transaction.objectStore(c_databaseName);
    var chatData = { "room": roomName, "when": when, "from": from, "message": message };
    var request = store.add(chatData, when);
    request.onerror = function (e) {
        console.log("Error", e.target.error.name);
    }
}

function updateChatRoomHtml(when, from, message)
{
    $("#chatList").append(
            $('<li>').append(from + ':' + message));
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
    userElement.append("Room: " + roomName);
    userElement.attr("id", c_room);
    $('#clientList').append(userElement);

    data.forEach(function (e) {

        var userElement = $('<li>');
        userElement.append(e.Name);
        userElement.attr("id", e.ID);
        $('#clientList').append(userElement);
    });

    $('#clientList li').click(function () {
        $("#chatList").html("");

        if (this.id == c_room)
        {
            reloadChatRoomDataFromDb();
        }

    });
}

function reloadChatRoomDataFromDb() {
    var transaction = dbRoomChat.transaction([c_databaseName]);
    var store = transaction.objectStore(c_databaseName);
    store.openCursor().onsuccess = function (event) {
        var cursor = event.target.result;
        if (cursor) {
            // Display only chats in the current room.
            if (cursor.value.room == roomName) {
                updateChatRoomHtml(cursor.value.when ,cursor.value.from, cursor.value.message);
            }
            cursor.continue();
        }
        else {
            // end of cursor.
        }
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