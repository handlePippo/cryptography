using System;
using System.Globalization;
using System.Text;

namespace Crittografia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string text;
            do
            {
                Console.WriteLine("Inserisci una frase di cifrare");
                text = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(text));

            string key;
            do
            {
                Console.WriteLine("Inserisci una chiave di cifratura in formato numerico");
                key = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(key) || !int.TryParse(key, out var _));

            (string encryptedWord, string encryptionKey) = Encrypter.Encrypt(text, key);
            _ = Decrypter.Decrypt(encryptedWord, encryptionKey);

            Console.ReadLine();
        }
    }

    internal class Encrypter
    {
        public static Tuple<string, string> Encrypt(string text, string key)
        {
            StringBuilder builder = new StringBuilder();
            int rotation = 0;
            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    // Calcolo la rotazione sulla chiave di cifratura
                    if (rotation == key.Length - 1)
                    {
                        rotation = 0;
                    }

                    // Prendo il valore Unicode del char corrente
                    int unicodeNumber = Convert.ToInt32(c);

                    // Incremento il valore Unicode in base alla rotazione corrente
                    unicodeNumber += CharUnicodeInfo.GetDigitValue(key[rotation]);

                    // Prendo il valore testuale dell'Unicode calcolato e e lo appendo al builder
                    var t = Convert.ToChar(unicodeNumber);
                    builder.Append(t);

                    // Incremento l'indice per la rotazione sulla chiave di cifratura
                    rotation++;
                }
                else
                {
                    // Nel caso sia un carattere speciale, lo appendo così com'è
                    builder.Append(c);
                }
            }
            Console.WriteLine(string.Format("Frase crittata: {0}", builder.ToString()));

            return Tuple.Create(builder.ToString(), key);
        }
    }

    internal class Decrypter
    {
        public static string Decrypt(string text, string key)
        {
            StringBuilder builder = new StringBuilder();
            int rotation = 0;
            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    // Calcolo la rotazione sulla chiave di cifratura
                    if (rotation == key.Length - 1)
                    {
                        rotation = 0;
                    }

                    // Prendo il valore Unicode del char corrente
                    int unicodeNumber = Convert.ToInt32(c);

                    // Decremento in base alla rotazione corrente
                    unicodeNumber -= CharUnicodeInfo.GetDigitValue(key[rotation]);

                    // Prendo il valore testuale dell'Unicode calcolato e lo appendo al builder
                    var t = Convert.ToChar(unicodeNumber);
                    builder.Append(t);

                    // Incremento l'indice per la rotazione sulla chiave di cifratura
                    rotation++;
                }
                else
                {
                    // Nel caso sia un carattere speciale, lo appendo così com'è
                    builder.Append(c);
                }
            }
            Console.WriteLine(string.Format("Frase decrittata: {0}", builder.ToString()));

            return builder.ToString();
        }
    }
}
