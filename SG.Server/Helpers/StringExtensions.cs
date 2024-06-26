﻿using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace SG.Server.Helpers
{
    internal static class StringExtensions
    {
        internal static string ConvertToHash(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));

                // Convert the byte array to a hexadecimal string

                // Using StringBuilder for faster string concat
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2")); // Use "x2" to format as hexadecimal
                }

                return builder.ToString();
            }
        }

        internal static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        internal static bool IsValidJson(this string input)
        {
            try
            {
                JsonDocument.Parse(input);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }
    }
}
