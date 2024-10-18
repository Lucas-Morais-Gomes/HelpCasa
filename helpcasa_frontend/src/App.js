import React from 'react';
import Login from './components/Login';
import Register from './components/Cadastro';
import Prestador from './components/Prestador';
import Empregador from './components/Empregador';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

const App = () => {
    return (
        <Router>
            <Routes>
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
                <Route path="/prestador" element={<Prestador />} />
                <Route path="/empregador" element={<Empregador />} />
            </Routes>
        </Router>
    );
};

export default App;
