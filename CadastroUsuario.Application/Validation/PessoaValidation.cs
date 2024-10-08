﻿using CadastroUsuario.Application.DTOs.Request;
using CadastroUsuario.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace CadastroUsuario.Api.Validation
{
    public class PessoaValidation : AbstractValidator<PessoaRequest>
    {
        public PessoaValidation()
        {

            RuleFor(n => n.Nome)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3).WithMessage("O campo Nome deve ter no mínimo 3 caracteres.")
                .MaximumLength(20).WithMessage("O campo Nome deve ter no máximo 20 caracteres.")
                .WithMessage("O campo nome, não pode ser vazio.");

            RuleFor(n => n.SobreNome)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3).WithMessage("O campo Sobrenome deve ter no mínimo 3 caracteres.")
                .MaximumLength(100).WithMessage("O campo Sobrenome deve ter no máximo 100 caracteres.")
                .WithMessage("O campo sobrenome, não pode ser vazio.");

            RuleFor(n => n.CPF)
                .NotEmpty()
                .NotNull()
                .Must(ValidarCPF).WithMessage("Por favor, insira um CPF válido.")
                .WithMessage("O campo CPF, não pode ser vazio.");

            RuleFor(n => n.DataNascimento)
                .NotEmpty()
                .NotNull()
                .Must(data => data > new DateTime(1900, 1, 1) && data <= DateTime.Now)
                .WithMessage("A data de nascimento deve ser maior que 01/01/1900 e menor ou igual à data atual.")
                .WithMessage("O campo Data Nascimento, não pode ser vazio.");

            RuleFor(n => n.Sexo)
                .NotEmpty()
                .NotNull()
                .Must(sexo => IsSexoValido(sexo))
                .WithMessage("Por favor, insira uma opção válida para o sexo.");

            //RuleFor(n => n.Foto)
            //    .NotEmpty()
            //    .NotNull()
            //    .Must(foto => IsValidSize(foto)).WithMessage("A foto deve ter no máximo 1 MB.")
            //    .Must(foto => IsValidFormat(foto)).WithMessage("A foto deve ser salva como .jpg.");


        }

        private bool ValidarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;

            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11) return false;


            if (cpf.All(c => c == cpf[0])) return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }


        private bool IsSexoValido(string sexo)
        {
            var sexosValidos = new HashSet<string> { "Masculino", "Feminino", "Outro" }; // Defina aqui as opções válidas para o campo Sexo
            return sexosValidos.Contains(sexo);
        }

        //private bool IsValidSize(long fileSize)
        //{
        //    return fileSize <= 1 * 1024 * 1024; // 1 MB em bytes
        //}

        //private bool IsValidFormat(string fileName)
        //{
        //    var validFormats = new HashSet<string> { ".jpg", ".jpeg" };
        //    var extension = Path.GetExtension(fileName).ToLowerInvariant();
        //    return validFormats.Contains(extension);
        //}

    }
}
