import Link from "next/link";
import styled, { css } from "styled-components";

export const Container = styled.div`
  min-height: 100vh;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: var(--secondary);
`;

export const FormWrapper = styled.div`
  width: 100%;
  max-width: 400px;
  background: var(--background);
  padding: 2rem;
  border-radius: 8px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  transition: all 0.3s ease;
`;

export const Title = styled.h1`
  text-align: center;
  font-size: 1.875rem;
  font-weight: 700;
  margin-bottom: 1.5rem;
`;

export const InputField = styled.div`
  margin-bottom: 1rem;
  position: relative;
`;

export const Input = styled.input<{ hasError?: boolean }>`
  width: 100%;
  padding: 0.625rem 2.5rem;
  border: 1px solid #d1d5db;
  border-radius: 0.5rem;
  outline: none;
  font-size: 1rem;
  color: #374151;
  transition: border-color 0.3s ease;

  ${({ hasError }) =>
    hasError &&
    css`
      border-color: red; /* Cor da borda para inputs com erro */
    `}

  &:focus {
    border-color: #3b82f6;
    box-shadow: 0 0 0 2px rgba(59, 130, 246, 0.3);
  }
`;

export const IconWrapper = styled.div`
  position: absolute;
  top: 50%;
  left: 0.75rem;
  transform: translateY(-50%);
  color: #9ca3af;
`;

export const ErrorMessage = styled.span`
  color: red;
  margin-top: 0.25rem;
`;

export const ForgotLink = styled.a`
  display: block;
  text-align: right;
  margin-bottom: 1rem;
  font-size: 0.875rem;
  color: #3b82f6;
  cursor: pointer;
  &:hover {
    text-decoration: underline;
  }
`;

export const Button = styled.button`
  width: 100%;
  padding: 0.75rem;
  background-color: var(--primary);
  color: var(--background);
  font-size: 1rem;
  border-radius: 0.5rem;
  border: none;
  cursor: pointer;
  transition: all 0.3s ease;
  margin: 0.3rem;

  &:hover {
    filter: brightness(0.8);
  }
`;

export const SignupText = styled.p`
  text-align: center;
  margin-top: 1.5rem;
  font-size: 0.875rem;
  color: #6b7280;
`;

export const RegisterLink = styled(Link)`
  color: #3b82f6;
  &:hover {
    text-decoration: underline;
  }
`;
