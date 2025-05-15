using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        private readonly PasswordHasher<Usuario> _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public UsuarioService(IUsuarioRepository repo, IJwtProvider jwtProvider)
        {
            _repo = repo;
            _passwordHasher = new PasswordHasher<Usuario>();
            _jwtProvider = jwtProvider;
        }

        public async Task ActualizarAsync(UsuarioDto usuarioDto)
        {
            var usuario = await _repo.GetUsuarioByIdAsync(usuarioDto.Id);

            usuario.Username = usuarioDto.Username;
            usuario.Nombre = usuarioDto.Nombre;
            usuario.ApellidoP = usuarioDto.ApellidoP;
            usuario.ApellidoM = usuarioDto.ApellidoM;
            usuario.Correo = usuarioDto.Correo;
            usuario.Rol = usuarioDto.Rol;
            usuario.Activo = usuarioDto.Activo;

            await _repo.ActualizarAsync(usuario);
        }

        public async Task<UsuarioRegistroRespuestaDto> AgregarAsync(UsuarioRegistroDto usuarioRegistroDto)
        {
            var usuariosExistentes = await _repo.GetUsuariosAsync();

            if (usuariosExistentes.Any(u => u.Correo == usuarioRegistroDto.Correo))
                throw new Exception("Ya existe un usuario con ese correo");

            var usuario_nuevo = new Usuario
            {
                Username = usuarioRegistroDto.Username,
                Nombre = usuarioRegistroDto.Nombre,
                ApellidoP = usuarioRegistroDto.ApellidoP,
                ApellidoM = usuarioRegistroDto.ApellidoM,
                Correo = usuarioRegistroDto.Correo,
                ContrasenaHash = usuarioRegistroDto.Password,
                Rol = usuarioRegistroDto.Role,
                Activo = true
            };

            // Hashear la contraseña
            usuario_nuevo.ContrasenaHash = _passwordHasher.HashPassword(usuario_nuevo, usuarioRegistroDto.Password);

            await _repo.AgregarAsync(usuario_nuevo);

            var token = _jwtProvider.GenerateToken(usuario_nuevo.Username, usuario_nuevo.Rol);

            return new UsuarioRegistroRespuestaDto
            {
                Usuario = new UsuarioDatosDto
                {
                    ID = usuario_nuevo.Id,
                    Username = usuario_nuevo.Username,
                    Nombre = usuario_nuevo.Nombre,
                    ApellidoP = usuario_nuevo.ApellidoP,
                    ApellidoM = usuario_nuevo.ApellidoM,
                    Correo = usuario_nuevo.Correo
                },
                Role = usuario_nuevo.Rol,
                Token = token,
            };
        }

        public async Task EliminarAsync(int usuarioId)
        {
            await _repo.EliminarAsync(usuarioId);
        }

        public async Task<UsuarioDto> GetUsuarioByIdAsync(int usuarioId)
        {
            var u = await _repo.GetUsuarioByIdAsync(usuarioId);

            return new UsuarioDto
            {
                Id = u.Id,
                Username = u.Username,
                Nombre = u.Nombre,
                ApellidoP = u.ApellidoP,
                ApellidoM = u.ApellidoM,
                Correo = u.Correo,
                Rol = u.Rol,
                Activo = u.Activo
            };
        }

        public async Task<UsuarioDto> GetUsuarioByUsernameAsync(string username)
        {
            var u = await _repo.GetUsuarioByUsernameAsync(username);

            return new UsuarioDto
            {
                Id = u.Id,
                Username = u.Username,
                Nombre = u.Nombre,
                ApellidoP = u.ApellidoP,
                ApellidoM = u.ApellidoM,
                Correo = u.Correo,
                Rol = u.Rol,
                Activo = u.Activo
            };
        }

        public async Task<IEnumerable<UsuarioDto>> GetUsuariosAsync()
        {
            var usuarios = await _repo.GetUsuariosAsync();

            return usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Username = u.Username,
                Nombre = u.Nombre,
                ApellidoP = u.ApellidoP,
                ApellidoM = u.ApellidoM,
                Correo = u.Correo,
                Rol = u.Rol,
                Activo = u.Activo
            });
        }

        public async Task<UsuarioLoginRespuestaDto> LoginAsync(UsuarioLoginDto usuarioLoginDto)
        {
            var usuario = await _repo.GetUsuarioByUsernameAsync(usuarioLoginDto.Username);

            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            //Verificar la contraseña
            var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.ContrasenaHash, usuarioLoginDto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Contraseña incorrecta.");
            }

            var token = _jwtProvider.GenerateToken(usuario.Username, usuario.Rol);

            return new UsuarioLoginRespuestaDto
            {
                Usuario = new UsuarioDatosDto
                {
                    ID = usuario.Id,
                    Username = usuario.Username,
                    Nombre = usuario.Nombre,
                    ApellidoP = usuario.ApellidoP,
                    ApellidoM = usuario.ApellidoM,
                    Correo = usuario.Correo
                },
                Role = usuario.Rol,
                Token = token
            };
        }
    }
}
