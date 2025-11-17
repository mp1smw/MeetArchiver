using System;
using System.Security.Cryptography;
using System.Text;

namespace DR_APIs.Utils
{
    /// <summary>
    /// Simple, secure password hashing helper using PBKDF2 (Rfc2898DeriveBytes with SHA-256).
    /// Produces a compact string: "{iterations}.{saltBase64}.{hashBase64}".
    /// Use <see cref="Hash"/> to create a hash and <see cref="Verify"/> to verify a password.
    /// </summary>
    public static class PasswordHasher
    {
        // Defaults - safe, reasonable values for modern apps
        private const int SaltSize = 16;      // 128-bit salt
        private const int HashSize = 32;      // 256-bit subkey
        private const int DefaultIterations = 100_000;

        /// <summary>
        /// Hash a plaintext password. Returns a string containing iterations, salt and hash.
        /// </summary>
        public static string Hash(string password, int iterations = DefaultIterations)
        {
            if (password is null) throw new ArgumentNullException(nameof(password));
            if (iterations <= 0) throw new ArgumentOutOfRangeException(nameof(iterations));

            // Generate salt
            var salt = new byte[SaltSize];
            RandomNumberGenerator.Fill(salt);

            // Derive subkey (hash)
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var subkey = pbkdf2.GetBytes(HashSize);

            // Return formatted string: iterations.salt.hash (base64)
            var saltB64 = Convert.ToBase64String(salt);
            var hashB64 = Convert.ToBase64String(subkey);
            return $"{iterations}.{saltB64}.{hashB64}";
        }

        /// <summary>
        /// Verify a plaintext password against a stored hash created by <see cref="Hash"/>.
        /// Returns true when the password matches.
        /// </summary>
        public static bool Verify(string password, string storedHash)
        {
            if (password is null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(storedHash)) return false;

            // Expected format: iterations.salt.hash
            var parts = storedHash.Split('.', 3);
            if (parts.Length != 3) return false;

            if (!int.TryParse(parts[0], out var iterations) || iterations <= 0) return false;

            byte[] salt;
            byte[] expectedSubkey;
            try
            {
                salt = Convert.FromBase64String(parts[1]);
                expectedSubkey = Convert.FromBase64String(parts[2]);
            }
            catch
            {
                return false;
            }

            // Derive subkey from incoming password
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var actualSubkey = pbkdf2.GetBytes(expectedSubkey.Length);

            // Constant-time comparison
            return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
        }
    }
}