class serverSettings
{
    static serverIp = "0.0.0.0";
    static serverPort = "2500";
    static serverMotd = "Unknown";
}

const http = require("http");
var fs = require("fs");
var content = fs.readFileSync("settings.ini", "utf8");

var contentSplit = content.split("\n");

for (i = 0; i < contentSplit.length; i++)
{
    if (contentSplit[i].split("=")[1])
    {
        let settingRow = contentSplit[i].split("=");
        switch (settingRow[0])
        {
            case "server_port":
            serverSettings.serverPort = settingRow[1];
            break;
            case "server_ip":
            serverSettings.serverIp = settingRow[1].replace("\r", "");
            break;
            case "server_motd":
            serverSettings.serverMotd = settingRow[1];
            break;
        }
    }
}


const server = http.createServer((req, res) => {
    res.statusCode = 200;
    res.setHeader("Content-Type", "text/plain");
    res.end(serverSettings.serverMotd.toString());
    });
    server.listen(serverSettings.serverPort, serverSettings.serverIp, () => {
    console.log(`Serverasda körs på http://${serverSettings.serverIp}:${serverSettings.serverPort}/`);
    });
    


