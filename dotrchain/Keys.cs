using System;
using System.Globalization;
using System.Linq;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace dotrchain
{
    public class PrivateKey
    {
        private ECPrivateKeyParameters privateKey;
        /// <summary>
        /// Bytes of the private key
        /// </summary>
        public byte[] Bytes
        {
            get
            {
                return privateKey.D.ToByteArrayUnsigned();
            }
        }
        /// <summary>
        /// create a private key from ECPrivateKeyParameters
        /// </summary>
        /// <param name="privateKey">ECPrivateKeyParameters</param>
        public PrivateKey(ECPrivateKeyParameters privateKey)
        {
            this.privateKey = privateKey;
        }
        /// <summary>
        /// create a private key from private key bytes
        /// </summary>
        /// <param name="privateKeyBytes">private key bytes</param>
        public PrivateKey(byte[] privateKeyBytes)
        {
            Org.BouncyCastle.Math.BigInteger d = new Org.BouncyCastle.Math.BigInteger(1, privateKeyBytes);
            var curve = ECNamedCurveTable.GetByName("secp256k1");
            var domainParams = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H, curve.GetSeed());
            this.privateKey = new ECPrivateKeyParameters("ECDSA", d, domainParams);
            //new ECPrivateKeyParameters(privateKey.get privateKey.getRaw(), SecP256K1Curve.secp256k1().getParams())
        }
        /// <summary>
        /// create a private key from private key hex string
        /// </summary>
        /// <param name="privateKeyHex">private key hex string</param>
        public PrivateKey(string privateKeyHex) :
            this(HexToBytes(privateKeyHex))
        { }
        /// <summary>
        /// create a private key from seed
        /// </summary>
        /// <param name="seed">seed</param>
        public PrivateKey(int seed)
        {            
            var curve = ECNamedCurveTable.GetByName("secp256k1");
            var domainParams = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H, curve.GetSeed());

            var secureRandom = new SecureRandom();
            secureRandom.SetSeed(BitConverter.GetBytes(seed));
            var keyParams = new ECKeyGenerationParameters(domainParams, secureRandom);

            var generator = new ECKeyPairGenerator("ECDSA");
            generator.Init(keyParams);
            var keyPair = generator.GenerateKeyPair();
            this.privateKey = keyPair.Private as ECPrivateKeyParameters;
        }
        /// <summary>
        /// generate a random private key
        /// </summary>
        /// <returns>private key</returns>
        public static PrivateKey Generate()
        {
            var curve = ECNamedCurveTable.GetByName("secp256k1");
            var domainParams = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H, curve.GetSeed());

            var secureRandom = new SecureRandom();
            var keyParams = new ECKeyGenerationParameters(domainParams, secureRandom);

            var generator = new ECKeyPairGenerator("ECDSA");
            generator.Init(keyParams);
            var keyPair = generator.GenerateKeyPair();            
            return new PrivateKey(keyPair.Private as ECPrivateKeyParameters);
        }
        /// <summary>
        /// get the private key hex string
        /// </summary>
        /// <returns>private key hex string</returns>
        public string ToHex()
        {
            return ToHex(this.Bytes);
        }

        static string ToHex(byte[] data) => 
            string.Concat(data.Select(x => x.ToString("x2")));


        private static byte[] HexToBytes(string hex) =>
            Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
    }
    public class PublicKey
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static PublicKey FromByets()
        {
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static PublicKey FromHex()
        {
            return null;
        }
        private ECPublicKeyParameters _pk;
        public PublicKey(ECPublicKeyParameters pk)
        {
            this._pk = pk;
        }
        static AsymmetricCipherKeyPair GenerateKeyPair()
        {
            var curve = ECNamedCurveTable.GetByName("secp256k1");
            var domainParams = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H, curve.GetSeed());

            var secureRandom = new SecureRandom();
            var keyParams = new ECKeyGenerationParameters(domainParams, secureRandom);

            var generator = new ECKeyPairGenerator("ECDSA");
            generator.Init(keyParams);
            var keyPair = generator.GenerateKeyPair();

            var privateKey = keyPair.Private as ECPrivateKeyParameters;
            var publicKey = keyPair.Public as ECPublicKeyParameters;

            Console.WriteLine($"Private key: {ToHex(privateKey.D.ToByteArrayUnsigned())}");
            Console.WriteLine($"Public key: {ToHex(publicKey.Q.GetEncoded())}");
            

            return keyPair;
        }

        static string ToHex(byte[] data) => String.Concat(data.Select(x => x.ToString("x2")));
    }
}
