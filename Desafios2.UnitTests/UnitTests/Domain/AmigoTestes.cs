using Xunit;
using System;
using Desafio.s2.Domain.Amigos;
using Desafio.s2.Domain.Constantes;

namespace Desafios2.UnitTests.UnitTests.Domain
{
    public class AmigoTestes
    {
        private const string TEXTO_COM_MAIS_DE_150_CARACTERES = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        #region nome

        [Fact]
        public void Amigo_DeveValidarNomeComoCampoObrigatorip()
        {
            var amigo = new Amigo("", "emailfornecido@teste.com.br", Guid.NewGuid());

            Assert.False(amigo.EhValido());
            Assert.Contains(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.NOME_OBRIGATORIO));
            Assert.Contains(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.MAX_MIN_LENTH_NOME));
        }

        [Fact]
        public void Amigo_DeveValidarNomeMinimoCaracteres()
        {
            var amigo = new Amigo("a", "emailfornecido@teste.com.br", Guid.NewGuid());

            Assert.False(amigo.EhValido());
            Assert.Contains(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.MAX_MIN_LENTH_NOME));
        }

        [Fact]
        public void Amigo_DeveValidarNomeMaximoCaracteres()
        {
            var amigo = new Amigo(TEXTO_COM_MAIS_DE_150_CARACTERES, "emailfornecido@teste.com.br", Guid.NewGuid());

            Assert.False(amigo.EhValido());
            Assert.Contains(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.MAX_MIN_LENTH_NOME));
        }

        [Fact]
        public void Amigo_NaoDeveValidarNome()
        {
            var amigo = new Amigo("Campo Nome Valido", "emailfornecido@teste.com.br", Guid.NewGuid());

            Assert.True(amigo.EhValido());
            Assert.DoesNotContain(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.NOME_OBRIGATORIO));
            Assert.DoesNotContain(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.MAX_MIN_LENTH_NOME));
        }

        #endregion

        #region email

        [Fact]
        public void Amigo_DeveValidarEmailComoCampoObrigatorio()
        {
            var amigo = new Amigo("Nome", "", Guid.NewGuid());

            Assert.False(amigo.EhValido());

            Assert.Contains(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.EMAIL_INVALIDO));
            Assert.Contains(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.EMAIL_OBRIGATORIO));
        }

        [Fact]
        public void Amigo_DeveValidarEmailMinimoCaracteres()
        {
            var amigo = new Amigo("Nome", "a", Guid.NewGuid());

            Assert.False(amigo.EhValido());

            Assert.Contains(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.MAX_MIN_LENTH_EMAIL));
            Assert.DoesNotContain(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.EMAIL_OBRIGATORIO));
        }

        [Fact]
        public void Amigo_DeveValidarEmailMaximoCaracteres()
        {
            var amigo = new Amigo("Nome", TEXTO_COM_MAIS_DE_150_CARACTERES, Guid.NewGuid());

            Assert.False(amigo.EhValido());

            Assert.Contains(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.MAX_MIN_LENTH_EMAIL));
            Assert.DoesNotContain(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.EMAIL_OBRIGATORIO));
        }

        [Fact]
        public void Amigo_NaoDeveValidarEmail()
        {
            var amigo = new Amigo("Nome", "teste@teste.com", Guid.NewGuid());

            Assert.True(amigo.EhValido());

            Assert.DoesNotContain(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.EMAIL_INVALIDO));
            Assert.DoesNotContain(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.MAX_MIN_LENTH_EMAIL));
            Assert.DoesNotContain(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.EMAIL_OBRIGATORIO));
        }

        [Fact]
        public void Amigo_DeveCriticarrEmailInvalido()
        {
            var amigo = new Amigo("Nome", "email invalido", Guid.NewGuid());

            Assert.False(amigo.EhValido());

            Assert.Contains(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.EMAIL_INVALIDO));
            Assert.DoesNotContain(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.EMAIL_OBRIGATORIO));
            Assert.DoesNotContain(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.MAX_MIN_LENTH_EMAIL));
        }

        #endregion

        #region id usuario

        [Fact]
        public void Amigo_DeveValidarIdUsuarioLogado()
        {
            var amigo = new Amigo("Nome", "teste@teste.com", Guid.Empty);

            Assert.False(amigo.EhValido());

            Assert.Contains(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.ID_USUARIO_LOGADO_OBRITORIO));
        }

        [Fact]
        public void Amigo_NaoDeveValidarIdUsuarioLogado()
        {
            var amigo = new Amigo("Nome", "teste@teste.com", Guid.NewGuid());

            Assert.True(amigo.EhValido());

            Assert.DoesNotContain(amigo.ValidationResult.Errors, a => a.ErrorMessage.Equals(AmigoConstantes.ID_USUARIO_LOGADO_OBRITORIO));
        }

        #endregion
    }
}