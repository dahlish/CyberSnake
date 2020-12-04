var http = require("http");
var url = require("url");
http.createServer(function (req, res){
    res.writeHead(200, {"Content-Type": "text/html"});
    var q = url.parse(req.url, true).query;
    let txt = q.year + " " + q.month + " " + q.name;
    res.end(txt);
}).listen(3000);

//CHEAT SHEET:
//PARAMS:
//...d.se/?year=2020&month=12&name=Christopher