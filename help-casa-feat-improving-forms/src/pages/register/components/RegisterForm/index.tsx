import React, { useState } from 'react';
import { useForm, SubmitHandler } from 'react-hook-form';
import { FaUser } from 'react-icons/fa';
import axios from 'axios';
import {
    Button,
    Container,
    FormWrapper,
    IconWrapper,
    Input,
    InputField,
    RegisterLink,
    SignupText,
    Title
} from './styles';

type FormValues = {
    name: string;
    email: string;
    password: string; // Campo de senha adicionado
    address: string;
    phone: string;
    experience: string;
    areaOfExpertise: string; // Novo campo adicionado
};

export function RegisterForm() {
    const [userType, setUserType] = useState<string | null>(null); // Estado para o tipo de usuário
    const [statusMessage, setStatusMessage] = useState<string | null>(null); // Estado para mensagem de sucesso

    const {
        register,
        handleSubmit,
        formState: { errors }
    } = useForm<FormValues>();

    const onSubmit: SubmitHandler<FormValues> = async (data) => {
        console.log(data); // Verifique se os dados estão sendo capturados
        try {
            const response = await axios.post('http://localhost:5289/api/auth/register', data);
            if (response.ok) {
                setStatusMessage('Usuário cadastrado com sucesso!');
            } else {
                console.error('Erro ao cadastrar usuário:', response.statusText);
                setStatusMessage(null);
            }
        } catch (error: unknown) {
            if (axios.isAxiosError(error)) {
                if (error.response && error.response.status === 409) {
                    setStatusMessage('Email já em uso.');
                } else {
                    console.error('Erro ao cadastrar usuário:', error.response?.statusText);
                    setStatusMessage('Erro ao cadastrar usuário.');
                }
            } else {
                console.error('Erro desconhecido:', error);
                setStatusMessage('Erro de rede.');
            }
        }
    };

    return (
        <Container>
            <FormWrapper>
                <Title>Cadastrar</Title>
                {/* Renderiza a mensagem de sucesso */}
                {statusMessage && <p>{statusMessage}</p>}{' '}
                {/* Passo 2: Selecione o tipo de usuário */}
                {!userType ? (
                    <>
                        <Button onClick={() => setUserType('employee')}>Sou Empregado</Button>
                        <Button onClick={() => setUserType('employer')}>Sou Empregador</Button>
                    </>
                ) : (
                    <form onSubmit={handleSubmit(onSubmit)}>
                        {/* Passo 3: Renderização condicional */}
                        {userType === 'employee' && (
                            <>
                                <InputField>
                                    <IconWrapper>
                                        <FaUser />
                                    </IconWrapper>
                                    <Input
                                        type="text"
                                        placeholder="Nome Completo"
                                        {...register('name', { required: true })}
                                    />
                                    {errors.name && <span>Nome é obrigatório</span>}
                                </InputField>

                                <InputField>
                                    <IconWrapper>
                                        <FaUser />
                                    </IconWrapper>
                                    <Input
                                        type="email"
                                        placeholder="E-mail"
                                        {...register('email', { required: true })}
                                    />
                                    {errors.email && <span>E-mail é obrigatório</span>}
                                </InputField>

                                <InputField>
                                    <IconWrapper>
                                        <FaUser />
                                    </IconWrapper>
                                    <Input
                                        type="password"
                                        placeholder="Senha"
                                        {...register('password', { required: true, minLength: 6 })}
                                    />
                                    {errors.password && (
                                        <span>
                                            A senha é obrigatória e deve ter no mínimo 6 caracteres
                                        </span>
                                    )}
                                </InputField>

                                <InputField>
                                    <IconWrapper>
                                        <FaUser />
                                    </IconWrapper>
                                    <Input
                                        type="text"
                                        placeholder="Endereço"
                                        {...register('address', { required: true })}
                                    />
                                    {errors.address && <span>Endereço é obrigatório</span>}
                                </InputField>

                                <InputField>
                                    <IconWrapper>
                                        <FaUser />
                                    </IconWrapper>
                                    <Input
                                        type="text"
                                        placeholder="Telefone"
                                        {...register('phone', { required: true })}
                                    />
                                    {errors.phone && <span>Telefone é obrigatório</span>}
                                </InputField>

                                <InputField>
                                    <IconWrapper>
                                        <FaUser />
                                    </IconWrapper>
                                    <Input
                                        type="text"
                                        placeholder="Área de Especialização"
                                        {...register('areaOfExpertise', { required: true })} // Novo campo de especialização
                                    />
                                    {errors.areaOfExpertise && (
                                        <span>Área de especialização é obrigatória</span>
                                    )}
                                </InputField>

                                <InputField>
                                    <IconWrapper>
                                        <FaUser />
                                    </IconWrapper>
                                    <Input
                                        type="text"
                                        placeholder="Experiência"
                                        {...register('experience', { required: true })}
                                    />
                                    {errors.experience && <span>Experiência é obrigatória</span>}
                                </InputField>
                            </>
                        )}

                        {userType === 'employer' && (
                            <>
                                <InputField>
                                    <IconWrapper>
                                        <FaUser />
                                    </IconWrapper>
                                    <Input
                                        type="text"
                                        placeholder="Nome Completo"
                                        {...register('name', { required: true })}
                                    />
                                    {errors.name && <span>Nome é obrigatório</span>}
                                </InputField>

                                <InputField>
                                    <IconWrapper>
                                        <FaUser />
                                    </IconWrapper>
                                    <Input
                                        type="email"
                                        placeholder="E-mail"
                                        {...register('email', { required: true })}
                                    />
                                    {errors.email && <span>E-mail é obrigatório</span>}
                                </InputField>

                                <InputField>
                                    <IconWrapper>
                                        <FaUser />
                                    </IconWrapper>
                                    <Input
                                        type="password"
                                        placeholder="Senha"
                                        {...register('password', { required: true, minLength: 6 })}
                                    />
                                    {errors.password && (
                                        <span>
                                            A senha é obrigatória e deve ter no mínimo 6 caracteres
                                        </span>
                                    )}
                                </InputField>

                                <InputField>
                                    <IconWrapper>
                                        <FaUser />
                                    </IconWrapper>
                                    <Input
                                        type="text"
                                        placeholder="Endereço"
                                        {...register('address', { required: true })}
                                    />
                                    {errors.address && <span>Endereço é obrigatório</span>}
                                </InputField>

                                <InputField>
                                    <IconWrapper>
                                        <FaUser />
                                    </IconWrapper>
                                    <Input
                                        type="text"
                                        placeholder="Telefone"
                                        {...register('phone', { required: true })}
                                    />
                                    {errors.phone && <span>Telefone é obrigatório</span>}
                                </InputField>
                            </>
                        )}

                        <Button type="submit">Cadastrar</Button>
                    </form>
                )}
                <SignupText>
                    Já tem uma conta? <RegisterLink href="/login">Login</RegisterLink>
                </SignupText>
            </FormWrapper>
        </Container>
    );
}

export default RegisterForm;
