var RoomListTrigger = '/getroomlist/';
var TryHardCodedServer = false;
var HardCodedServerDetails = '{ "machineName": "169.254.123.121", "servers": [ { "Name": "FC", "Port": 8000, "Pid": 8640 } ] }';

var socket = null;
var clientName = null;
var clientID = null;


var clientListCache = null;
var serverMachineName = null;
var serverPort = null;
var everConnected = false;

var queryStringParams = null;

$(function () {

    queryStringParams = getUrlVars();

    // check if params are available , else throw error.
    socket = new WebSocket('ws://' + queryStringParams.serverMachineName + ':' + queryStringParams.serverPort + '/');
    
    setupSocket();

    $('#send').click(function () {
        $('#textToSend').text();
    });
});

function setupSocket() {
    socket.onopen = function () {
        send("RegisterClient", [clientName, ($('#spectator').is(":checked") ? 1 : 0)]);
        if (isTryingAdmin) {
            send("RegisterAdmin", [$('#adminPass').val()]);
        }
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

// Read a page's GET URL variables and return them as an associative array.
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}