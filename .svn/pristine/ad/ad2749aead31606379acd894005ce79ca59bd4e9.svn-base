using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class RandomNumberGenerator
    {
        public static string RandomNumberGeneration()
        {
            long ticks = DateTime.Now.Ticks;
            byte[] bytes = BitConverter.GetBytes(ticks);
            return Convert.ToBase64String(bytes).Replace('+', '_').Replace('/', '-').TrimEnd('=');
        }
        public static string RandomStyleSKUGeneration()
        {
            int incNumber = 0001;

            string formattedIncNumber = String.Format("{0:D4}", incNumber);
            incNumber++;
            return formattedIncNumber;
        }

		public void GetBytes(byte[] data)
		{
			throw new NotImplementedException();
		}

		public static implicit operator RandomNumberGenerator(RNGCryptoServiceProvider v)
		{
			throw new NotImplementedException();
		}
	}
}
