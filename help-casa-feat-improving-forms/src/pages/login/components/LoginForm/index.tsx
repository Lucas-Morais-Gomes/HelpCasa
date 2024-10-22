import { useState } from "react";
import { FaUser, FaLock } from "react-icons/fa";
import {
  Button,
  Container,
  ForgotLink,
  FormWrapper,
  IconWrapper,
  Input,
  InputField,
  RegisterLink,
  SignupText,
  Title,
} from "./styles";
import { useRouter } from "next/router";

export function LoginForm() {
  const router = useRouter();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = (event: any) => {
    event.preventDefault();
    console.log("Dados de Login:", { username, password });
    router.push("/");
  };

  return (
    <Container>
      <FormWrapper>
        <Title>Login</Title>
        <form onSubmit={handleSubmit}>
          <InputField>
            <IconWrapper>
              <FaUser />
            </IconWrapper>
            <Input
              type="text"
              placeholder="E-mail"
              required
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
          </InputField>

          <InputField>
            <IconWrapper>
              <FaLock />
            </IconWrapper>
            <Input
              type="password"
              placeholder="Senha"
              required
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </InputField>

          <ForgotLink href="#">Esqueceu sua senha?</ForgotLink>

          <Button type="submit">Login</Button>

          <SignupText>
            Não tem uma conta?{" "}
            <RegisterLink href="/register">Cadastrar</RegisterLink>
          </SignupText>
        </form>
      </FormWrapper>
    </Container>
  );
}

export default LoginForm;
