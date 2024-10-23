import React, { useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import { FaUser } from "react-icons/fa";
import axios from "axios";
import {
  Button,
  Container,
  ErrorMessage,
  FormWrapper,
  IconWrapper,
  Input,
  InputField,
  RegisterLink,
  SignupText,
  Title,
} from "./styles"; // Supondo que você tenha estilizações básicas.

type FormValues = {
  Name: string;
  Email: string;
  PasswordDto: string;
  Address: string;
  Phone: string;
  Experience?: string; // Opcional para empregador
  AreaOfExpertise?: string; // Opcional para empregador
};

export function RegisterForm() {
  const [userType, setUserType] = useState<string | null>(null);
  const [statusMessage, setStatusMessage] = useState<string | null>(null);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FormValues>();

  const onSubmit: SubmitHandler<FormValues> = async (data) => {
    try {
      const formData = {
        ...data,
        userType,
      };

      const response = await axios.post(
        "http://localhost:5289/api/auth/register",
        formData
      );
      if (response.status) {
        setStatusMessage("Usuário cadastrado com sucesso!");
      } else {
        setStatusMessage(null);
      }
    } catch (error) {
      if (axios.isAxiosError(error)) {
        if (error.response && error.response.status === 409) {
          setStatusMessage("Email já em uso.");
        } else {
          setStatusMessage("Erro ao cadastrar usuário.");
        }
      } else {
        setStatusMessage("Erro de rede.");
      }
    }
  };

  return (
    <Container>
      <FormWrapper>
        <Title>Cadastrar</Title>
        {statusMessage && <p>{statusMessage}</p>}
        {!userType ? (
          <>
            <Button onClick={() => setUserType("employee")}>
              Sou Empregado
            </Button>
            <Button onClick={() => setUserType("employer")}>
              Sou Empregador
            </Button>
          </>
        ) : (
          <form onSubmit={handleSubmit(onSubmit)}>
            {/* Renderização condicional para Empregado */}
            {userType === "employee" && (
              <>
                <InputField>
                  <IconWrapper>
                    <FaUser />
                  </IconWrapper>
                  <Input
                    type="text"
                    placeholder="Nome Completo"
                    {...register("Name", { required: "Nome é obrigatório" })}
                    style={{
                      borderColor: errors.Name ? "red" : "initial", // Borda vermelha se houver erro
                    }}
                  />
                  {errors.Name && (
                    <span style={{ color: "red" }}>{errors.Name.message}</span>
                  )}
                </InputField>

                <InputField>
                  <IconWrapper>
                    <FaUser />
                  </IconWrapper>
                  <Input
                    type="Email"
                    placeholder="E-mail"
                    {...register("Email", { required: "E-mail é obrigatório" })}
                    style={{
                      borderColor: errors.Email ? "red" : "initial",
                    }}
                  />
                  {errors.Email && (
                    <span style={{ color: "red" }}>{errors.Email.message}</span>
                  )}
                </InputField>

                {/* Campo de senha com mensagens fixas */}
                <div style={{ color: "red", marginBottom: "8px" }}>
                  A senha deve ter:
                  <ul>
                    <li>Entre 6 a 10 caracteres</li>
                    <li>Ao menos uma letra maiúscula</li>
                    <li>Ao menos um caractere especial</li>
                  </ul>
                </div>

                <InputField>
                  <IconWrapper>
                    <FaUser />
                  </IconWrapper>
                  <Input
                    type="password"
                    placeholder="Senha"
                    {...register("PasswordDto", {
                      required: "Senha é obrigatória",
                      minLength: {
                        value: 6,
                        message: "A senha deve ter no mínimo 6 caracteres",
                      },
                      maxLength: {
                        value: 10,
                        message: "A senha deve ter no máximo 10 caracteres",
                      },
                      pattern: {
                        value: /^(?=.*[A-Z])(?=.*[!@#$%^&*])/,
                        message:
                          "A senha deve conter ao menos 1 letra maiúscula e 1 caractere especial",
                      },
                    })}
                    style={{
                      borderColor: errors.PasswordDto ? "red" : "initial", // Borda vermelha se houver erro
                    }}
                  />
                  {errors.PasswordDto && (
                    <ErrorMessage>{errors.PasswordDto.message}</ErrorMessage>
                  )}
                </InputField>

                {/* Restante dos campos para empregado */}
                <InputField>
                  <IconWrapper>
                    <FaUser />
                  </IconWrapper>
                  <Input
                    type="text"
                    placeholder="Endereço"
                    {...register("Address", {
                      required: "Endereço é obrigatório",
                    })}
                    style={{
                      borderColor: errors.Address ? "red" : "initial",
                    }}
                  />
                  {errors.Address && (
                    <span style={{ color: "red" }}>
                      {errors.Address.message}
                    </span>
                  )}
                </InputField>

                <InputField>
                  <IconWrapper>
                    <FaUser />
                  </IconWrapper>
                  <Input
                    type="text"
                    placeholder="Telefone"
                    {...register("Phone", {
                      required: "Telefone é obrigatório",
                    })}
                    style={{
                      borderColor: errors.Phone ? "red" : "initial",
                    }}
                  />
                  {errors.Phone && (
                    <span style={{ color: "red" }}>{errors.Phone.message}</span>
                  )}
                </InputField>

                <InputField>
                  <IconWrapper>
                    <FaUser />
                  </IconWrapper>
                  <Input
                    type="text"
                    placeholder="Área de Especialização"
                    {...register("AreaOfExpertise", {
                      required: "Área de especialização é obrigatória",
                    })}
                    style={{
                      borderColor: errors.AreaOfExpertise ? "red" : "initial",
                    }}
                  />
                  {errors.AreaOfExpertise && (
                    <span style={{ color: "red" }}>
                      {errors.AreaOfExpertise.message}
                    </span>
                  )}
                </InputField>

                <InputField>
                  <IconWrapper>
                    <FaUser />
                  </IconWrapper>
                  <Input
                    type="text"
                    placeholder="Experiência"
                    {...register("Experience", {
                      required: "Experiência é obrigatória",
                    })}
                    style={{
                      borderColor: errors.Experience ? "red" : "initial",
                    }}
                  />
                  {errors.Experience && (
                    <span style={{ color: "red" }}>
                      {errors.Experience.message}
                    </span>
                  )}
                </InputField>
              </>
            )}

            {userType === "employer" && (
              <>
                {/* Renderização condicional para Empregador */}
                <InputField>
                  <IconWrapper>
                    <FaUser />
                  </IconWrapper>
                  <Input
                    type="text"
                    placeholder="Nome Completo"
                    {...register("Name", { required: "Nome é obrigatório" })}
                    style={{
                      borderColor: errors.Name ? "red" : "initial",
                    }}
                  />
                  {errors.Name && (
                    <span style={{ color: "red" }}>{errors.Name.message}</span>
                  )}
                </InputField>

                <InputField>
                  <IconWrapper>
                    <FaUser />
                  </IconWrapper>
                  <Input
                    type="Email"
                    placeholder="E-mail"
                    {...register("Email", { required: "E-mail é obrigatório" })}
                    style={{
                      borderColor: errors.Email ? "red" : "initial",
                    }}
                  />
                  {errors.Email && (
                    <span style={{ color: "red" }}>{errors.Email.message}</span>
                  )}
                </InputField>

                {/* Campo de senha com mensagens fixas */}
                <div style={{ color: "red", marginBottom: "8px" }}>
                  A senha deve ter:
                  <ul>
                    <li>Entre 6 a 10 caracteres</li>
                    <li>Ao menos uma letra maiúscula</li>
                    <li>Ao menos um caractere especial</li>
                  </ul>
                </div>

                <InputField>
                  <IconWrapper>
                    <FaUser />
                  </IconWrapper>
                  <Input
                    type="password" // Corrigido de PasswordDto para password
                    placeholder="Senha"
                    {...register("PasswordDto", {
                      required: "Senha é obrigatória",
                      minLength: {
                        value: 6,
                        message: "A senha deve ter no mínimo 6 caracteres",
                      },
                      maxLength: {
                        value: 10,
                        message: "A senha deve ter no máximo 10 caracteres",
                      },
                      pattern: {
                        value: /^(?=.*[A-Z])(?=.*[!@#$%^&*])/,
                        message:
                          "A senha deve conter ao menos 1 letra maiúscula e 1 caractere especial",
                      },
                    })}
                    style={{
                      borderColor: errors.PasswordDto ? "red" : "initial",
                    }}
                  />
                  {errors.PasswordDto && (
                    <ErrorMessage>{errors.PasswordDto.message}</ErrorMessage>
                  )}
                </InputField>

                <InputField>
                  <IconWrapper>
                    <FaUser />
                  </IconWrapper>
                  <Input
                    type="text"
                    placeholder="Endereço"
                    {...register("Address", {
                      required: "Endereço é obrigatório",
                    })}
                    style={{
                      borderColor: errors.Address ? "red" : "initial",
                    }}
                  />
                  {errors.Address && (
                    <span style={{ color: "red" }}>
                      {errors.Address.message}
                    </span>
                  )}
                </InputField>

                <InputField>
                  <IconWrapper>
                    <FaUser />
                  </IconWrapper>
                  <Input
                    type="text"
                    placeholder="Telefone"
                    {...register("Phone", {
                      required: "Telefone é obrigatório",
                    })}
                    style={{
                      borderColor: errors.Phone ? "red" : "initial",
                    }}
                  />
                  {errors.Phone && (
                    <span style={{ color: "red" }}>{errors.Phone.message}</span>
                  )}
                </InputField>
              </>
            )}

            <Button type="submit">Cadastrar</Button>
            <SignupText>
              Já tem uma conta?{" "}
              <RegisterLink href="/login">Faça login</RegisterLink>
            </SignupText>
          </form>
        )}
      </FormWrapper>
    </Container>
  );
}
