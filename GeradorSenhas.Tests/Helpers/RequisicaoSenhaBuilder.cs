using GeradorSenhas.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GeradorSenhas.Tests.Helpers
{
    internal class RequisicaoSenhaBuilder
    {
        private bool podeConterLetrasMaiusculas;
        private bool podeConterLetrasMinúsculas;
        private bool podeConterLetrasAcentuadas;
        private bool podeConterNumeros;
        private bool podeConterCaracteresEspeciais;

        public RequisicaoSenhaBuilder()
        {
            Reset();
        }

        public RequisicaoSenhaBuilder ComLetrasAcentuadas()
        {
            podeConterLetrasAcentuadas = true;
            return this;
        }

        public RequisicaoSenhaBuilder ComNumeros()
        {
            podeConterNumeros= true;
            return this;
        }

        public RequisicaoSenhaBuilder ComCaracteresEspeciais()
        {
            podeConterCaracteresEspeciais = true;
            return this;
        }

        public RequisicaoSenhaBuilder SemLetrasAcentuadas()
        {
            podeConterLetrasAcentuadas = false;
            return this;
        }

        public RequisicaoSenhaBuilder SemNumeros()
        {
            podeConterNumeros= false;
            return this;
        }

        public RequisicaoSenhaBuilder SemCaracteresEspeciais()
        {
            podeConterCaracteresEspeciais = false;
            return this;
        }
        public RequisicaoSenhaBuilder ComLetrasMaiusculas()
        {
            podeConterLetrasMaiusculas = true;
            return this;
        }

        public RequisicaoSenhaBuilder SemLetrasMaiusculas()
        {
            podeConterLetrasMaiusculas = false;
            return this;
        }

        public RequisicaoSenhaBuilder ComLetrasMinusculas()
        {
            podeConterLetrasMinúsculas = true;
            return this;
        }

        public RequisicaoSenhaBuilder SemLetrasMinusculas()
        {
            podeConterLetrasMinúsculas = false;
            return this;
        }

        public RequisicaoSenhaBuilder Reset()
        {
            podeConterLetrasMaiusculas = false;
            podeConterLetrasMinúsculas = false;
            podeConterLetrasAcentuadas = false;
            podeConterNumeros = false;
            podeConterCaracteresEspeciais = false;

            return this;
        }

        public RequisicaoSenha Build()
        {
            return new RequisicaoSenha
            {
                PodeConterCaracteresEspeciais = podeConterCaracteresEspeciais,
                PodeConterLetrasAcentuadas = podeConterLetrasAcentuadas,
                PodeConterLetrasMaiusculas = podeConterLetrasMaiusculas,
                PodeConterLetrasMinúsculas = podeConterLetrasMinúsculas,
                PodeConterNumeros = podeConterNumeros,
            };
        }

        public RequisicaoSenha BuildRequisicaoInvalida()
        {
            return new RequisicaoSenha
            {
                PodeConterCaracteresEspeciais = podeConterCaracteresEspeciais,
                PodeConterLetrasAcentuadas = podeConterLetrasAcentuadas,
                PodeConterLetrasMaiusculas = podeConterLetrasMaiusculas,
                PodeConterLetrasMinúsculas = podeConterLetrasMinúsculas,
                PodeConterNumeros = podeConterNumeros,
            };
        }
    }
}
