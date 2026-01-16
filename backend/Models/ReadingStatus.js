const mongoose = require('mongoose');

const ReadingStatusSchema = new mongoose.Schema({
    Name: { type: String, required: true } 
});

module.exports = mongoose.model('ReadingStatus', ReadingStatusSchema);