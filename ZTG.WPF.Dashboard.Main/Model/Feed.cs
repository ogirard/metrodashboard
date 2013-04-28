// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Feed.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using ZTG.WPF.Dashboard.Shared.WPF;

namespace ZTG.WPF.Dashboard.Main.Model
{
  public class Feed : NotificationObject
  {
    public Guid Id { get; private set; }

    private string _name;

    /// <summary>
    /// Gets or sets the Name.
    /// </summary>
    /// <value>The Name value.</value>
    public string Name
    {
      get
      {
        return _name;
      }

      set
      {
        ChangeAndNotify(ref _name, value, "Name");
      }
    }

    private string _description;

    /// <summary>
    /// Gets or sets the Description.
    /// </summary>
    /// <value>The Description value.</value>
    public string Description
    {
      get
      {
        return _description;
      }

      set
      {
        ChangeAndNotify(ref _description, value, "Description");
      }
    }

    private Uri _path;

    /// <summary>
    /// Gets or sets the Path.
    /// </summary>
    /// <value>The Path value.</value>
    public Uri Path
    {
      get
      {
        return _path;
      }

      set
      {
        ChangeAndNotify(ref _path, value, "Path");
      }
    }

    private string _tags;

    /// <summary>
    /// Gets or sets the Tags.
    /// </summary>
    /// <value>The Tags value.</value>
    public string Tags
    {
      get
      {
        return _tags;
      }

      set
      {
        ChangeAndNotify(ref _tags, value, "Tags");
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Feed"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    public Feed(Guid id)
    {
      Id = id;
    }
  }
}