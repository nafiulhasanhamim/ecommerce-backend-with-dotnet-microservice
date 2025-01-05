const express = require('express');
const cors = require('cors');
require('dotenv').config()
const body_parser = require('body-parser')
const app = express();
const port = process.env.PORT || 5000;


app.use(cors());

app.use(body_parser.json())
const mongoose = require('mongoose')
app.use(express.json())
app.use('/api', require('./routes'))


app.get('/', (req, res) => {
    res.send('app is running')
})

app.listen(port, () => {
    console.log(`Skill Share Server is running on port ${port}`)
})