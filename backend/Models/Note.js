const mongoose = require('mongoose');

const NoteSchema = new mongoose.Schema({
    content: { type: String, required: true }, 
    bookId: { type: String, required: true },  
    dateAdded: { type: Date, default: Date.now }
});

module.exports = mongoose.model('Note', NoteSchema);