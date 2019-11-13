﻿using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DominicanWhoCodes.Controls;
using DominicanWhoCodes.Modules.Developers.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;

namespace DominicanWhoCodes.Modules.Developers.Views
{
    public partial class DevelopersPage : ReactiveContentPage<DevelopersViewModel>, ISearchPage
    {
        public DevelopersPage()
        {
            InitializeComponent();
            SearchBarTextChanged += HandleSearchBarTextChanged;

            this.WhenActivated(
                disposables =>
                {
                    this.OneWayBind(this.ViewModel,
                      x => x.IsBusy,
                      x => x.activityIndicator.IsRunning)
                      .DisposeWith(disposables);

                    this.OneWayBind(this.ViewModel,
                      x => x.IsBusy,
                      x => x.activityIndicator.IsVisible)
                      .DisposeWith(disposables);

                    this.OneWayBind(this.ViewModel,
                       x => x.Developers,
                       x => x.developersList.ItemsSource)
                       .DisposeWith(disposables);

                    this.OneWayBind(this.ViewModel,
                       x => x.Title,
                       x => x.navTitle.NavigationTitle)
                    .DisposeWith(disposables);

                    this.Bind(this.ViewModel,
                      x => x.SelectedDeveloper,
                      x => x.developersList.SelectedItem)
                   .DisposeWith(disposables);
                });
        }

        public event EventHandler<string> SearchBarTextChanged;

        public void OnSearchBarTextChanged(string text) => SearchBarTextChanged?.Invoke(this, text);

        void HandleSearchBarTextChanged(object sender, string searchBarText) => ViewModel.SearchText = searchBarText;

        protected override void OnAppearing() => ViewModel.UpdateStatusbar();

        protected override void OnDisappearing() => ViewModel.SelectedDeveloper = null;
    }
}
