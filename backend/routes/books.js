const express = require('express');
const router = express.Router();
const Book = require('../models/Book');


router.get('/', async (req, res) => {
    try {
        const books = await Book.find();
        res.json(books); 
    } catch (err) {
        res.status(500).json({ message: err.message });
    }
});


router.post('/', async (req, res) => {
    console.log("Les données reçues:", req.body);

    const book = new Book({
        title: req.body.title,            
        categoryName: req.body.categoryName, 
        statusName: req.body.statusName      
    });

    try {
        const newBook = await book.save();
        console.log("✅ d'enregistrement MongoDB");
        res.status(201).json(newBook);
    } catch (err) {
        console.error("❌Erreur d'enregistrement", err.message);
        res.status(400).json({ message: err.message });
    }
});
module.exports = router;