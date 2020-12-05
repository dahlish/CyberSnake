const http = require("http");
const hostname = "127.0.0.1";
const port = "3000";
var fs = require("fs");

const server = http.createServer((req, res) => {
    fs.readFile("counter.txt", function(err, data) {
        var nbr = Number(data.toString());
        nbr++;
        fs.writeFile("counter.txt", nbr.toString(), function(err){
            if (err) throw err;
         });

         res.setHeader("Content-Type", "text/html;  charset=utf-8");
         res.end(`Denna sida har laddats ${nbr} gånger.`);
    });
});
    server.listen(port, hostname, () => {
    console.log(`Server körs på http://${hostname}:${port}/`);
    });
