// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FlowDirectionManager.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows;

namespace ZTG.WPF.Dashboard.Shared.Localization.WPF
{
  /// <summary>
  /// Manages the flow direction setting
  /// </summary>
  public class FlowDirectionManager
  {
    private static FlowDirectionManager _instance;

    /// <summary>
    /// Gets the instance.
    /// </summary>
    /// <value>The instance.</value>
    public static FlowDirectionManager Instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = new FlowDirectionManager();
        }

        return _instance;
      }
    }

    private FlowDirection _flowDirection;

    /// <summary>
    /// Prevents a default instance of the <see cref="FlowDirectionManager"/> class from being created.
    /// </summary>
    private FlowDirectionManager()
    {
      FlowDirection = TranslationManager.Language.TextInfo.IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
      TranslationManager.LanguageChanged += OnLanguageChanged;
    }

    /// <summary>
    /// Called when [language changed].
    /// </summary>
    private void OnLanguageChanged(object sender, object newLanguage)
    {
      FlowDirection = TranslationManager.Language.TextInfo.IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
    }

    /// <summary>
    /// Gets or sets the flow direction.
    /// </summary>
    /// <value>The flow direction.</value>
    public FlowDirection FlowDirection
    {
      get { return _flowDirection; }
      set { _flowDirection = value; }
    }
  }
}
