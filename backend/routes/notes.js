const express = require('express');
const router = express.Router();
const Note = require('../models/Note');


router.get('/:bookId', async (req, res) => {
    try {
        const notes = await Note.find({ bookId: req.params.bookId }); 
        res.json(notes);
    } catch (err) {
        res.status(500).json({ message: err.message });
    }
});


router.post('/', async (req, res) => {
    //console.log("البيانات المستلمة:", req.body);

    const note = new Note({
        content: req.body.content,  
        bookId: req.body.bookId     
    });

    try {
        const savedNote = await note.save();
        res.status(201).json(savedNote);
    } catch (err) {
        console.error("Erreur d'enregistrement:", err.message);
        res.status(400).json({ message: err.message });
    }
});

module.exports = router;