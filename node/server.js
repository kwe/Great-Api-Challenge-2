const fetch = (...args) =>
  import('node-fetch').then(({ default: fetch }) => fetch(...args));

// create an express api
const express = require('express');

const app = express();
const port = 3000;

// create a route for / that returns "Hello World!" as a json object
app.get('/', (req, res) => res.json({ message: 'Hello World!' }));

// create a route for /joke that returns a random joke as a json object
app.get('/joke', (req, res) => {
  const jokes = [
    'What do you call a fake noodle? An impasta.',
    'How many apples grow on a tree? All of them.',
    "Want to hear a joke about paper? Nevermind it's tearable.",
    "I just watched a program about beavers. It was the best dam program I've ever seen.",
    'Why did the coffee file a police report? It got mugged.',
    "How does a penguin build it's house? Igloos it together.",
  ];
  const randomJoke = jokes[Math.floor(Math.random() * jokes.length)];
  res.json({ message: randomJoke });
});

// create a route for /time that returns the current time as a json object
app.get('/time', (req, res) => {
  const date = new Date();
  const time = date.toLocaleTimeString();
  res.json({ message: time });
});

// create a route for /posts that calls https://jsonplaceholder.typicode.com/posts using fetch and returns the result as a json object
app.get('/posts', (req, res) => {
  fetch('https://jsonplaceholder.typicode.com/posts')
    .then((response) => response.json())
    .then((json) => res.json({ message: json }));
});

// start the server
app.listen(port, () => console.log(`Example app listening on port ${port}!`));
