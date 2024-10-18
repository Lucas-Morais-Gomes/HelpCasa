import React, { useState } from 'react';
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom'; // Importando useNavigate

const Login = () => {
    const [email, setEmail] = useState('');
    const [senha, setSenha] = useState('');
    const [errorMessage, setErrorMessage] = useState('');
    const navigate = useNavigate(); // Use useNavigate em vez de useHistory

    const handleSubmit = async (event) => {
        event.preventDefault();

        const usuarioLogin = {
            email,
            senha
        };

        try {
            const response = await axios.post('http://localhost:5289/api/auth/login', usuarioLogin);

            // Redirecione com base no tipo de usuário (se você estiver retornando o tipo de usuário)
            if (response.data.tipoUsuario === 'Empregador') {
                navigate('/empregador'); // Redirecione para a página do Empregador
            } else {
                navigate('/prestador'); // Redirecione para a página do Prestador
            }
        } catch (error) {
            setErrorMessage('Email ou senha inválidos.');
        }
    };

    return (
        <div>
            <h2>Login</h2>
            {errorMessage && <p style={{ color: 'red' }}>{errorMessage}</p>}
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Email:</label>
                    <input
                        type="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Senha:</label>
                    <input
                        type="password"
                        value={senha}
                        onChange={(e) => setSenha(e.target.value)}
                        required
                    />
                </div>
                <button type="submit">Entrar</button>
            </form>
            <p>
                Não tem uma conta? <Link to="/register">Registre-se aqui</Link>
            </p>
        </div>
    );
};

export default Login;
