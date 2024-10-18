import React from 'react';
import { useNavigate } from 'react-router-dom'; // Importando useNavigate

const Empregador = () => {
    const navigate = useNavigate(); // Use useNavigate em vez de useHistory

    const handleLogout = () => {
        // Você pode adicionar lógica para limpar o token ou estado do usuário aqui
        navigate('/login'); // Redirecione para a tela de login
    };

    return (
        <div>
            <h2>Página do Empregador</h2>
            <p>Bem-vindo à sua conta de Empregador!</p>
            <button onClick={handleLogout}>Sair</button>
        </div>
    );
};

export default Empregador;
