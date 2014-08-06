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
    public partial class PlugIn1 : StandardPlugIn
    {
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            registerReplaceWith();
            LoadOptions();
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            //
            // TODO: Add your finalization code here.
            //

            base.FinalizePlugIn();
        }
        #endregion

        #region Register Plugin
        public void registerReplaceWith()
        {
            DevExpress.CodeRush.Core.CodeProvider ReplaceWith = new DevExpress.CodeRush.Core.CodeProvider(components);
            ((System.ComponentModel.ISupportInitialize)(ReplaceWith)).BeginInit();
            ReplaceWith.ProviderName = "ReplaceWith"; // Should be Unique
            ReplaceWith.DisplayName = "Replace With";
            ReplaceWith.CheckAvailability += ReplaceWith_CheckAvailability;
            EventNexus.OptionsChanged += PlugIn1_OptionsChanged;
            ReplaceWith.Apply += ReplaceWith_Apply;
            ((System.ComponentModel.ISupportInitialize)(ReplaceWith)).EndInit();
        }
        #endregion

        private List<Replacement> mReplacements;
        #region Load Options
        private void PlugIn1_OptionsChanged(OptionsChangedEventArgs ea)
        {
            LoadOptions();
        }
        private void LoadOptions()
        {
            mReplacements = Options1.LoadReplacements(Options1.Storage);
        }
        #endregion

        public void ReplaceWith_CheckAvailability(Object sender, CheckContentAvailabilityEventArgs ea)
        {
            var SelectedText = CodeRush.Selection.Text;

            var Applicable = from replacement in mReplacements
                             where replacement.Replace == SelectedText
                             select replacement;
            switch (Applicable.Count())
            {
                case 0:
                    ea.Available = false;
                    break;
                //case 1:
                //    
                //    ea.Available = true;
                //    break;
                default:
                    foreach (Replacement replacement in Applicable)
                    {
                        ea.AddSubMenuItem(new ReplaceMenu(replacement.Caption, replacement));
                    }
                    ea.Available = true;
                    break;
            }

            ea.Available = Applicable.Count() > 0; // Change this to return true, only when your Code should be available.
        }

        private void ReplaceWith_Apply(Object sender, ApplyContentEventArgs ea)
        {
            // Provider multiple possible values which can be used to replace the selection
            var ReplaceMenu = (ReplaceMenu)ea.SelectedSubMenuItem;
            var SelectedText = CodeRush.Selection.Text;
            ea.TextDocument.SetText(ea.Selection.Range, ReplaceMenu.Replacement.With);
        }

    }
}