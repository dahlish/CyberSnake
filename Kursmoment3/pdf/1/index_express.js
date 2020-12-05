const express = require("express");
const app = express();
const port = 3000;

app.use(express.static("public"));
app.get("/", (req, res) => {
    res.send("Hello world!");
});
app.get("/lol", (req, res) => {
    res.send("Not.");
});
app.listen(port, () => console.log(`Server listening on port ${port}`));