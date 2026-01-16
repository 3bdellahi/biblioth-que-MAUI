const express = require('express');
const router = express.Router();
const ReadingStatus = require('../models/ReadingStatus');

// GET: /api/statuses
router.get('/', async (req, res) => {
    try {
        const statuses = await ReadingStatus.find();
        res.json(statuses); 
    } catch (err) {
        res.status(500).json({ message: err.message });
    }
});

module.exports = router;