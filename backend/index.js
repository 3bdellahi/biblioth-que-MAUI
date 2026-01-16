const express = require('express');
const cors = require('cors');
const connectDB = require('./db'); 


const app = express();
const PORT = 5000;


connectDB();


app.use(cors()); 
app.use(express.json());

//route
const categoryRoutes = require('./routes/categories');
app.use('/api/categories', categoryRoutes);


const bookRoutes = require('./routes/books');
app.use('/api/books', bookRoutes);


const noteRoutes = require('./routes/notes');
app.use('/api/notes', noteRoutes);


app.get('/', (req, res) => {
    res.send("Le serveur fonctionne avec succès et la base de données est connectée !");
});


app.listen(PORT, '0.0.0.0', () => {
    console.log(`connecte: http://192.168.100.20:${PORT}`);
});
