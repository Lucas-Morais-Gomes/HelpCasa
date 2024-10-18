import React from 'react';
import { useNavigate } from 'react-router-dom'; // Importando useNavigate

const Prestador = () => {
    const navigate = useNavigate(); // Use useNavigate em vez de useHistory

    const handleLogout = () => {
        // Você pode adicionar lógica para limpar o token ou estado do usuário aqui
        navigate('/login'); // Redirecione para a tela de login
    };

    return (
        <div>
            <h2>Página do Prestador</h2>
            <p>Bem-vindo à sua conta de Prestador!</p>
            <button onClick={handleLogout}>Sair</button>
        </div>
    );
};

export default Prestador;
