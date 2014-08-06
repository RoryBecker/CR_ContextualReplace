using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.Refactor.Core;

namespace CR_ContextualReplace
{
    public class Replacement
    {
        public Replacement(string replace, string with) : this(replace, with, with) { }
        public Replacement(string replace, string with, string caption)
        {
            Replace = replace;
            With = with;
            Caption = caption;
        }
        public string Replace { get; set; }
        public string With { get; set; }
        public string Caption { get; set; }
        public override string ToString()
        {
            return Replace + " -> " + With;
        }
    }
}
