const mongoose = require('mongoose');

const connectDB = async () => {
    try {
        await mongoose.connect('mongodb://localhost:27017/LibraryDB');
        console.log("le bd conectee");
    } catch (err) {
        console.error("errure :", err.message);
        process.exit(1);
    }
};

module.exports = connectDB;