using Sitecore.Configuration;
using Sitecore.Shell.Controls.RichTextEditor.Pipelines.SaveRichTextContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Sitecore.Support.Shell.Controls.RichTextEditor.Pipelines.SaveRichTextContent
{
  public class ProtectExternalLink: Sitecore.Shell.Controls.RichTextEditor.Pipelines.SaveRichTextContent.ProtectExternalLink
  {
    public new void Process(SaveRichTextContentArgs args)
    {
      if (string.IsNullOrEmpty(args.Content))
      {
        return;
      }
      if (!Settings.ProtectExternalLinksWithBlankTarget)
      {
        return;
      }
      MatchCollection aTagMatchCollection = this.GetATagMatchCollection(args);
      foreach (Match match in aTagMatchCollection)
      {
        if (match.Success)
        {
          string value = match.Value;
          if (!string.IsNullOrEmpty(value) && value.Contains("_blank") && !this.IsInternalLink(value))
          {
            string protectedLink = value.Insert(2, " rel=\"noopener noreferrer\" ");
            args.Content = args.Content.Replace(value, protectedLink);
            //args.Content = this.GetProtectedHtml(args, args.Content.IndexOf(" ", match.Index, StringComparison.InvariantCultureIgnoreCase), match.Length);
          }
        }
      }
    }
  }
}