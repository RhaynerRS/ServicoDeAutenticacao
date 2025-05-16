using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.RegularExpressions;

namespace Projeto.AuthService.Dominio.Usuarios.Entidades
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public Usuario(string name, string password, string role, string email)
        {

            Guid = Guid.NewGuid();
            Name = name;
            Role = role;
            SetEmail(email);
            SetPassword(password);
        }

        public void SetPassword(string password)
        {
            Password = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public void SetEmail(string email)
        {
            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (string.IsNullOrWhiteSpace(email) && !Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
                throw new ArgumentException("email invalido");

            Email = email;
        }

    }
}
