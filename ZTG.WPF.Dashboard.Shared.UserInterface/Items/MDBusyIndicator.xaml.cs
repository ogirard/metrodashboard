//----------------------------------------------------------------------------------------------------
// <copyright file="MDBusyIndicator.xaml.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Media;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Items
{
  /// <summary>
  /// Interaction logic for MDBusyIndicator.xaml
  /// </summary>
  public partial class MDBusyIndicator
  {
    /// <summary>
    /// The IsBusy dependency property
    /// </summary>
    public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register("IsBusy", typeof(bool), typeof(MDBusyIndicator), new PropertyMetadata(false, IsBusyChangedHandler));

    private static void IsBusyChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var busyIndicator = d as MDBusyIndicator;
      if (busyIndicator == null)
      {
        return;
      }

      busyIndicator.Visibility = busyIndicator.IsBusy ? Visibility.Visible : Visibility.Hidden;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is busy.
    /// </summary>
    public bool IsBusy
    {
      get
      {
        return (bool)GetValue(IsBusyProperty);
      }

      set
      {
        SetValue(IsBusyProperty, value);
      }
    }

    /// <summary>
    /// The BulletsBrush dependency property
    /// </summary>
    public static readonly DependencyProperty BulletsBrushProperty = DependencyProperty.Register("BulletsBrush", typeof(Brush), typeof(MDBusyIndicator), new PropertyMetadata(Brushes.White));

    /// <summary>
    /// Gets or sets the bullets brush.
    /// </summary>
    public Brush BulletsBrush
    {
      get
      {
        return (Brush)GetValue(BulletsBrushProperty);
      }

      set
      {
        SetValue(BulletsBrushProperty, value);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MDBusyIndicator"/> class.
    /// </summary>
    public MDBusyIndicator()
    {
      InitializeComponent();
      Visibility = IsBusy ? Visibility.Visible : Visibility.Hidden;
    }
  }
}
