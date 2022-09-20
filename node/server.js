// create an express api
const express = require('express');
const app = express();
const port = 3000;

// create a route for / that returns "Hello World!" as a json object
app.get('/', (req, res) => res.json({ message: 'Hello World!' }));

// start the server
app.listen(port, () => console.log(`Example app listening on port ${port}!`));
