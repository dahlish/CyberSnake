var express = require("express");
var app = express();
var http = require("http").createServer(app);
var io = require("socket.io")(http);
const port = process.env.port || 5000;
const { v4: uuid } = require("uuid")
var peopleTyping = [];

class User
{
    static count = 0;
    static allUsers = [];

    constructor(nickname, socket)
    {
        this.id = uuid();
        this.nickname = nickname;
        this.isTyping = false;     
        this.socketId = socket;
        User.count++;
    }
}


app.use(express.static("css"));

app.get("/", (req, res) => {
    res.sendFile(__dirname + "/index.html");
});


io.on("connection", (socket) => {
    var isTyping = false;
    var intervalIdentifier;
    var user = new User(`New User ${User.count}`, socket.id);
    User.allUsers.push(user);
    console.log(`A user connected, name: ${user.nickname}, ID: ${user.id}`);
    socket.broadcast.emit("user connected", `<b>Welcome to the party, ${user.nickname} just connected.</b>`);
    io.to(socket.id).emit("help", "Welcome to the chat app! To see online users, type: /online");
    io.to(socket.id).emit("help", `To send a private message, type /tell "nickname" <message>`);

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
        isTyping = false;
        for (i = 0; i < peopleTyping.length; i++)
        {
            if (peopleTyping[i].id == user.id)
            {
                peopleTyping.splice(i, 1);
            }
        }

        ResetTypingStatus();
    });

    socket.on("chat message", (chatInfo) => {
        console.log(`Message from ${user.nickname}: ` + chatInfo.message);
        isTyping = false;
        if (chatInfo.message.toString().startsWith("/tell "))
        {
            var splitMessage = chatInfo.message.split('"');
            var receiver = splitMessage[1];
            var tell = "";
            var foundUser = false;

            for (i = 0; i < splitMessage.length; i++)
            {
                if (i > 1)
                {
                    tell += splitMessage[i];
                }
            }

            for (i = 0; i < User.allUsers.length; i++)
            {
                if(User.allUsers[i].nickname == receiver)
                {
                    io.to(socket.id).emit("private", `<i><b>You told ${receiver}:</b> ${tell} </i>`);
                    io.to(User.allUsers[i].socketId).emit("private", `<i><b>${user.nickname} tells you:</b> ${tell}</i>`);
                    foundUser = true;
                }
            }

            if (!foundUser)
            {
                if (receiver == undefined)
                {
                    receiver = "";
                }
                io.to(socket.id).emit("private", `<b>Invalid name "${receiver}".</b>`);
            }

            if(peopleTyping.length < 1)
            {
                console.log("Nobody is typing.");
                io.emit("nobody typing");
            }
            else
            {
                io.emit("broadcast typing", peopleTyping);
            }

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
        }
        else if (chatInfo.message == "/online")
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
                    onlineUsers += User.allUsers[i].nickname;
                }
            }

            io.to(socket.id).emit("display users", `<b>These users are currently online: ${onlineUsers}</b>`);

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

        }
        else
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
                socket.broadcast.emit("chat message", `${user.nickname}: ${chatInfo.message}`);
        }

        if(peopleTyping.length < 1)
        {
            console.log("Nobody is typing.");
            io.emit("nobody typing");
        }
        else
        {
            io.emit("broadcast typing", peopleTyping);
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
            io.emit("nobody typing");
            clearInterval(intervalIdentifier);
        }

    }
});

http.listen(port, () => {
});