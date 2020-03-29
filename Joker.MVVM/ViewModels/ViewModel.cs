﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Joker.Contracts;

namespace Joker.MVVM.ViewModels
{
  public class ViewModel<TModel> : ViewModel, IViewModel<TModel>
    where TModel : IVersion
  {
    public ViewModel(TModel model)
    {
      if(model == null)
        throw new ArgumentNullException(nameof(model));

      Model = model;
    }

    public TModel Model { get; }
  }

  public class ViewModel : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}