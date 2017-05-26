namespace Sitecore.Feature.Products
{
    using Data;

    public struct Templates
    {
        public struct Product
        {
            public static readonly ID ID = new ID("{6ABD63AA-668E-4479-91CD-9037448CD754}");

            public struct Fields
            {
                public static readonly ID Code = new ID("{5AF6C814-E0F4-4D80-9159-4CBA0C727764}");
                public const string Code_FieldName = "Code";

                public static readonly ID InternalName = new ID("{213A1074-16F7-4C91-82AE-5576F9FC713B}");
                public const string InternalName_FieldName = "Internal Name";

                public static readonly ID Timestamp = new ID("{1F21047D-8545-4BC4-9F48-AC215B8D6B73}");
                public const string Timestamp_FieldName = "Timestamp";
            }
        }
    }
}