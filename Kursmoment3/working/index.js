var express = require("express");
var app = express();
var http = require("http").createServer(app);
var io = require("socket.io")(http);
const { v4: uuid } = require("uuid")
var connectCounter = 0;
var peopleTyping = [];

class User
{
    static count = 0;
    static allUsers = [];

    constructor(nickname)
    {
        this.id = uuid();
        this.nickname = nickname;
        this.isTyping = false;     
        User.count++;
    }
}


app.use(express.static("css"));

app.get("/", (req, res) => {
    res.sendFile(__dirname + "/index.html");
});


io.on("connection", (socket) => {
    connectCounter++;
    var isTyping = false;
    var intervalIdentifier;
    var user = new User(`New User ${connectCounter}`);
    User.allUsers.push(user);
    console.log(`A user connected, name: ${user.nickname}, ID: ${user.id}`);
    socket.broadcast.emit("user connected", `<b>Welcome to the party, ${user.nickname} just connected.</b>`);
    io.to(socket.id).emit("help", "Welcome to the chat app! To see online users, type: /online");

    socket.emit("user", user);

    socket.on("nickname", (nickname) =>{

        if(user.nickname != nickname && nickname != "")
        {
            var indexToRemove = peopleTyping.indexOf(user.nickname);
            peopleTyping.splice(indexToRemove, 1);
            io.emit("changed nickname", `<b>${user.nickname} is now called ${nickname}.</b>`);
            user.nickname = nickname;
        }

        socket.emit("user", user);
    });

    socket.on("disconnect", () => {
        console.log(`${user.nickname} with id ${user.id} disconnected from server.`);
        socket.broadcast.emit("user disconnected", `<b>${user.nickname} just left the party.</b>`);
        User.allUsers.splice(User.allUsers.indexOf(user), 1);
    });

    socket.on("chat message", (chatInfo) => {
        console.log(`Message from ${user.nickname}: ` + chatInfo.message);
        isTyping = false;

        if (chatInfo.message != "/online")
        {
            for (i = 0; i < User.allUsers.length; i++)
            {
                if (User.allUsers[i].id == chatInfo.user.id)
                {
                    for (y = 0; y < peopleTyping.length; y++)
                    {
                        if (peopleTyping[y].id == chatInfo.user.id)
                        {
                            peopleTyping.splice(y, 1);
                        }
                    }
                }
            }

            io.emit("chat message", `${user.nickname}: ${chatInfo.message}`);

            if(peopleTyping.length < 1)
            {
                console.log("Nobody is typing.");
                io.emit("nobody typing");
            }
            else
            {
                io.emit("broadcast typing", peopleTyping);
            }
        }
        else
        {
            var onlineUsers = "";
            for (i = 0; i < User.allUsers.length; i++)
            {
                if (i != User.allUsers.length - 1)
                {
                    onlineUsers += User.allUsers[i].nickname + ", ";
                }
                else
                {
                    onlineUsers += User.allUsers[i].nickname; //FIX ONLINE USERS, SPECIFIC SOCKET?
                }
            }

            io.to(socket.id).emit("display users", `<b>These users are currently online: ${onlineUsers}</b>`);

        }
    });

    socket.on("send typing", (userObj) => {
        console.log(userObj.nickname + " is typing.");
        clearInterval(intervalIdentifier);
        intervalIdentifier = setInterval(ResetTypingStatus, 3000);
        if (isTyping == false)
        {
            peopleTyping.push(userObj);
            isTyping = true;
            console.log(peopleTyping);
            io.emit("broadcast typing", peopleTyping);
        }
    });

    function ResetTypingStatus()
    {
        isTyping = false;
        for (i = 0; i < User.allUsers.length; i++)
        {
            if (User.allUsers[i].id == user.id)
            {
                for (y = 0; y < peopleTyping.length; y++)
                {
                    if (peopleTyping[y].id == user.id)
                    {
                        peopleTyping.splice(y, 1);
                    }
                }
            }
        }

        io.emit("broadcast typing", peopleTyping);

        if(peopleTyping.length < 1)
        {
            console.log("Noone is typing");
            io.emit("nobody typing");
            clearInterval(intervalIdentifier);
        }

    }
});

http.listen(3000, () => {
    console.log("Listening on *:3000");
});