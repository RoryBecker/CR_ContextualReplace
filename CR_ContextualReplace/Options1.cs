using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using System.Xml.Linq;
using System.Xml;

namespace CR_ContextualReplace
{
    [UserLevel(UserLevel.Advanced)]
    public partial class Options1 : OptionsPage
    {
        // DXCore-generated code...
        #region Initialize
        protected override void Initialize()
        {
            base.Initialize();
            HookupOptions();
        }
        #endregion
        #region GetCategory
        public static string GetCategory()
        {
            return @"Editor\Selections";
        }
        #endregion
        #region GetPageName
        public static string GetPageName()
        {
            return @"Contextual Replace";
        }
        #endregion

        private void HookupOptions()
        {
            RestoreDefaults += OnRestoreDefaults;
            PreparePage += OnPreparePage;
            CommitChanges += OnCommitChanges;
        }

        #region Add Remove UI
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            lstReplacements.Items.Add(new Replacement(txtReplace.Text, txtWith.Text));
            txtReplace.Text = String.Empty;
            txtWith.Text = String.Empty;
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            if (lstReplacements.SelectedItems.Count != 1)
                return;
            var item = (Replacement)lstReplacements.SelectedItems[0];
            txtReplace.Text = item.Replace;
            txtWith.Text = item.With;
            lstReplacements.Items.Remove(item);
        }
        #endregion

        #region Load Save Options
        private void OnRestoreDefaults(Object sender, DevExpress.CodeRush.Core.OptionsPageEventArgs ea)
        {
            // Setup defaults here
            lstReplacements.Items.Clear();
        }

        private void OnPreparePage(Object sender, DevExpress.CodeRush.Core.OptionsPageStorageEventArgs ea)
        {
            // Load options here
            var Replacements = LoadReplacements(ea.Storage);
            lstReplacements.Items.AddRange(Replacements.ToArray());
        }
        public static List<Replacement> LoadReplacements(DecoupledStorage storage)
        {
            var xml = storage.ReadString("Replacements", "Replacements", "<root/>");
            List<Replacement> Replacements = new List<Replacement>();
            XElement root = XElement.Parse(xml);
            foreach (XElement item in root.Elements())
            {
                Replacement Replacement = new Replacement(item.Attribute("replace").Value, item.Attribute("with").Value);
                Replacements.Add(Replacement);
            }
            return Replacements;
        }

        private void OnCommitChanges(Object sender, DevExpress.CodeRush.Core.CommitChangesEventArgs ea)
        {
            // Save changes here.
            XElement root = new XElement("root");
            
            foreach (Replacement item in lstReplacements.Items)
            {
                XElement ReplacementXML = new XElement("replacement");
                ReplacementXML.Add(new XAttribute("replace", item.Replace));
                ReplacementXML.Add(new XAttribute("with", item.With));
                root.Add(ReplacementXML);
            }
            ea.Storage.WriteString("Replacements", "Replacements", root.ToString());
        }
        #endregion
    }
}