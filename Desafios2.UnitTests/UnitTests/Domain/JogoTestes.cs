using Xunit;
using System;
using Desafio.s2.Domain.Jogos;
using Desafio.s2.Domain.Constantes;

namespace Desafios2.UnitTests.UnitTests.Domain
{
    public class JogoTestes
    {
        private const string TEXTO_COM_MAIS_DE_150_CARACTERES =
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        private Guid CategoriaId;
        private Guid UsuarioLogadoId;

        #region nome

        [Fact]
        public void Jogo_DeveValidarNomeComoCampoObrigatorip()
        {
            CategoriaId = Guid.NewGuid();
            UsuarioLogadoId = Guid.NewGuid();

            var jogo = new Jogo("", CategoriaId, UsuarioLogadoId);

            Assert.False(jogo.EhValido());
            Assert.Contains(jogo.ValidationResult.Errors, a => a.ErrorMessage.Equals(JogoConstantes.NOME_OBRIGATORIO));
            Assert.Contains(jogo.ValidationResult.Errors, a => a.ErrorMessage.Equals(JogoConstantes.MAX_MIN_LENTH_NOME));
        }

        [Fact]
        public void Jogo_DeveValidarNomeMinimoCaracteres()
        {
            CategoriaId = Guid.NewGuid();
            UsuarioLogadoId = Guid.NewGuid();

            var jogo = new Jogo("a", CategoriaId, UsuarioLogadoId);

            Assert.False(jogo.EhValido());
            Assert.Contains(jogo.ValidationResult.Errors, a => a.ErrorMessage.Equals(JogoConstantes.MAX_MIN_LENTH_NOME));
        }

        [Fact]
        public void Jogo_DeveValidarNomeMaximoCaracteres()
        {
            CategoriaId = Guid.NewGuid();
            UsuarioLogadoId = Guid.NewGuid();

            var jogo = new Jogo(TEXTO_COM_MAIS_DE_150_CARACTERES, CategoriaId, UsuarioLogadoId);

            Assert.False(jogo.EhValido());
            Assert.Contains(jogo.ValidationResult.Errors, a => a.ErrorMessage.Equals(JogoConstantes.MAX_MIN_LENTH_NOME));
        }

        [Fact]
        public void Jogo_NaoDeveValidarNome()
        {
            CategoriaId = Guid.NewGuid();
            UsuarioLogadoId = Guid.NewGuid();

            var jogo = new Jogo("Campo Nome Valido", CategoriaId, UsuarioLogadoId);

            Assert.True(jogo.EhValido());
            Assert.DoesNotContain(jogo.ValidationResult.Errors, a => a.ErrorMessage.Equals(JogoConstantes.NOME_OBRIGATORIO));
            Assert.DoesNotContain(jogo.ValidationResult.Errors, a => a.ErrorMessage.Equals(JogoConstantes.MAX_MIN_LENTH_NOME));
        }

        #endregion

        #region id usuario

        [Fact]
        public void Jogo_DeveValidarIdUsuarioLogado()
        {
            CategoriaId = Guid.NewGuid();
            UsuarioLogadoId = Guid.Empty;

            var jogo = new Jogo("Nome", CategoriaId, UsuarioLogadoId);

            Assert.False(jogo.EhValido());
            Assert.Contains(jogo.ValidationResult.Errors, a => a.ErrorMessage.Equals(JogoConstantes.ID_USUARIO_LOGADO_OBRITORIO));
        }

        [Fact]
        public void Jogo_NaoDeveValidarIdUsuarioLogado()
        {
            CategoriaId = Guid.NewGuid();
            UsuarioLogadoId = Guid.NewGuid();

            var jogo = new Jogo("Nome", CategoriaId, UsuarioLogadoId);

            Assert.True(jogo.EhValido());
            Assert.DoesNotContain(jogo.ValidationResult.Errors, a => a.ErrorMessage.Equals(JogoConstantes.ID_USUARIO_LOGADO_OBRITORIO));
        }

        #endregion

        #region categoria

        [Fact]
        public void Jogo_DeveValidarCategoria()
        {
            CategoriaId = Guid.Empty;
            UsuarioLogadoId = Guid.NewGuid();

            var jogo = new Jogo("Nome", CategoriaId, UsuarioLogadoId);

            Assert.True(jogo.EhValido());
            Assert.DoesNotContain(jogo.ValidationResult.Errors, a => a.ErrorMessage.Equals(JogoConstantes.ID_USUARIO_LOGADO_OBRITORIO));
        }

        [Fact]
        public void Jogo_NaoDeveValidarCategoria()
        {
            CategoriaId = Guid.NewGuid();
            UsuarioLogadoId = Guid.NewGuid();

            var jogo = new Jogo("Nome", CategoriaId, UsuarioLogadoId);

            Assert.True(jogo.EhValido());
            Assert.DoesNotContain(jogo.ValidationResult.Errors, a => a.ErrorMessage.Equals(JogoConstantes.ID_USUARIO_LOGADO_OBRITORIO));
        }

        #endregion
    }
}