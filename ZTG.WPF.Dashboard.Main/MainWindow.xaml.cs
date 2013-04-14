//----------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

namespace ZTG.WPF.Dashboard.Main
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Gets or sets the view model.
    /// </summary>
    public MainWindowViewModel ViewModel
    {
      get
      {
        return (MainWindowViewModel)DataContext;
      }

      set
      {
        DataContext = value;
      }
    }
  }
}
