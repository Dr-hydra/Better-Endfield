// ========================================================
// Dumped by @desirepro
// Assembly: Mono.Security.dll
// Classes:  104
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000002  // size: 0x10
    public sealed class Locale
    {
        // Methods
        // RVA: 0x0464E630  token: 0x6000001
        public static System.String GetText(System.String msg) { }
        // RVA: 0x05397134  token: 0x6000002
        public static System.String GetText(System.String fmt, System.Object[] args) { }

    }

    // TypeToken: 0x2000007  // size: 0x20
    public class ContentInfo
    {
        // Fields
        private System.String contentType;  // 0x10
        private Mono.Security.ASN1 content;  // 0x18

        // Properties
        Mono.Security.ASN1 ASN1 { get; /* RVA: 0x0A70FFD8 */ }
        Mono.Security.ASN1 Content { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.String ContentType { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }

        // Methods
        // RVA: 0x0A70FCF0  token: 0x6000023
        public System.Void .ctor() { }
        // RVA: 0x0A70FDD0  token: 0x6000024
        public System.Void .ctor(System.String oid) { }
        // RVA: 0x0A70FD58  token: 0x6000025
        public System.Void .ctor(System.Byte[] data) { }
        // RVA: 0x0A70FE00  token: 0x6000026
        public System.Void .ctor(Mono.Security.ASN1 asn1) { }
        // RVA: 0x0A70FC54  token: 0x600002C
        private Mono.Security.ASN1 GetASN1() { }

    }

    // TypeToken: 0x2000008  // size: 0x30
    public class EncryptedData
    {
        // Fields
        private System.Byte _version;  // 0x10
        private Mono.Security.PKCS7.ContentInfo _content;  // 0x18
        private Mono.Security.PKCS7.ContentInfo _encryptionAlgorithm;  // 0x20
        private System.Byte[] _encrypted;  // 0x28

        // Properties
        Mono.Security.PKCS7.ContentInfo EncryptionAlgorithm { get; /* RVA: 0x04D862C0 */ }
        System.Byte[] EncryptedContent { get; /* RVA: 0x0A710D68 */ }

        // Methods
        // RVA: 0x04D8CCB0  token: 0x600002D
        public System.Void .ctor() { }
        // RVA: 0x0A710948  token: 0x600002E
        public System.Void .ctor(Mono.Security.ASN1 asn1) { }

    }

    // TypeToken: 0x2000009  // size: 0x48
    public class SignedData
    {
        // Fields
        private System.Byte version;  // 0x10
        private System.String hashAlgorithm;  // 0x18
        private Mono.Security.PKCS7.ContentInfo contentInfo;  // 0x20
        private Mono.Security.X509.X509CertificateCollection certs;  // 0x28
        private System.Collections.ArrayList crls;  // 0x30
        private Mono.Security.PKCS7.SignerInfo signerInfo;  // 0x38
        private System.Boolean mda;  // 0x40

        // Properties
        Mono.Security.X509.X509CertificateCollection Certificates { get; /* RVA: 0x04D86240 */ }
        Mono.Security.PKCS7.ContentInfo ContentInfo { get; /* RVA: 0x04D862C0 */ }
        System.String HashName { set; /* RVA: 0x0A718824 */ }
        Mono.Security.PKCS7.SignerInfo SignerInfo { get; /* RVA: 0x04D85A50 */ }

        // Methods
        // RVA: 0x0A71824C  token: 0x6000031
        public System.Void .ctor(Mono.Security.ASN1 asn1) { }
        // RVA: 0x0A7180DC  token: 0x6000036
        private System.String OidToName(System.String oid) { }

    }

    // TypeToken: 0x200000A  // size: 0x50
    public class SignerInfo
    {
        // Fields
        private System.Byte version;  // 0x10
        private System.String hashAlgorithm;  // 0x18
        private System.Collections.ArrayList authenticatedAttributes;  // 0x20
        private System.Collections.ArrayList unauthenticatedAttributes;  // 0x28
        private System.Byte[] signature;  // 0x30
        private System.String issuer;  // 0x38
        private System.Byte[] serial;  // 0x40
        private System.Byte[] ski;  // 0x48

        // Properties
        System.String IssuerName { get; /* RVA: 0x04D85A50 */ }
        System.Byte[] SerialNumber { get; /* RVA: 0x0A718D20 */ }
        System.Collections.ArrayList AuthenticatedAttributes { get; /* RVA: 0x04D862C0 */ }
        System.String HashName { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Byte[] Signature { get; /* RVA: 0x0A718D74 */ }
        System.Collections.ArrayList UnauthenticatedAttributes { get; /* RVA: 0x04D86240 */ }
        System.Byte Version { get; /* RVA: 0x015EFCE0 */ }

        // Methods
        // RVA: 0x0A71885C  token: 0x6000037
        public System.Void .ctor() { }
        // RVA: 0x0A7188F0  token: 0x6000038
        public System.Void .ctor(Mono.Security.ASN1 asn1) { }

    }

    // TypeToken: 0x200000D  // size: 0x30
    public class DeriveBytes
    {
        // Fields
        private static System.Byte[] keyDiversifier;  // static @ 0x0
        private static System.Byte[] ivDiversifier;  // static @ 0x8
        private static System.Byte[] macDiversifier;  // static @ 0x10
        private System.String _hashName;  // 0x10
        private System.Int32 _iterations;  // 0x18
        private System.Byte[] _password;  // 0x20
        private System.Byte[] _salt;  // 0x28

        // Properties
        System.String HashName { set; /* RVA: 0x053908C0 */ }
        System.Int32 IterationCount { set; /* RVA: 0x011EF9B0 */ }
        System.Byte[] Password { set; /* RVA: 0x0A71075C */ }
        System.Byte[] Salt { set; /* RVA: 0x0A7107E8 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000065
        public System.Void .ctor() { }
        // RVA: 0x0A71002C  token: 0x600006A
        private System.Void Adjust(System.Byte[] a, System.Int32 aOff, System.Byte[] b) { }
        // RVA: 0x0A710218  token: 0x600006B
        private System.Byte[] Derive(System.Byte[] diversifier, System.Int32 n) { }
        // RVA: 0x0A710154  token: 0x600006C
        public System.Byte[] DeriveKey(System.Int32 size) { }
        // RVA: 0x0A7100F0  token: 0x600006D
        public System.Byte[] DeriveIV(System.Int32 size) { }
        // RVA: 0x0A7101B4  token: 0x600006E
        public System.Byte[] DeriveMAC(System.Int32 size) { }
        // RVA: 0x0A710618  token: 0x600006F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000010  // size: 0x28
    public class X509CrlEntry
    {
        // Fields
        private System.Byte[] sn;  // 0x10
        private System.DateTime revocationDate;  // 0x18
        private Mono.Security.X509.X509ExtensionCollection extensions;  // 0x20

        // Properties
        System.Byte[] SerialNumber { get; /* RVA: 0x0A71DF6C */ }
        System.DateTime RevocationDate { get; /* RVA: 0x0385B100 */ }
        Mono.Security.X509.X509ExtensionCollection Extensions { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x0A71DE74  token: 0x6000089
        private System.Void .ctor(Mono.Security.ASN1 entry) { }

    }

    // TypeToken: 0x2000013  // size: 0x18
    public class X509CertificateEnumerator : System.Collections.IEnumerator
    {
        // Fields
        private System.Collections.IEnumerator enumerator;  // 0x10

        // Properties
        Mono.Security.X509.X509Certificate Current { get; /* RVA: 0x0A71B624 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0A71B578 */ }

        // Methods
        // RVA: 0x0A71B5C4  token: 0x60000B5
        public System.Void .ctor(Mono.Security.X509.X509CertificateCollection mappings) { }
        // RVA: 0x0A71B4E4  token: 0x60000B8
        private virtual System.Boolean System.Collections.IEnumerator.MoveNext() { }
        // RVA: 0x0A71B52C  token: 0x60000B9
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x0A71B49C  token: 0x60000BA
        public System.Boolean MoveNext() { }

    }

    // TypeToken: 0x200001C  // size: 0x28
    public abstract class AttributeTypeAndValue
    {
        // Fields
        private System.String oid;  // 0x10
        private System.String attrValue;  // 0x18
        private System.Int32 upperBound;  // 0x20
        private System.Byte encoding;  // 0x24

        // Properties
        System.String Value { set; /* RVA: 0x0A70D120 */ }

        // Methods
        // RVA: 0x0A70D0D4  token: 0x60000E8
        protected System.Void .ctor(System.String oid, System.Int32 upperBound) { }
        // RVA: 0x0A70D0FC  token: 0x60000E9
        protected System.Void .ctor(System.String oid, System.Int32 upperBound, System.Byte encoding) { }
        // RVA: 0x0A70CEFC  token: 0x60000EB
        private Mono.Security.ASN1 GetASN1(System.Byte encoding) { }
        // RVA: 0x0A70CEF0  token: 0x60000EC
        private Mono.Security.ASN1 GetASN1() { }
        // RVA: 0x0A70D080  token: 0x60000ED
        private System.Byte SelectBestEncoding() { }

    }

    // TypeToken: 0x200001D  // size: 0x28
    public class CommonName : AttributeTypeAndValue
    {
        // Methods
        // RVA: 0x0A70FC10  token: 0x60000EE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001E  // size: 0x28
    public class SerialNumber : AttributeTypeAndValue
    {
        // Methods
        // RVA: 0x0A718090  token: 0x60000EF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001F  // size: 0x28
    public class LocalityName : AttributeTypeAndValue
    {
        // Methods
        // RVA: 0x0A710E44  token: 0x60000F0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000020  // size: 0x28
    public class StateOrProvinceName : AttributeTypeAndValue
    {
        // Methods
        // RVA: 0x0A718DC8  token: 0x60000F1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000021  // size: 0x28
    public class OrganizationName : AttributeTypeAndValue
    {
        // Methods
        // RVA: 0x0A710E98  token: 0x60000F2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000022  // size: 0x28
    public class OrganizationalUnitName : AttributeTypeAndValue
    {
        // Methods
        // RVA: 0x0A710EDC  token: 0x60000F3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000023  // size: 0x28
    public class EmailAddress : AttributeTypeAndValue
    {
        // Methods
        // RVA: 0x0A7108FC  token: 0x60000F4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000024  // size: 0x28
    public class DomainComponent : AttributeTypeAndValue
    {
        // Methods
        // RVA: 0x0A7108B0  token: 0x60000F5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000025  // size: 0x28
    public class UserId : AttributeTypeAndValue
    {
        // Methods
        // RVA: 0x0A718E94  token: 0x60000F6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000026  // size: 0x28
    public class Oid : AttributeTypeAndValue
    {
        // Methods
        // RVA: 0x0A710E88  token: 0x60000F7
        public System.Void .ctor(System.String oid) { }

    }

    // TypeToken: 0x2000027  // size: 0x28
    public class Title : AttributeTypeAndValue
    {
        // Methods
        // RVA: 0x0A718E50  token: 0x60000F8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000028  // size: 0x28
    public class CountryName : AttributeTypeAndValue
    {
        // Methods
        // RVA: 0x0A70FFE0  token: 0x60000F9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000029  // size: 0x28
    public class DnQualifier : AttributeTypeAndValue
    {
        // Methods
        // RVA: 0x0A710864  token: 0x60000FA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002A  // size: 0x28
    public class Surname : AttributeTypeAndValue
    {
        // Methods
        // RVA: 0x0A718E0C  token: 0x60000FB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002B  // size: 0x28
    public class GivenName : AttributeTypeAndValue
    {
        // Methods
        // RVA: 0x0A710DBC  token: 0x60000FC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002C  // size: 0x28
    public class Initial : AttributeTypeAndValue
    {
        // Methods
        // RVA: 0x0A710E00  token: 0x60000FD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004F  // size: 0x30
    public class PrivateKeyInfo
    {
        // Fields
        private System.Int32 _version;  // 0x10
        private System.String _algorithm;  // 0x18
        private System.Byte[] _key;  // 0x20
        private System.Collections.ArrayList _list;  // 0x28

        // Properties
        System.String Algorithm { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Byte[] PrivateKey { get; /* RVA: 0x0A72BAC0 */ set; /* RVA: 0x0A72BB14 */ }

        // Methods
        // RVA: 0x0A72BA28  token: 0x60001A6
        public System.Void .ctor() { }
        // RVA: 0x0A72BA90  token: 0x60001A7
        public System.Void .ctor(System.Byte[] data) { }
        // RVA: 0x0A72AEB0  token: 0x60001AC
        private System.Void Decode(System.Byte[] data) { }
        // RVA: 0x0A72B4FC  token: 0x60001AD
        public System.Byte[] GetBytes() { }
        // RVA: 0x0A72B984  token: 0x60001AE
        private static System.Byte[] RemoveLeadingZero(System.Byte[] bigInt) { }
        // RVA: 0x0A72B8EC  token: 0x60001AF
        private static System.Byte[] Normalize(System.Byte[] bigInt, System.Int32 length) { }
        // RVA: 0x0A72A998  token: 0x60001B0
        public static System.Security.Cryptography.RSA DecodeRSA(System.Byte[] keypair) { }
        // RVA: 0x0A72B1F8  token: 0x60001B1
        public static System.Byte[] Encode(System.Security.Cryptography.RSA rsa) { }
        // RVA: 0x0A72A868  token: 0x60001B2
        public static System.Security.Cryptography.DSA DecodeDSA(System.Byte[] privateKey, System.Security.Cryptography.DSAParameters dsaParameters) { }
        // RVA: 0x0A72B1B4  token: 0x60001B3
        public static System.Byte[] Encode(System.Security.Cryptography.DSA dsa) { }
        // RVA: 0x0A72B3F4  token: 0x60001B4
        public static System.Byte[] Encode(System.Security.Cryptography.AsymmetricAlgorithm aa) { }

    }

    // TypeToken: 0x2000050  // size: 0x30
    public class EncryptedPrivateKeyInfo
    {
        // Fields
        private System.String _algorithm;  // 0x10
        private System.Byte[] _salt;  // 0x18
        private System.Int32 _iterations;  // 0x20
        private System.Byte[] _data;  // 0x28

        // Properties
        System.String Algorithm { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.Byte[] EncryptedData { get; /* RVA: 0x0A724CBC */ set; /* RVA: 0x0A724DAC */ }
        System.Byte[] Salt { get; /* RVA: 0x0A724D10 */ }
        System.Int32 IterationCount { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x0A724E1C */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60001B5
        public System.Void .ctor() { }
        // RVA: 0x0A724CB4  token: 0x60001B6
        public System.Void .ctor(System.Byte[] data) { }
        // RVA: 0x0A7246D4  token: 0x60001BE
        private System.Void Decode(System.Byte[] data) { }
        // RVA: 0x0A724AB4  token: 0x60001BF
        public System.Byte[] GetBytes() { }

    }

    // TypeToken: 0x2000052  // size: 0x80
    public sealed class KeyGeneratedEventHandler : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x08337CA4  token: 0x60001CD
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x60001CE
        public virtual System.Void Invoke(System.Object sender, System.EventArgs e) { }

    }

    // TypeToken: 0x2000056  // size: 0x14
    public sealed struct Sign
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Mono.Math.BigInteger.Sign Negative;  // const
        public static Mono.Math.BigInteger.Sign Zero;  // const
        public static Mono.Math.BigInteger.Sign Positive;  // const

    }

    // TypeToken: 0x2000057  // size: 0x20
    public sealed class ModulusRing
    {
        // Fields
        private Mono.Math.BigInteger mod;  // 0x10
        private Mono.Math.BigInteger constant;  // 0x18

        // Methods
        // RVA: 0x0A728DB0  token: 0x600020D
        public System.Void .ctor(Mono.Math.BigInteger modulus) { }
        // RVA: 0x0A728664  token: 0x600020E
        public System.Void BarrettReduction(Mono.Math.BigInteger x) { }
        // RVA: 0x0A728ABC  token: 0x600020F
        public Mono.Math.BigInteger Multiply(Mono.Math.BigInteger a, Mono.Math.BigInteger b) { }
        // RVA: 0x0A728920  token: 0x6000210
        public Mono.Math.BigInteger Difference(Mono.Math.BigInteger a, Mono.Math.BigInteger b) { }
        // RVA: 0x0A728C9C  token: 0x6000211
        public Mono.Math.BigInteger Pow(Mono.Math.BigInteger a, Mono.Math.BigInteger k) { }
        // RVA: 0x0A728C18  token: 0x6000212
        public Mono.Math.BigInteger Pow(System.UInt32 b, Mono.Math.BigInteger exp) { }

    }

    // TypeToken: 0x2000058  // size: 0x10
    public sealed class Kernel
    {
        // Methods
        // RVA: 0x0A724EA0  token: 0x6000213
        public static Mono.Math.BigInteger AddSameSign(Mono.Math.BigInteger bi1, Mono.Math.BigInteger bi2) { }
        // RVA: 0x0A725C64  token: 0x6000214
        public static Mono.Math.BigInteger Subtract(Mono.Math.BigInteger big, Mono.Math.BigInteger small) { }
        // RVA: 0x0A72553C  token: 0x6000215
        public static System.Void MinusEq(Mono.Math.BigInteger big, Mono.Math.BigInteger small) { }
        // RVA: 0x0A7258AC  token: 0x6000216
        public static System.Void PlusEq(Mono.Math.BigInteger bi1, Mono.Math.BigInteger bi2) { }
        // RVA: 0x0A725070  token: 0x6000217
        public static Mono.Math.BigInteger.Sign Compare(Mono.Math.BigInteger bi1, Mono.Math.BigInteger bi2) { }
        // RVA: 0x0A725BF8  token: 0x6000218
        public static System.UInt32 SingleByteDivideInPlace(Mono.Math.BigInteger n, System.UInt32 d) { }
        // RVA: 0x0A72530C  token: 0x6000219
        public static System.UInt32 DwordMod(Mono.Math.BigInteger n, System.UInt32 d) { }
        // RVA: 0x0A7251A4  token: 0x600021A
        public static Mono.Math.BigInteger[] DwordDivMod(Mono.Math.BigInteger n, System.UInt32 d) { }
        // RVA: 0x0A72634C  token: 0x600021B
        public static Mono.Math.BigInteger[] multiByteDivide(Mono.Math.BigInteger bi1, Mono.Math.BigInteger bi2) { }
        // RVA: 0x0A725364  token: 0x600021C
        public static Mono.Math.BigInteger LeftShift(Mono.Math.BigInteger bi, System.Int32 n) { }
        // RVA: 0x0A725A44  token: 0x600021D
        public static Mono.Math.BigInteger RightShift(Mono.Math.BigInteger bi, System.Int32 n) { }
        // RVA: 0x0A725798  token: 0x600021E
        public static System.Void Multiply(System.UInt32[] x, System.UInt32 xOffset, System.UInt32 xLen, System.UInt32[] y, System.UInt32 yOffset, System.UInt32 yLen, System.UInt32[] d, System.UInt32 dOffset) { }
        // RVA: 0x0A725664  token: 0x600021F
        public static System.Void MultiplyMod2p32pmod(System.UInt32[] x, System.Int32 xOffset, System.Int32 xLen, System.UInt32[] y, System.Int32 yOffest, System.Int32 yLen, System.UInt32[] d, System.Int32 dOffset, System.Int32 mod) { }
        // RVA: 0x0A725E10  token: 0x6000220
        public static System.UInt32 modInverse(Mono.Math.BigInteger bi, System.UInt32 modulus) { }
        // RVA: 0x0A725EBC  token: 0x6000221
        public static Mono.Math.BigInteger modInverse(Mono.Math.BigInteger bi, Mono.Math.BigInteger modulus) { }

    }

    // TypeToken: 0x200005F  // size: 0x13
    public sealed struct __StaticArrayInitTypeSize=3
    {
    }

    // TypeToken: 0x2000060  // size: 0x19
    public sealed struct __StaticArrayInitTypeSize=9
    {
    }

    // TypeToken: 0x2000061  // size: 0x1A
    public sealed struct __StaticArrayInitTypeSize=10
    {
    }

    // TypeToken: 0x2000062  // size: 0x22
    public sealed struct __StaticArrayInitTypeSize=18
    {
    }

    // TypeToken: 0x2000063  // size: 0x24
    public sealed struct __StaticArrayInitTypeSize=20
    {
    }

    // TypeToken: 0x2000064  // size: 0x30
    public sealed struct __StaticArrayInitTypeSize=32
    {
    }

    // TypeToken: 0x2000065  // size: 0x40
    public sealed struct __StaticArrayInitTypeSize=48
    {
    }

    // TypeToken: 0x2000066  // size: 0x50
    public sealed struct __StaticArrayInitTypeSize=64
    {
    }

    // TypeToken: 0x2000067  // size: 0x110
    public sealed struct __StaticArrayInitTypeSize=256
    {
    }

    // TypeToken: 0x2000068  // size: 0xC4C
    public sealed struct __StaticArrayInitTypeSize=3132
    {
    }

    // TypeToken: 0x200005E  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=64 001D686DB504E20C792EAA07FE09224A45FF328E24A80072D04D16ABC5C2B5D2;  // static @ 0x0
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=3132 25E3E48132FBDBE9B7C0C6C54D7C10A5DE12A105AA3E5DE2A0DC808BF245B7A5;  // static @ 0x40
        private static readonly System.Int64 290C4A052C215D096172EB81AEE671FB3286E5C1DB5E73F96021FC09825DDB88;  // static @ 0xc80
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=3 32D0830B8EE1D49A66F395C8EA80E02BFC07C2A12A8EA8C8B484AF02108A1950;  // static @ 0xc88
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=3 35BF50EEF3270FD8CA09E66FC5B0481C5A151B14F6A634854E32F63633D49DCB;  // static @ 0xc8b
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=3 3C0C410618682C4DF0474E034114CC8E562F05A512B521AC367571017BDFA75D;  // static @ 0xc8e
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=3 44F5B1A2C48314502ACCBF186D1A2F9F7F176825898F32F1A2047B956194F174;  // static @ 0xc91
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=48 4800FBFC4566EB02D1727A4B1C949CCBC7535C216A0766564C199308631B5DD6;  // static @ 0xc94
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=20 533B8C444F951E83EFF7305E3807B66CE0005DE0A2D0A44873C130895A3BE6AA;  // static @ 0xcc4
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=256 55D0BF716B334D123E0088CFB3F8E2FEA17AF5025BB527F95EEB09BA978EA329;  // static @ 0xcd8
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=3 59BE5A634187B8A57216EFF5371A47732C05744B1C1A0A6382A6D5622C9FFDCE;  // static @ 0xdd8
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=32 5DF6E0E2761359D30A8275058E299FCC0381534545F55CF43E41983F5D4C9456;  // static @ 0xddb
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=3 5F8C6B3C66B972606D85E7651F67ADBD02E8316876884674E8328FA710747E5B;  // static @ 0xdfb
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=10 60C6ED13AF98DBFEEDA8F8197FFFCC349BB04395CC81DF0D477CBC57BF5B398B;  // static @ 0xdfe
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=10 64B3E7D737AFF47D4C3BBD81D2D06D697DDD8EB60F29E13E4425D19D8BBCA1F7;  // static @ 0xe08
        private static readonly System.Int64 6772A9B8BF207A3CFE6EE68769D6985B69522183F24A2A3D41BC3B4602953426;  // static @ 0xe18
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=9 684312AFB7719E57993D2826FFBAF7EA965614F20F91D999FB19B01E21AA62E6;  // static @ 0xe20
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=64 6AA56C4BCD208911792AD24C7681FEFB93BED51903AFC54860C9BD37E41E5A31;  // static @ 0xe29
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=3 796E63069E193A008CB4E85573AA1FE53C5F4E58B42A7F61FD0EEE1D89B5120B;  // static @ 0xe69
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=64 7C8975E1E60A5C8337F28EDF8C33C3B180360B7279644A9BC1AF3C51E6220BF5;  // static @ 0xe6c
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=3 850D7367E4FB0766E2CBC3ACF5AB42B4E98348E58E5A789845D4FCCDB63D2AEE;  // static @ 0xeac
        private static readonly System.Int64 992F16C986809AB68C7466CC3EC6F12B2506A962EA539753E5D84A2FB7FF8A24;  // static @ 0xeb0
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=3 9A65C09A11757751BFED67A414E00B188DC4C7757FCB6CBD33A916DDE4A3D925;  // static @ 0xeb8
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=18 9ACEFCC0C950280B64AB9E045E38C34ABF71EC70A0DC61B9C621C6BFB4F78047;  // static @ 0xebb
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=3 B1E34F4A11EE411B83415EF0B252A0B2BBCFCAC2E592865E09C12E4252C93A75;  // static @ 0xecd
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=3 C92FAD7F348A682E7D5B7E74C76B5D019174EE7BC87545B25A1FDD49FBCC2D0B;  // static @ 0xed0
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=3 E17B8359E685992B0DE6242AAA24FCB7404173CBB7FF8646FF7D658139F41B5F;  // static @ 0xed3
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=64 F83B332BE4E6A5A4B1C56AAF6DB52657DA495E149870057D8590AB9D7A6167AD;  // static @ 0xed6
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=3 FB6D7301FFDCB5FBA5807A19B4F0606947897C1105240B6BBA815352DBBE2064;  // static @ 0xf16

        // Methods
        // RVA: 0x0687A850  token: 0x6000230
        private static System.UInt32 ComputeStringHash(System.String s) { }

    }

namespace Mono.Math
{

    // TypeToken: 0x2000055  // size: 0x20
    public class BigInteger
    {
        // Fields
        private System.UInt32 length;  // 0x10
        private System.UInt32[] data;  // 0x18
        private static readonly System.UInt32[] smallPrimes;  // static @ 0x0
        private static System.Security.Cryptography.RandomNumberGenerator rng;  // static @ 0x8

        // Properties
        System.Security.Cryptography.RandomNumberGenerator Rng { get; /* RVA: 0x0A723B08 */ }

        // Methods
        // RVA: 0x0A723758  token: 0x60001E1
        public System.Void .ctor(Mono.Math.BigInteger.Sign sign, System.UInt32 len) { }
        // RVA: 0x0A7236C0  token: 0x60001E2
        public System.Void .ctor(Mono.Math.BigInteger bi) { }
        // RVA: 0x0A723A54  token: 0x60001E3
        public System.Void .ctor(Mono.Math.BigInteger bi, System.UInt32 len) { }
        // RVA: 0x0A7237B8  token: 0x60001E4
        public System.Void .ctor(System.Byte[] inData) { }
        // RVA: 0x0A7239E0  token: 0x60001E5
        public System.Void .ctor(System.UInt32 ui) { }
        // RVA: 0x0A723DC0  token: 0x60001E6
        public static Mono.Math.BigInteger op_Implicit(System.UInt32 value) { }
        // RVA: 0x0A723E20  token: 0x60001E7
        public static Mono.Math.BigInteger op_Implicit(System.Int32 value) { }
        // RVA: 0x0A723BB0  token: 0x60001E8
        public static Mono.Math.BigInteger op_Addition(Mono.Math.BigInteger bi1, Mono.Math.BigInteger bi2) { }
        // RVA: 0x0A724240  token: 0x60001E9
        public static Mono.Math.BigInteger op_Subtraction(Mono.Math.BigInteger bi1, Mono.Math.BigInteger bi2) { }
        // RVA: 0x0A72401C  token: 0x60001EA
        public static System.UInt32 op_Modulus(Mono.Math.BigInteger bi, System.UInt32 ui) { }
        // RVA: 0x0A723FF0  token: 0x60001EB
        public static Mono.Math.BigInteger op_Modulus(Mono.Math.BigInteger bi1, Mono.Math.BigInteger bi2) { }
        // RVA: 0x0A723C78  token: 0x60001EC
        public static Mono.Math.BigInteger op_Division(Mono.Math.BigInteger bi1, Mono.Math.BigInteger bi2) { }
        // RVA: 0x0A724024  token: 0x60001ED
        public static Mono.Math.BigInteger op_Multiply(Mono.Math.BigInteger bi1, Mono.Math.BigInteger bi2) { }
        // RVA: 0x0A723FBC  token: 0x60001EE
        public static Mono.Math.BigInteger op_LeftShift(Mono.Math.BigInteger bi1, System.Int32 shiftVal) { }
        // RVA: 0x0A724238  token: 0x60001EF
        public static Mono.Math.BigInteger op_RightShift(Mono.Math.BigInteger bi1, System.Int32 shiftVal) { }
        // RVA: 0x0A722DA8  token: 0x60001F1
        public static Mono.Math.BigInteger GenerateRandom(System.Int32 bits, System.Security.Cryptography.RandomNumberGenerator rng) { }
        // RVA: 0x0A722F08  token: 0x60001F2
        public static Mono.Math.BigInteger GenerateRandom(System.Int32 bits) { }
        // RVA: 0x0A722BB4  token: 0x60001F3
        public System.Int32 BitCount() { }
        // RVA: 0x0A7232E8  token: 0x60001F4
        public System.Boolean TestBit(System.Int32 bitNum) { }
        // RVA: 0x0A723288  token: 0x60001F5
        public System.Void SetBit(System.UInt32 bitNum) { }
        // RVA: 0x0A723294  token: 0x60001F6
        public System.Void SetBit(System.UInt32 bitNum, System.Boolean value) { }
        // RVA: 0x0A723138  token: 0x60001F7
        public System.Int32 LowestSetBit() { }
        // RVA: 0x0A722F54  token: 0x60001F8
        public System.Byte[] GetBytes() { }
        // RVA: 0x0A723CA4  token: 0x60001F9
        public static System.Boolean op_Equality(Mono.Math.BigInteger bi1, System.UInt32 ui) { }
        // RVA: 0x0A723F64  token: 0x60001FA
        public static System.Boolean op_Inequality(Mono.Math.BigInteger bi1, System.UInt32 ui) { }
        // RVA: 0x0A723CFC  token: 0x60001FB
        public static System.Boolean op_Equality(Mono.Math.BigInteger bi1, Mono.Math.BigInteger bi2) { }
        // RVA: 0x0A723ED0  token: 0x60001FC
        public static System.Boolean op_Inequality(Mono.Math.BigInteger bi1, Mono.Math.BigInteger bi2) { }
        // RVA: 0x0A723DA8  token: 0x60001FD
        public static System.Boolean op_GreaterThan(Mono.Math.BigInteger bi1, Mono.Math.BigInteger bi2) { }
        // RVA: 0x0A723FDC  token: 0x60001FE
        public static System.Boolean op_LessThan(Mono.Math.BigInteger bi1, Mono.Math.BigInteger bi2) { }
        // RVA: 0x0A723D90  token: 0x60001FF
        public static System.Boolean op_GreaterThanOrEqual(Mono.Math.BigInteger bi1, Mono.Math.BigInteger bi2) { }
        // RVA: 0x0A723FC4  token: 0x6000200
        public static System.Boolean op_LessThanOrEqual(Mono.Math.BigInteger bi1, Mono.Math.BigInteger bi2) { }
        // RVA: 0x0A7235D8  token: 0x6000201
        public System.String ToString(System.UInt32 radix) { }
        // RVA: 0x0A723388  token: 0x6000202
        public System.String ToString(System.UInt32 radix, System.String characterSet) { }
        // RVA: 0x0A72323C  token: 0x6000203
        private System.Void Normalize() { }
        // RVA: 0x0A722C10  token: 0x6000204
        public System.Void Clear() { }
        // RVA: 0x0A72306C  token: 0x6000205
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0A723624  token: 0x6000206
        public virtual System.String ToString() { }
        // RVA: 0x0A722C58  token: 0x6000207
        public virtual System.Boolean Equals(System.Object o) { }
        // RVA: 0x0A7231AC  token: 0x6000208
        public Mono.Math.BigInteger ModInverse(Mono.Math.BigInteger modulus) { }
        // RVA: 0x0A7231B4  token: 0x6000209
        public Mono.Math.BigInteger ModPow(Mono.Math.BigInteger exp, Mono.Math.BigInteger n) { }
        // RVA: 0x0A722D38  token: 0x600020A
        public static Mono.Math.BigInteger GeneratePseudoPrime(System.Int32 bits) { }
        // RVA: 0x0A7230B0  token: 0x600020B
        public System.Void Incr2() { }
        // RVA: 0x0A723634  token: 0x600020C
        private static System.Void .cctor() { }

    }

}

namespace Mono.Math.Prime
{

    // TypeToken: 0x2000059  // size: 0x14
    public sealed struct ConfidenceFactor
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Mono.Math.Prime.ConfidenceFactor ExtraLow;  // const
        public static Mono.Math.Prime.ConfidenceFactor Low;  // const
        public static Mono.Math.Prime.ConfidenceFactor Medium;  // const
        public static Mono.Math.Prime.ConfidenceFactor High;  // const
        public static Mono.Math.Prime.ConfidenceFactor ExtraHigh;  // const
        public static Mono.Math.Prime.ConfidenceFactor Provable;  // const

    }

    // TypeToken: 0x200005A  // size: 0x80
    public sealed class PrimalityTest : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0A72A29C  token: 0x6000222
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x015802E0  token: 0x6000223
        public virtual System.Boolean Invoke(Mono.Math.BigInteger bi, Mono.Math.Prime.ConfidenceFactor confidence) { }

    }

    // TypeToken: 0x200005B  // size: 0x10
    public sealed class PrimalityTests
    {
        // Methods
        // RVA: 0x0A72A3C4  token: 0x6000224
        private static System.Int32 GetSPPRounds(Mono.Math.BigInteger bi, Mono.Math.Prime.ConfidenceFactor confidence) { }
        // RVA: 0x0A72A578  token: 0x6000225
        public static System.Boolean RabinMillerTest(Mono.Math.BigInteger n, Mono.Math.Prime.ConfidenceFactor confidence) { }

    }

}

namespace Mono.Math.Prime.Generator
{

    // TypeToken: 0x200005C  // size: 0x10
    public abstract class PrimeGeneratorBase
    {
        // Properties
        Mono.Math.Prime.ConfidenceFactor Confidence { get; /* RVA: 0x02BBE540 */ }
        Mono.Math.Prime.PrimalityTest PrimalityTest { get; /* RVA: 0x0A72A800 */ }
        System.Int32 TrialDivisionBounds { get; /* RVA: 0x04DA3390 */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000229
        public virtual Mono.Math.BigInteger GenerateNewPrime(System.Int32 bits) { }
        // RVA: 0x041E1670  token: 0x600022A
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200005D  // size: 0x10
    public class SequentialSearchPrimeGeneratorBase : Mono.Math.Prime.Generator.PrimeGeneratorBase
    {
        // Methods
        // RVA: 0x0A72DA54  token: 0x600022B
        protected virtual Mono.Math.BigInteger GenerateSearchBase(System.Int32 bits, System.Object context) { }
        // RVA: 0x0A72DA48  token: 0x600022C
        public virtual Mono.Math.BigInteger GenerateNewPrime(System.Int32 bits) { }
        // RVA: 0x0A72D834  token: 0x600022D
        public virtual Mono.Math.BigInteger GenerateNewPrime(System.Int32 bits, System.Object context) { }
        // RVA: 0x02FFF600  token: 0x600022E
        protected virtual System.Boolean IsPrimeAcceptable(Mono.Math.BigInteger bi, System.Object context) { }
        // RVA: 0x041E1670  token: 0x600022F
        public System.Void .ctor() { }

    }

}

namespace Mono.Security
{

    // TypeToken: 0x2000003  // size: 0x28
    public class ASN1
    {
        // Fields
        private System.Byte m_nTag;  // 0x10
        private System.Byte[] m_aValue;  // 0x18
        private System.Collections.ArrayList elist;  // 0x20

        // Properties
        System.Int32 Count { get; /* RVA: 0x0A70CD60 */ }
        System.Byte Tag { get; /* RVA: 0x015EFCE0 */ }
        System.Int32 Length { get; /* RVA: 0x09EEF220 */ }
        System.Byte[] Value { get; /* RVA: 0x0A70CE5C */ set; /* RVA: 0x0A70CE9C */ }
        Mono.Security.ASN1 Item { get; /* RVA: 0x0A70CD88 */ }

        // Methods
        // RVA: 0x05391954  token: 0x6000003
        public System.Void .ctor(System.Byte tag) { }
        // RVA: 0x03219380  token: 0x6000004
        public System.Void .ctor(System.Byte tag, System.Byte[] data) { }
        // RVA: 0x0A70CC0C  token: 0x6000005
        public System.Void .ctor(System.Byte[] data) { }
        // RVA: 0x0A70C118  token: 0x600000B
        private System.Boolean CompareArray(System.Byte[] array1, System.Byte[] array2) { }
        // RVA: 0x0A70C17C  token: 0x600000C
        public System.Boolean CompareValue(System.Byte[] value) { }
        // RVA: 0x0A70C09C  token: 0x600000D
        public Mono.Security.ASN1 Add(Mono.Security.ASN1 asn1) { }
        // RVA: 0x0A70C468  token: 0x600000E
        public virtual System.Byte[] GetBytes() { }
        // RVA: 0x0A70C270  token: 0x600000F
        protected System.Void Decode(System.Byte[] asn1, System.Int32& anPos, System.Int32 anLength) { }
        // RVA: 0x0A70C18C  token: 0x6000010
        protected System.Void DecodeTLV(System.Byte[] asn1, System.Int32& pos, System.Byte& tag, System.Int32& length, System.Byte[]& content) { }
        // RVA: 0x0A70C374  token: 0x6000012
        public Mono.Security.ASN1 Element(System.Int32 index, System.Byte anTag) { }
        // RVA: 0x0A70C9E4  token: 0x6000013
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000004  // size: 0x10
    public static class ASN1Convert
    {
        // Methods
        // RVA: 0x0A70B5A4  token: 0x6000014
        public static Mono.Security.ASN1 FromInt32(System.Int32 value) { }
        // RVA: 0x0A70B6D4  token: 0x6000015
        public static Mono.Security.ASN1 FromOid(System.String oid) { }
        // RVA: 0x0A70B7AC  token: 0x6000016
        public static Mono.Security.ASN1 FromUnsignedBigInteger(System.Byte[] big) { }
        // RVA: 0x0A70BD50  token: 0x6000017
        public static System.Int32 ToInt32(Mono.Security.ASN1 asn1) { }
        // RVA: 0x0A70BE74  token: 0x6000018
        public static System.String ToOid(Mono.Security.ASN1 asn1) { }
        // RVA: 0x0A70B8C8  token: 0x6000019
        public static System.DateTime ToDateTime(Mono.Security.ASN1 time) { }

    }

    // TypeToken: 0x2000005  // size: 0x10
    public sealed class BitConverterLE
    {
        // Methods
        // RVA: 0x0A70DA14  token: 0x600001A
        private static System.Byte[] GetUIntBytes(System.Byte* bytes) { }
        // RVA: 0x0A70DAF0  token: 0x600001B
        private static System.Byte[] GetULongBytes(System.Byte* bytes) { }
        // RVA: 0x0A70D9F8  token: 0x600001C
        private static System.Byte[] GetBytes(System.Int32 value) { }
        // RVA: 0x0A70D9DC  token: 0x600001D
        private static System.Byte[] GetBytes(System.Int64 value) { }
        // RVA: 0x0A70DD94  token: 0x600001E
        private static System.Void UShortFromBytes(System.Byte* dst, System.Byte[] src, System.Int32 startIndex) { }
        // RVA: 0x0A70DC9C  token: 0x600001F
        private static System.Void UIntFromBytes(System.Byte* dst, System.Byte[] src, System.Int32 startIndex) { }
        // RVA: 0x0A70DC4C  token: 0x6000020
        private static System.Int32 ToInt32(System.Byte[] value, System.Int32 startIndex) { }
        // RVA: 0x0A70DC74  token: 0x6000021
        private static System.UInt16 ToUInt16(System.Byte[] value, System.Int32 startIndex) { }
        // RVA: 0x0A70DC4C  token: 0x6000022
        private static System.UInt32 ToUInt32(System.Byte[] value, System.Int32 startIndex) { }

    }

    // TypeToken: 0x2000006  // size: 0x10
    public sealed class PKCS7
    {
    }

}

namespace Mono.Security.Authenticode
{

    // TypeToken: 0x2000053  // size: 0x40
    public class AuthenticodeBase
    {
        // Fields
        private System.Byte[] fileblock;  // 0x10
        private System.IO.Stream fs;  // 0x18
        private System.Int32 blockNo;  // 0x20
        private System.Int32 blockLength;  // 0x24
        private System.Int32 peOffset;  // 0x28
        private System.Int32 dirSecurityOffset;  // 0x2c
        private System.Int32 dirSecuritySize;  // 0x30
        private System.Int32 coffSymbolTableOffset;  // 0x34
        private System.Boolean pe64;  // 0x38

        // Properties
        System.Int32 PEOffset { get; /* RVA: 0x0A721478 */ }

        // Methods
        // RVA: 0x0A721430  token: 0x60001CF
        public System.Void .ctor() { }
        // RVA: 0x0A720FD4  token: 0x60001D1
        private System.Void Open(System.String filename) { }
        // RVA: 0x0A721074  token: 0x60001D2
        private System.Void Open(System.Byte[] rawdata) { }
        // RVA: 0x0A720AC4  token: 0x60001D3
        private System.Void Close() { }
        // RVA: 0x0A72134C  token: 0x60001D4
        private System.Void ReadFirstBlock() { }
        // RVA: 0x0A721104  token: 0x60001D5
        private System.Int32 ProcessFirstBlock() { }
        // RVA: 0x0A720F24  token: 0x60001D6
        private System.Byte[] GetSecurityEntry() { }
        // RVA: 0x0A720B00  token: 0x60001D7
        private System.Byte[] GetHash(System.Security.Cryptography.HashAlgorithm hash) { }

    }

    // TypeToken: 0x2000054  // size: 0x98
    public class AuthenticodeDeformatter : Mono.Security.Authenticode.AuthenticodeBase
    {
        // Fields
        private System.String filename;  // 0x40
        private System.Byte[] rawdata;  // 0x48
        private System.Byte[] hash;  // 0x50
        private Mono.Security.X509.X509CertificateCollection coll;  // 0x58
        private Mono.Security.ASN1 signedHash;  // 0x60
        private System.DateTime timestamp;  // 0x68
        private Mono.Security.X509.X509Certificate signingCertificate;  // 0x70
        private System.Int32 reason;  // 0x78
        private System.Boolean trustedRoot;  // 0x7c
        private System.Boolean trustedTimestampRoot;  // 0x7d
        private System.Byte[] entry;  // 0x80
        private Mono.Security.X509.X509Chain signerChain;  // 0x88
        private Mono.Security.X509.X509Chain timestampChain;  // 0x90

        // Properties
        System.Byte[] RawData { set; /* RVA: 0x0A722AC8 */ }
        Mono.Security.X509.X509Certificate SigningCertificate { get; /* RVA: 0x04D862B0 */ }

        // Methods
        // RVA: 0x0A7229EC  token: 0x60001D8
        public System.Void .ctor() { }
        // RVA: 0x0A722A98  token: 0x60001D9
        public System.Void .ctor(System.Byte[] rawData) { }
        // RVA: 0x0A72149C  token: 0x60001DC
        private System.Boolean CheckSignature() { }
        // RVA: 0x0A721798  token: 0x60001DD
        private System.Boolean CompareIssuerSerial(System.String issuer, System.Byte[] serial, Mono.Security.X509.X509Certificate x509) { }
        // RVA: 0x0A722144  token: 0x60001DE
        private System.Boolean VerifySignature(Mono.Security.PKCS7.SignedData sd, System.Byte[] calculatedMessageDigest, System.Security.Cryptography.HashAlgorithm ha) { }
        // RVA: 0x0A721934  token: 0x60001DF
        private System.Boolean VerifyCounterSignature(Mono.Security.PKCS7.SignerInfo cs, System.Byte[] signature) { }
        // RVA: 0x0A721858  token: 0x60001E0
        private System.Void Reset() { }

    }

}

namespace Mono.Security.Cryptography
{

    // TypeToken: 0x2000048  // size: 0x10
    public sealed class CryptoConvert
    {
        // Methods
        // RVA: 0x0A7245E4  token: 0x600017F
        public static System.String ToHex(System.Byte[] input) { }
        // RVA: 0x0A72440C  token: 0x6000180
        private static System.Byte FromHexChar(System.Char c) { }
        // RVA: 0x0A724494  token: 0x6000181
        public static System.Byte[] FromHex(System.String hex) { }

    }

    // TypeToken: 0x2000049  // size: 0x28
    public abstract class MD2 : System.Security.Cryptography.HashAlgorithm
    {
        // Methods
        // RVA: 0x04DA3AD0  token: 0x6000182
        protected System.Void .ctor() { }
        // RVA: 0x0A726ECC  token: 0x6000183
        public static Mono.Security.Cryptography.MD2 Create() { }

    }

    // TypeToken: 0x200004A  // size: 0x50
    public class MD2Managed : Mono.Security.Cryptography.MD2
    {
        // Fields
        private System.Byte[] state;  // 0x28
        private System.Byte[] checksum;  // 0x30
        private System.Byte[] buffer;  // 0x38
        private System.Int32 count;  // 0x40
        private System.Byte[] x;  // 0x48
        private static readonly System.Byte[] PI_SUBST;  // static @ 0x0

        // Methods
        // RVA: 0x0A726D10  token: 0x6000184
        private System.Byte[] Padding(System.Int32 nLength) { }
        // RVA: 0x0A726E14  token: 0x6000185
        public System.Void .ctor() { }
        // RVA: 0x0A726A74  token: 0x6000186
        public virtual System.Void Initialize() { }
        // RVA: 0x0A7268C8  token: 0x6000187
        protected virtual System.Void HashCore(System.Byte[] array, System.Int32 ibStart, System.Int32 cbSize) { }
        // RVA: 0x0A7269C4  token: 0x6000188
        protected virtual System.Byte[] HashFinal() { }
        // RVA: 0x0A726AD0  token: 0x6000189
        private System.Void MD2Transform(System.Byte[] state, System.Byte[] checksum, System.Byte[] block, System.Int32 index) { }
        // RVA: 0x0A726D88  token: 0x600018A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200004B  // size: 0x28
    public abstract class MD4 : System.Security.Cryptography.HashAlgorithm
    {
        // Methods
        // RVA: 0x04DA3AD0  token: 0x600018B
        protected System.Void .ctor() { }
        // RVA: 0x0A72820C  token: 0x600018C
        public static Mono.Security.Cryptography.MD4 Create() { }

    }

    // TypeToken: 0x200004C  // size: 0x50
    public class MD4Managed : Mono.Security.Cryptography.MD4
    {
        // Fields
        private System.UInt32[] state;  // 0x28
        private System.Byte[] buffer;  // 0x30
        private System.UInt32[] count;  // 0x38
        private System.UInt32[] x;  // 0x40
        private System.Byte[] digest;  // 0x48

        // Methods
        // RVA: 0x0A72812C  token: 0x600018D
        public System.Void .ctor() { }
        // RVA: 0x0A7272F8  token: 0x600018E
        public virtual System.Void Initialize() { }
        // RVA: 0x0A727078  token: 0x600018F
        protected virtual System.Void HashCore(System.Byte[] array, System.Int32 ibStart, System.Int32 cbSize) { }
        // RVA: 0x0A7271F8  token: 0x6000190
        protected virtual System.Byte[] HashFinal() { }
        // RVA: 0x0A7280D0  token: 0x6000191
        private System.Byte[] Padding(System.Int32 nLength) { }
        // RVA: 0x04DA3B20  token: 0x6000192
        private System.UInt32 F(System.UInt32 x, System.UInt32 y, System.UInt32 z) { }
        // RVA: 0x04DA3B80  token: 0x6000193
        private System.UInt32 G(System.UInt32 x, System.UInt32 y, System.UInt32 z) { }
        // RVA: 0x04DA3BD0  token: 0x6000194
        private System.UInt32 H(System.UInt32 x, System.UInt32 y, System.UInt32 z) { }
        // RVA: 0x04DA3BE0  token: 0x6000195
        private System.UInt32 ROL(System.UInt32 x, System.Byte n) { }
        // RVA: 0x04DA3AE0  token: 0x6000196
        private System.Void FF(System.UInt32& a, System.UInt32 b, System.UInt32 c, System.UInt32 d, System.UInt32 x, System.Byte s) { }
        // RVA: 0x04DA3B30  token: 0x6000197
        private System.Void GG(System.UInt32& a, System.UInt32 b, System.UInt32 c, System.UInt32 d, System.UInt32 x, System.Byte s) { }
        // RVA: 0x04DA3B90  token: 0x6000198
        private System.Void HH(System.UInt32& a, System.UInt32 b, System.UInt32 c, System.UInt32 d, System.UInt32 x, System.Byte s) { }
        // RVA: 0x0A726FB4  token: 0x6000199
        private System.Void Encode(System.Byte[] output, System.UInt32[] input) { }
        // RVA: 0x0A726F1C  token: 0x600019A
        private System.Void Decode(System.UInt32[] output, System.Byte[] input, System.Int32 index) { }
        // RVA: 0x0A7273C4  token: 0x600019B
        private System.Void MD4Transform(System.UInt32[] state, System.Byte[] block, System.Int32 index) { }

    }

    // TypeToken: 0x200004D  // size: 0x10
    public sealed class PKCS1
    {
        // Fields
        private static System.Byte[] emptySHA1;  // static @ 0x0
        private static System.Byte[] emptySHA256;  // static @ 0x8
        private static System.Byte[] emptySHA384;  // static @ 0x10
        private static System.Byte[] emptySHA512;  // static @ 0x18

        // Methods
        // RVA: 0x0A7293F0  token: 0x600019C
        private static System.Boolean Compare(System.Byte[] array1, System.Byte[] array2) { }
        // RVA: 0x0A729E14  token: 0x600019D
        public static System.Byte[] I2OSP(System.Byte[] x, System.Int32 size) { }
        // RVA: 0x0A729E94  token: 0x600019E
        public static System.Byte[] OS2IP(System.Byte[] x) { }
        // RVA: 0x03219480  token: 0x600019F
        public static System.Byte[] RSAVP1(System.Security.Cryptography.RSA rsa, System.Byte[] s) { }
        // RVA: 0x0A729F44  token: 0x60001A0
        public static System.Boolean Verify_v15(System.Security.Cryptography.RSA rsa, System.Security.Cryptography.HashAlgorithm hash, System.Byte[] hashValue, System.Byte[] signature, System.Boolean tryNonStandardEncoding) { }
        // RVA: 0x0A7297E4  token: 0x60001A1
        public static System.Byte[] Encode_v15(System.Security.Cryptography.HashAlgorithm hash, System.Byte[] hashValue, System.Int32 emLength) { }
        // RVA: 0x0A729AFC  token: 0x60001A2
        private static System.String HashNameFromOid(System.String oid, System.Boolean throwOnError) { }
        // RVA: 0x0A729794  token: 0x60001A3
        private static System.Security.Cryptography.HashAlgorithm CreateFromOid(System.String oid) { }
        // RVA: 0x0A729454  token: 0x60001A4
        private static System.Security.Cryptography.HashAlgorithm CreateFromName(System.String name) { }
        // RVA: 0x0A72A100  token: 0x60001A5
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200004E  // size: 0x10
    public sealed class PKCS8
    {
    }

    // TypeToken: 0x2000051  // size: 0x70
    public class RSAManaged : System.Security.Cryptography.RSA
    {
        // Fields
        private System.Boolean isCRTpossible;  // 0x20
        private System.Boolean keyBlinding;  // 0x21
        private System.Boolean keypairGenerated;  // 0x22
        private System.Boolean m_disposed;  // 0x23
        private Mono.Math.BigInteger d;  // 0x28
        private Mono.Math.BigInteger p;  // 0x30
        private Mono.Math.BigInteger q;  // 0x38
        private Mono.Math.BigInteger dp;  // 0x40
        private Mono.Math.BigInteger dq;  // 0x48
        private Mono.Math.BigInteger qInv;  // 0x50
        private Mono.Math.BigInteger n;  // 0x58
        private Mono.Math.BigInteger e;  // 0x60
        private Mono.Security.Cryptography.RSAManaged.KeyGeneratedEventHandler KeyGenerated;  // 0x68

        // Properties
        System.Int32 KeySize { get; /* RVA: 0x0A72D71C */ }
        System.Boolean PublicOnly { get; /* RVA: 0x0A72D7B4 */ }

        // Methods
        // RVA: 0x0A72D650  token: 0x60001C0
        public System.Void .ctor() { }
        // RVA: 0x0A72D660  token: 0x60001C1
        public System.Void .ctor(System.Int32 keySize) { }
        // RVA: 0x0A72C6AC  token: 0x60001C2
        protected virtual System.Void Finalize() { }
        // RVA: 0x0A72C724  token: 0x60001C3
        private System.Void GenerateKeyPair() { }
        // RVA: 0x0A72BBDC  token: 0x60001C6
        public virtual System.Byte[] DecryptValue(System.Byte[] rgb) { }
        // RVA: 0x0A72C1BC  token: 0x60001C7
        public virtual System.Byte[] EncryptValue(System.Byte[] rgb) { }
        // RVA: 0x0A72C2E8  token: 0x60001C8
        public virtual System.Security.Cryptography.RSAParameters ExportParameters(System.Boolean includePrivateParameters) { }
        // RVA: 0x0A72CA94  token: 0x60001C9
        public virtual System.Void ImportParameters(System.Security.Cryptography.RSAParameters parameters) { }
        // RVA: 0x0A72BF4C  token: 0x60001CA
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x0A72D0B8  token: 0x60001CB
        public virtual System.String ToXmlString(System.Boolean includePrivateParameters) { }
        // RVA: 0x0A72C9E8  token: 0x60001CC
        private System.Byte[] GetPaddedValue(Mono.Math.BigInteger value, System.Int32 length) { }

    }

}

namespace Mono.Security.Interface
{

    // TypeToken: 0x2000038  // size: 0x11
    public sealed struct AlertLevel
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Mono.Security.Interface.AlertLevel Warning;  // const
        public static Mono.Security.Interface.AlertLevel Fatal;  // const

    }

    // TypeToken: 0x2000039  // size: 0x11
    public sealed struct AlertDescription
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Mono.Security.Interface.AlertDescription CloseNotify;  // const
        public static Mono.Security.Interface.AlertDescription UnexpectedMessage;  // const
        public static Mono.Security.Interface.AlertDescription BadRecordMAC;  // const
        public static Mono.Security.Interface.AlertDescription DecryptionFailed_RESERVED;  // const
        public static Mono.Security.Interface.AlertDescription RecordOverflow;  // const
        public static Mono.Security.Interface.AlertDescription DecompressionFailure;  // const
        public static Mono.Security.Interface.AlertDescription HandshakeFailure;  // const
        public static Mono.Security.Interface.AlertDescription NoCertificate_RESERVED;  // const
        public static Mono.Security.Interface.AlertDescription BadCertificate;  // const
        public static Mono.Security.Interface.AlertDescription UnsupportedCertificate;  // const
        public static Mono.Security.Interface.AlertDescription CertificateRevoked;  // const
        public static Mono.Security.Interface.AlertDescription CertificateExpired;  // const
        public static Mono.Security.Interface.AlertDescription CertificateUnknown;  // const
        public static Mono.Security.Interface.AlertDescription IlegalParameter;  // const
        public static Mono.Security.Interface.AlertDescription UnknownCA;  // const
        public static Mono.Security.Interface.AlertDescription AccessDenied;  // const
        public static Mono.Security.Interface.AlertDescription DecodeError;  // const
        public static Mono.Security.Interface.AlertDescription DecryptError;  // const
        public static Mono.Security.Interface.AlertDescription ExportRestriction;  // const
        public static Mono.Security.Interface.AlertDescription ProtocolVersion;  // const
        public static Mono.Security.Interface.AlertDescription InsuficientSecurity;  // const
        public static Mono.Security.Interface.AlertDescription InternalError;  // const
        public static Mono.Security.Interface.AlertDescription UserCancelled;  // const
        public static Mono.Security.Interface.AlertDescription NoRenegotiation;  // const
        public static Mono.Security.Interface.AlertDescription UnsupportedExtension;  // const

    }

    // TypeToken: 0x200003A  // size: 0x18
    public class Alert
    {
        // Fields
        private Mono.Security.Interface.AlertLevel level;  // 0x10
        private Mono.Security.Interface.AlertDescription description;  // 0x11

        // Properties
        Mono.Security.Interface.AlertLevel Level { get; /* RVA: 0x015EFCE0 */ }
        Mono.Security.Interface.AlertDescription Description { get; /* RVA: 0x04D88380 */ }

        // Methods
        // RVA: 0x0A720A4C  token: 0x6000141
        public System.Void .ctor(Mono.Security.Interface.AlertDescription description) { }
        // RVA: 0x0A720A58  token: 0x6000142
        private System.Void inferAlertLevel() { }
        // RVA: 0x0A7209B8  token: 0x6000143
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200003B  // size: 0x20
    public class ValidationResult
    {
        // Fields
        private System.Boolean trusted;  // 0x10
        private System.Boolean user_denied;  // 0x11
        private System.Int32 error_code;  // 0x14
        private System.Nullable<Mono.Security.Interface.MonoSslPolicyErrors> policy_errors;  // 0x18

        // Properties
        System.Boolean Trusted { get; /* RVA: 0x015EFCE0 */ }
        System.Boolean UserDenied { get; /* RVA: 0x04D88380 */ }

        // Methods
        // RVA: 0x04DA3C50  token: 0x6000144
        public System.Void .ctor(System.Boolean trusted, System.Boolean user_denied, System.Int32 error_code, System.Nullable<Mono.Security.Interface.MonoSslPolicyErrors> policy_errors) { }

    }

    // TypeToken: 0x200003C
    public interface ICertificateValidator
    {
    }

    // TypeToken: 0x200003D  // size: 0x12
    public sealed struct CipherSuiteCode
    {
        // Fields
        public System.UInt16 value__;  // 0x10
        public static Mono.Security.Interface.CipherSuiteCode TLS_NULL_WITH_NULL_NULL;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_NULL_MD5;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_NULL_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_EXPORT_WITH_RC4_40_MD5;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_RC4_128_MD5;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_RC4_128_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_EXPORT_WITH_RC2_CBC_40_MD5;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_IDEA_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_EXPORT_WITH_DES40_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_DES_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_3DES_EDE_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_DSS_EXPORT_WITH_DES40_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_DSS_WITH_DES_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_DSS_WITH_3DES_EDE_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_RSA_EXPORT_WITH_DES40_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_RSA_WITH_DES_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_RSA_WITH_3DES_EDE_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_DSS_EXPORT_WITH_DES40_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_DSS_WITH_DES_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_DSS_WITH_3DES_EDE_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_EXPORT_WITH_DES40_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_DES_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_3DES_EDE_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_anon_EXPORT_WITH_RC4_40_MD5;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_anon_WITH_RC4_128_MD5;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_anon_EXPORT_WITH_DES40_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_anon_WITH_DES_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_anon_WITH_3DES_EDE_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_AES_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_DSS_WITH_AES_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_RSA_WITH_AES_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_DSS_WITH_AES_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_AES_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_anon_WITH_AES_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_AES_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_DSS_WITH_AES_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_RSA_WITH_AES_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_DSS_WITH_AES_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_AES_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_anon_WITH_AES_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_CAMELLIA_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_DSS_WITH_CAMELLIA_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_RSA_WITH_CAMELLIA_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_DSS_WITH_CAMELLIA_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_CAMELLIA_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_anon_WITH_CAMELLIA_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_CAMELLIA_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_DSS_WITH_CAMELLIA_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_RSA_WITH_CAMELLIA_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_DSS_WITH_CAMELLIA_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_CAMELLIA_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_anon_WITH_CAMELLIA_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_CAMELLIA_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_DSS_WITH_CAMELLIA_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_RSA_WITH_CAMELLIA_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_DSS_WITH_CAMELLIA_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_CAMELLIA_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_anon_WITH_CAMELLIA_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_CAMELLIA_256_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_DSS_WITH_CAMELLIA_256_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_RSA_WITH_CAMELLIA_256_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_DSS_WITH_CAMELLIA_256_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_CAMELLIA_256_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_anon_WITH_CAMELLIA_256_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_SEED_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_DSS_WITH_SEED_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_RSA_WITH_SEED_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_DSS_WITH_SEED_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_SEED_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_anon_WITH_SEED_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_RC4_128_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_3DES_EDE_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_AES_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_AES_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_PSK_WITH_RC4_128_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_PSK_WITH_3DES_EDE_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_PSK_WITH_AES_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_PSK_WITH_AES_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_PSK_WITH_RC4_128_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_PSK_WITH_3DES_EDE_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_PSK_WITH_AES_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_PSK_WITH_AES_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_ECDSA_WITH_NULL_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_ECDSA_WITH_RC4_128_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_ECDSA_WITH_3DES_EDE_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_ECDSA_WITH_AES_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_ECDSA_WITH_AES_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_ECDSA_WITH_NULL_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_ECDSA_WITH_RC4_128_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_ECDSA_WITH_3DES_EDE_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_RSA_WITH_NULL_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_RSA_WITH_RC4_128_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_RSA_WITH_3DES_EDE_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_RSA_WITH_AES_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_RSA_WITH_AES_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_RSA_WITH_NULL_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_RSA_WITH_RC4_128_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_RSA_WITH_3DES_EDE_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_anon_WITH_NULL_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_anon_WITH_RC4_128_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_anon_WITH_3DES_EDE_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_anon_WITH_AES_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_anon_WITH_AES_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_NULL_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_PSK_WITH_NULL_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_PSK_WITH_NULL_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_SRP_SHA_WITH_3DES_EDE_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_SRP_SHA_RSA_WITH_3DES_EDE_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_SRP_SHA_DSS_WITH_3DES_EDE_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_SRP_SHA_WITH_AES_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_SRP_SHA_RSA_WITH_AES_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_SRP_SHA_DSS_WITH_AES_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_SRP_SHA_WITH_AES_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_SRP_SHA_RSA_WITH_AES_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_SRP_SHA_DSS_WITH_AES_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_NULL_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_AES_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_AES_256_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_DSS_WITH_AES_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_RSA_WITH_AES_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_DSS_WITH_AES_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_AES_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_DSS_WITH_AES_256_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_RSA_WITH_AES_256_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_DSS_WITH_AES_256_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_AES_256_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_anon_WITH_AES_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_anon_WITH_AES_256_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_AES_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_AES_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_AES_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_AES_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_RSA_WITH_AES_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_RSA_WITH_AES_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_DSS_WITH_AES_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_DSS_WITH_AES_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_DSS_WITH_AES_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_DSS_WITH_AES_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_anon_WITH_AES_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_anon_WITH_AES_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_ECDSA_WITH_AES_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_ECDSA_WITH_AES_256_CBC_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_RSA_WITH_AES_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_RSA_WITH_AES_256_CBC_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_ECDSA_WITH_AES_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_ECDSA_WITH_AES_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_ECDSA_WITH_AES_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_ECDSA_WITH_AES_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_RSA_WITH_AES_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_RSA_WITH_AES_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_RSA_WITH_AES_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_RSA_WITH_AES_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_AES_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_AES_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_PSK_WITH_AES_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_PSK_WITH_AES_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_PSK_WITH_AES_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_PSK_WITH_AES_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_AES_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_AES_256_CBC_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_NULL_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_NULL_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_PSK_WITH_AES_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_PSK_WITH_AES_256_CBC_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_PSK_WITH_NULL_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_PSK_WITH_NULL_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_PSK_WITH_AES_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_PSK_WITH_AES_256_CBC_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_PSK_WITH_NULL_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_PSK_WITH_NULL_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_PSK_WITH_RC4_128_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_PSK_WITH_3DES_EDE_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_PSK_WITH_AES_128_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_PSK_WITH_AES_256_CBC_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_PSK_WITH_AES_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_PSK_WITH_AES_256_CBC_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_PSK_WITH_NULL_SHA;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_PSK_WITH_NULL_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_PSK_WITH_NULL_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_EMPTY_RENEGOTIATION_INFO_SCSV;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_ECDSA_WITH_CAMELLIA_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_ECDSA_WITH_CAMELLIA_256_CBC_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_ECDSA_WITH_CAMELLIA_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_ECDSA_WITH_CAMELLIA_256_CBC_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_RSA_WITH_CAMELLIA_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_RSA_WITH_CAMELLIA_256_CBC_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_RSA_WITH_CAMELLIA_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_RSA_WITH_CAMELLIA_256_CBC_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_CAMELLIA_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_CAMELLIA_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_CAMELLIA_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_CAMELLIA_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_RSA_WITH_CAMELLIA_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_RSA_WITH_CAMELLIA_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_DSS_WITH_CAMELLIA_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_DSS_WITH_CAMELLIA_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_DSS_WITH_CAMELLIA_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_DSS_WITH_CAMELLIA_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_anon_WITH_CAMELLIA_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DH_anon_WITH_CAMELLIA_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_ECDSA_WITH_CAMELLIA_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_ECDSA_WITH_CAMELLIA_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_ECDSA_WITH_CAMELLIA_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_ECDSA_WITH_CAMELLIA_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_RSA_WITH_CAMELLIA_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_RSA_WITH_CAMELLIA_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_RSA_WITH_CAMELLIA_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDH_RSA_WITH_CAMELLIA_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_CAMELLIA_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_CAMELLIA_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_PSK_WITH_CAMELLIA_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_PSK_WITH_CAMELLIA_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_PSK_WITH_CAMELLIA_128_GCM_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_PSK_WITH_CAMELLIA_256_GCM_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_CAMELLIA_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_CAMELLIA_256_CBC_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_PSK_WITH_CAMELLIA_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_PSK_WITH_CAMELLIA_256_CBC_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_PSK_WITH_CAMELLIA_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_PSK_WITH_CAMELLIA_256_CBC_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_PSK_WITH_CAMELLIA_128_CBC_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_PSK_WITH_CAMELLIA_256_CBC_SHA384;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_AES_128_CCM;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_AES_256_CCM;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_AES_128_CCM;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_AES_256_CCM;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_AES_128_CCM_8;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_AES_256_CCM_8;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_AES_128_CCM_8;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_AES_256_CCM_8;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_AES_128_CCM;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_AES_256_CCM;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_PSK_WITH_AES_128_CCM;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_PSK_WITH_AES_256_CCM;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_AES_128_CCM_8;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_AES_256_CCM_8;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_DHE_WITH_AES_128_CCM_8;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_DHE_WITH_AES_256_CCM_8;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_RSA_WITH_CHACHA20_POLY1305_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_ECDSA_WITH_CHACHA20_POLY1305_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_CHACHA20_POLY1305_SHA256;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_ESTREAM_SALSA20_SHA1;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_WITH_SALSA20_SHA1;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_RSA_WITH_ESTREAM_SALSA20_SHA1;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_RSA_WITH_SALSA20_SHA1;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_ECDSA_WITH_ESTREAM_SALSA20_SHA1;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_ECDSA_WITH_SALSA20_SHA1;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_ESTREAM_SALSA20_SHA1;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_PSK_WITH_SALSA20_SHA1;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_PSK_WITH_ESTREAM_SALSA20_SHA1;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_ECDHE_PSK_WITH_SALSA20_SHA1;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_PSK_WITH_ESTREAM_SALSA20_SHA1;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_RSA_PSK_WITH_SALSA20_SHA1;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_PSK_WITH_ESTREAM_SALSA20_SHA1;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_PSK_WITH_SALSA20_SHA1;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_ESTREAM_SALSA20_SHA1;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_DHE_RSA_WITH_SALSA20_SHA1;  // const
        public static Mono.Security.Interface.CipherSuiteCode TLS_FALLBACK_SCSV;  // const

    }

    // TypeToken: 0x200003E  // size: 0x20
    public class MonoTlsConnectionInfo
    {
        // Fields
        private Mono.Security.Interface.CipherSuiteCode <CipherSuiteCode>k__BackingField;  // 0x10
        private Mono.Security.Interface.TlsProtocols <ProtocolVersion>k__BackingField;  // 0x14
        private System.String <PeerDomainName>k__BackingField;  // 0x18

        // Properties
        Mono.Security.Interface.CipherSuiteCode CipherSuiteCode { get; /* RVA: 0x04D96190 */ set; /* RVA: 0x04D94F60 */ }
        Mono.Security.Interface.TlsProtocols ProtocolVersion { get; /* RVA: 0x04D882A0 */ set; /* RVA: 0x04D8F540 */ }
        System.String PeerDomainName { set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x0A728F78  token: 0x600014C
        public virtual System.String ToString() { }
        // RVA: 0x041E1670  token: 0x600014D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003F  // size: 0x14
    public sealed struct MonoSslPolicyErrors
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Mono.Security.Interface.MonoSslPolicyErrors None;  // const
        public static Mono.Security.Interface.MonoSslPolicyErrors RemoteCertificateNotAvailable;  // const
        public static Mono.Security.Interface.MonoSslPolicyErrors RemoteCertificateNameMismatch;  // const
        public static Mono.Security.Interface.MonoSslPolicyErrors RemoteCertificateChainErrors;  // const

    }

    // TypeToken: 0x2000040  // size: 0x80
    public sealed class MonoRemoteCertificateValidationCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0A728EA4  token: 0x600014E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014D0D00  token: 0x600014F
        public virtual System.Boolean Invoke(System.String targetHost, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, Mono.Security.Interface.MonoSslPolicyErrors sslPolicyErrors) { }

    }

    // TypeToken: 0x2000041  // size: 0x80
    public sealed class MonoLocalCertificateSelectionCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0976A0E8  token: 0x6000150
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014D0D00  token: 0x6000151
        public virtual System.Security.Cryptography.X509Certificates.X509Certificate Invoke(System.String targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection localCertificates, System.Security.Cryptography.X509Certificates.X509Certificate remoteCertificate, System.String[] acceptableIssuers) { }

    }

    // TypeToken: 0x2000042  // size: 0x10
    public abstract class MonoTlsProvider
    {
        // Properties
        System.Guid ID { get; /* RVA: -1  // abstract */ }
        System.String Name { get; /* RVA: -1  // abstract */ }
        System.Boolean SupportsSslStream { get; /* RVA: -1  // abstract */ }
        System.Boolean SupportsConnectionInfo { get; /* RVA: -1  // abstract */ }
        System.Boolean SupportsMonoExtensions { get; /* RVA: -1  // abstract */ }
        System.Security.Authentication.SslProtocols SupportedProtocols { get; /* RVA: -1  // abstract */ }
        System.Boolean SupportsCleanShutdown { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000152
        private System.Void .ctor() { }

    }

    // TypeToken: 0x2000043  // size: 0x10
    public static class MonoTlsProviderFactory
    {
        // Methods
        // RVA: 0x0A729010  token: 0x600015A
        public static Mono.Security.Interface.MonoTlsProvider GetProvider() { }

    }

    // TypeToken: 0x2000044  // size: 0x80
    public sealed class MonoTlsSettings
    {
        // Fields
        private Mono.Security.Interface.MonoRemoteCertificateValidationCallback <RemoteCertificateValidationCallback>k__BackingField;  // 0x10
        private Mono.Security.Interface.MonoLocalCertificateSelectionCallback <ClientCertificateSelectionCallback>k__BackingField;  // 0x18
        private System.Nullable<System.DateTime> <CertificateValidationTime>k__BackingField;  // 0x20
        private System.Security.Cryptography.X509Certificates.X509CertificateCollection <TrustAnchors>k__BackingField;  // 0x30
        private System.Object <UserSettings>k__BackingField;  // 0x38
        private System.String[] <CertificateSearchPaths>k__BackingField;  // 0x40
        private System.Boolean <SendCloseNotify>k__BackingField;  // 0x48
        private System.String[] <ClientCertificateIssuers>k__BackingField;  // 0x50
        private System.Boolean <DisallowUnauthenticatedCertificateRequest>k__BackingField;  // 0x58
        private System.Nullable<Mono.Security.Interface.TlsProtocols> <EnabledProtocols>k__BackingField;  // 0x5c
        private Mono.Security.Interface.CipherSuiteCode[] <EnabledCiphers>k__BackingField;  // 0x68
        private System.Boolean cloned;  // 0x70
        private System.Boolean checkCertName;  // 0x71
        private System.Boolean checkCertRevocationStatus;  // 0x72
        private System.Nullable<System.Boolean> useServicePointManagerCallback;  // 0x73
        private System.Boolean skipSystemValidators;  // 0x75
        private System.Boolean callbackNeedsChain;  // 0x76
        private Mono.Security.Interface.ICertificateValidator certificateValidator;  // 0x78
        private static Mono.Security.Interface.MonoTlsSettings defaultSettings;  // static @ 0x0

        // Properties
        Mono.Security.Interface.MonoRemoteCertificateValidationCallback RemoteCertificateValidationCallback { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        Mono.Security.Interface.MonoLocalCertificateSelectionCallback ClientCertificateSelectionCallback { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Nullable<System.Boolean> UseServicePointManagerCallback { get; /* RVA: 0x04DA3C20 */ set; /* RVA: 0x04DA3C40 */ }
        System.Boolean CallbackNeedsCertificateChain { get; /* RVA: 0x04D91610 */ }
        System.Nullable<System.DateTime> CertificateValidationTime { get; /* RVA: 0x04D8C200 */ set; /* RVA: 0x04D8C210 */ }
        System.Security.Cryptography.X509Certificates.X509CertificateCollection TrustAnchors { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        System.Object UserSettings { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }
        System.String[] CertificateSearchPaths { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        System.Boolean SendCloseNotify { get; /* RVA: 0x04D867D0 */ set; /* RVA: 0x04D86810 */ }
        System.String[] ClientCertificateIssuers { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x02FDAEA0 */ }
        System.Boolean DisallowUnauthenticatedCertificateRequest { get; /* RVA: 0x04D86F70 */ set; /* RVA: 0x04D86F80 */ }
        System.Nullable<Mono.Security.Interface.TlsProtocols> EnabledProtocols { get; /* RVA: 0x04DA3C10 */ set; /* RVA: 0x04DA3C30 */ }
        Mono.Security.Interface.CipherSuiteCode[] EnabledCiphers { get; /* RVA: 0x04D86230 */ set; /* RVA: 0x0485F170 */ }
        Mono.Security.Interface.MonoTlsSettings DefaultSettings { get; /* RVA: 0x0A7292E8 */ }
        Mono.Security.Interface.ICertificateValidator CertificateValidator { get; /* RVA: 0x04D862A0 */ }

        // Methods
        // RVA: 0x04DA3C00  token: 0x6000174
        public System.Void .ctor() { }
        // RVA: 0x0A729140  token: 0x6000176
        public static Mono.Security.Interface.MonoTlsSettings CopyDefaultSettings() { }
        // RVA: 0x0A72904C  token: 0x6000178
        public Mono.Security.Interface.MonoTlsSettings CloneWithValidator(Mono.Security.Interface.ICertificateValidator validator) { }
        // RVA: 0x0A7290E0  token: 0x6000179
        public Mono.Security.Interface.MonoTlsSettings Clone() { }
        // RVA: 0x0A729164  token: 0x600017A
        private System.Void .ctor(Mono.Security.Interface.MonoTlsSettings other) { }

    }

    // TypeToken: 0x2000045  // size: 0x98
    public sealed class TlsException : System.Exception
    {
        // Fields
        private Mono.Security.Interface.Alert alert;  // 0x90

        // Methods
        // RVA: 0x0A72DC24  token: 0x600017B
        public System.Void .ctor(Mono.Security.Interface.Alert alert) { }
        // RVA: 0x0A72DAB4  token: 0x600017C
        public System.Void .ctor(Mono.Security.Interface.Alert alert, System.String message) { }
        // RVA: 0x0A72DBAC  token: 0x600017D
        public System.Void .ctor(Mono.Security.Interface.AlertDescription description) { }
        // RVA: 0x0A72DB24  token: 0x600017E
        public System.Void .ctor(Mono.Security.Interface.AlertDescription description, System.String message) { }

    }

    // TypeToken: 0x2000046  // size: 0x12
    public sealed struct TlsProtocolCode
    {
        // Fields
        public System.Int16 value__;  // 0x10
        public static Mono.Security.Interface.TlsProtocolCode Tls10;  // const
        public static Mono.Security.Interface.TlsProtocolCode Tls11;  // const
        public static Mono.Security.Interface.TlsProtocolCode Tls12;  // const

    }

    // TypeToken: 0x2000047  // size: 0x14
    public sealed struct TlsProtocols
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Mono.Security.Interface.TlsProtocols Zero;  // const
        public static Mono.Security.Interface.TlsProtocols Tls10Client;  // const
        public static Mono.Security.Interface.TlsProtocols Tls10Server;  // const
        public static Mono.Security.Interface.TlsProtocols Tls10;  // const
        public static Mono.Security.Interface.TlsProtocols Tls11Client;  // const
        public static Mono.Security.Interface.TlsProtocols Tls11Server;  // const
        public static Mono.Security.Interface.TlsProtocols Tls11;  // const
        public static Mono.Security.Interface.TlsProtocols Tls12Client;  // const
        public static Mono.Security.Interface.TlsProtocols Tls12Server;  // const
        public static Mono.Security.Interface.TlsProtocols Tls12;  // const
        public static Mono.Security.Interface.TlsProtocols ClientMask;  // const
        public static Mono.Security.Interface.TlsProtocols ServerMask;  // const

    }

}

namespace Mono.Security.Protocol.Ntlm
{

    // TypeToken: 0x200002F  // size: 0x30
    public class ChallengeResponse : System.IDisposable
    {
        // Fields
        private static System.Byte[] magic;  // static @ 0x0
        private static System.Byte[] nullEncMagic;  // static @ 0x8
        private System.Boolean _disposed;  // 0x10
        private System.Byte[] _challenge;  // 0x18
        private System.Byte[] _lmpwd;  // 0x20
        private System.Byte[] _ntpwd;  // 0x28

        // Properties
        System.String Password { set; /* RVA: 0x0A70F8E8 */ }
        System.Byte[] Challenge { set; /* RVA: 0x0A70F7C8 */ }
        System.Byte[] LM { get; /* RVA: 0x0A70F6E0 */ }
        System.Byte[] NT { get; /* RVA: 0x0A70F754 */ }

        // Methods
        // RVA: 0x0A70F674  token: 0x6000108
        public System.Void .ctor() { }
        // RVA: 0x0A70F628  token: 0x6000109
        public System.Void .ctor(System.String password, System.Byte[] challenge) { }
        // RVA: 0x0A70F01C  token: 0x600010A
        protected virtual System.Void Finalize() { }
        // RVA: 0x0A70EF58  token: 0x600010F
        public virtual System.Void Dispose() { }
        // RVA: 0x0A70EFA8  token: 0x6000110
        private System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x0A70F094  token: 0x6000111
        private System.Byte[] GetResponse(System.Byte[] pwd) { }
        // RVA: 0x0A70F364  token: 0x6000112
        private System.Byte[] PrepareDESKey(System.Byte[] key56bits, System.Int32 position) { }
        // RVA: 0x0A70F240  token: 0x6000113
        private System.Byte[] PasswordToKey(System.String password, System.Int32 position) { }
        // RVA: 0x0A70F544  token: 0x6000114
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000030  // size: 0x10
    public static class ChallengeResponse2
    {
        // Fields
        private static System.Byte[] magic;  // static @ 0x0
        private static System.Byte[] nullEncMagic;  // static @ 0x8

        // Methods
        // RVA: 0x0A70DE48  token: 0x6000115
        private static System.Byte[] Compute_LM(System.String password, System.Byte[] challenge) { }
        // RVA: 0x0A70E084  token: 0x6000116
        private static System.Byte[] Compute_NTLM_Password(System.String password) { }
        // RVA: 0x0A70E184  token: 0x6000117
        private static System.Byte[] Compute_NTLM(System.String password, System.Byte[] challenge) { }
        // RVA: 0x0A70E1E0  token: 0x6000118
        private static System.Void Compute_NTLMv2_Session(System.String password, System.Byte[] challenge, System.Byte[]& lm, System.Byte[]& ntlm) { }
        // RVA: 0x0A70E3B4  token: 0x6000119
        private static System.Byte[] Compute_NTLMv2(Mono.Security.Protocol.Ntlm.Type2Message type2, System.String username, System.String password, System.String domain) { }
        // RVA: 0x0A70E7DC  token: 0x600011A
        public static System.Void Compute(Mono.Security.Protocol.Ntlm.Type2Message type2, Mono.Security.Protocol.Ntlm.NtlmAuthLevel level, System.String username, System.String password, System.String domain, System.Byte[]& lm, System.Byte[]& ntlm) { }
        // RVA: 0x0A70E9A4  token: 0x600011B
        private static System.Byte[] GetResponse(System.Byte[] challenge, System.Byte[] pwd) { }
        // RVA: 0x0A70EC94  token: 0x600011C
        private static System.Byte[] PrepareDESKey(System.Byte[] key56bits, System.Int32 position) { }
        // RVA: 0x0A70EB5C  token: 0x600011D
        private static System.Byte[] PasswordToKey(System.String password, System.Int32 position) { }
        // RVA: 0x0A70EE74  token: 0x600011E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000031  // size: 0x18
    public abstract class MessageBase
    {
        // Fields
        private static System.Byte[] header;  // static @ 0x0
        private System.Int32 _type;  // 0x10
        private Mono.Security.Protocol.Ntlm.NtlmFlags _flags;  // 0x14

        // Properties
        Mono.Security.Protocol.Ntlm.NtlmFlags Flags { get; /* RVA: 0x04D882A0 */ set; /* RVA: 0x04D8F540 */ }
        System.Int32 Type { get; /* RVA: 0x02B2E2D0 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600011F
        protected System.Void .ctor(System.Int32 messageType) { }
        // RVA: 0x0A7284FC  token: 0x6000123
        protected System.Byte[] PrepareMessage(System.Int32 messageSize) { }
        // RVA: 0x0A728340  token: 0x6000124
        protected virtual System.Void Decode(System.Byte[] message) { }
        // RVA: 0x0A72825C  token: 0x6000125
        protected System.Boolean CheckHeader(System.Byte[] message) { }
        // RVA: -1  // abstract  token: 0x6000126
        public virtual System.Byte[] GetBytes() { }
        // RVA: 0x0A7285D8  token: 0x6000127
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000032  // size: 0x14
    public sealed struct NtlmAuthLevel
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Mono.Security.Protocol.Ntlm.NtlmAuthLevel LM_and_NTLM;  // const
        public static Mono.Security.Protocol.Ntlm.NtlmAuthLevel LM_and_NTLM_and_try_NTLMv2_Session;  // const
        public static Mono.Security.Protocol.Ntlm.NtlmAuthLevel NTLM_only;  // const
        public static Mono.Security.Protocol.Ntlm.NtlmAuthLevel NTLMv2_only;  // const

    }

    // TypeToken: 0x2000033  // size: 0x14
    public sealed struct NtlmFlags
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Mono.Security.Protocol.Ntlm.NtlmFlags NegotiateUnicode;  // const
        public static Mono.Security.Protocol.Ntlm.NtlmFlags NegotiateOem;  // const
        public static Mono.Security.Protocol.Ntlm.NtlmFlags RequestTarget;  // const
        public static Mono.Security.Protocol.Ntlm.NtlmFlags NegotiateNtlm;  // const
        public static Mono.Security.Protocol.Ntlm.NtlmFlags NegotiateDomainSupplied;  // const
        public static Mono.Security.Protocol.Ntlm.NtlmFlags NegotiateWorkstationSupplied;  // const
        public static Mono.Security.Protocol.Ntlm.NtlmFlags NegotiateAlwaysSign;  // const
        public static Mono.Security.Protocol.Ntlm.NtlmFlags NegotiateNtlm2Key;  // const
        public static Mono.Security.Protocol.Ntlm.NtlmFlags Negotiate128;  // const
        public static Mono.Security.Protocol.Ntlm.NtlmFlags Negotiate56;  // const

    }

    // TypeToken: 0x2000034  // size: 0x10
    public static class NtlmSettings
    {
        // Fields
        private static Mono.Security.Protocol.Ntlm.NtlmAuthLevel defaultAuthLevel;  // static @ 0x0

        // Properties
        Mono.Security.Protocol.Ntlm.NtlmAuthLevel DefaultAuthLevel { get; /* RVA: 0x0A7293A0 */ }

        // Methods
        // RVA: 0x0A729364  token: 0x6000129
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000035  // size: 0x28
    public class Type1Message : Mono.Security.Protocol.Ntlm.MessageBase
    {
        // Fields
        private System.String _host;  // 0x18
        private System.String _domain;  // 0x20

        // Properties
        System.String Domain { set; /* RVA: 0x0A72E094 */ }
        System.String Host { set; /* RVA: 0x0A72E108 */ }

        // Methods
        // RVA: 0x0A72E028  token: 0x600012A
        public System.Void .ctor() { }
        // RVA: 0x0A72DC9C  token: 0x600012D
        protected virtual System.Void Decode(System.Byte[] message) { }
        // RVA: 0x0A72DD7C  token: 0x600012E
        public virtual System.Byte[] GetBytes() { }

    }

    // TypeToken: 0x2000036  // size: 0x30
    public class Type2Message : Mono.Security.Protocol.Ntlm.MessageBase
    {
        // Fields
        private System.Byte[] _nonce;  // 0x18
        private System.String _targetName;  // 0x20
        private System.Byte[] _targetInfo;  // 0x28

        // Properties
        System.Byte[] Nonce { get; /* RVA: 0x0A72E4B4 */ }
        System.String TargetName { get; /* RVA: 0x04D862C0 */ }
        System.Byte[] TargetInfo { get; /* RVA: 0x0A72E504 */ }

        // Methods
        // RVA: 0x0A72E430  token: 0x600012F
        public System.Void .ctor(System.Byte[] message) { }
        // RVA: 0x0A72E2E8  token: 0x6000130
        protected virtual System.Void Finalize() { }
        // RVA: 0x0A72E17C  token: 0x6000134
        protected virtual System.Void Decode(System.Byte[] message) { }
        // RVA: 0x0A72E370  token: 0x6000135
        public virtual System.Byte[] GetBytes() { }

    }

    // TypeToken: 0x2000037  // size: 0x60
    public class Type3Message : Mono.Security.Protocol.Ntlm.MessageBase
    {
        // Fields
        private Mono.Security.Protocol.Ntlm.NtlmAuthLevel _level;  // 0x18
        private System.Byte[] _challenge;  // 0x20
        private System.String _host;  // 0x28
        private System.String _domain;  // 0x30
        private System.String _username;  // 0x38
        private System.String _password;  // 0x40
        private Mono.Security.Protocol.Ntlm.Type2Message _type2;  // 0x48
        private System.Byte[] _lm;  // 0x50
        private System.Byte[] _nt;  // 0x58

        // Properties
        System.String Domain { set; /* RVA: 0x0A72F138 */ }
        System.String Password { set; /* RVA: 0x042B4AE0 */ }
        System.String Username { set; /* RVA: 0x046A91B0 */ }

        // Methods
        // RVA: 0x0A72EF9C  token: 0x6000136
        public System.Void .ctor(Mono.Security.Protocol.Ntlm.Type2Message type2) { }
        // RVA: 0x0A72E844  token: 0x6000137
        protected virtual System.Void Finalize() { }
        // RVA: 0x0A72E5B0  token: 0x600013B
        protected virtual System.Void Decode(System.Byte[] message) { }
        // RVA: 0x0A72E554  token: 0x600013C
        private System.String DecodeString(System.Byte[] buffer, System.Int32 offset, System.Int32 len) { }
        // RVA: 0x0A72E7C8  token: 0x600013D
        private System.Byte[] EncodeString(System.String text) { }
        // RVA: 0x0A72E910  token: 0x600013E
        public virtual System.Byte[] GetBytes() { }

    }

}

namespace Mono.Security.X509
{

    // TypeToken: 0x200000B  // size: 0x20
    public class SafeBag
    {
        // Fields
        private System.String _bagOID;  // 0x10
        private Mono.Security.ASN1 _asn1;  // 0x18

        // Properties
        System.String BagOID { get; /* RVA: 0x02B2ECC0 */ }
        Mono.Security.ASN1 ASN1 { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x032AF420  token: 0x6000041
        public System.Void .ctor(System.String bagOID, Mono.Security.ASN1 asn1) { }

    }

    // TypeToken: 0x200000C  // size: 0x48
    public class PKCS12 : System.ICloneable
    {
        // Fields
        private System.Byte[] _password;  // 0x10
        private System.Collections.ArrayList _keyBags;  // 0x18
        private System.Collections.ArrayList _secretBags;  // 0x20
        private Mono.Security.X509.X509CertificateCollection _certs;  // 0x28
        private System.Boolean _keyBagsChanged;  // 0x30
        private System.Boolean _secretBagsChanged;  // 0x31
        private System.Boolean _certsChanged;  // 0x32
        private System.Int32 _iterations;  // 0x34
        private System.Collections.ArrayList _safeBags;  // 0x38
        private System.Security.Cryptography.RandomNumberGenerator _rng;  // 0x40
        private static System.Int32 password_max_length;  // static @ 0x0

        // Properties
        System.String Password { set; /* RVA: 0x0A717F34 */ }
        System.Int32 IterationCount { get; /* RVA: 0x04D86730 */ set; /* RVA: 0x04D86760 */ }
        System.Collections.ArrayList Keys { get; /* RVA: 0x0A7178FC */ }
        Mono.Security.X509.X509CertificateCollection Certificates { get; /* RVA: 0x0A71752C */ }
        System.Security.Cryptography.RandomNumberGenerator RNG { get; /* RVA: 0x0A717F00 */ }
        System.Int32 MaximumPasswordLength { get; /* RVA: 0x0A717EB0 */ }

        // Methods
        // RVA: 0x0A717420  token: 0x6000044
        public System.Void .ctor() { }
        // RVA: 0x0A7173E4  token: 0x6000045
        public System.Void .ctor(System.Byte[] data) { }
        // RVA: 0x0A717398  token: 0x6000046
        public System.Void .ctor(System.Byte[] data, System.String password) { }
        // RVA: 0x0A71240C  token: 0x6000047
        private System.Void Decode(System.Byte[] data) { }
        // RVA: 0x0A7132F4  token: 0x6000048
        protected virtual System.Void Finalize() { }
        // RVA: 0x0A7123AC  token: 0x600004F
        private System.Boolean Compare(System.Byte[] expected, System.Byte[] actual) { }
        // RVA: 0x0A7155AC  token: 0x6000050
        private System.Security.Cryptography.SymmetricAlgorithm GetSymmetricAlgorithm(System.String algorithmOid, System.Byte[] salt, System.Int32 iterationCount) { }
        // RVA: 0x0A712DD0  token: 0x6000051
        public System.Byte[] Decrypt(System.String algorithmOid, System.Byte[] salt, System.Int32 iterationCount, System.Byte[] encryptedData) { }
        // RVA: 0x0A712CFC  token: 0x6000052
        public System.Byte[] Decrypt(Mono.Security.PKCS7.EncryptedData ed) { }
        // RVA: 0x0A712EB4  token: 0x6000053
        public System.Byte[] Encrypt(System.String algorithmOid, System.Byte[] salt, System.Int32 iterationCount, System.Byte[] data) { }
        // RVA: 0x0A715380  token: 0x6000054
        private System.Security.Cryptography.DSAParameters GetExistingParameters(System.Boolean& found) { }
        // RVA: 0x0A7114EC  token: 0x6000055
        private System.Void AddPrivateKey(Mono.Security.Cryptography.PKCS8.PrivateKeyInfo pki) { }
        // RVA: 0x0A716710  token: 0x6000056
        private System.Void ReadSafeBag(Mono.Security.ASN1 safeBag) { }
        // RVA: 0x0A715C14  token: 0x6000057
        private Mono.Security.ASN1 Pkcs8ShroudedKeyBagSafeBag(System.Security.Cryptography.AsymmetricAlgorithm aa, System.Collections.IDictionary attributes) { }
        // RVA: 0x0A711828  token: 0x6000058
        private Mono.Security.ASN1 CertificateSafeBag(Mono.Security.X509.X509Certificate x509, System.Collections.IDictionary attributes) { }
        // RVA: 0x0A715B00  token: 0x6000059
        private System.Byte[] MAC(System.Byte[] password, System.Byte[] salt, System.Int32 iterations, System.Byte[] data) { }
        // RVA: 0x0A7133C8  token: 0x600005A
        public System.Byte[] GetBytes() { }
        // RVA: 0x0A712FD4  token: 0x600005B
        private Mono.Security.PKCS7.ContentInfo EncryptedContentInfo(Mono.Security.ASN1 safeBags, System.String algorithmOid) { }
        // RVA: 0x0A710F20  token: 0x600005C
        public System.Void AddCertificate(Mono.Security.X509.X509Certificate cert) { }
        // RVA: 0x0A710F2C  token: 0x600005D
        public System.Void AddCertificate(Mono.Security.X509.X509Certificate cert, System.Collections.IDictionary attributes) { }
        // RVA: 0x0A717350  token: 0x600005E
        public System.Void RemoveCertificate(Mono.Security.X509.X509Certificate cert) { }
        // RVA: 0x0A716F74  token: 0x600005F
        public System.Void RemoveCertificate(Mono.Security.X509.X509Certificate cert, System.Collections.IDictionary attrs) { }
        // RVA: 0x0A712324  token: 0x6000060
        private System.Boolean CompareAsymmetricAlgorithm(System.Security.Cryptography.AsymmetricAlgorithm a1, System.Security.Cryptography.AsymmetricAlgorithm a2) { }
        // RVA: 0x0A711164  token: 0x6000061
        public System.Void AddPkcs8ShroudedKeyBag(System.Security.Cryptography.AsymmetricAlgorithm aa, System.Collections.IDictionary attributes) { }
        // RVA: 0x0A712248  token: 0x6000062
        public virtual System.Object Clone() { }
        // RVA: 0x0A71735C  token: 0x6000064
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000E  // size: 0x10
    public sealed class X501
    {
        // Fields
        private static System.Byte[] countryName;  // static @ 0x0
        private static System.Byte[] organizationName;  // static @ 0x8
        private static System.Byte[] organizationalUnitName;  // static @ 0x10
        private static System.Byte[] commonName;  // static @ 0x18
        private static System.Byte[] localityName;  // static @ 0x20
        private static System.Byte[] stateOrProvinceName;  // static @ 0x28
        private static System.Byte[] streetAddress;  // static @ 0x30
        private static System.Byte[] serialNumber;  // static @ 0x38
        private static System.Byte[] domainComponent;  // static @ 0x40
        private static System.Byte[] userid;  // static @ 0x48
        private static System.Byte[] email;  // static @ 0x50
        private static System.Byte[] dnQualifier;  // static @ 0x58
        private static System.Byte[] title;  // static @ 0x60
        private static System.Byte[] surname;  // static @ 0x68
        private static System.Byte[] givenName;  // static @ 0x70
        private static System.Byte[] initial;  // static @ 0x78

        // Methods
        // RVA: 0x0A71AA18  token: 0x6000070
        public static System.String ToString(Mono.Security.ASN1 seq) { }
        // RVA: 0x0A71A8AC  token: 0x6000071
        public static System.String ToString(Mono.Security.ASN1 seq, System.Boolean reversed, System.String separator, System.Boolean quotes) { }
        // RVA: 0x0A718ED8  token: 0x6000072
        private static System.Void AppendEntry(System.Text.StringBuilder sb, Mono.Security.ASN1 entry, System.Boolean quotes) { }
        // RVA: 0x0A7197A4  token: 0x6000073
        private static Mono.Security.X509.X520.AttributeTypeAndValue GetAttributeFromOid(System.String attributeType) { }
        // RVA: 0x0A71A014  token: 0x6000074
        private static System.Boolean IsOid(System.String oid) { }
        // RVA: 0x0A71A0A0  token: 0x6000075
        private static Mono.Security.X509.X520.AttributeTypeAndValue ReadAttribute(System.String value, System.Int32& pos) { }
        // RVA: 0x0A719F74  token: 0x6000076
        private static System.Boolean IsHex(System.Char c) { }
        // RVA: 0x0A71A39C  token: 0x6000077
        private static System.String ReadHex(System.String value, System.Int32& pos) { }
        // RVA: 0x0A71A214  token: 0x6000078
        private static System.Int32 ReadEscaped(System.Text.StringBuilder sb, System.String value, System.Int32 pos) { }
        // RVA: 0x0A71A520  token: 0x6000079
        private static System.Int32 ReadQuoted(System.Text.StringBuilder sb, System.String value, System.Int32 pos) { }
        // RVA: 0x0A71A658  token: 0x600007A
        private static System.String ReadValue(System.String value, System.Int32& pos) { }
        // RVA: 0x0A719624  token: 0x600007B
        public static Mono.Security.ASN1 FromString(System.String rdn) { }
        // RVA: 0x0A71AB18  token: 0x600007C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000F  // size: 0x60
    public class X509Crl
    {
        // Fields
        private System.String issuer;  // 0x10
        private System.Byte version;  // 0x18
        private System.DateTime thisUpdate;  // 0x20
        private System.DateTime nextUpdate;  // 0x28
        private System.Collections.ArrayList entries;  // 0x30
        private System.String signatureOID;  // 0x38
        private System.Byte[] signature;  // 0x40
        private Mono.Security.X509.X509ExtensionCollection extensions;  // 0x48
        private System.Byte[] encoded;  // 0x50
        private System.Byte[] hash_value;  // 0x58

        // Properties
        Mono.Security.X509.X509ExtensionCollection Extensions { get; /* RVA: 0x04D86200 */ }
        System.Byte[] Hash { get; /* RVA: 0x0A71ED50 */ }
        System.String IssuerName { get; /* RVA: 0x02B2ECC0 */ }
        System.DateTime NextUpdate { get; /* RVA: 0x04D86240 */ }

        // Methods
        // RVA: 0x0A71EC7C  token: 0x600007D
        public System.Void .ctor(System.Byte[] crl) { }
        // RVA: 0x0A71E15C  token: 0x600007E
        private System.Void Parse(System.Byte[] crl) { }
        // RVA: 0x0A71B224  token: 0x6000083
        private System.Boolean Compare(System.Byte[] array1, System.Byte[] array2) { }
        // RVA: 0x0A71DFBC  token: 0x6000084
        public Mono.Security.X509.X509Crl.X509CrlEntry GetCrlEntry(Mono.Security.X509.X509Certificate x509) { }
        // RVA: 0x0A71E040  token: 0x6000085
        public Mono.Security.X509.X509Crl.X509CrlEntry GetCrlEntry(System.Byte[] serialNumber) { }
        // RVA: 0x0A71E9D8  token: 0x6000086
        private System.Boolean VerifySignature(System.Security.Cryptography.DSA dsa) { }
        // RVA: 0x0A71E918  token: 0x6000087
        private System.Boolean VerifySignature(System.Security.Cryptography.RSA rsa) { }
        // RVA: 0x0A71E7A0  token: 0x6000088
        public System.Boolean VerifySignature(System.Security.Cryptography.AsymmetricAlgorithm aa) { }

    }

    // TypeToken: 0x2000011  // size: 0xC0
    public class X509Certificate : System.Runtime.Serialization.ISerializable
    {
        // Fields
        private Mono.Security.ASN1 decoder;  // 0x10
        private System.Byte[] m_encodedcert;  // 0x18
        private System.DateTime m_from;  // 0x20
        private System.DateTime m_until;  // 0x28
        private Mono.Security.ASN1 issuer;  // 0x30
        private System.String m_issuername;  // 0x38
        private System.String m_keyalgo;  // 0x40
        private System.Byte[] m_keyalgoparams;  // 0x48
        private Mono.Security.ASN1 subject;  // 0x50
        private System.String m_subject;  // 0x58
        private System.Byte[] m_publickey;  // 0x60
        private System.Byte[] signature;  // 0x68
        private System.String m_signaturealgo;  // 0x70
        private System.Byte[] m_signaturealgoparams;  // 0x78
        private System.Byte[] certhash;  // 0x80
        private System.Security.Cryptography.RSA _rsa;  // 0x88
        private System.Security.Cryptography.DSA _dsa;  // 0x90
        private System.Int32 version;  // 0x98
        private System.Byte[] serialnumber;  // 0xa0
        private System.Byte[] issuerUniqueID;  // 0xa8
        private System.Byte[] subjectUniqueID;  // 0xb0
        private Mono.Security.X509.X509ExtensionCollection extensions;  // 0xb8
        private static System.String encoding_error;  // static @ 0x0

        // Properties
        System.Security.Cryptography.DSA DSA { get; /* RVA: 0x0A71C6F8 */ set; /* RVA: 0x0A71D45C */ }
        Mono.Security.X509.X509ExtensionCollection Extensions { get; /* RVA: 0x04D85EF0 */ }
        System.Byte[] Hash { get; /* RVA: 0x0A71CA3C */ }
        System.String IssuerName { get; /* RVA: 0x04D85A50 */ }
        System.String KeyAlgorithm { get; /* RVA: 0x04D85A60 */ }
        System.Byte[] KeyAlgorithmParameters { get; /* RVA: 0x0A71CD90 */ set; /* RVA: 0x035A41A0 */ }
        System.Byte[] PublicKey { get; /* RVA: 0x0A71CDE4 */ }
        System.Security.Cryptography.RSA RSA { get; /* RVA: 0x0A71CE38 */ set; /* RVA: 0x0A71D4A0 */ }
        System.Byte[] RawData { get; /* RVA: 0x0A71D018 */ }
        System.Byte[] SerialNumber { get; /* RVA: 0x0A71D06C */ }
        System.Byte[] Signature { get; /* RVA: 0x0A71D0C4 */ }
        System.String SubjectName { get; /* RVA: 0x02E56440 */ }
        System.DateTime ValidFrom { get; /* RVA: 0x04D862C0 */ }
        System.DateTime ValidUntil { get; /* RVA: 0x04D86240 */ }
        System.Int32 Version { get; /* RVA: 0x04D862D0 */ }
        System.Boolean IsCurrent { get; /* RVA: 0x0A71CC58 */ }
        System.Boolean IsSelfSigned { get; /* RVA: 0x0A71CCA4 */ }

        // Methods
        // RVA: 0x0A71B884  token: 0x600008D
        private System.Void Parse(System.Byte[] data) { }
        // RVA: 0x0A71C57C  token: 0x600008E
        public System.Void .ctor(System.Byte[] data) { }
        // RVA: 0x0A71B6E4  token: 0x600008F
        private System.Byte[] GetUnsignedBigInteger(System.Byte[] integer) { }
        // RVA: 0x0A71C49C  token: 0x60000A3
        public System.Boolean WasCurrent(System.DateTime instant) { }
        // RVA: 0x0A71C3E8  token: 0x60000A4
        private System.Boolean VerifySignature(System.Security.Cryptography.DSA dsa) { }
        // RVA: 0x0A71C2F0  token: 0x60000A5
        private System.Boolean VerifySignature(System.Security.Cryptography.RSA rsa) { }
        // RVA: 0x0A71C178  token: 0x60000A6
        public System.Boolean VerifySignature(System.Security.Cryptography.AsymmetricAlgorithm aa) { }
        // RVA: 0x0A71B68C  token: 0x60000A8
        public virtual System.Void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0A71B77C  token: 0x60000A9
        private static System.Byte[] PEM(System.String type, System.Byte[] data) { }
        // RVA: 0x0A71C520  token: 0x60000AA
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000012  // size: 0x18
    public class X509CertificateCollection : System.Collections.CollectionBase, System.Collections.IEnumerable
    {
        // Properties
        Mono.Security.X509.X509Certificate Item { get; /* RVA: 0x0A71B43C */ }

        // Methods
        // RVA: 0x0A71B434  token: 0x60000AB
        public System.Void .ctor() { }
        // RVA: 0x0A71B1A8  token: 0x60000AD
        public System.Int32 Add(Mono.Security.X509.X509Certificate value) { }
        // RVA: 0x0A71B0DC  token: 0x60000AE
        public System.Void AddRange(Mono.Security.X509.X509CertificateCollection value) { }
        // RVA: 0x0A71B27C  token: 0x60000AF
        public System.Boolean Contains(Mono.Security.X509.X509Certificate value) { }
        // RVA: 0x0A71B294  token: 0x60000B0
        public Mono.Security.X509.X509CertificateCollection.X509CertificateEnumerator GetEnumerator() { }
        // RVA: 0x0A71B410  token: 0x60000B1
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: 0x08479504  token: 0x60000B2
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0A71B2F4  token: 0x60000B3
        public System.Int32 IndexOf(Mono.Security.X509.X509Certificate value) { }
        // RVA: 0x0A71B224  token: 0x60000B4
        private System.Boolean Compare(System.Byte[] array1, System.Byte[] array2) { }

    }

    // TypeToken: 0x2000014  // size: 0x38
    public class X509Chain
    {
        // Fields
        private Mono.Security.X509.X509CertificateCollection roots;  // 0x10
        private Mono.Security.X509.X509CertificateCollection certs;  // 0x18
        private Mono.Security.X509.X509Certificate _root;  // 0x20
        private Mono.Security.X509.X509CertificateCollection _chain;  // 0x28
        private Mono.Security.X509.X509ChainStatusFlags _status;  // 0x30

        // Properties
        Mono.Security.X509.X509CertificateCollection TrustAnchors { get; /* RVA: 0x0A71DDE4 */ }

        // Methods
        // RVA: 0x0A71DD80  token: 0x60000BB
        public System.Void .ctor() { }
        // RVA: 0x0A71DD14  token: 0x60000BD
        public System.Void LoadCertificates(Mono.Security.X509.X509CertificateCollection collection) { }
        // RVA: 0x0A71D4E0  token: 0x60000BE
        public System.Boolean Build(Mono.Security.X509.X509Certificate leaf) { }
        // RVA: 0x0A71DD34  token: 0x60000BF
        public System.Void Reset() { }
        // RVA: 0x0A71DCA4  token: 0x60000C0
        private System.Boolean IsValid(Mono.Security.X509.X509Certificate cert) { }
        // RVA: 0x0A71D848  token: 0x60000C1
        private Mono.Security.X509.X509Certificate FindCertificateParent(Mono.Security.X509.X509Certificate child) { }
        // RVA: 0x0A71D990  token: 0x60000C2
        private Mono.Security.X509.X509Certificate FindCertificateRoot(Mono.Security.X509.X509Certificate potentialRoot) { }
        // RVA: 0x0A71DC74  token: 0x60000C3
        private System.Boolean IsTrusted(Mono.Security.X509.X509Certificate potentialTrusted) { }
        // RVA: 0x0A71DB44  token: 0x60000C4
        private System.Boolean IsParent(Mono.Security.X509.X509Certificate child, Mono.Security.X509.X509Certificate parent) { }

    }

    // TypeToken: 0x2000015  // size: 0x14
    public sealed struct X509ChainStatusFlags
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Mono.Security.X509.X509ChainStatusFlags InvalidBasicConstraints;  // const
        public static Mono.Security.X509.X509ChainStatusFlags NoError;  // const
        public static Mono.Security.X509.X509ChainStatusFlags NotSignatureValid;  // const
        public static Mono.Security.X509.X509ChainStatusFlags NotTimeNested;  // const
        public static Mono.Security.X509.X509ChainStatusFlags NotTimeValid;  // const
        public static Mono.Security.X509.X509ChainStatusFlags PartialChain;  // const
        public static Mono.Security.X509.X509ChainStatusFlags UntrustedRoot;  // const

    }

    // TypeToken: 0x2000016  // size: 0x28
    public class X509Extension
    {
        // Fields
        protected System.String extnOid;  // 0x10
        protected System.Boolean extnCritical;  // 0x18
        protected Mono.Security.ASN1 extnValue;  // 0x20

        // Properties
        System.String Oid { get; /* RVA: 0x02B2ECC0 */ }
        System.Boolean Critical { get; /* RVA: 0x04D86340 */ }
        Mono.Security.ASN1 Value { get; /* RVA: 0x0A71F9E4 */ }

        // Methods
        // RVA: 0x0A71F5B0  token: 0x60000C5
        public System.Void .ctor(Mono.Security.ASN1 asn1) { }
        // RVA: 0x0A71F890  token: 0x60000C6
        public System.Void .ctor(Mono.Security.X509.X509Extension extension) { }
        // RVA: 0x041E1670  token: 0x60000C7
        protected virtual System.Void Decode() { }
        // RVA: 0x041E1670  token: 0x60000C8
        protected virtual System.Void Encode() { }
        // RVA: 0x0A71F1C0  token: 0x60000CC
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x08479504  token: 0x60000CD
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0A71F3CC  token: 0x60000CE
        private System.Void WriteLine(System.Text.StringBuilder sb, System.Int32 n, System.Int32 pos) { }
        // RVA: 0x0A71F2CC  token: 0x60000CF
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000017  // size: 0x20
    public sealed class X509ExtensionCollection : System.Collections.CollectionBase, System.Collections.IEnumerable
    {
        // Fields
        private System.Boolean readOnly;  // 0x18

        // Properties
        Mono.Security.X509.X509Extension Item { get; /* RVA: 0x0A71F144 */ }

        // Methods
        // RVA: 0x0A71B434  token: 0x60000D0
        public System.Void .ctor() { }
        // RVA: 0x0A71F01C  token: 0x60000D1
        public System.Void .ctor(Mono.Security.ASN1 asn1) { }
        // RVA: 0x0A71EEF8  token: 0x60000D2
        public System.Int32 IndexOf(System.String oid) { }
        // RVA: 0x0A71B410  token: 0x60000D3
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000018  // size: 0x30
    public class X509Store
    {
        // Fields
        private System.String _storePath;  // 0x10
        private Mono.Security.X509.X509CertificateCollection _certificates;  // 0x18
        private System.Collections.ArrayList _crls;  // 0x20
        private System.Boolean _crl;  // 0x28
        private System.Boolean _newFormat;  // 0x29

        // Properties
        Mono.Security.X509.X509CertificateCollection Certificates { get; /* RVA: 0x0A720704 */ }
        System.Collections.ArrayList Crls { get; /* RVA: 0x0A720740 */ }

        // Methods
        // RVA: 0x0A7206DC  token: 0x60000D5
        private System.Void .ctor(System.String path, System.Boolean crl, System.Boolean newFormat) { }
        // RVA: 0x0A7205A0  token: 0x60000D8
        private System.Byte[] Load(System.String filename) { }
        // RVA: 0x0A7204B8  token: 0x60000D9
        private Mono.Security.X509.X509Certificate LoadCertificate(System.String filename) { }
        // RVA: 0x0A72052C  token: 0x60000DA
        private Mono.Security.X509.X509Crl LoadCrl(System.String filename) { }
        // RVA: 0x0A720400  token: 0x60000DB
        private System.Boolean CheckStore(System.String path, System.Boolean throwException) { }
        // RVA: 0x0A7200A4  token: 0x60000DC
        private Mono.Security.X509.X509CertificateCollection BuildCertificatesCollection(System.String storeName) { }
        // RVA: 0x0A72025C  token: 0x60000DD
        private System.Collections.ArrayList BuildCrlsCollection(System.String storeName) { }

    }

    // TypeToken: 0x2000019  // size: 0x10
    public sealed class X509StoreManager
    {
        // Fields
        private static System.String _userPath;  // static @ 0x0
        private static System.String _localMachinePath;  // static @ 0x8
        private static System.String _newUserPath;  // static @ 0x10
        private static System.String _newLocalMachinePath;  // static @ 0x18
        private static Mono.Security.X509.X509Stores _userStore;  // static @ 0x20
        private static Mono.Security.X509.X509Stores _machineStore;  // static @ 0x28

        // Properties
        System.String CurrentUserPath { get; /* RVA: 0x0A71FA14 */ }
        System.String LocalMachinePath { get; /* RVA: 0x0A71FBE0 */ }
        System.String NewCurrentUserPath { get; /* RVA: 0x0A71FDBC */ }
        System.String NewLocalMachinePath { get; /* RVA: 0x0A71FED4 */ }
        Mono.Security.X509.X509Stores CurrentUser { get; /* RVA: 0x0A71FB1C */ }
        Mono.Security.X509.X509Stores LocalMachine { get; /* RVA: 0x0A71FCF8 */ }
        Mono.Security.X509.X509CertificateCollection TrustedRootCertificates { get; /* RVA: 0x0A71FFEC */ }

    }

    // TypeToken: 0x200001A  // size: 0x28
    public class X509Stores
    {
        // Fields
        private System.String _storePath;  // 0x10
        private System.Boolean _newFormat;  // 0x18
        private Mono.Security.X509.X509Store _trusted;  // 0x20

        // Properties
        Mono.Security.X509.X509Store TrustedRoot { get; /* RVA: 0x0A7208E4 */ }

        // Methods
        // RVA: 0x032B1EA0  token: 0x60000E5
        private System.Void .ctor(System.String path, System.Boolean newFormat) { }
        // RVA: 0x0A7207D4  token: 0x60000E7
        public Mono.Security.X509.X509Store Open(System.String storeName, System.Boolean create) { }

    }

    // TypeToken: 0x200001B  // size: 0x10
    public class X520
    {
    }

}

namespace Mono.Security.X509.Extensions
{

    // TypeToken: 0x200002D  // size: 0x30
    public class AuthorityKeyIdentifierExtension : Mono.Security.X509.X509Extension
    {
        // Fields
        private System.Byte[] aki;  // 0x28

        // Properties
        System.Byte[] Identifier { get; /* RVA: 0x0A70D5A0 */ }

        // Methods
        // RVA: 0x0A70D598  token: 0x60000FE
        public System.Void .ctor(Mono.Security.X509.X509Extension extension) { }
        // RVA: 0x0A70D1E8  token: 0x60000FF
        protected virtual System.Void Decode() { }
        // RVA: 0x0A70D304  token: 0x6000100
        protected virtual System.Void Encode() { }
        // RVA: 0x0A70D448  token: 0x6000102
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200002E  // size: 0x30
    public class BasicConstraintsExtension : Mono.Security.X509.X509Extension
    {
        // Fields
        private System.Boolean cA;  // 0x28
        private System.Int32 pathLenConstraint;  // 0x2c

        // Properties
        System.Boolean CertificateAuthority { get; /* RVA: 0x011F33C0 */ }

        // Methods
        // RVA: 0x0A70D598  token: 0x6000103
        public System.Void .ctor(Mono.Security.X509.X509Extension extension) { }
        // RVA: 0x0A70D5F4  token: 0x6000104
        protected virtual System.Void Decode() { }
        // RVA: 0x0A70D73C  token: 0x6000105
        protected virtual System.Void Encode() { }
        // RVA: 0x0A70D884  token: 0x6000107
        public virtual System.String ToString() { }

    }

}

