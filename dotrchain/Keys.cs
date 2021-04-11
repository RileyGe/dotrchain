﻿using System;
using System.Linq;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using NokitaKaze.Base58Check;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Asn1;
using System.IO;

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
            BigInteger d = new BigInteger(1, privateKeyBytes);
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
            this(Util.HexToBytes(privateKeyHex))
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
            ////for test
            //var prKey = new PrivateKey(keyPair.Private as ECPrivateKeyParameters);
            //var puKey = new PublicKey(keyPair.Public as ECPublicKeyParameters);

            //if(!prKey.PublicKey.Equals(puKey))            
            //    throw new Exception("get public key from private key error!");

            return new PrivateKey(keyPair.Private as ECPrivateKeyParameters);
        }
        /// <summary>
        /// get the private key hex string
        /// </summary>
        /// <returns>private key hex string</returns>
        public string ToHex()
        {
            return Util.BytesToHex(this.Bytes);
        }

        public byte[] Sign(byte[] data)
        {
            byte[] hashed = Util.Blake2b(data);
            ECDsaSigner signer = new ECDsaSigner();
            var order = SecNamedCurves.GetByName("secp256k1").Curve.Order;
            signer.Init(true, this.privateKey);            
            var rs = signer.GenerateSignature(hashed);
            var r = rs[0];
            var s = rs[1];
            if (s.CompareTo(order.Divide(BigInteger.Two)) > 0)            
                s = order.Subtract(s);            
            var ms = new MemoryStream(72);
            var generate = new DerSequenceGenerator(ms);
            generate.AddObject(new DerInteger(r));
            generate.AddObject(new DerInteger(s));
            generate.Close();
            return ms.ToArray();
        }       

        public PublicKey PublicKey
        {
            get
            {
                X9ECParameters curve = SecNamedCurves.GetByName("secp256k1");
                ECDomainParameters domain = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H);
                ECPoint q = domain.G.Multiply(this.privateKey.D);
                return new PublicKey(new ECPublicKeyParameters(q, domain));
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is PrivateKey)
            {
                var objPk = obj as PrivateKey;
                return Enumerable.SequenceEqual(this.Bytes, objPk.Bytes);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }        
    }
    public class PublicKey
    {
        private ECPublicKeyParameters publicKey;
        public PublicKey(byte[] publicKeyBytes)
        {
            X9ECParameters curveParams = ECNamedCurveTable.GetByName("secp256k1");
            ECCurve curve = curveParams.Curve;
            ECPoint decodePoint = curve.DecodePoint(publicKeyBytes);
            ECDomainParameters domainParams = new ECDomainParameters(curve, curveParams.G, curveParams.N, curveParams.H);
            this.publicKey = new ECPublicKeyParameters(decodePoint, domainParams);
        }
        public PublicKey(string publicKeyHex) : this(Util.HexToBytes(publicKeyHex)) { }
        public PublicKey(ECPublicKeyParameters pk)
        {
            this.publicKey = pk;
        }
        public byte[] Bytes
        {
            get
            {
                return this.publicKey.Q.GetEncoded();
            }
        }

        public bool Verify(byte[] signature, byte[] data)
        {
            var hashed = Util.Blake2b(data);
            //ISigner signer = SignerUtilities.GetSigner("NONEwithECDSA");
            //signer.Init(false, this.publicKey);
            //signer.BlockUpdate(hashed, 0, hashed.Length);
            //return signer.VerifySignature(signature);

            ECDsaSigner signer = new ECDsaSigner();
            //X9ECParameters paramsd = SecNamedCurves.GetByName("secp256k1");
            //ECDomainParameters ecParams = new ECDomainParameters(paramsd.Curve, paramsd.G, paramsd.N, paramsd.H);
            var decoder = new Asn1InputStream(signature);
            var seq = decoder.ReadObject() as DerSequence;
            if ((seq == null) || (seq.Count != 2))
                throw new ArgumentException("InvalidDERSignature");
            var R = ((DerInteger)seq[0]).Value;
            var S = ((DerInteger)seq[1]).Value;
            signer.Init(false, this.publicKey);
            return signer.VerifySignature(hashed, R, S);
        }

        public string ToHex()
        {
            return Util.BytesToHex(this.Bytes);
        }

        public string EthAddress
        {
            get
            {
                var digest = new KeccakDigest(256);
                var rawBytes = this.Bytes.Skip(1).ToArray();
                digest.BlockUpdate(rawBytes, 0, rawBytes.Length);
                var calculatedHash = new byte[digest.GetDigestSize()];
                digest.DoFinal(calculatedHash, 0);
                var rawHex = Util.BytesToHex(calculatedHash);
                return rawHex.Substring(rawHex.Length - 40);
            }
        }

        public string RevAddress
        {
            get 
            {
                var prefix = new byte[] { 0, 0, 0, 0 };
                var digest = new KeccakDigest(256);
                var rawBytes = this.Bytes.Skip(1).ToArray();
                digest.BlockUpdate(rawBytes, 0, rawBytes.Length);
                var calculatedHash = new byte[digest.GetDigestSize()];
                digest.DoFinal(calculatedHash, 0);

                digest.BlockUpdate(calculatedHash, calculatedHash.Length - 20, 20);
                var pubKeyHash = new byte[digest.GetDigestSize()];
                digest.DoFinal(pubKeyHash, 0);

                var payload = prefix.Concat(pubKeyHash).ToArray();
                Blake2bDigest black2b = new Blake2bDigest(32 * 8);
                black2b.BlockUpdate(payload, 0, payload.Length);
                var payloadChecksum = new byte[black2b.GetDigestSize()];
                black2b.DoFinal(payloadChecksum, 0);                
                payloadChecksum = payloadChecksum.Take(4).ToArray();
                var addressBytes = payload.Concat(payloadChecksum).ToArray();                
                return Base58CheckEncoding.EncodePlain(addressBytes);
            }
        }

        public override bool Equals(object obj)
        {
            if(obj is PublicKey)
            {
                var objPk = obj as PublicKey;
                return Enumerable.SequenceEqual(this.Bytes, objPk.Bytes);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
