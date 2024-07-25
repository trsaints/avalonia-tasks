using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TodoAvalonia.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddItemCommand))]
    private string? _newItemContent;
    
    public ObservableCollection<TodoItemViewModel> TodoItems { get; } = [];

    private bool IsValid() => !string.IsNullOrWhiteSpace(NewItemContent);

    [RelayCommand(CanExecute = nameof(IsValid))]
    private void AddItem()
    {
        TodoItems.Add(new TodoItemViewModel { Content = NewItemContent});

        NewItemContent = "";
    }

    [RelayCommand]
    private void RemoveItem(TodoItemViewModel item)
    {
        TodoItems.Remove(item);
    }
}