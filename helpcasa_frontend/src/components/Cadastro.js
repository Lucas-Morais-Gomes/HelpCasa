import React, { useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom'; // Importe o Link do React Router

const Register = () => {
    const [nome, setNome] = useState('');
    const [email, setEmail] = useState('');
    const [senha, setSenha] = useState('');
    const [endereco, setEndereco] = useState('');
    const [telefone, setTelefone] = useState('');
    const [prestador, setPrestador] = useState(false); // Para o checkbox
    const [errorMessage, setErrorMessage] = useState('');

    const handleSubmit = async (event) => {
        event.preventDefault();

        const usuarioDto = {
            nome,
            email,
            senha,
            endereco,
            telefone,
            tipoUsuario: prestador ? 'Empregado' : 'Empregador' // Define o tipo de usuário com base no checkbox
        };

        try {
            await axios.post('http://localhost:5289/api/auth/register', usuarioDto);
            // Redirecione ou mostre uma mensagem de sucesso
        } catch (error) {
            if (error.response && error.response.status === 409) {
                setErrorMessage('Email já em uso');
            } else {
                setErrorMessage('Ocorreu um erro. Tente novamente.');
            }
        }
    };

    return (
        <div>
            <h2>Registro</h2>
            {errorMessage && <p style={{ color: 'red' }}>{errorMessage}</p>}
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Nome:</label>
                    <input
                        type="text"
                        value={nome}
                        onChange={(e) => setNome(e.target.value)}
                        required
                    />
                </div>
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
                <div>
                    <label>Endereço:</label>
                    <input
                        type="text"
                        value={endereco}
                        onChange={(e) => setEndereco(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Telefone:</label>
                    <input
                        type="text"
                        value={telefone}
                        onChange={(e) => setTelefone(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>
                        <input
                            type="checkbox"
                            checked={prestador}
                            onChange={(e) => setPrestador(e.target.checked)}
                        />
                        Sou Prestador
                    </label>
                </div>
                <button type="submit">Registrar</button>
            </form>
            <p>
                Já tem uma conta? <Link to="/login">Faça login aqui</Link>
            </p>
        </div>
    );
};

export default Register;
