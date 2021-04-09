// <auto-generated>
//   This file was generated by a tool; you should avoid making direct changes.
//   Consider using 'partial classes' to extend these types
//   Input: scalapb.proto
// </auto-generated>

#region Designer generated code
#pragma warning disable CS0612, CS0618, CS1591, CS3021, IDE0079, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
namespace Scalapb
{

    [global::ProtoBuf.ProtoContract()]
    public partial class ScalaPbOptions : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"package_name")]
        [global::System.ComponentModel.DefaultValue("")]
        public string PackageName
        {
            get => __pbn__PackageName ?? "";
            set => __pbn__PackageName = value;
        }
        public bool ShouldSerializePackageName() => __pbn__PackageName != null;
        public void ResetPackageName() => __pbn__PackageName = null;
        private string __pbn__PackageName;

        [global::ProtoBuf.ProtoMember(2, Name = @"flat_package")]
        public bool FlatPackage
        {
            get => __pbn__FlatPackage.GetValueOrDefault();
            set => __pbn__FlatPackage = value;
        }
        public bool ShouldSerializeFlatPackage() => __pbn__FlatPackage != null;
        public void ResetFlatPackage() => __pbn__FlatPackage = null;
        private bool? __pbn__FlatPackage;

        [global::ProtoBuf.ProtoMember(3, Name = @"import")]
        public global::System.Collections.Generic.List<string> Imports { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(4, Name = @"preamble")]
        public global::System.Collections.Generic.List<string> Preambles { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(5, Name = @"single_file")]
        public bool SingleFile
        {
            get => __pbn__SingleFile.GetValueOrDefault();
            set => __pbn__SingleFile = value;
        }
        public bool ShouldSerializeSingleFile() => __pbn__SingleFile != null;
        public void ResetSingleFile() => __pbn__SingleFile = null;
        private bool? __pbn__SingleFile;

        [global::ProtoBuf.ProtoMember(7, Name = @"no_primitive_wrappers")]
        public bool NoPrimitiveWrappers
        {
            get => __pbn__NoPrimitiveWrappers.GetValueOrDefault();
            set => __pbn__NoPrimitiveWrappers = value;
        }
        public bool ShouldSerializeNoPrimitiveWrappers() => __pbn__NoPrimitiveWrappers != null;
        public void ResetNoPrimitiveWrappers() => __pbn__NoPrimitiveWrappers = null;
        private bool? __pbn__NoPrimitiveWrappers;

        [global::ProtoBuf.ProtoMember(6, Name = @"primitive_wrappers")]
        public bool PrimitiveWrappers
        {
            get => __pbn__PrimitiveWrappers.GetValueOrDefault();
            set => __pbn__PrimitiveWrappers = value;
        }
        public bool ShouldSerializePrimitiveWrappers() => __pbn__PrimitiveWrappers != null;
        public void ResetPrimitiveWrappers() => __pbn__PrimitiveWrappers = null;
        private bool? __pbn__PrimitiveWrappers;

        [global::ProtoBuf.ProtoMember(8, Name = @"collection_type")]
        [global::System.ComponentModel.DefaultValue("")]
        public string CollectionType
        {
            get => __pbn__CollectionType ?? "";
            set => __pbn__CollectionType = value;
        }
        public bool ShouldSerializeCollectionType() => __pbn__CollectionType != null;
        public void ResetCollectionType() => __pbn__CollectionType = null;
        private string __pbn__CollectionType;

        [global::ProtoBuf.ProtoMember(9, Name = @"preserve_unknown_fields")]
        [global::System.ComponentModel.DefaultValue(true)]
        public bool PreserveUnknownFields
        {
            get => __pbn__PreserveUnknownFields ?? true;
            set => __pbn__PreserveUnknownFields = value;
        }
        public bool ShouldSerializePreserveUnknownFields() => __pbn__PreserveUnknownFields != null;
        public void ResetPreserveUnknownFields() => __pbn__PreserveUnknownFields = null;
        private bool? __pbn__PreserveUnknownFields;

        [global::ProtoBuf.ProtoMember(10, Name = @"object_name")]
        [global::System.ComponentModel.DefaultValue("")]
        public string ObjectName
        {
            get => __pbn__ObjectName ?? "";
            set => __pbn__ObjectName = value;
        }
        public bool ShouldSerializeObjectName() => __pbn__ObjectName != null;
        public void ResetObjectName() => __pbn__ObjectName = null;
        private string __pbn__ObjectName;

        [global::ProtoBuf.ProtoMember(11, Name = @"scope")]
        [global::System.ComponentModel.DefaultValue(OptionsScope.File)]
        public OptionsScope Scope
        {
            get => __pbn__Scope ?? OptionsScope.File;
            set => __pbn__Scope = value;
        }
        public bool ShouldSerializeScope() => __pbn__Scope != null;
        public void ResetScope() => __pbn__Scope = null;
        private OptionsScope? __pbn__Scope;

        [global::ProtoBuf.ProtoMember(12, Name = @"lenses")]
        [global::System.ComponentModel.DefaultValue(true)]
        public bool Lenses
        {
            get => __pbn__Lenses ?? true;
            set => __pbn__Lenses = value;
        }
        public bool ShouldSerializeLenses() => __pbn__Lenses != null;
        public void ResetLenses() => __pbn__Lenses = null;
        private bool? __pbn__Lenses;

        [global::ProtoBuf.ProtoMember(13, Name = @"retain_source_code_info")]
        public bool RetainSourceCodeInfo
        {
            get => __pbn__RetainSourceCodeInfo.GetValueOrDefault();
            set => __pbn__RetainSourceCodeInfo = value;
        }
        public bool ShouldSerializeRetainSourceCodeInfo() => __pbn__RetainSourceCodeInfo != null;
        public void ResetRetainSourceCodeInfo() => __pbn__RetainSourceCodeInfo = null;
        private bool? __pbn__RetainSourceCodeInfo;

        [global::ProtoBuf.ProtoMember(14, Name = @"map_type")]
        [global::System.ComponentModel.DefaultValue("")]
        public string MapType
        {
            get => __pbn__MapType ?? "";
            set => __pbn__MapType = value;
        }
        public bool ShouldSerializeMapType() => __pbn__MapType != null;
        public void ResetMapType() => __pbn__MapType = null;
        private string __pbn__MapType;

        [global::ProtoBuf.ProtoMember(15, Name = @"no_default_values_in_constructor")]
        public bool NoDefaultValuesInConstructor
        {
            get => __pbn__NoDefaultValuesInConstructor.GetValueOrDefault();
            set => __pbn__NoDefaultValuesInConstructor = value;
        }
        public bool ShouldSerializeNoDefaultValuesInConstructor() => __pbn__NoDefaultValuesInConstructor != null;
        public void ResetNoDefaultValuesInConstructor() => __pbn__NoDefaultValuesInConstructor = null;
        private bool? __pbn__NoDefaultValuesInConstructor;

        [global::ProtoBuf.ProtoMember(16)]
        [global::System.ComponentModel.DefaultValue(EnumValueNaming.AsInProto)]
        public EnumValueNaming enum_value_naming
        {
            get => __pbn__enum_value_naming ?? EnumValueNaming.AsInProto;
            set => __pbn__enum_value_naming = value;
        }
        public bool ShouldSerializeenum_value_naming() => __pbn__enum_value_naming != null;
        public void Resetenum_value_naming() => __pbn__enum_value_naming = null;
        private EnumValueNaming? __pbn__enum_value_naming;

        [global::ProtoBuf.ProtoMember(17, Name = @"enum_strip_prefix")]
        [global::System.ComponentModel.DefaultValue(false)]
        public bool EnumStripPrefix
        {
            get => __pbn__EnumStripPrefix ?? false;
            set => __pbn__EnumStripPrefix = value;
        }
        public bool ShouldSerializeEnumStripPrefix() => __pbn__EnumStripPrefix != null;
        public void ResetEnumStripPrefix() => __pbn__EnumStripPrefix = null;
        private bool? __pbn__EnumStripPrefix;

        [global::ProtoBuf.ProtoMember(21, Name = @"bytes_type")]
        [global::System.ComponentModel.DefaultValue("")]
        public string BytesType
        {
            get => __pbn__BytesType ?? "";
            set => __pbn__BytesType = value;
        }
        public bool ShouldSerializeBytesType() => __pbn__BytesType != null;
        public void ResetBytesType() => __pbn__BytesType = null;
        private string __pbn__BytesType;

        [global::ProtoBuf.ProtoMember(23, Name = @"java_conversions")]
        public bool JavaConversions
        {
            get => __pbn__JavaConversions.GetValueOrDefault();
            set => __pbn__JavaConversions = value;
        }
        public bool ShouldSerializeJavaConversions() => __pbn__JavaConversions != null;
        public void ResetJavaConversions() => __pbn__JavaConversions = null;
        private bool? __pbn__JavaConversions;

        [global::ProtoBuf.ProtoMember(18)]
        public global::System.Collections.Generic.List<AuxMessageOptions> aux_message_options { get; } = new global::System.Collections.Generic.List<AuxMessageOptions>();

        [global::ProtoBuf.ProtoMember(19)]
        public global::System.Collections.Generic.List<AuxFieldOptions> aux_field_options { get; } = new global::System.Collections.Generic.List<AuxFieldOptions>();

        [global::ProtoBuf.ProtoMember(20)]
        public global::System.Collections.Generic.List<AuxEnumOptions> aux_enum_options { get; } = new global::System.Collections.Generic.List<AuxEnumOptions>();

        [global::ProtoBuf.ProtoMember(22)]
        public global::System.Collections.Generic.List<AuxEnumValueOptions> aux_enum_value_options { get; } = new global::System.Collections.Generic.List<AuxEnumValueOptions>();

        [global::ProtoBuf.ProtoMember(1001, Name = @"test_only_no_java_conversions")]
        public bool TestOnlyNoJavaConversions
        {
            get => __pbn__TestOnlyNoJavaConversions.GetValueOrDefault();
            set => __pbn__TestOnlyNoJavaConversions = value;
        }
        public bool ShouldSerializeTestOnlyNoJavaConversions() => __pbn__TestOnlyNoJavaConversions != null;
        public void ResetTestOnlyNoJavaConversions() => __pbn__TestOnlyNoJavaConversions = null;
        private bool? __pbn__TestOnlyNoJavaConversions;

        [global::ProtoBuf.ProtoContract()]
        public partial class AuxMessageOptions : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1, Name = @"target")]
            [global::System.ComponentModel.DefaultValue("")]
            public string Target
            {
                get => __pbn__Target ?? "";
                set => __pbn__Target = value;
            }
            public bool ShouldSerializeTarget() => __pbn__Target != null;
            public void ResetTarget() => __pbn__Target = null;
            private string __pbn__Target;

            [global::ProtoBuf.ProtoMember(2, Name = @"options")]
            public MessageOptions Options { get; set; }

        }

        [global::ProtoBuf.ProtoContract()]
        public partial class AuxFieldOptions : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1, Name = @"target")]
            [global::System.ComponentModel.DefaultValue("")]
            public string Target
            {
                get => __pbn__Target ?? "";
                set => __pbn__Target = value;
            }
            public bool ShouldSerializeTarget() => __pbn__Target != null;
            public void ResetTarget() => __pbn__Target = null;
            private string __pbn__Target;

            [global::ProtoBuf.ProtoMember(2, Name = @"options")]
            public FieldOptions Options { get; set; }

        }

        [global::ProtoBuf.ProtoContract()]
        public partial class AuxEnumOptions : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1, Name = @"target")]
            [global::System.ComponentModel.DefaultValue("")]
            public string Target
            {
                get => __pbn__Target ?? "";
                set => __pbn__Target = value;
            }
            public bool ShouldSerializeTarget() => __pbn__Target != null;
            public void ResetTarget() => __pbn__Target = null;
            private string __pbn__Target;

            [global::ProtoBuf.ProtoMember(2, Name = @"options")]
            public EnumOptions Options { get; set; }

        }

        [global::ProtoBuf.ProtoContract()]
        public partial class AuxEnumValueOptions : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1, Name = @"target")]
            [global::System.ComponentModel.DefaultValue("")]
            public string Target
            {
                get => __pbn__Target ?? "";
                set => __pbn__Target = value;
            }
            public bool ShouldSerializeTarget() => __pbn__Target != null;
            public void ResetTarget() => __pbn__Target = null;
            private string __pbn__Target;

            [global::ProtoBuf.ProtoMember(2, Name = @"options")]
            public EnumValueOptions Options { get; set; }

        }

        [global::ProtoBuf.ProtoContract()]
        public enum OptionsScope
        {
            [global::ProtoBuf.ProtoEnum(Name = @"FILE")]
            File = 0,
            [global::ProtoBuf.ProtoEnum(Name = @"PACKAGE")]
            Package = 1,
        }

        [global::ProtoBuf.ProtoContract()]
        public enum EnumValueNaming
        {
            [global::ProtoBuf.ProtoEnum(Name = @"AS_IN_PROTO")]
            AsInProto = 0,
            [global::ProtoBuf.ProtoEnum(Name = @"CAMEL_CASE")]
            CamelCase = 1,
        }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class MessageOptions : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"extends")]
        public global::System.Collections.Generic.List<string> Extends { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(2, Name = @"companion_extends")]
        public global::System.Collections.Generic.List<string> CompanionExtends { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(3, Name = @"annotations")]
        public global::System.Collections.Generic.List<string> Annotations { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(4, Name = @"type")]
        [global::System.ComponentModel.DefaultValue("")]
        public string Type
        {
            get => __pbn__Type ?? "";
            set => __pbn__Type = value;
        }
        public bool ShouldSerializeType() => __pbn__Type != null;
        public void ResetType() => __pbn__Type = null;
        private string __pbn__Type;

        [global::ProtoBuf.ProtoMember(5, Name = @"companion_annotations")]
        public global::System.Collections.Generic.List<string> CompanionAnnotations { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(6, Name = @"sealed_oneof_extends")]
        public global::System.Collections.Generic.List<string> SealedOneofExtends { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(7, Name = @"no_box")]
        public bool NoBox
        {
            get => __pbn__NoBox.GetValueOrDefault();
            set => __pbn__NoBox = value;
        }
        public bool ShouldSerializeNoBox() => __pbn__NoBox != null;
        public void ResetNoBox() => __pbn__NoBox = null;
        private bool? __pbn__NoBox;

        [global::ProtoBuf.ProtoMember(8, Name = @"unknown_fields_annotations")]
        public global::System.Collections.Generic.List<string> UnknownFieldsAnnotations { get; } = new global::System.Collections.Generic.List<string>();

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class FieldOptions : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"type")]
        [global::System.ComponentModel.DefaultValue("")]
        public string Type
        {
            get => __pbn__Type ?? "";
            set => __pbn__Type = value;
        }
        public bool ShouldSerializeType() => __pbn__Type != null;
        public void ResetType() => __pbn__Type = null;
        private string __pbn__Type;

        [global::ProtoBuf.ProtoMember(2, Name = @"scala_name")]
        [global::System.ComponentModel.DefaultValue("")]
        public string ScalaName
        {
            get => __pbn__ScalaName ?? "";
            set => __pbn__ScalaName = value;
        }
        public bool ShouldSerializeScalaName() => __pbn__ScalaName != null;
        public void ResetScalaName() => __pbn__ScalaName = null;
        private string __pbn__ScalaName;

        [global::ProtoBuf.ProtoMember(3, Name = @"collection_type")]
        [global::System.ComponentModel.DefaultValue("")]
        public string CollectionType
        {
            get => __pbn__CollectionType ?? "";
            set => __pbn__CollectionType = value;
        }
        public bool ShouldSerializeCollectionType() => __pbn__CollectionType != null;
        public void ResetCollectionType() => __pbn__CollectionType = null;
        private string __pbn__CollectionType;

        [global::ProtoBuf.ProtoMember(4, Name = @"key_type")]
        [global::System.ComponentModel.DefaultValue("")]
        public string KeyType
        {
            get => __pbn__KeyType ?? "";
            set => __pbn__KeyType = value;
        }
        public bool ShouldSerializeKeyType() => __pbn__KeyType != null;
        public void ResetKeyType() => __pbn__KeyType = null;
        private string __pbn__KeyType;

        [global::ProtoBuf.ProtoMember(5, Name = @"value_type")]
        [global::System.ComponentModel.DefaultValue("")]
        public string ValueType
        {
            get => __pbn__ValueType ?? "";
            set => __pbn__ValueType = value;
        }
        public bool ShouldSerializeValueType() => __pbn__ValueType != null;
        public void ResetValueType() => __pbn__ValueType = null;
        private string __pbn__ValueType;

        [global::ProtoBuf.ProtoMember(6, Name = @"annotations")]
        public global::System.Collections.Generic.List<string> Annotations { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(7, Name = @"map_type")]
        [global::System.ComponentModel.DefaultValue("")]
        public string MapType
        {
            get => __pbn__MapType ?? "";
            set => __pbn__MapType = value;
        }
        public bool ShouldSerializeMapType() => __pbn__MapType != null;
        public void ResetMapType() => __pbn__MapType = null;
        private string __pbn__MapType;

        [global::ProtoBuf.ProtoMember(30, Name = @"no_box")]
        public bool NoBox
        {
            get => __pbn__NoBox.GetValueOrDefault();
            set => __pbn__NoBox = value;
        }
        public bool ShouldSerializeNoBox() => __pbn__NoBox != null;
        public void ResetNoBox() => __pbn__NoBox = null;
        private bool? __pbn__NoBox;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class EnumOptions : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"extends")]
        public global::System.Collections.Generic.List<string> Extends { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(2, Name = @"companion_extends")]
        public global::System.Collections.Generic.List<string> CompanionExtends { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(3, Name = @"type")]
        [global::System.ComponentModel.DefaultValue("")]
        public string Type
        {
            get => __pbn__Type ?? "";
            set => __pbn__Type = value;
        }
        public bool ShouldSerializeType() => __pbn__Type != null;
        public void ResetType() => __pbn__Type = null;
        private string __pbn__Type;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class EnumValueOptions : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"extends")]
        public global::System.Collections.Generic.List<string> Extends { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(2, Name = @"scala_name")]
        [global::System.ComponentModel.DefaultValue("")]
        public string ScalaName
        {
            get => __pbn__ScalaName ?? "";
            set => __pbn__ScalaName = value;
        }
        public bool ShouldSerializeScalaName() => __pbn__ScalaName != null;
        public void ResetScalaName() => __pbn__ScalaName = null;
        private string __pbn__ScalaName;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class OneofOptions : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"extends")]
        public global::System.Collections.Generic.List<string> Extends { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(2, Name = @"scala_name")]
        [global::System.ComponentModel.DefaultValue("")]
        public string ScalaName
        {
            get => __pbn__ScalaName ?? "";
            set => __pbn__ScalaName = value;
        }
        public bool ShouldSerializeScalaName() => __pbn__ScalaName != null;
        public void ResetScalaName() => __pbn__ScalaName = null;
        private string __pbn__ScalaName;

    }

    //public static partial class Extensions
    //{
    //    public static ScalaPbOptions GetOptions(this global::Google.Protobuf.Reflection.FileOptions obj)
    //        => obj == null ? default : global::ProtoBuf.Extensible.GetValue<ScalaPbOptions>(obj, 1020);

    //    public static void SetOptions(this global::Google.Protobuf.Reflection.FileOptions obj, ScalaPbOptions value)
    //        => global::ProtoBuf.Extensible.AppendValue<ScalaPbOptions>(obj, 1020, value);

    //    public static MessageOptions GetMessage(this global::Google.Protobuf.Reflection.MessageOptions obj)
    //        => obj == null ? default : global::ProtoBuf.Extensible.GetValue<MessageOptions>(obj, 1020);

    //    public static void SetMessage(this global::Google.Protobuf.Reflection.MessageOptions obj, MessageOptions value)
    //        => global::ProtoBuf.Extensible.AppendValue<MessageOptions>(obj, 1020, value);

    //    public static FieldOptions GetField(this global::Google.Protobuf.Reflection.FieldOptions obj)
    //        => obj == null ? default : global::ProtoBuf.Extensible.GetValue<FieldOptions>(obj, 1020);

    //    public static void SetField(this global::Google.Protobuf.Reflection.FieldOptions obj, FieldOptions value)
    //        => global::ProtoBuf.Extensible.AppendValue<FieldOptions>(obj, 1020, value);

    //    public static EnumOptions GetEnumOptions(this global::Google.Protobuf.Reflection.EnumOptions obj)
    //        => obj == null ? default : global::ProtoBuf.Extensible.GetValue<EnumOptions>(obj, 1020);

    //    public static void SetEnumOptions(this global::Google.Protobuf.Reflection.EnumOptions obj, EnumOptions value)
    //        => global::ProtoBuf.Extensible.AppendValue<EnumOptions>(obj, 1020, value);

    //    public static EnumValueOptions GetEnumValue(this global::Google.Protobuf.Reflection.EnumValueOptions obj)
    //        => obj == null ? default : global::ProtoBuf.Extensible.GetValue<EnumValueOptions>(obj, 1020);

    //    public static void SetEnumValue(this global::Google.Protobuf.Reflection.EnumValueOptions obj, EnumValueOptions value)
    //        => global::ProtoBuf.Extensible.AppendValue<EnumValueOptions>(obj, 1020, value);

    //    public static OneofOptions GetOneof(this global::Google.Protobuf.Reflection.OneofOptions obj)
    //        => obj == null ? default : global::ProtoBuf.Extensible.GetValue<OneofOptions>(obj, 1020);

    //    public static void SetOneof(this global::Google.Protobuf.Reflection.OneofOptions obj, OneofOptions value)
    //        => global::ProtoBuf.Extensible.AppendValue<OneofOptions>(obj, 1020, value);

    //}
}

#pragma warning restore CS0612, CS0618, CS1591, CS3021, IDE0079, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
#endregion
