const mongoose = require('mongoose');

const BookSchema = new mongoose.Schema({
    title: { type: String, required: true },
    categoryName: { type: String, required: true },
    statusName: { type: String, default: 'L état de lecture' },
    createdAt: { type: Date, default: Date.now }      
});

module.exports = mongoose.model('Book', BookSchema);