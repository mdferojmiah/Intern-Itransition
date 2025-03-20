using System;
using System.Security.Cryptography;
using System.Text;

public class RandomGenerator{
    // this class if for generating random number and hash
    public static (int number, byte[] key, string hmac) GenerateRandom(int range){
        using(RandomNumberGenerator rng = RandomNumberGenerator.Create()){
            byte[] key = new byte[32];
            rng.GetBytes(key);
            int number = RandomNumberGenerator.GetInt32(0, range);

            using(HMACSHA3_256 hmacsha3 = new HMACSHA3_256(key)){
                byte[] messageBytes = BitConverter.GetBytes(number);
                byte[] hmacBytes = hmacsha3.ComputeHash(messageBytes);
                string hmac = BitConverter.ToString(hmacBytes).Replace("-", "");
                return (number, key, hmac);
            }
        }
    }

    public static int CalculateResult(int computerNumber, int userNumber, int range){
        return (computerNumber + userNumber) % range;
    }
}