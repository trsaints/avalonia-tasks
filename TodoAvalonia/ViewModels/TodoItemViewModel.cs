using System;
using CommunityToolkit.Mvvm.ComponentModel;
using TodoAvalonia.Models;

namespace TodoAvalonia.ViewModels;

public partial class TodoItemViewModel : ViewModelBase
{
    [ObservableProperty] 
    private bool _isChecked;  
    
    [ObservableProperty] 
    private string? _content;
    
    public TodoItemViewModel() {}

    public TodoItemViewModel(TodoItem item)
    {
        IsChecked = item.IsChecked;
        Content = item.Content;
    }

    public TodoItem Get() => new() { Content = Content, IsChecked = IsChecked };
}