var express = require("express");
var app = express();
var fs = require("fs");
var bodyParser = require("body-parser") 

app.use(express.static("public"));
app.use(bodyParser.urlencoded({ 
    extended:true
})); 

app.post("/", function(req, res){
    let formInfo = `Name: ${req.body.fullName}\nEmail: ${req.body.email}\nPhone: ${req.body.phoneNumber}\nAddress: ${req.body.address}`;
    console.log("Writing to file!");
    fs.writeFile("form.txt", formInfo, function(err){
        if (err) throw err;
    });
});

app.listen(3000, () => console.log("Server listening on port 3000"));


