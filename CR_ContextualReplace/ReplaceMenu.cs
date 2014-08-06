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
    public class ReplaceMenu : SubMenuItem
    {
        public ReplaceMenu(string caption, Replacement replacement)
            : base("", caption, "")
        {
            Replacement = replacement;
        }
        public Replacement Replacement { get; set; }

    }
}
