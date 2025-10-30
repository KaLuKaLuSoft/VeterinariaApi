using System.Security.Cryptography;

namespace VeterinariaApi.Seguridad
{
    public class PasswordHelper
    {
        private const int SaltSize = 16; // Tamaño del salt en bytes
        private const int KeySize = 32; // Tamaño de la clave (hash) en bytes
        private const int Iterations = 10000; // Número de iteraciones para PBKDF2

        // Método para encriptar (hashear) una contraseña con salt
        public string HashPassword(string password)
        {
            // Generar salt aleatorio
            byte[] salt = GenerateSalt(SaltSize);

            // Hashing con PBKDF2
            byte[] hash = PBKDF2(password, salt, Iterations, KeySize);

            // Combinar salt y hash en un solo string
            string saltBase64 = Convert.ToBase64String(salt);
            string hashBase64 = Convert.ToBase64String(hash);
            return $"{saltBase64}:{hashBase64}";
        }

        // Método para verificar si la contraseña proporcionada coincide con el hash almacenado
        public bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            // Separar el salt y el hash
            var parts = storedHashedPassword.Split(':');
            if (parts.Length != 2)
            {
                throw new FormatException("Formato de hash almacenado incorrecto.");
            }

            string saltBase64 = parts[0];
            string hashBase64 = parts[1];

            // Convertir el salt desde Base64 a byte[]
            byte[] salt = Convert.FromBase64String(saltBase64);

            // Hashear la contraseña ingresada con el mismo salt
            byte[] hashToCompare = PBKDF2(enteredPassword, salt, Iterations, KeySize);

            // Convertir hash a Base64 y comparar con el hash almacenado
            string hashToCompareBase64 = Convert.ToBase64String(hashToCompare);
            return hashToCompareBase64 == hashBase64;
        }

        // Método para generar un salt aleatorio usando RandomNumberGenerator
        private byte[] GenerateSalt(int size)
        {
            byte[] salt = new byte[size];
            RandomNumberGenerator.Fill(salt); // API actualizada para generar salt
            return salt;
        }

        // Método para realizar el hashing de la contraseña usando PBKDF2 con un algoritmo seguro
        private byte[] PBKDF2(string password, byte[] salt, int iterations, int keySize)
        {
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return rfc2898DeriveBytes.GetBytes(keySize);
            }
        }
    }
}
