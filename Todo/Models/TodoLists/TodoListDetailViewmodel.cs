﻿using System.Collections.Generic;
using System.ComponentModel;
using Todo.Models.TodoItems;

namespace Todo.Models.TodoLists
{
    public class TodoListDetailViewmodel
    {
        public int TodoListId { get; }
        public string Title { get; }
        public ICollection<TodoItemSummaryViewmodel> Items { get; }

        public string SortOrder { get; set; } = "importance";

        [DisplayName("Hide Done Items")]
        public bool HideDoneItems { get; set; } = false;

        public TodoListDetailViewmodel(int todoListId, string title, ICollection<TodoItemSummaryViewmodel> items)
        {
            Items = items;
            TodoListId = todoListId;
            Title = title;
        }
    }
}