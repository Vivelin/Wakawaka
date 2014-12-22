﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wakawaka.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Wakawaka.Resources.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An ATX header can only be a level 1 to 6 heading..
        /// </summary>
        internal static string AtxHeaderOutOfRange {
            get {
                return ResourceManager.GetString("AtxHeaderOutOfRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A Markdown header must be a value between 1 and 6..
        /// </summary>
        internal static string HeaderOutOfRange {
            get {
                return ResourceManager.GetString("HeaderOutOfRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; does not represent a recognized member type. Valid prefixes include &apos;N&apos;, &apos;T&apos;, &apos;F&apos;, &apos;P&apos;, &apos;M&apos;, &apos;E&apos; and &apos;!&apos;..
        /// </summary>
        internal static string InvalidPrefix {
            get {
                return ResourceManager.GetString("InvalidPrefix", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified element does not have a &apos;name&apos; attribute..
        /// </summary>
        internal static string MissingName {
            get {
                return ResourceManager.GetString("MissingName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified element&apos;s name is too short..
        /// </summary>
        internal static string NameTooShort {
            get {
                return ResourceManager.GetString("NameTooShort", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You cannot add XML documentation to an error..
        /// </summary>
        internal static string NoErrorDoc {
            get {
                return ResourceManager.GetString("NoErrorDoc", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You cannot add XML documentation to a namespace..
        /// </summary>
        internal static string NoNamespaceDoc {
            get {
                return ResourceManager.GetString("NoNamespaceDoc", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A Setext header can only be a level 1 or 2 heading..
        /// </summary>
        internal static string SetextHeaderOutOfRange {
            get {
                return ResourceManager.GetString("SetextHeaderOutOfRange", resourceCulture);
            }
        }
    }
}
