var app = require('express')();
var http = require('http').Server(app);
var io = require('socket.io')(http);

var net = require('net');
var HOST = '10.12.10.46';

//var HOST = '192.168.1.21';
var PORT = 80;
var client = new net.Socket();

app.get('/', function (req, res) {
    res.sendfile('index.html');
});

io.on('connection', function (socket) {
    socket.on('command', function (msg) {
        //io.emit('text change', msg);
        client.connect(PORT, HOST, function () {
            console.log('\nCONNECTED TO: ' + HOST + ':' + PORT + '\n- Command:' + msg);

            // Write a message to the socket as soon as the client is connected, the server will receive it as message from the client
            client.write(msg);
        });
    });
});

http.listen(3000, function () {
    console.log('listening on *:3000');
});
//# sourceMappingURL=server.js.map
